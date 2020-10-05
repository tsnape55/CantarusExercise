using SupermarketInterfaces;
using SupermarketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout
{
    public class Checkout : ICheckout
    {

        private IPricings _pricings;
        private List<CheckoutLine> _lines;

        public void Configure(IPricings pricings)
        {
            _pricings = pricings;
            _lines = new List<CheckoutLine>();
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
            throw new NotImplementedException();
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
                var pricing = _pricings.GetProductBySku(line.Sku);
                if (pricing != null)
                {
                    total += (line.Quantity * pricing.Price);
                }
            }

            return total;
        }

        public float Total() => Subtotal() - Savings();
    }
}
