using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketModels
{
    public class CheckoutLine
    {
        public CheckoutLine(string sku)
        {
            Sku = sku;
            Quantity = 1;
        }

        public string Sku { get; set; }
        public int Quantity { get; set; }
    }
}
