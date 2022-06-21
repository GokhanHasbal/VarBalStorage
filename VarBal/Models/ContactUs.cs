using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarBal.Models;
using VarBal.AdminModel;

namespace VarBal.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }

    }
}