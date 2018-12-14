using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class IndexViewModel
    {
        /// <summary>
        /// Specific data model for the National Park Geek Home Page
        /// </summary>
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public string ParkDescription { get; set; }
    }
}