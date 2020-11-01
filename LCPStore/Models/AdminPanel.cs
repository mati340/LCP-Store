using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCPStore.Models
{
    public class AdminPanel
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
