using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone
{
    public class VendingMachine
    {
        #region variables

        /// <summary>
        /// private constant string member variables for the ralative paths 
        /// for the initial stock inventory and where to write/update the
        /// sales report and log
        /// inventory item objects are created and stored in a public member
        /// variable list, and sales report info is read or created and entered
        /// into the sales report dictionary with item name and quantity sold
        /// </summary>
        private const string _inventoryPath = @"..\..\..\etc\vendingmachine.csv";
        private const string _reportPath = @"..\..\..\Sales-Report.txt";
        private const string _logPath = @"..\..\..\Log.txt";

        public List<Item> _itemList = new List<Item>();
        private Dictionary<string, int> _salesReport = new Dictionary<string, int>();

        #endregion

        #region properties

        /// <summary>
        /// properties for the amount of money currently in the machine,
        /// total sales for each transaction or group of transactions (to
        /// write to the sales report), properties for string pertaining to 
        /// the final 2 columns of the log, and a property for the event
        /// column of the log
        /// </summary>
        public decimal MachineBalance { get; private set; } = 0;
        private decimal TotalSales { get; set; }
        private string LogColA { get; set; }
        private string LogColB { get; set; }
        private string Event { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// VendingMachine constructor, 
        /// populates the list of inventory items
        /// </summary>
        public VendingMachine()
        {
            PopulateList();
        }

        #endregion

        #region methods

        /// <summary>
        /// takes user input and increments the MachineBalance
        /// by the number of dollars per bill "fed"
        /// also sets property values for LogColA and LogColB
        /// to be written in the log
        /// finally, prints appropriate info to the log
        /// </summary>
        /// <param name="input"></param>
        public void FeedMoney(char input)
        {
            if (input == '1')
            {
                MachineBalance += 1;
                LogColA = "$1.00";
            }
            else if (input == '2')
            {
                MachineBalance += 2;
                LogColA = "$2.00";
            }
            else if (input == '3')
            {
                MachineBalance += 5;
                LogColA = "$5.00";
            }
            else if (input == '4')
            {
                MachineBalance += 10;
                LogColA = "$10.00";
            }
            else if (input == '5')
            {
                MachineBalance += 20;
                LogColA = "$20.00";
            }

            LogColB = MachineBalance.ToString("C");
            Event = "FEED MONEY:";

            PrintToLog();
        }

        /// <summary>
        /// takes slot ID input and returns the appropriate Item object
        /// </summary>
        /// <param name="slot">slot ID entered by user</param>
        /// <returns>returns Item object</returns>
        public Item Dispense(string slot)
        {
            Item product = null;

            foreach (Item item in _itemList)
            {
                bool selectionMatch = item.Slot.ToLower() == slot.ToLower();

                if (selectionMatch)
                {
                    product = item;
                }
            }
            return product;
        }

        /// <summary>
        /// updates the MachineBalance by finding the Item
        /// matching the slot ID entered and subtracts the item's
        /// price from the MachineBalance
        /// also updates TotalSales by adding the item price
        /// to TotalSales
        /// also updates property values for the appropriate log
        /// Event, LogColA, and LogColB
        /// finally, prints to log
        /// </summary>
        /// <param name="slot">slot ID entered by user</param>
        public void UpdateBalance(string slot)
        {
            foreach (Item item in _itemList)
            {
                bool selectionMatch = item.Slot.ToLower() == slot.ToLower();

                if (selectionMatch)
                {
                    Event = $"{item.Name} {item.Slot}";
                    LogColA = MachineBalance.ToString("C");

                    MachineBalance -= item.Price;
                    TotalSales += item.Price;

                    LogColB = MachineBalance.ToString("C");

                    PrintToLog();
                }
            }
        }

        /// <summary>
        /// calculates change by dividing the MachineBalance
        /// into quarters, then dimes, then nickels
        /// updates appropriate log properties and writes to log
        /// </summary>
        /// <returns>string displaying what coins user received after transaction</returns>
        public string FinishTransaction()
        {
            int balance = (int)(MachineBalance * 100);

            int nickel = 5;
            int dime = 10;
            int quarter = 25;

            int nickelCount = 0;
            int dimeCount = 0;
            int quarterCount = 0;

            LogColA = MachineBalance.ToString("C");

            while (balance > 0)
            {
                if (balance >= quarter)
                {
                    balance -= quarter;
                    quarterCount++;
                }
                else if (balance >= dime)
                {
                    balance -= dime;
                    dimeCount++;
                }
                else if (balance >= nickel)
                {
                    balance -= nickel;
                    nickelCount++;
                }
            }

            string change = $"Your change is {nickelCount} nickels, " +
                $"{dimeCount} dimes, and {quarterCount} quarters.";

            MachineBalance = 0;
            LogColB = 0.ToString("C");
            Event = "GIVE CHANGE:";

            PrintToLog();
            GenerateSalesReport();

            return change;
        }

        /// <summary>
        /// reads in inventory text file and checks the type of each item
        /// before adding it to the inventory list
        /// also initializes the sales report dictionary with item names
        /// and a default quantity sold value of 0
        /// </summary>
        private void PopulateList()
        {
            using (StreamReader sr = new StreamReader(_inventoryPath))
            {
                while(!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('|');

                    if (line[3] == "Candy")
                    {
                        _itemList.Add(new Candy(line[0], line[1], decimal.Parse(line[2])));
                    }
                    else if (line[3] == "Chip")
                    {
                        _itemList.Add(new Chip(line[0], line[1], decimal.Parse(line[2])));
                    }
                    else if (line[3] == "Drink")
                    {
                        _itemList.Add(new Drink(line[0], line[1], decimal.Parse(line[2])));
                    }
                    else if (line[3] == "Gum")
                    {
                        _itemList.Add(new Gum(line[0], line[1], decimal.Parse(line[2])));
                    }
                }

                foreach (Item item in _itemList)
                {
                    _salesReport[item.Name] = 0;
                }
            }
        }
        
        /// <summary>
        /// bool that checks if the slot ID entered by the user 
        /// is actually the slot ID of any item
        /// </summary>
        /// <param name="slot">slot ID entered by user</param>
        /// <returns>true or false</returns>
        public bool ProductExists(string slot)
        {
            bool result = false;

            foreach (Item item in _itemList)
            {
                bool selectionMatch = item.Slot.ToLower() == slot.ToLower();

                if (selectionMatch)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// bool that checks if an item's quantity is 0 or greater than 0
        /// </summary>
        /// <param name="slot">slot ID entered by user</param>
        /// <returns>true or false</returns>
        public bool SoldOut(string slot)
        {
            bool result = true;

            foreach (Item item in _itemList)
            {
                bool selectionMatch = item.Slot.ToLower() == slot.ToLower();

                if (selectionMatch)
                {
                    if (item.Qty > 0)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// reads in present sales report, if exists, then updates
        /// sales report dictionary values, adding previous total sales
        /// to total sales after each user transaction
        /// finally, writes new updated values to the sales report
        /// by overwriting existing data
        /// </summary>
        private void GenerateSalesReport()
        {
            if (File.Exists(_reportPath))
            {
                using (StreamReader sr = new StreamReader(_reportPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (line.Contains("$"))
                        {
                            string[] totalLine = line.Split('$');
                            TotalSales += decimal.Parse(totalLine[1]);
                        }
                        else if (line != "")
                        {
                            string[] dictString = line.Split('|');
                            string itemName = dictString[0];
                            int lastQtySold = int.Parse(dictString[1]);

                            foreach (Item item in _itemList)
                            {
                                if (item.Name == itemName)
                                {
                                    _salesReport[itemName] = lastQtySold + item.QtySold;
                                }
                            }
                        }
                    }
                }

                using (StreamWriter sw = new StreamWriter(_reportPath, false))
                {
                    foreach (KeyValuePair<string, int> entry in _salesReport)
                    {
                        sw.WriteLine($"{entry.Key}|{entry.Value}");
                    }

                    sw.WriteLine();
                    sw.WriteLine($"**TOTAL SALES** {TotalSales.ToString("C")}");
                    TotalSales = 0;
                }
            }
            else
            {

                using (StreamWriter sw = new StreamWriter(_reportPath, false))
                {
                    foreach (KeyValuePair<string, int> line in _salesReport)
                    {
                        string key = line.Key;
                        int qtySold = 0;

                        foreach (Item item in _itemList)
                        {
                            if (item.Name == key)
                            {
                                qtySold = item.QtySold;
                            }
                        }

                        sw.WriteLine($"{line.Key}|{line.Value + qtySold}");
                    }

                    sw.WriteLine();
                    sw.WriteLine($"**TOTAL SALES** {TotalSales.ToString("C")}");
                }
            }
        }

        /// <summary>
        /// write a formatted line of data to the log
        /// </summary>
        private void PrintToLog()
        {
            using (StreamWriter sw = new StreamWriter(_logPath, true))
            {
                sw.WriteLine(DateAndTime().PadRight(25, ' ')
                    + Event.PadRight(20, ' ') + LogColA.PadRight(10, ' ') + LogColB.PadRight(10, ' '));
            }
        }

        /// <summary>
        /// get the current date and time
        /// </summary>
        /// <returns></returns>
        private string DateAndTime()
        {
            return DateTime.Now.ToString();
        }

        #endregion
    }
}