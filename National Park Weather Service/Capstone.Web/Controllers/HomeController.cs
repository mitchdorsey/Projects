using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        private const string tempTypekey = "tempType";
        private const string parkCodeKey = "parkCode";

        private INPGeekDAL _dal = null;

        public HomeController(INPGeekDAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IList<IndexViewModel> model = _dal.GetParksForHomePage();
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Detail(string parkCode)
        {
            DetailViewModel model = new DetailViewModel();
            model.ParkDetails = _dal.GetAllDetailsByParkCode(parkCode);
            model.FiveDayForecast = _dal.GetFiveDayForecast(parkCode);

            //save the park code of the detail view the user navigates to
            Session[parkCodeKey] = model.ParkDetails.ParkCode;

            //pull current temperature unit of measurement
            string sessionTempType = Session[tempTypekey] as string;
            
            //check current temperature unit to display
            if (sessionTempType != null && sessionTempType == "Celsius")
            {
                model.TempType = sessionTempType;
                model.FiveDayForecast = ConvertToCelsius(model.FiveDayForecast);
            }
            else
            {
                Session[tempTypekey] = model.TempType;
            }

            return View("Detail", model);
        }

        [HttpPost]
        public ActionResult DetailTempUpdate(string tempType)
        {
            string sessionParkCode = Session[parkCodeKey] as string;
            Session[tempTypekey] = tempType;            

            return Detail(sessionParkCode);
        }

        // GET: Home/Survey
        // Return the empty survey view
        [HttpGet]
        public ActionResult Survey()
        {
            return View();
        }

        // POST: Home/Survey
        // Validate the model and redirect to SurveyResults (if successful) or return the 
        // Survey view (if validation fails)        
        [HttpPost]
        public ActionResult Survey(SurveyViewModel model)
         {
            string action = "";
            // Let's validate the model before proceeding
            if (!ModelState.IsValid)
            {
                return View("Survey");
            }
            //Add survey to database and confirm it worked
            int numRowsAffected = _dal.AddSurveyToDatabase(model);
            if(numRowsAffected > 0)
            {
                action = "SurveyResults";
            }
            else
            {
                action = "Survey";
            }

            return RedirectToAction(action);
        }

        // GET: Home/SurveyResults
        // Return the empty survey view
        [HttpGet]
        public ActionResult SurveyResults()
        {
            IList<SurveyResultsViewModel> model = _dal.GetSurveyResults();
            return View("SurveyResults", model);
        }

        private IList<ForecastDay> ConvertToCelsius(IList<ForecastDay> fiveDayForecast)
        {
            IList<ForecastDay> updatedForecast = new List<ForecastDay>();

            for (int i = 0; i < fiveDayForecast.Count(); i++)
            {
                fiveDayForecast[i].High = (int)((5.0 / 9.0) * (fiveDayForecast[i].High - 32));
                fiveDayForecast[i].Low = (int)((5.0 / 9.0) * (fiveDayForecast[i].Low - 32));
                updatedForecast.Add(fiveDayForecast[i]);
            }

            return updatedForecast;
        }
}
}