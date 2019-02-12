using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Checkout.Test
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void Scan_Adds_Item_To_Basket_GetTotalPrice_Returns_Price()
        {
            var wrapper = new CheckoutTestWrapper();
            wrapper.SetupServices();

            var target = wrapper.GetTarget();

            Assert.Zero(target.GetTotalPrice());

            target.Scan("A");

            var result = target.GetTotalPrice();
            Assert.NotZero(result);
            Assert.AreEqual(50, result);
        }

        [Test]
        public void Scan_Checks_Item_Code()
        {
            var wrapper = new CheckoutTestWrapper();
            wrapper.SetupServices();

            var target = wrapper.GetTarget();

            target.Scan("A");

            wrapper.MockUnitService.Verify(x => x.ValidateUnit("A"));
        }

        [Test]
        public void Scan_If_Item_Not_Valid_ThrowsException()
        {
            var wrapper = new CheckoutTestWrapper();
            wrapper.SetupServices();

            wrapper.MockUnitService.Setup(x => x.ValidateUnit(It.IsAny<string>())).Returns(false);

            var target = wrapper.GetTarget();
            
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => target.Scan("A"));

            Assert.IsTrue(ex.Message.Contains("Invalid Stock Keeping Unit"));
        }

        [Test]
        public void GetTotalPrice_Returns_Total_Price_Of_Items_In_Basket()
        {

        }

        [Test]
        public void GetTotalPrice_If_Offer_Valid_Returns_Offer_Price()
        {

        }

        [Test]
        public void GetTotalPrice_If_Offer_Valid_With_Non_Sequential_Items_Calculates_Correctly()
        {

        }

        [Test]
        public void GetTotalPrice_If_Offer_Valid_And_Items_Outside_Offer_Calculates_Correctly()
        {

        }
    }
}
