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
        public int? month;
        public int? day;
        public int? year;
        public string weekDay;
        public int? tempMin;
        public int? tempMax;
        public int? windAvg;
        public int? windMax;
        public int? rainTotal;
    }
    public class HourForecast
    {
        public int? hour;
        public DateTime timestamp;
        public int? temp;
        public int? wind;
        public int? rain;
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
        private WebClient wc;

        public WundergroundWeatherForecast(string apiKey)
        {
            this.apiKey = apiKey;
            wc = new WebClient();
        }

        private DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        private int? GetIntValue(JToken token, string key)
        {
            int value;
            return Int32.TryParse(token.SelectToken(key).ToString(), out value) ? (int?) value : null;
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
                    day = GetIntValue(token, "date.day"),
                    month = GetIntValue(token, "date.month"),
                    year = GetIntValue(token, "date.year"),
                    weekDay = token.SelectToken("date.weekday").ToString(),
                    tempMin = GetIntValue(token, "low.celsius"),
                    tempMax = GetIntValue(token, "high.celsius"),
                    windAvg = GetIntValue(token, "avewind.kph"),
                    windMax = GetIntValue(token, "maxwind.kph"),
                    rainTotal = GetIntValue(token, "qpf_allday.mm")
                };
            }

            foreach (var hourlyForecast in hourlyResp.GetValue("hourly_forecast"))
            {
                fc.Hourly["T-" + fc.Hourly.Count] = new HourForecast()
                {
                    hour = GetIntValue(hourlyForecast, "FCTTIME.hour"),
                    timestamp = FromUnixTime(Int64.Parse(hourlyForecast.SelectToken("FCTTIME.epoch")?.ToString())),
                    temp = GetIntValue(hourlyForecast, "temp.metric"),
                    wind = GetIntValue(hourlyForecast, "wspd.metric"),
                    rain = GetIntValue(hourlyForecast, "qpf.metric")
                };
            }

            return fc;
        }

        private JObject GetPredictionResponse(Uri uri)
        {
            StreamReader sr = new StreamReader(wc.OpenRead(uri));
            return JObject.Parse(sr.ReadToEnd());
        }
    }
}
