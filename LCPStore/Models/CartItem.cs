using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCPStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
        public ICollection<ProductCartItem> ProductCartItems { get; set; }
    }
}
