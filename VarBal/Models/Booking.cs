using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarBal.Models;
using VarBal.AdminModel;

namespace VarBal.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public Warehouse Warehouse { get; set; }
        public int WarehouseId { get; set; }
        public string Size { get; set; }
        public string Note { get; set; }
        public DateTime BookTime { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }

    }
}