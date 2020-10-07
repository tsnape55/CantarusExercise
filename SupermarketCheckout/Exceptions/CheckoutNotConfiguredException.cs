using System;

namespace SupermarketCheckout.Exceptions
{
    public class CheckoutNotConfiguredException : Exception
    {
        public CheckoutNotConfiguredException()
        {
        }

        public CheckoutNotConfiguredException(string message) : base(message)
        {
        }
    }
}
