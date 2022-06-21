using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class Seo
    {
        public int Id { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string GoogleAnalityc { get; set; }
        public string YandexMetrica { get; set; }
        public string Title { get; set; }
        public string Ico { get; set; }
        public string Logo { get; set; }
        public bool State { get; set; }
        public bool Delete { get; set; }
    }
}