using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LCPStore.Models
{
    public enum Delivery
    {
        Standart,
        Premium
    }
    public class Order
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public double TotalPay { get; set; }
        public Delivery Delivery { get; set; }
        public Account Account { get; set; }
        [ForeignKey("Cart")]
        public int? CartId { get; set; }
        public Cart Cart { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }
    }
}
