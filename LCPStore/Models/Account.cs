using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LCPStore.Models
{
    public enum Role
    {
        Admin,
        Customer
    }
    public enum Gender
    {
        Male,
        Female
    }
    public class Account
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Ëmail")]
        [EmailAddress]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Registered { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Cart Cart { get; set; }
        public Role Role { get; set; }
        //public string Country { get; set; }
        //public string City { get; set; }
        //public string Address { get; set; }
        //public string ZipCode { get; set; }
        //[DataType(DataType.PhoneNumber)]
        //public string PhoneNumber { get; set; }
    }
}
