# WundergroundLib
Helper library to work with the Wunderground weather api, 
especially in the context of using weather as feature for ML 

Get your API Key here: https://www.wunderground.com/weather/api

Nuget: https://www.nuget.org/packages/WundergroundLib/

##Usage
```cs
var wwfc = new WundergroundWeatherForecast(WUNDERGROUND_API_KEY);

// retrieve the stationId
var stationId = wwfc.GetStationId("Germany", "Berlin");

// get the raw response objects as JObject
JObject dailyResp = GetResponse(DataFeatures.forecast, stationId);
JObject hourlyResp = GetResponse(DataFeatures.hourly, stationId); 

// get the forecast as a WeatherForecast object
WeatherForecast forecast = wwfc.GetForecast(StationId);
Console.WriteLine(JsonConvert.SerializeObject(forecast));

```
##Output of Console.WriteLine(JsonConvert.SerializeObject(forecast));
```json
{
  "Daily": {
    "T-0": {
      "month": 6,
      "day": 17,
      "year": 2016,
      "weekDay": "Friday",
      "tempMin": 12,
      "tempMax": 17,
      "windAvg": 9,
      "windMax": 18,
      "rainTotal": 2
    },
    "T-1": {
      "month": 6,
      "day": 18,
      "year": 2016,
      "weekDay": "Saturday",
      "tempMin": 12,
      "tempMax": 19,
      "windAvg": 14,
      "windMax": 16,
      "rainTotal": 11
    },
    "T-2": {
      "month": 6,
      "day": 19,
      "year": 2016,
      "weekDay": "Sunday",
      "tempMin": 8,
      "tempMax": 18,
      "windAvg": 14,
      "windMax": 16,
      "rainTotal": 1
    },
    "T-3": {
      "month": 6,
      "day": 20,
      "year": 2016,
      "weekDay": "Monday",
      "tempMin": 15,
      "tempMax": 23,
      "windAvg": 13,
      "windMax": 16,
      "rainTotal": 9
    }
  },
  "Hourly": {
    "T-0": {
      "hour": 17,
      "timestamp": "2016-06-17T15:00:00Z",
      "temp": 16,
      "wind": 11,
      "rain": 0
    },
    "T-1": {
      "hour": 18,
      "timestamp": "2016-06-17T16:00:00Z",
      "temp": 17,
      "wind": 10,
      "rain": 0
    },
    "T-2": {
      "hour": 19,
      "timestamp": "2016-06-17T17:00:00Z",
      "temp": 17,
      "wind": 8,
      "rain": 0
    },
    "T-3": {
      "hour": 20,
      "timestamp": "2016-06-17T18:00:00Z",
      "temp": 17,
      "wind": 8,
      "rain": 2
    },
    "T-4": {
      "hour": 21,
      "timestamp": "2016-06-17T19:00:00Z",
      "temp": 16,
      "wind": 6,
      "rain": 0
    },
    "T-5": {
      "hour": 22,
      "timestamp": "2016-06-17T20:00:00Z",
      "temp": 15,
      "wind": 6,
      "rain": 0
    },
    "T-6": {
      "hour": 23,
      "timestamp": "2016-06-17T21:00:00Z",
      "temp": 14,
      "wind": 8,
      "rain": 0
    },
    "T-7": {
      "hour": 0,
      "timestamp": "2016-06-17T22:00:00Z",
      "temp": 14,
      "wind": 6,
      "rain": 0
    },
    "T-8": {
      "hour": 1,
      "timestamp": "2016-06-17T23:00:00Z",
      "temp": 13,
      "wind": 6,
      "rain": 0
    },
    "T-9": {
      "hour": 2,
      "timestamp": "2016-06-18T00:00:00Z",
      "temp": 13,
      "wind": 6,
      "rain": 0
    },
    "T-10": {
      "hour": 3,
      "timestamp": "2016-06-18T01:00:00Z",
      "temp": 13,
      "wind": 6,
      "rain": 0
    },
    "T-11": {
      "hour": 4,
      "timestamp": "2016-06-18T02:00:00Z",
      "temp": 13,
      "wind": 6,
      "rain": 0
    },
    "T-12": {
      "hour": 5,
      "timestamp": "2016-06-18T03:00:00Z",
      "temp": 13,
      "wind": 6,
      "rain": 0
    },
    "T-13": {
      "hour": 6,
      "timestamp": "2016-06-18T04:00:00Z",
      "temp": 13,
      "wind": 8,
      "rain": 0
    },
    "T-14": {
      "hour": 7,
      "timestamp": "2016-06-18T05:00:00Z",
      "temp": 13,
      "wind": 8,
      "rain": 0
    },
    "T-15": {
      "hour": 8,
      "timestamp": "2016-06-18T06:00:00Z",
      "temp": 14,
      "wind": 10,
      "rain": 0
    },
    "T-16": {
      "hour": 9,
      "timestamp": "2016-06-18T07:00:00Z",
      "temp": 16,
      "wind": 10,
      "rain": 0
    },
    "T-17": {
      "hour": 10,
      "timestamp": "2016-06-18T08:00:00Z",
      "temp": 16,
      "wind": 11,
      "rain": 0
    },
    "T-18": {
      "hour": 11,
      "timestamp": "2016-06-18T09:00:00Z",
      "temp": 17,
      "wind": 11,
      "rain": 1
    },
    "T-19": {
      "hour": 12,
      "timestamp": "2016-06-18T10:00:00Z",
      "temp": 17,
      "wind": 13,
      "rain": 1
    },
    "T-20": {
      "hour": 13,
      "timestamp": "2016-06-18T11:00:00Z",
      "temp": 18,
      "wind": 16,
      "rain": 1
    },
    "T-21": {
      "hour": 14,
      "timestamp": "2016-06-18T12:00:00Z",
      "temp": 18,
      "wind": 16,
      "rain": 1
    },
    "T-22": {
      "hour": 15,
      "timestamp": "2016-06-18T13:00:00Z",
      "temp": 18,
      "wind": 16,
      "rain": 1
    },
    "T-23": {
      "hour": 16,
      "timestamp": "2016-06-18T14:00:00Z",
      "temp": 18,
      "wind": 13,
      "rain": 1
    },
    "T-24": {
      "hour": 17,
      "timestamp": "2016-06-18T15:00:00Z",
      "temp": 18,
      "wind": 13,
      "rain": 1
    },
    "T-25": {
      "hour": 18,
      "timestamp": "2016-06-18T16:00:00Z",
      "temp": 18,
      "wind": 14,
      "rain": 1
    },
    "T-26": {
      "hour": 19,
      "timestamp": "2016-06-18T17:00:00Z",
      "temp": 17,
      "wind": 14,
      "rain": 0
    },
    "T-27": {
      "hour": 20,
      "timestamp": "2016-06-18T18:00:00Z",
      "temp": 16,
      "wind": 14,
      "rain": 0
    },
    "T-28": {
      "hour": 21,
      "timestamp": "2016-06-18T19:00:00Z",
      "temp": 15,
      "wind": 11,
      "rain": 0
    },
    "T-29": {
      "hour": 22,
      "timestamp": "2016-06-18T20:00:00Z",
      "temp": 14,
      "wind": 10,
      "rain": 0
    },
    "T-30": {
      "hour": 23,
      "timestamp": "2016-06-18T21:00:00Z",
      "temp": 14,
      "wind": 10,
      "rain": 0
    },
    "T-31": {
      "hour": 0,
      "timestamp": "2016-06-18T22:00:00Z",
      "temp": 13,
      "wind": 10,
      "rain": 0
    },
    "T-32": {
      "hour": 1,
      "timestamp": "2016-06-18T23:00:00Z",
      "temp": 13,
      "wind": 10,
      "rain": 0
    },
    "T-33": {
      "hour": 2,
      "timestamp": "2016-06-19T00:00:00Z",
      "temp": 13,
      "wind": 10,
      "rain": 0
    },
    "T-34": {
      "hour": 3,
      "timestamp": "2016-06-19T01:00:00Z",
      "temp": 13,
      "wind": 11,
      "rain": 0
    },
    "T-35": {
      "hour": 4,
      "timestamp": "2016-06-19T02:00:00Z",
      "temp": 13,
      "wind": 11,
      "rain": 0
    }
  }
}
```

