using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Gum : Item
    {
        #region constructor

        /// <summary>
        /// Gum object constructor, calls Item constructor
        /// passes in slot ID, item name, and item price
        /// </summary>
        /// <param name="slot">slot ID</param>
        /// <param name="name">item name</param>
        /// <param name="price">item price</param>
        public Gum(string slot, string name, decimal price) : base(slot, name, price)
        {

        }

        #endregion

        #region methods

        /// <summary>
        /// ascii art for Gum object
        /// </summary>
        /// <returns>ascii art</returns>
        public override string ItemArt()
        {
            return @"                                                           (&,.*#*             
                                                     .%############%%*.,#(.    
                                                /%#####%&%##%%##############&%.
                                           ####&,             *########%&%%%%..
                                     *#%*                 %#####&%%%%%%%%%%..
                                (%###&                  %###%&%%%%%%%%%%%%%%%.,
                          .%#######               .%###&%%%%%%%%%%%%%%%%%%%%%.,
                     (%###########%       ,(&%####&%%%%%%%%%%%%%%%%%%%%%%%%%%.(
               .%#########################%%%%%%%%%%%%%%%%%%%%%%%%%%%%%&,    
              .****/%#,,#%#############&%%%%%%%%%%%%%%%%%%%%%%%%%%%%#          
              .**#(... ..../%*.(%#&%%%%%%%%%%%%%%%%%%%%%%%%%%%%#               
              .%*./%/...........,&%%%%%%%%%%%%%%%%%%%%%%%&*                    
              ,/%*.(#,.##,,##,...&%%%%%%%%%%%%%%%%%%%.                         
              *.(%,.(#,,##,,#(.*.&%%%%%%%%%%%%%(                               
         ,%%#########,,#(.*%/..%%%%%%%%&.                                    
  /%,*%%#####################%%/.%%%#                                          
#(./%*..  ..../%%#########%&%&*                                                
       ,#/./%*......,%&%(                                                      ";
        }

        /// <summary>
        /// sound for Gum Object
        /// </summary>
        /// <returns>string sound</returns>
        public override string MakeSound()
        {
            return "Chew Chew, Yum!";
        }

        #endregion
    }
}
