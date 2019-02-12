using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.Application.Models;

namespace Checkout.Application.Services
{
    public interface IOfferService
    {
        IEnumerable<Offer> GetOffers(IEnumerable<string> items);
    }
}
