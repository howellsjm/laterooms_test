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
        Dictionary<string, int> _basket;

        public CheckoutService(IUnitService unitService)
        {
            _unitService = unitService;
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

            //Total Unit Prices
            var totalPrice = units.Join(_basket, u => u.StockKeepingUnit, b => b.Key, (u, b) => u.Price * b.Value).Sum();

            return totalPrice;
        }
    }
}
