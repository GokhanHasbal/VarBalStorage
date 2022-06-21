using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Process { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public bool Status { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }

        public int? SenderId { get; set; } //yollayan
        public int? ReceiverId { get; set; } //alan
        public int? Stock { get; set; }

        public DateTime SendTime { get; set; }
        public DateTime? ReceiveTime { get; set; }

    }
}