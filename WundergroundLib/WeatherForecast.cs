using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WundergroundLib
{
    public class DayForecast
    {
        public int month;
        public int day;
        public int year;
        public string weekDay;
        public int tempMin;
        public int tempMax;
        public int windAvg;
        public int windMax;
        public int rainTotal;
    }
    public class HourForecast
    {
        public int hour;
        public DateTime timestamp;
        public int temp;
        public int wind;
        public int rain;
    }

    public class WeatherForecast
    {
        public WeatherForecast()
        {
            Daily = new Dictionary<string, DayForecast>();
            Hourly = new Dictionary<string, HourForecast>();
        }
        public Dictionary<string, DayForecast> Daily { get; }
        public Dictionary<string, HourForecast> Hourly { get; }

    }
    public class WundergroundWeatherForecast
    {
        private string apiKey;

        public WundergroundWeatherForecast(string apiKey)
        {
            this.apiKey = apiKey;
        }

        private DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public string GetStationId(string country, string city)
        {
            Uri geoLookupUri = new Uri(
                string.Format("http://api.wunderground.com/api/{0}/geolookup/q/{1}/{2}.json", apiKey, country, city));
            JObject geoLookupFCResponse = GetPredictionResponse(geoLookupUri);

            string stationId = geoLookupFCResponse.SelectToken("location.l").ToString();
            // e.g. /q/zmw:00000.1.10513 is the stationId for Köln
            return stationId;
        }

        public WeatherForecast GetForecast(string stationId)
        {
            WeatherForecast fc = new WeatherForecast();
            Uri hourlyUri = new Uri(
                string.Format("http://api.wunderground.com/api/{0}/hourly/q/zmw:{1}.json", apiKey, stationId));
            Uri dailyUri = new Uri(
                string.Format("http://api.wunderground.com/api/{0}/forecast/q/zmw:{1}.json", apiKey, stationId));

            var dailyResp = GetPredictionResponse(dailyUri);
            var hourlyResp = GetPredictionResponse(hourlyUri);

            foreach (var token in dailyResp.SelectToken("forecast.simpleforecast.forecastday"))
            {
                fc.Daily["T-" + fc.Daily.Count] = new DayForecast()
                {
                    day = Int32.Parse(token.SelectToken("date.day").ToString()),
                    month = Int32.Parse(token.SelectToken("date.month").ToString()),
                    year = Int32.Parse(token.SelectToken("date.year").ToString()),
                    weekDay = token.SelectToken("date.weekday").ToString(),
                    tempMin = Int32.Parse(token.SelectToken("low.celsius").ToString()),
                    tempMax = Int32.Parse(token.SelectToken("high.celsius").ToString()),
                    windAvg = Int32.Parse(token.SelectToken("avewind.kph").ToString()),
                    windMax = Int32.Parse(token.SelectToken("maxwind.kph").ToString()),
                    rainTotal = Int32.Parse(token.SelectToken("qpf_allday.mm").ToString())
                };
            }

            foreach (var hourlyForecast in hourlyResp.GetValue("hourly_forecast"))
            {
                fc.Hourly["T-" + fc.Hourly.Count] = new HourForecast()
                {
                    hour = Int32.Parse(hourlyForecast.SelectToken("FCTTIME.hour")?.ToString()),
                    timestamp = FromUnixTime(Int64.Parse(hourlyForecast.SelectToken("FCTTIME.epoch")?.ToString())),
                    temp = Int32.Parse(hourlyForecast.SelectToken("temp.metric")?.ToString()),
                    wind = Int32.Parse(hourlyForecast.SelectToken("wspd.metric")?.ToString()),
                    rain = Int32.Parse(hourlyForecast.SelectToken("qpf.metric")?.ToString()),
                };
            }

            return fc;
        }

        private static JObject GetPredictionResponse(Uri uri)
        {
            WebClient wc = new WebClient();
            StreamReader sr = new StreamReader(wc.OpenRead(uri));
            return JObject.Parse(sr.ReadToEnd());
        }
    }
}
