using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;
using System.Threading;

namespace Capstone
{
    public class VendingMenus
    {
        #region variables

        /// <summary>
        /// title string, "press (r) to return" string was reused several times
        /// so it made sense to pull it out into its own member variable, also
        /// the VendingMachine object is initialized as null in a member variable,
        /// Item objects are added to the purchase list each time a transaction is made
        /// </summary>
        private string _title = @"████████╗ █████╗ ███████╗████████╗██╗   ██╗    ███████╗███╗   ██╗ █████╗  ██████╗██╗  ██╗███████╗██╗
╚══██╔══╝██╔══██╗██╔════╝╚══██╔══╝╚██╗ ██╔╝    ██╔════╝████╗  ██║██╔══██╗██╔════╝██║ ██╔╝██╔════╝██║
   ██║   ███████║███████╗   ██║    ╚████╔╝     ███████╗██╔██╗ ██║███████║██║     █████╔╝ ███████╗██║
   ██║   ██╔══██║╚════██║   ██║     ╚██╔╝      ╚════██║██║╚██╗██║██╔══██║██║     ██╔═██╗ ╚════██║╚═╝
   ██║   ██║  ██║███████║   ██║      ██║       ███████║██║ ╚████║██║  ██║╚██████╗██║  ██╗███████║██╗
   ╚═╝   ╚═╝  ╚═╝╚══════╝   ╚═╝      ╚═╝       ╚══════╝╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝";

        private string _pressR = "Press (r) to return to the previous menu";
        private List<Item> _purchaseList = new List<Item>();
        public VendingMachine _vm = null;

        #endregion

        #region constructor

        //constructs a VendingMenus object, passing a VendingMachine object as an argument
        public VendingMenus(VendingMachine vm)
        {
            _vm = vm;
        }

        #endregion

        #region methods

        /// <summary>
        /// displays first menu, with title
        /// options for displaying the list of items available for purchase,
        /// purchasing items, and walking away from the vending machine (exit)
        /// </summary>
        public void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine($"\n{_title}\n");
                Console.WriteLine($"{{  For Your Health  }}\n".PadLeft(57, ' '));
                Console.WriteLine("(1) Display Items\n".PadLeft(29, ' '));
                Console.WriteLine("(2) Purchase Items\n".PadLeft(35, ' '));
                Console.WriteLine("(3) Walk Away From Vending Machine\n".PadLeft(56, ' '));

                char input = Console.ReadKey().KeyChar;

                if (input == '1')
                {
                    DisplayMenu();
                }
                else if (input == '2')
                {
                    PurchaseMenu();
                }
                else if (input == '3')
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid selection!");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// menu that diplays all available items by slot position,
        /// item name, price, and quantity in the vending machine,
        /// there is an option to go back to the main menu
        /// </summary>
        public void DisplayMenu()
        {
            

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n " + _title + "\n");

                DisplayItems();

                Console.WriteLine($"\n{_pressR}");

                bool isLetterR = (('r' == Console.ReadKey().KeyChar) || ('R' == Console.ReadKey().KeyChar));

                if (isLetterR)
                {
                exit = true;
                }
            }
        }

        /// <summary>
        /// menu that displays options for feeding money into the machine,
        /// selecting a product for purchase, finishing the transaction,
        /// or going back to the main menu
        /// also displays the current money in the machine
        /// </summary>
        public void PurchaseMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n" + _title + "\n");
                Console.WriteLine($"{{  Current Money In Machine: {_vm.MachineBalance.ToString("C")}  }}\n".PadLeft(66, ' '));
                Console.WriteLine("(1) Feed Money\n".PadLeft(26, ' '));
                Console.WriteLine("(2) Select Product\n".PadLeft(35, ' '));
                Console.WriteLine("(3) Finish Transaction\n\n".PadLeft(45, ' '));
                Console.WriteLine($"{_pressR}".PadLeft(64, ' '));

                char purchaseMenuInput = Console.ReadKey().KeyChar;

