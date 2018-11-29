using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Capstone.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void FeedMoneyTest()
        {
            VendingMachine testMachine = new VendingMachine();

            testMachine.FeedMoney('1');
            Assert.AreEqual(1, testMachine.MachineBalance, "User selects option to add $1 to machine.");

            
            testMachine.FeedMoney('2');
            Assert.AreEqual(3, testMachine.MachineBalance, "User has already inserted $1, selects option to add $2 to machine.");

            
            testMachine.FeedMoney('3');
            Assert.AreEqual(8, testMachine.MachineBalance, "User has already inserted $3, selects option to add $5 to machine.");

       
            testMachine.FeedMoney('4');
            Assert.AreEqual(18, testMachine.MachineBalance, "User has already inserted $8, selects option to add $10 to machine.");

            
            testMachine.FeedMoney('5');
            Assert.AreEqual(38, testMachine.MachineBalance, "User has already inserted $18, selects option to add $20 to machine.");
        }
        [TestMethod]
        public void DispenseTest()
        {
            VendingMachine testMachine = new VendingMachine();
            

            Assert.AreEqual("Potato Crisps", testMachine.Dispense("A1").Name, "User inputs Slot ID \"A1\" to purchase item.");
            Assert.AreEqual("Stackers", testMachine.Dispense("A2").Name, "User inputs Slot ID \"A2\" to purchase item.");
            Assert.AreEqual("Grain Waves", testMachine.Dispense("A3").Name, "User inputs Slot ID \"A3\" to purchase item.");
            Assert.AreEqual("Cloud Popcorn", testMachine.Dispense("A4").Name, "User inputs Slot ID \"A4\" to purchase item.");

            Assert.AreEqual("Moonpie", testMachine.Dispense("B1").Name, "User inputs Slot ID \"B1\" to purchase item.");
            Assert.AreEqual("Cowtales", testMachine.Dispense("B2").Name, "User inputs Slot ID \"B2\" to purchase item.");
            Assert.AreEqual("Wonka Bar", testMachine.Dispense("B3").Name, "User inputs Slot ID \"B3\" to purchase item.");
            Assert.AreEqual("Crunchie", testMachine.Dispense("B4").Name, "User inputs Slot ID \"B4\" to purchase item.");

            Assert.AreEqual("Cola", testMachine.Dispense("C1").Name, "User inputs Slot ID \"C1\" to purchase item.");
            Assert.AreEqual("Dr. Salt", testMachine.Dispense("C2").Name, "User inputs Slot ID \"C2\" to purchase item.");
            Assert.AreEqual("Mountain Melter", testMachine.Dispense("C3").Name, "User inputs Slot ID \"C3\" to purchase item.");
            Assert.AreEqual("Heavy", testMachine.Dispense("C4").Name, "User inputs Slot ID \"C4\" to purchase item.");

            Assert.AreEqual("U-Chews", testMachine.Dispense("D1").Name, "User inputs Slot ID \"D1\" to purchase item.");
            Assert.AreEqual("Little League Chew", testMachine.Dispense("D2").Name, "User inputs Slot ID \"D2\" to purchase item.");
            Assert.AreEqual("Chiclets", testMachine.Dispense("D3").Name, "User inputs Slot ID \"D3\" to purchase item.");
            Assert.AreEqual("Triplemint", testMachine.Dispense("D4").Name, "User inputs Slot ID \"D4\" to purchase item.");

        }
        [TestMethod]
        public void FinishTransactionTest()
        {
            VendingMachine testMachine = new VendingMachine();

            testMachine.FeedMoney('3');//feeds $5 into machine
            
            testMachine.Dispense("D3").QtyUpdate();
            testMachine.UpdateBalance("D3");

            
            Assert.AreEqual("Your change is 0 nickels, 0 dimes, and 17 quarters.", 
                    testMachine.FinishTransaction(), "Returns $4.25 worth of coins.");

            testMachine.FeedMoney('3');//feeds $5 into machine

            testMachine.Dispense("A3").QtyUpdate();
            testMachine.UpdateBalance("A3");


            Assert.AreEqual("Your change is 0 nickels, 0 dimes, and 9 quarters.",
                    testMachine.FinishTransaction(), "Returns $2.25 worth of coins.");

            testMachine.FeedMoney('3');//feeds $5 into machine

            testMachine.Dispense("D1").QtyUpdate();
            testMachine.UpdateBalance("D1");


            Assert.AreEqual("Your change is 1 nickels, 1 dimes, and 16 quarters.",
                    testMachine.FinishTransaction(), "Returns $4.15 worth of coins.");
        }
        [TestMethod]
        public void UpdateBalanceTest()
        {
            VendingMachine testMachine = new VendingMachine();
            
            testMachine.FeedMoney('5');
            testMachine.UpdateBalance("B3");

            Assert.AreEqual((decimal)18.50, testMachine.MachineBalance, "Starting balance is $20, " +
                    "price of \"B3\"($1.50) is subtracted.");

            testMachine.UpdateBalance("C1");

            Assert.AreEqual((decimal)17.25, testMachine.MachineBalance, "Starting balance is $18.50, " +
                    "price of \"C1\"($1.25) is subtracted.");

            testMachine.UpdateBalance("A4");

            Assert.AreEqual((decimal)13.60, testMachine.MachineBalance, "Starting balance is $17.25, " +
                    "price of \"A4\"($3.65) is subtracted.");
        }
        

    }
}
