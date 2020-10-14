using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCPStore.Models
{
    public class ProductCartItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CartItemId { get; set; }
        public CartItem CartItem { get; set; }
    }
}
