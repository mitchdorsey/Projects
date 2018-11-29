using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Chip : Item
    {
        #region constructor

        /// <summary>
        /// Chip object constructor, calls Item constructor
        /// passes in slot ID, item name, and item price
        /// </summary>
        /// <param name="slot">slot ID</param>
        /// <param name="name">item name</param>
        /// <param name="price">item price</param>
        public Chip(string slot, string name, decimal price) : base(slot, name, price)
        {

        }

        #endregion

        #region methods

        /// <summary>
        /// ascii art for Chip object
        /// </summary>
        /// <returns>ascii art</returns>
        public override string ItemArt()
        {
            return @"          ,(((/*/%%%%%%%%%#*       .*/#%%%%# 
         (%%%####////**/**//##(((///((//((#  
          %**,,,,,,,,,,**,,,,,,,******,***   
           #**,,,,,,,*/************,,***/%   
           ./****,,,,,,*/*******,,,,*,*/(    
            (**,****,,,,,*/*,*,,,,,****/,    
            %/*,,,,,,,***,,,,,,,,,*****%     
            ***,,,,,,,,,,,,,,,******/*%     
            ***,,,,,,,,*,,,,,,,,***/*/(     
             *##**##*#,,#/#%##,*#/#**%**     
            %*#(,,,,/#(##/#%#(****####*/%    
            (**(#((%,#,*(*(%#(,,,*##(#**%    
            **,(###%%###############(/**%    
            /****,,,,,,,***********/%/**#    
           %*//*,,,,,,,,,,,,,,,,,,,,***/%    
         ./**,,,,,,,,,,,,,,,,,,,,,,,****    
         ./***,,,,,,,,,,,,,,,,,,,,*******%   
        ,/,*/**,,,,,,,,,,,**,,,,,,,***/**/   
        /,*******************************/   
       %****/*/********************/******.  
      %%//#%##(((((##%%%%###(##(/*///(/%//(  
                                        (%% ";
        }

        /// <summary>
        /// sound for Chip Object
        /// </summary>
        /// <returns>string sound</returns>
        public override string MakeSound()
        {
            return "Crunch Crunch, Yum!";
        }

        #endregion
    }
}
