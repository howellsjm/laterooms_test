using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Application
{
    public class CheckoutService : ICheckout
    {
        public void Scan(string item)
        {
            throw new NotImplementedException();
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }
    }
}
