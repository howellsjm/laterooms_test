using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Application.Models
{
    public class Offer
    {
        public string StockKeepingUnit { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
