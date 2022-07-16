using MauiApp2.APIrelated;
using Newtonsoft.Json;

namespace MauiApp2;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class WeatherPage : ContentPage
{
    public WeatherPage()
    {
        InitializeComponent();
        getGeoLocation();
        GetWeatherInfo();
        GetForecastInfo();
        GetHourlyForecastInfo4days();
    }
    private String location = String.Empty;
    private double lat = 0.0;
    private double lon = 0.0;
    private string apiKey_LocationIQ = "pk.5e4da3c8cc77b0682ebd7f7d89c54366";
    private string apiKeyOpenWeatherMap = "12e07f28b47bd828daa4bc71267e24ac";
    private string apiKeyfreeWeatherApi = "288f568d6cf847868cd115211221906";
    DateTime currentDateWithTime = DateTime.UtcNow;

    private String dayShortening(String day)
    {
        day = day.ToLower();

        if (day.Equals("monday"))
            return "Mon";
        if (day.Equals("tuesday"))
            return "Tue";
        if (day.Equals("wednesday"))
            return "Wed";
        if (day.Equals("thursday"))
            return "Thu";
        if (day.Equals("friday"))
            return "Fri";
        if (day.Equals("saturday"))
            return "Sat";
        if (day.Equals("sunday"))
            return "Sun";
        return "can not get the day of week";
    }

    private String monthIntToStringConverter(int month)
    {
        if (month == 1)
            return "Jan";
        if (month == 2)
            return "Feb";
        if (month == 3)
            return "Mar";
        if (month == 4)
            return "Apr";
        if (month == 5)
            return "May";
        if (month == 6)
            return "Jun";
        if (month == 7)
            return "Jul";
        if (month == 8)
            return "Aug";
        if (month == 9)
            return "Sep";
        if (month == 10)
            return "Oct";
        if (month == 11)
            return "Nov";
        if (month == 12)
            return "Dec";
        return "month can not be converted into string";
    }

    private String imageSourceSelectorMainpageFrom(String iconSourceFromAPI)
    {
        if (iconSourceFromAPI.Contains("day"))
        {
            return "d" + iconSourceFromAPI.Substring(39);//adds the image number from day-images
        }
        else
        {
            return "n" + iconSourceFromAPI.Substring(41);//adds the image number from night-images
        }
    }

    private String imageSourceSelectorForecastFrom(String iconSourceFromAPI)
    {
        String lastCharacter = iconSourceFromAPI.Substring(iconSourceFromAPI.Length - 1);
        if (lastCharacter.Equals("d"))
        {
            if (iconSourceFromAPI.Equals("01d"))
            {
                return "d" + "113" + ".png";
            }
            if (iconSourceFromAPI.Equals("02d"))
            {
                return "d" + "116" + ".png";
            }
            if (iconSourceFromAPI.Equals("03d"))
            {
                return "d" + "119" + ".png";
            }
            if (iconSourceFromAPI.Equals("04d"))
            {
                return "d" + "122" + ".png";
            }
            if (iconSourceFromAPI.Equals("09d"))
            {
                return "d" + "308" + ".png";
            }
            if (iconSourceFromAPI.Equals("10d"))
            {
                return "d" + "305" + ".png";
            }
            if (iconSourceFromAPI.Equals("11d"))
            {
                return "d" + "200" + ".png";
            }
            if (iconSourceFromAPI.Equals("13d"))
            {
                return "d" + "338" + ".png";
            }
            if (iconSourceFromAPI.Equals("50d"))
            {
                return "d" + "143" + ".png";
            }
        }
        else
        {
            if (iconSourceFromAPI.Equals("01n"))
            {
                return "n" + "113" + ".png";

            }
            if (iconSourceFromAPI.Equals("02n"))
            {
                return "n" + "116" + ".png";
            }
            if (iconSourceFromAPI.Equals("03n"))
            {
                return "n" + "119" + ".png";
            }
            if (iconSourceFromAPI.Equals("04n"))
            {
                return "n" + "122" + ".png";
            }
            if (iconSourceFromAPI.Equals("09n"))
            {
                return "n" + "308" + ".png";
            }
            if (iconSourceFromAPI.Equals("10n"))
            {
                return "n" + "305" + ".png";
            }
            if (iconSourceFromAPI.Equals("11n"))
            {
                return "n" + "200" + ".png";
            }
            if (iconSourceFromAPI.Equals("13n"))
            {
                return "n" + "338" + ".png";
            }
            if (iconSourceFromAPI.Equals("50n"))
            {
                return "n" + "143" + ".png";
            }
        }
        return "forecast-icon can not be determined";
    }

