using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    /// <summary>
    /// This Model contains all the information needed for the Detail view.
    /// </summary>
    public class DetailViewModel
    {
        //Populated by DAL method, GetAllDetailsByParkCode()
        //Contains ALL of the information from the "park" table in the database
        public ParkDetails ParkDetails { get; set; }
        //Populated by DAL method, GetFiveDayForecast()
        public IList<ForecastDay> FiveDayForecast { get; set; }
        //This property defaults to Fahrenheit,
        //but it can be changed to Celsius by the user
        //and stored in the session data.
        public string TempType { get; set; } = "Fahrenheit";
    }
}