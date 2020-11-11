using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LCPStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Category Category { get; set; }
        [Required]
        public string Description { get; set; }
        //public double InStock { get; set; }
        [Required]
        public int Price { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public DateTime Created { get; set; }
        public byte[] Image { get; set; }
        [NotMapped]
        [Required]
        [DisplayName("Image File")]
        public IFormFile ImageFile { get; set; }

        //[DisplayFormat(DataFormatString = "{0:P0}")]
        //[Column(TypeName = "decimal(18, 2)")]
        //[Range(0.01, 0.99)]
        //public decimal Discount { get; set; }
    }
}
