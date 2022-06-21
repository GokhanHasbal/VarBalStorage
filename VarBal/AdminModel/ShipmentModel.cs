using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarBal.Models;

namespace VarBal.AdminModel
{
    public class ShipmentModel
    {
        public List<Shipment> Shipments { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        public List<User> Users { get; set; }

        public List<Product> Products { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        public Vehicle SingleVehicle { get; set; }
        public Product SingleProduct { get; set; }
        public User SingleUser { get; set; }
        public Warehouse SingleSenderWareHouse { get; set; }
        public Warehouse SingleReceiverWareHouse { get; set; }
        public Shipment SingleShipment { get; set; }

    }
}