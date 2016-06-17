# WundergroundLib
Helper library to work with the Wunderground weather api

Get your API Key here: https://www.wunderground.com/weather/api

```cs
var wwfc = new WundergroundWeatherForecast(WUNDERGROUND_API_KEY);
var stationId = wwfc.GetStationId("Germany", "Berlin");
var forecast = wwfc.GetForecast(StationId);

```

Nuget: https://www.nuget.org/packages/WundergroundLib/
