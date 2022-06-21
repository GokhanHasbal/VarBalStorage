using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public Product Product { get; set; }
        public int PoductId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public Address Address { get; set; }
        public int AdressId { get; set; }
        public Shipment Shipment { get; set; }
        public int CargoId { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public Permission Permission { get; set; }
        public int PermissionId { get; set; }
        public List<Invoice> Invoice { get; set; }
        

    }
}