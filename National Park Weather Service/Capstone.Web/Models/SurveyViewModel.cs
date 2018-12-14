using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    /// <summary>
    /// This Model is used for the binding of the the data gotten from the Survey view.
    /// </summary>
    public class SurveyViewModel
    {
        //The user chooses a Park Name in the survey,
        //and we get the Park Code that corresponds to that name.
        [Required(ErrorMessage = "*")]
        public string ParkCode { get; set; }

        [Required(ErrorMessage = "Invalid Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        public string State { get; set; }

        [Required(ErrorMessage = "*")]
        public string ActivityLevel { get; set; }
    }
}