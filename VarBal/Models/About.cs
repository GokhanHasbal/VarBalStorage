using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class About
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool State  { get; set; }
        public bool Delete  { get; set; }
        public double? Price  { get; set; }
    }
}