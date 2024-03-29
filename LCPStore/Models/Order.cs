﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Total Pay")]
        public double TotalPay { get; set; }
        public Delivery Delivery { get; set; }
        public Account Account { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }
    }
}
