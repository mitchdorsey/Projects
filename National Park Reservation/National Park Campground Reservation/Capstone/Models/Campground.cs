using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Campground
    {
        public int CampgroundID { get; set; }

        public int ParkID { get; set; }

        public string CampgroundName { get; set; }

        public int OpenFrom { get; set; }

        public string DisplayMonthOpen
        {
            get
            {
                if (OpenFrom == 1)
                {
                    return "January";
                }
                else if (OpenFrom == 2)
                {
                    return "Febuary";
                }
                else if (OpenFrom == 3)
                {
                    return "March";
                }
                else if (OpenFrom == 4)
                {
                    return "April";
                }
                else if (OpenFrom == 5)
                {
                    return "May";
                }
                else if (OpenFrom == 6)
                {
                    return "June";
                }
                else if (OpenFrom == 7)
                {
                    return "July";
                }
                else if (OpenFrom == 8)
                {
                    return "August";
                }
                else if (OpenFrom == 9)
                {
                    return "September";
                }
                else if (OpenFrom == 10)
                {
                    return "October";
                }
                else if (OpenFrom == 11)
                {
                    return "November";
                }
                else if (OpenFrom == 12)
                {
                    return "December";
                }
                else
                    return "Invalid";

            }
            
        }
        
        public int OpenTill { get; set; }

        public string DisplayMonthClose
        {
            get
            {
                if (OpenTill == 1)
                {
                    return "January";
                }
                else if (OpenTill == 2)
                {
                    return "Febuary";
                }
                else if (OpenTill == 3)
                {
                    return "March";
                }
                else if (OpenTill == 4)
                {
                    return "April";
                }
                else if (OpenTill == 5)
                {
                    return "May";
                }
                else if (OpenTill == 6)
                {
                    return "June";
                }
                else if (OpenTill == 7)
                {
                    return "July";
                }
                else if (OpenTill == 8)
                {
                    return "August";
                }
                else if (OpenTill == 9)
                {
                    return "September";
                }
                else if (OpenTill == 10)
                {
                    return "October";
                }
                else if (OpenTill == 11)
                {
                    return "November";
                }
                else if (OpenTill == 12)
                {
                    return "December";
                }
                else
                    return "Invalid";

            }

        }

        public decimal DailyFee { get; set; }
    }
}
