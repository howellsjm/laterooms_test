﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Checkout.Application.Models;

namespace Checkout.Application.Services
{
    public interface IUnitService
    {
        bool ValidateUnit(string item);
        IEnumerable<Unit> GetUnits(IEnumerable<string> items);
    }
}
