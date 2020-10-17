using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LCPStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public string Description { get; set; }
        //public double InStock { get; set; }
        public int Price { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public DateTime Created { get; set; }
        public byte[] Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        //[DisplayFormat(DataFormatString = "{0:P0}")]
        //[Column(TypeName = "decimal(18, 2)")]
        //[Range(0.01, 0.99)]
        //public decimal Discount { get; set; }
    }
}
