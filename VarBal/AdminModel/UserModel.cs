using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarBal.Models;

namespace VarBal.AdminModel
{
    public class UserModel
    {
        public List<User> User { get; set; }
        public List<Address> Address { get; set; }
        public List<Permission> Permission { get; set; }
        public Address SingleAddress { get; set; }
        public User SingleUser { get; set; }
    }
}