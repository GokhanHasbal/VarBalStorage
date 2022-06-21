using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int InvoiceNo { get; set; }
        
        public double TotalPrice { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }

    }
}