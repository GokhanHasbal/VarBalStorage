using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int? WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string BuildingNumber { get; set; }
        public string FullAddress { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public bool Selection { get; set; }
        public List<Order> Order { get; set; }

    }
}