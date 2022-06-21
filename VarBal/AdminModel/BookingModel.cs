using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarBal.Models;

namespace VarBal.AdminModel
{
    public class BookingModel
    {
        public List<Warehouse> Warehouse { get; set; }
        public List<User> User { get; set; }
        public List<Address> Address { get; set; }
        public User SingleUser { get; set; }
        public Address SingleAdres { get; set; }
    }
}