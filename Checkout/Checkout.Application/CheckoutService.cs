using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.Application.Services;

namespace Checkout.Application
{
    public class CheckoutService : ICheckout
    {
        readonly IUnitService _unitService;
        readonly IOfferService _offerService;
        Dictionary<string, int> _basket;

        public CheckoutService(IUnitService unitService, IOfferService offerService)
        {
            _unitService = unitService;
            _offerService = offerService;
            _basket = new Dictionary<string, int>();
        }

        public void Scan(string item)
        {
            if(!_unitService.ValidateUnit(item))
                throw new ArgumentOutOfRangeException(nameof(item), "Invalid Stock Keeping Unit");

            if (_basket.ContainsKey(item))
            {
                _basket[item]++;
            }
            else
            {
                _basket.Add(item, 1);
            }
        }

        public int GetTotalPrice()
        {
            var units = _unitService.GetUnits(_basket.Keys);
            var offers = _offerService.GetOffers(_basket.Keys);

            //Total Unit Prices
            var prices = units.GroupJoin(offers, u => u.StockKeepingUnit, o => o.StockKeepingUnit, (u,o) => new { u.StockKeepingUnit, u.Price, Offer = o.FirstOrDefault() });

            var totalPrice = prices.Join(_basket, p => p.StockKeepingUnit, b => b.Key, (p, b) =>
            {
                if (p.Offer == null)
                    return p.Price * b.Value;

                var qOffer = b.Value / p.Offer.Quantity;
                var qOuter = b.Value % p.Offer.Quantity;

                var outerPrice = p.Price * qOuter;
                var offerPrice = p.Offer.Price * qOffer;

                return outerPrice + offerPrice;
            }).Sum();

            return totalPrice;
        }
    }
}