    private async void getGeoLocation()
    {
        try
        {
            var loc = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Lowest));
            lat = loc.Latitude;
            lon = loc.Longitude;
            var url = $"https://us1.locationiq.com/v1/reverse?key={apiKey_LocationIQ}&lat={lat}&lon={lon}&format=json";
            var result = await ApiCaller.Get(url);
            if (result.Successful)
            {
                var locationObject = JsonConvert.DeserializeObject<locationObject.Rootobject>(result.Response);
                location = locationObject.address.city;
            }
        }
        catch(Exception ex)
        {
            location = "Munich";
            lat = 48.137154;
            lon = 11.576124;
            Console.WriteLine(ex.Message);
        }
    }

    private async void GetWeatherInfo()
    {
        var url = $"https://api.weatherapi.com/v1/current.json?key=288f568d6cf847868cd115211221906&q={location}&aqi=no";
        var result = await ApiCaller.Get(url);

        if (result.Successful)
        {
            try
            {
                var weatherobject = JsonConvert.DeserializeObject<weatherObject_WeatherAPI.Rootobject>(result.Response);

                temperatureOfTodayLabel.Text = Math.Round(weatherobject.current.feelslike_c).ToString();//main temperature indicator
                locationLabel.Text = weatherobject.location.name + ",";//city name label
                DateTime date = DateTime.Parse(weatherobject.location.localtime);//parsing the date and the local time
                currentDateWithTime = date;
                exactDateWithTimeLabel.Text = monthIntToStringConverter(date.Month) + " " + date.Day.ToString() + " - " + date.TimeOfDay.Hours
                    + ":" + ((date.TimeOfDay.Minutes.ToString().Length == 2) ? date.TimeOfDay.Minutes : "0" + date.TimeOfDay.Minutes);

                mainHumidityLabel.Text = "%" + weatherobject.current.humidity.ToString();//humidity in procent
                mainWindLabel.Text = weatherobject.current.wind_kph.ToString() + "   " + weatherobject.current.wind_dir;//wind kpH and direction
                mainPressureLabel.Text = (weatherobject.current.pressure_mb / (float)1000.0).ToString(); //pressure in mBar
                mainCloudinessLabel.Text = "%" + weatherobject.current.cloud.ToString(); //cloudiness in procent
                mainPrecipitationLabel.Text = weatherobject.current.precip_mm.ToString();//precipitation in mm
                dayLabel.Text = dayShortening(date.DayOfWeek.ToString());
                mainWeatherImage.Source = imageSourceSelectorMainpageFrom(weatherobject.current.condition.icon);//selecting which image to choose
            }
            catch (JsonException jE)
            {
                Console.WriteLine(jE.Message);
            }
        }
        else
        {
            await DisplayAlert("Weather Info", "Noweather info", "OK");
        }
    }

    List<List> OneDayLaterForecastOf3H_8Times = new List<List>();
    List<List> TwoDayLaterForecastOf3H_8Times = new List<List>();
    List<List> ThreeDayLaterForecastOf3H_8Times = new List<List>();

    private async void GetForecastInfo()
    {
        double temperatureOf_1dayLater = 0;
        double temperatureOf_2dayLater = 0;
        double temperatureOf_3dayLater = 0;

        double minTempOf_1dayLater = 100;//setting a big value for comparison 
        double minTempOf_2dayLater = 100;
        double minTempOf_3dayLater = 100;

        int humidityOf_1dayLater = 0;
        int humidityOf_2dayLater = 0;
        int humidityOf_3dayLater = 0;

        double precipitationOf_1dayLater = 0;
        double precipitationOf_2dayLater = 0;
        double precipitationOf_3dayLater = 0;

        double windOf_1dayLater = 0;
        double windOf_2dayLater = 0;
        double windOf_3dayLater = 0;

        String weekDayof1dayLater;
        String weekDayof2dayLater;
        String weekDayof3dayLater;

        var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={apiKeyOpenWeatherMap}&units=metric";
        var result = await ApiCaller.Get(url);

        if (result.Successful)
        {
            try
            {
                var foreCastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);
                DateTime dateOfToday = DateTime.Today;
                int todayInt = dateOfToday.Day;

                int _1dayLaterInt = dateOfToday.AddDays(1).Day;
                int _2dayLaterInt = dateOfToday.AddDays(2).Day;
                int _3dayLaterInt = dateOfToday.AddDays(3).Day;

                foreach (var list in foreCastInfo.list)
                {
                    DateTime dateOfListItem = DateTime.Parse(list.dt_txt);

                    if (dateOfListItem.Day == _1dayLaterInt)
                    {
                        OneDayLaterForecastOf3H_8Times.Add(list);
                        //minTemp calculation
                        if (list.main.temp_min < minTempOf_1dayLater)
                        {
                            minTempOf_1dayLater = list.main.temp_min;
                        }
                        //wind, precipitation, humidity, temperature calculation
                        if (dateOfListItem.Hour >= 9 && dateOfListItem.Hour <= 21)
                        {
                            windOf_1dayLater += list.wind.speed;
                            precipitationOf_1dayLater += ((list.rain == null) ? 0.0 : (double)list.rain._3h);//null check: list.rain returns null if there is no rain
                            temperatureOf_1dayLater += list.main.temp;
                            if (dateOfListItem.Hour == 12)
                            {
                                _1dayLaterImage.Source = imageSourceSelectorForecastFrom(list.weather[0].icon);
                            }
                        }
                        humidityOf_1dayLater += list.main.humidity;
                    }
                    if (dateOfListItem.Day == _2dayLaterInt)
                    {
                        TwoDayLaterForecastOf3H_8Times.Add(list);
                        if (list.main.temp_min < minTempOf_2dayLater)
                        {
                            minTempOf_2dayLater = list.main.temp_min;
                        }
                        if (dateOfListItem.Hour >= 9 && dateOfListItem.Hour <= 21)
                        {
                            windOf_2dayLater += list.wind.speed;
                            precipitationOf_2dayLater += ((list.rain == null) ? 0.0 : (double)list.rain._3h);//null check: list.rain returns null if there is no rain
                            temperatureOf_2dayLater += list.main.temp;
                            if (dateOfListItem.Hour == 12)
                            {
                                _2dayLaterImage.Source = imageSourceSelectorForecastFrom(list.weather[0].icon);
                            }
                        }
                        humidityOf_2dayLater += list.main.humidity;
                    }
                    if (dateOfListItem.Day == _3dayLaterInt)
                    {
                        ThreeDayLaterForecastOf3H_8Times.Add(list);
                        if (list.main.temp_min < minTempOf_3dayLater)
                        {
                            minTempOf_3dayLater = list.main.temp_min;
                        }
                        if (dateOfListItem.Hour >= 9 && dateOfListItem.Hour <= 21)
                        {
                            windOf_3dayLater += list.wind.speed;
                            precipitationOf_3dayLater += ((list.rain == null) ? 0.0 : (double)list.rain._3h);//null check: list.rain returns null if there is no rain
                            temperatureOf_3dayLater += list.main.temp;
                            if (dateOfListItem.Hour == 12)
                            {
                                _3dayLaterImage.Source = imageSourceSelectorForecastFrom(list.weather[0].icon);
                            }
                        }
                        humidityOf_3dayLater += list.main.humidity;
                    }
                }
                double toBeDivided = 4.0;
                double msec_To_kmH_ConverterValue = 3.6;
                int humidityDivider = 8;
                temperatureOf_1dayLater /= toBeDivided;
                temperatureOf_2dayLater /= toBeDivided;
                temperatureOf_3dayLater /= toBeDivided;

                humidityOf_1dayLater /= humidityDivider;
                humidityOf_2dayLater /= humidityDivider;
                humidityOf_3dayLater /= humidityDivider;

                precipitationOf_1dayLater /= toBeDivided;
                precipitationOf_2dayLater /= toBeDivided;
                precipitationOf_3dayLater /= toBeDivided;

                windOf_1dayLater /= toBeDivided;
                windOf_2dayLater /= toBeDivided;
                windOf_3dayLater /= toBeDivided;
                windOf_1dayLater *= msec_To_kmH_ConverterValue;
                windOf_2dayLater *= msec_To_kmH_ConverterValue;
                windOf_3dayLater *= msec_To_kmH_ConverterValue;

                weekDayof1dayLater = dayShortening(dateOfToday.AddDays(1).DayOfWeek.ToString());
                weekDayof2dayLater = dayShortening(dateOfToday.AddDays(2).DayOfWeek.ToString());
                weekDayof3dayLater = dayShortening(dateOfToday.AddDays(3).DayOfWeek.ToString());

                _1DayLater_day_temp_minTemp_Label.Text = weekDayof1dayLater + "\n" + Math.Round(temperatureOf_1dayLater)
                    + "°" + "    " + Math.Round(minTempOf_1dayLater) + "°";

                _2DayLater_day_temp_minTemp_Label.Text = weekDayof2dayLater + "\n" + Math.Round(temperatureOf_2dayLater)
                    + "°" + "    " + Math.Round(minTempOf_2dayLater) + "°";

                _3DayLater_day_temp_minTemp_Label.Text = weekDayof3dayLater + "\n" + Math.Round(temperatureOf_3dayLater)
                    + "°" + "    " + Math.Round(minTempOf_3dayLater) + "°";

                _1DayLater_day_humi_peri_wind_Label.Text = "Humidity: " + humidityOf_1dayLater + " %" + "\n" +
                    "Precipitation: " + Math.Round(precipitationOf_1dayLater) + " mm" + "\n" +
                    "Wind: " + Math.Round(windOf_1dayLater) + " kmH";

                _2DayLater_day_humi_peri_wind_Label.Text = "Humidity: " + humidityOf_2dayLater + " %" + "\n" +
                    "Precipitation: " + Math.Round(precipitationOf_2dayLater) + " mm" + "\n" +
                    "Wind: " + Math.Round(windOf_2dayLater) + " kmH";

                _3DayLater_day_humi_peri_wind_Label.Text = "Humidity: " + humidityOf_3dayLater + " %" + "\n" +
                    "Precipitation: " + Math.Round(precipitationOf_3dayLater) + " mm" + "\n" +
                    "Wind: " + Math.Round(windOf_3dayLater) + " kmH";

            }
            catch (JsonException jE)
            {
                Console.WriteLine(jE.Message);
            }
        }
        else
        {
            await DisplayAlert("Forecast Info", "Noforecast info", "OK");
        }

    }

    private async void GetHourlyForecastInfo4days()
    {
        var days = 2;
        var url = $"https://api.weatherapi.com/v1/forecast.json?key={apiKeyfreeWeatherApi}&q={location}&days={days}&aqi=no&alerts=no";
        var result = await ApiCaller.Get(url);

        if (result.Successful)
        {
            try
            {
                var weatherobjectHourly = JsonConvert.DeserializeObject<Rootobject>(result.Response);

                var localtime = weatherobjectHourly.location.localtime;
                
                DateTime currentDateHoursApproximated;

                //deleting the minutes ex.: 14:27 -> 14:00
                if (currentDateWithTime.ToString().Contains("AM"))
                {
                    currentDateHoursApproximated = DateTime.Parse(currentDateWithTime.Month + "/" +
                        currentDateWithTime.Day + "/" + currentDateWithTime.Year + " " + currentDateWithTime.Hour + ":00:00 AM");
                }
                else
                {
                    currentDateHoursApproximated = DateTime.Parse(currentDateWithTime.Month + "/" +
                            currentDateWithTime.Day + "/" + currentDateWithTime.Year + " " + currentDateWithTime.Hour + ":00:00 PM");
                }
                eightHoursLaterLabel.Text = 
                    currentDateHoursApproximated.AddHours(1).Hour.ToString() + ":00" + "               " +
                    currentDateHoursApproximated.AddHours(2).Hour.ToString() + ":00" + "               " +
                    currentDateHoursApproximated.AddHours(3).Hour.ToString() + ":00" + "               " +
                    currentDateHoursApproximated.AddHours(4).Hour.ToString() + ":00" + "               " +
                    currentDateHoursApproximated.AddHours(5).Hour.ToString() + ":00" + "               " +
                    currentDateHoursApproximated.AddHours(6).Hour.ToString() + ":00" + "               " +
                    currentDateHoursApproximated.AddHours(7).Hour.ToString() + ":00" + "               " +
                    currentDateHoursApproximated.AddHours(8).Hour.ToString() + ":00" + "               " ;
                
                foreach (var day in weatherobjectHourly.forecast.forecastday)
                {
                    foreach (var h in day.hour)
                    {
                        DateTime dt = DateTime.Parse(h.time);
                        currentDateWithTime = DateTime.Parse(currentDateWithTime.ToString("MM/dd/yyyy HH:mm"));//to have the same time format
                        

                        if (currentDateHoursApproximated.AddHours(1).Equals(dt))
                        {
                            ImageHourlyForecast1h.Source = imageSourceSelectorMainpageFrom(h.condition.icon); continue;
                        }
                        if (currentDateHoursApproximated.AddHours(2).Equals(dt))
                        {
                            ImageHourlyForecast2h.Source = imageSourceSelectorMainpageFrom(h.condition.icon); continue;
                        }
                        if (currentDateHoursApproximated.AddHours(3).Equals(dt))
                        {
                            ImageHourlyForecast3h.Source = imageSourceSelectorMainpageFrom(h.condition.icon); continue;
                        }
                        if (currentDateHoursApproximated.AddHours(4).Equals(dt))
                        {
                            ImageHourlyForecast4h.Source = imageSourceSelectorMainpageFrom(h.condition.icon); continue;
                        }
                        if (currentDateHoursApproximated.AddHours(5).Equals(dt))
                        {
                            ImageHourlyForecast5h.Source = imageSourceSelectorMainpageFrom(h.condition.icon); continue;
                        }
                        if (currentDateHoursApproximated.AddHours(6).Equals(dt))
                        {
                            ImageHourlyForecast6h.Source = imageSourceSelectorMainpageFrom(h.condition.icon); continue;
                        }
                        if (currentDateHoursApproximated.AddHours(7).Equals(dt))
                        {
                            ImageHourlyForecast7h.Source = imageSourceSelectorMainpageFrom(h.condition.icon); continue;
                        }
                        if (currentDateHoursApproximated.AddHours(8).Equals(dt))
                        {
                            ImageHourlyForecast8h.Source = imageSourceSelectorMainpageFrom(h.condition.icon); break;

                        }
                    }
                }
            }
            catch (JsonException jE)
            {
                Console.WriteLine(jE.Message);
            }
        }
        else
        {
            await DisplayAlert("Hourly weather Info", "No hourly weather info", "OK");
        }
    }
}
