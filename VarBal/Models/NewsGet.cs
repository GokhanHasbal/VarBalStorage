using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class NewsGet
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public DateTime Date { get; set; }
        public bool View { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
    }
}