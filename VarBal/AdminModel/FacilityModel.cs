using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarBal.Models;


namespace VarBal.AdminModel
{
    public class FacilityModel
    {
        public List<Address> Address { get; set; }
        public List<Warehouse> Warehouse   { get; set; }

        public Warehouse SingleWarehouse { get; set; }
        public Address SingleAddress { get; set; }

    }
}