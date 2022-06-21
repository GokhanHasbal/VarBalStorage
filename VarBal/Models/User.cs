using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }

        public string Password { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public int PermissionId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Permission Permission { get; set; }
        public List<Address> Address { get; set; }
        public List<Logs> Logs { get; set; }
        public List<Order> Order { get; set; }
        public List<Shipment> Shipment { get; set; }

    }
}