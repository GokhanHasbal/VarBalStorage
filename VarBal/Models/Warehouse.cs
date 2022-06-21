using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OfficeHours { get; set; }
        public string AccessHours { get; set; }
        public string GoogleMaps { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public List<Product> Product { get; set; }
        public List<Address> Address { get; set; }
        public List<Shipment> Shipment { get; set; }
        public List<Booking> Booking { get; set; }
    }
}