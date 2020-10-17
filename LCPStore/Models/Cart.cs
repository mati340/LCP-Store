using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace LCPStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public double SumToPay { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [ForeignKey("Order")]
        [AllowNull]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
