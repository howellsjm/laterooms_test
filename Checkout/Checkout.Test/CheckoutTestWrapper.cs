using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.Application;
using Checkout.Application.Models;
using Checkout.Application.Services;
using Moq;
using NUnit.Framework.Constraints;

namespace Checkout.Test
{
    public class CheckoutTestWrapper
    {
        public Mock<IUnitService> MockUnitService { get; set; }
        public Mock<IOfferService> MockOfferService { get; set; }
        public List<Unit> Units { get; set; }
        public List<Offer> Offers { get; set; }

        public CheckoutTestWrapper()
        {
            MockUnitService = new Mock<IUnitService>();
            MockOfferService = new Mock<IOfferService>();

            Units = new List<Unit>
            {
                new Unit {StockKeepingUnit = "A", Price = 50},
                new Unit {StockKeepingUnit = "B", Price = 30},
                new Unit {StockKeepingUnit = "C", Price = 20},
                new Unit {StockKeepingUnit = "D", Price = 15}
            };

            Offers = new List<Offer>
            {
                new Offer {StockKeepingUnit = "A", Quantity = 3, Price = 130},
                new Offer {StockKeepingUnit = "B", Quantity = 2, Price = 45}
            };
        }

        public void SetupServices()
        {
            MockUnitService.Setup(x => x.ValidateUnit(It.IsAny<string>())).Returns(true);
            MockUnitService.Setup(x => x.GetUnits(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> items) => Units.Join(items, u => u.StockKeepingUnit, i => i, (u,i) => u));
            MockOfferService.Setup(x => x.GetOffers(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> items) => Offers.Join(items, o => o.StockKeepingUnit, i => i, (o, i) => o));
        }

        public ICheckout GetTarget()
        {
            return new CheckoutService(MockUnitService.Object, MockOfferService.Object);
        }
    }
}
