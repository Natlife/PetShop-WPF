using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleTest.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public double TotalAmount { get; set; }
        public List<Order> Orders { get; set; }
    }
}
