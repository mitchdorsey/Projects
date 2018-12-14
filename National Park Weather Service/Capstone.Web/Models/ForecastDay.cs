using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ForecastDay
    {
        public string TempType { get; set; } = "Fahrenheit";

        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }
        public string Advisory {
            get
            {
                return GetAdvisoryMessage(Forecast, High, Low);
            }
        }

        public string GetAdvisoryMessage(string forecast, int high, int low)
        {
            string result = "";
            string baseMessage = "";
            string tempMessage = null;

            if(forecast == "snow")
            {
                baseMessage = "Pack snowshoes!";
            }
            else if (forecast == "rain")
            {
                baseMessage = "Pack rain gear and waterproof shoes!";
            }
            else if (forecast == "thunderstorms")
            {
                baseMessage = "Seek shelter, and avoid hiking on exposed ridges!";
            }
            else if (forecast == "sunny")
            {
                baseMessage = "Pack sunblock!";
            }
            else if (forecast == "partly cloudy")
            {
                baseMessage = "Partly Cloudy!";
            }
            else if(forecast == "cloudy")
            {
                baseMessage = "Not much sun today!";
            }

            result = baseMessage;

            if (High > 75)
            {
                tempMessage = "Bring an extra gallon of water!";
            }
            else if ((High - Low) > 20)
            {
                tempMessage = "Wear breathable layers!";
            }
            else if (Low < 20)
            {
                tempMessage = "Beware of exposure to frigid temperatures!";
            }
            
            if (tempMessage != null)
            {
                result += $" {tempMessage}";
            }

            return result;
        }
    }
}