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
        private List<CheckoutLine> _lines;

        public Checkout()
        {
            _lines = new List<CheckoutLine>();
        }

        public void Configure(IPricings pricings)
        {
            _pricings = pricings;
        }

        public void Empty()
        {
            _lines = new List<CheckoutLine>();
        }

        public void Remove(string sku)
        {
            var lineToRemove = _lines.FirstOrDefault(x => x.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));
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
                _lines.Remove(lineToRemove);
            }
        }

        public float Savings()
        {
            var total = 0f;

            foreach (var line in _lines)
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
            var existingLine = _lines.FirstOrDefault(x => x.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));
            if (existingLine != null)
            {
                existingLine.Quantity++;
            }
            else
            {
                _lines.Add(new CheckoutLine(sku));
            }
        }

        public float Subtotal()
        {
            var total = 0f;

            foreach (var line in _lines)
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
