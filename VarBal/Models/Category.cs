using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarBal.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public string Description { get; set; }
        public int? MainCategoryId { get; set; }
        public List<Product> Product { get; set; }

    }
}