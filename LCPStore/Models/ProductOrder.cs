using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCPStore.Models
{
    public class ProductOrder
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
