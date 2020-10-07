using SupermarketCheckout.Exceptions;
using SupermarketInterfaces;
using SupermarketModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public class Checkout : ICheckout
    {

        private IPricings _pricings;
        private bool _configured;

        public Checkout()
        {
            Lines = new List<CheckoutLine>();
            _configured = false;
        }

        public List<CheckoutLine> Lines { get; private set; }

        public void Configure(IPricings pricings)
        {
            if (pricings == null)
            {
                throw new CheckoutNotConfiguredException("Pricings cannot be null");
            }

            if (pricings.Products == null || !pricings.Products.Any())
            {
                throw new CheckoutNotConfiguredException("Pricings must have products.");
            }

            _pricings = pricings;
            _configured = true;
        }

        public void Empty()
        {
            Lines = new List<CheckoutLine>();
        }

        public void Remove(string sku)
        {
            var lineToRemove = Lines.FirstOrDefault(x => x.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));
            if (lineToRemove == null)
            {
                return;
            }

            if (lineToRemove.Quantity > 1)
            {
                lineToRemove.Quantity--;
            }
            else
            {
                Lines.Remove(lineToRemove);
            }
        }

        public float Savings()
        {

            if (!_configured)
            {
                throw new CheckoutNotConfiguredException();
            }

            var total = 0f;

            foreach (var line in Lines)
            {
                var lineSaving = 0f;
                var cost = LinePrice(line);
                var deals = _pricings.GetDealsBySku(line.Sku).Where(x => x.Count >= line.Quantity);

                foreach (var deal in deals)
                {
                    var dealSaving = cost - deal.Price;
                    if (dealSaving > lineSaving)
                    {
                        lineSaving = dealSaving;
                    }
                }

                total += lineSaving;
            }

            return total;
        }

        public void Scan(string sku)
        {

            if (!_configured)
            {
                throw new CheckoutNotConfiguredException();
            }

            var inPricing = _pricings.GetProductBySku(sku) != null;

            if(!inPricing)
            {
                throw new InvalidSkuException("A product with that sku has not been configured.");
            }

            var existingLine = Lines.FirstOrDefault(x => x.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));
            if (existingLine != null)
            {
                existingLine.Quantity++;
            }
            else
            {
                Lines.Add(new CheckoutLine(sku));
            }
        }

        public float Subtotal()
        {
            var total = 0f;

            foreach (var line in Lines)
            {
                total += LinePrice(line);
            }

            return total;
        }

        public float Total() => Subtotal() - Savings();


        private float LinePrice(CheckoutLine line)
        {
            var price = 0f;
            var pricing = _pricings.GetProductBySku(line.Sku);
            if (pricing != null)
            {
                price = line.Quantity * pricing.Price;
            }

            return price;
        }
    }
}
