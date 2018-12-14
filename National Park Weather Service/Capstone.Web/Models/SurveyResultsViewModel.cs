using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    /// <summary>
    /// This Model gets populated by the DAL.
    /// </summary>
    public class SurveyResultsViewModel
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        //This property comes from the subquery in the DAL method, GetSurveyResults().
        //SurveyCount is an alias and not an actualy column in the "survery_count" table
        public int SurveyCount { get; set; }
    }
}