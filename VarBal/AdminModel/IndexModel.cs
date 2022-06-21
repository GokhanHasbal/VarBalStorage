using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarBal.Models;

namespace VarBal.AdminModel
{
    public class IndexModel
    {
        public List<User> User { get; set; }
        public List<Permission> Permission { get; set; }
        public List<Shipment> Shipment { get; set; }
        public List<Contact> Contact { get; set; }
        public int UserTotalCount { get; set; }
        public int UserActiveCount { get; set; }
        public int UserPasiveCount { get; set; }
    }
}