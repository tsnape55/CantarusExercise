﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Exceptions
{
    public class InvalidSkuException : Exception
    {
        public InvalidSkuException(string message) : base(message)
        {
        }
    }
}