                if (purchaseMenuInput == '1')
                {
                    FeedMoneyMenu();
                }
                else if (purchaseMenuInput == '2')
                {
                    SelectProductMenu();
                }
                else if (purchaseMenuInput == '3')
                {
                    FinishTransactionMenu();
                }
                else if (purchaseMenuInput == 'r' || purchaseMenuInput == 'R')
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Not a valid selection!");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// displays the menu allowing the user to select which size
        /// of bill(s) to feed into the machine
        /// includes an option to return to the previous menu when finished
        /// also displays the total amount of money in tthe machine
        /// !this menu doesn't need to handle invalid input
        /// any invalid dollar bill simply isn't accepted and has no affect
        /// </summary>
        public void FeedMoneyMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n" + _title);
                Console.WriteLine("Select Amount of Money to Feed:\n");
                Console.WriteLine("(1) $1\n(2) $2\n(3) $5\n(4) $10\n(5) $20");
                Console.WriteLine($"\n{_pressR} when you are finished.\n");
                Console.Write($"Current Money Provided: {_vm.MachineBalance.ToString("C")}");

                char selection = Console.ReadKey().KeyChar;

                if(selection == 'r' || selection == 'R')
                {
                    exit = true;
                }
                else
                {
                    _vm.FeedMoney(selection);
                }
            }
        }

        /// <summary>
        /// dislays menu to select product, shows the same Display() method
        /// called by the DisplayMenu()
        /// allows entry of slot ID, then checks validity of entry
        /// if valid, displays a string telling the user what item they
        /// purchased, and ascii art of the item type
        /// also tells user if an item is sold out, if the user is out
        /// of money, or if selection is invalid
        /// automatically returns to the previous menu after each input
        /// </summary>
        public void SelectProductMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n" + _title + "\n");

                DisplayItems();

                Console.WriteLine("\nEnter Slot ID for the item you would like to purchase: ");
                string selection = Console.ReadLine();

                Item itemPurchased = _vm.Dispense(selection);

                bool enoughMoney = true;
                bool productExists = _vm.ProductExists(selection);
                bool soldOut = _vm.SoldOut(selection);

                if (!productExists)
                {
                    Console.WriteLine("Not a valid selection!");
                    Console.ReadKey();
                    exit = true;
                }
                else
                {
                    enoughMoney = _vm.MachineBalance >= itemPurchased.Price;
                }
                
                if (productExists && soldOut)
                {
                    Console.WriteLine("Selection is SOLD OUT.");
                    Console.WriteLine("\nPress any key to return to the Purchase Menu.");
                    Console.ReadKey();

                    exit = true;
                }
                else if (productExists && enoughMoney)
                {
                    _purchaseList.Add(itemPurchased);
                    _vm.UpdateBalance(selection);
                    itemPurchased.QtyUpdate();

                    Console.WriteLine($"\n              You purchased: {itemPurchased.Name}!!!\n");
                    Console.WriteLine(itemPurchased.ItemArt());
                    Console.WriteLine("\n\nPress any key to return to the Purchase Menu.");
                    Console.ReadKey();

                    exit = true;
                }
                else if (productExists && !enoughMoney)
                {
                    Console.WriteLine("Add more money to machine to purchase this item!");
                    Console.ReadKey();

                    exit = true;
                }
            }
        }

        /// <summary>
        /// finalizes user's transaction
        /// displays how many of each coin user receives when change is dispensed,
        /// displays a sound string for each item in purchase list unique to its type,
        /// with options to return to the previous menu or walk away form the vending
        /// machine, also checks for validity of user's input
        /// </summary>
        public void FinishTransactionMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine($"\n [{_vm.FinishTransaction()}]\n");

                foreach (Item item in _purchaseList)
                {
                    Console.WriteLine($"     {item.MakeSound()}");
                }

                _purchaseList.RemoveRange(0, _purchaseList.Count);

                Console.WriteLine($"\n{_pressR}");
                Console.WriteLine("\nPress (w) to walk away from the vending machine");

                char selection = Console.ReadKey().KeyChar;

                if (selection == 'r' || selection == 'R')
                {
                    exit = true;
                }
                else if (selection == 'w' || selection == 'W')
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Not a valid selection!");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// this method displays a formatted table for every item in the 
        /// vending machine, by slot ID, name, price, and quantity in machine
        /// used twice in the programs code
        /// </summary>
        private void DisplayItems()
        {
            Console.WriteLine("Slot ID".PadRight(10, ' ') +
                              "Item Name".PadRight(21, ' ') +
                              "Price".PadRight(10, ' ') +
                              "Quantity".PadRight(10, ' '));

            Console.WriteLine("".PadRight(49, '-'));

            foreach (Item item in _vm._itemList)
            {
                Console.WriteLine($"{item.Slot.PadRight(9, ' ')} " +
                                  $"{item.Name.PadRight(20, ' ')} " +
                                  $"{item.Price.ToString("C").PadRight(10, ' ')}" +
                                  $"{item.Qty}");
            }
        }

        #endregion
    }
}
