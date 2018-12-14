using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface INPGeekDAL
    {
        IList<IndexViewModel> GetParksForHomePage();
        ParkDetails GetAllDetailsByParkCode(string parkCode);
        IList<ForecastDay> GetFiveDayForecast(string parkCode);
        int AddSurveyToDatabase(SurveyViewModel model);
        IList<SurveyResultsViewModel> GetSurveyResults();
    }
}
