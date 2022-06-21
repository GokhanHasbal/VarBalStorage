using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarBal.Models;
namespace VarBal.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public string Type { get; set; }
        public List<Shipment> Shipments { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
    }
}