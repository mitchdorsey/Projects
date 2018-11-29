using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public abstract class Item
    {
        #region properties

        /// <summary>
        /// properties inherited by each item type subclass for
        /// slot ID, item name, item price, inventory quantity,
        /// and quantity sold during each transaction event
        /// </summary>
        public string Slot { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Qty { get; private set; } = 5;
        public int QtySold { get; private set; } = 0;

        #endregion

        #region constructor

        /// <summary>
        /// constructor for Item objects, slot ID, 
        /// item name, and price are passed in as
        /// arguments
        /// </summary>
        /// <param name="slot">slot ID</param>
        /// <param name="name">item name</param>
        /// <param name="price">item price</param>
        public Item(string slot, string name, decimal price)
        {
            Slot = slot;
            Name = name;
            Price = price;
        }

        #endregion

        #region methods

        /// <summary>
        /// decrements inventory quantity by 1,
        /// and increments quantity sold by 1
        /// </summary>
        public void QtyUpdate()
        {
            Qty--;
            QtySold++;
        }

        /// <summary>
        /// abstract method to return a string sound
        /// </summary>
        /// <returns></returns>
        public abstract string MakeSound();
        /// <summary>
        /// abstract method to return a string for ascii art
        /// </summary>
        /// <returns></returns>
        public abstract string ItemArt();

        #endregion
    }
}
