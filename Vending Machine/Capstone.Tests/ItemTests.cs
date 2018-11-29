using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone;

namespace Capstone.Tests
{
    [TestClass]
    public class ItemTests
    {

        [TestMethod]
        public void RemoveQtyTest()
        {
            VendingMachine testMachine = new VendingMachine();

            

            testMachine.Dispense("C1").QtyUpdate();
            testMachine.Dispense("D4").QtyUpdate();
            testMachine.Dispense("A2").QtyUpdate();

            Assert.AreEqual(4, testMachine.Dispense("C1").Qty, "Adds 1 to number of items sold of Slot ID \"C1\".");
            Assert.AreEqual(4, testMachine.Dispense("D4").Qty, "Adds 1 to number of items sold of Slot ID \"D4\".");
            Assert.AreEqual(4, testMachine.Dispense("A2").Qty, "Adds 1 to number of items sold of Slot ID \"A2\".");
        }
        [TestMethod]
        public void QtySoldTest()
        {
            VendingMachine testMachine = new VendingMachine();

            testMachine.FeedMoney('5'); //feeds $20 into machine

            testMachine.Dispense("A4").QtyUpdate(); //subtracts one from qty and adds one to qtySold
            testMachine.Dispense("D2").QtyUpdate();
            testMachine.Dispense("B1").QtyUpdate();

            Assert.AreEqual(1, testMachine.Dispense("A4").QtySold, "Adds 1 to number of items sold of Slot ID \"A4\".");
            Assert.AreEqual(1, testMachine.Dispense("D2").QtySold, "Adds 1 to number of items sold of Slot ID \"D2\".");
            Assert.AreEqual(1, testMachine.Dispense("B1").QtySold, "Adds 1 to number of items sold of Slot ID \"B1\".");

        }
    }
}
