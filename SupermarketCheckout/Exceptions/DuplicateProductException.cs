﻿using System;

namespace SupermarketCheckout.Exceptions
{
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException(string message) : base(message)
        {
        }
    }
}
