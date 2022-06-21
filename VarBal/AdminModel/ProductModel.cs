using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;

namespace VarBal.AdminModel
{
    
    public class ProductModel
    {
        public List<Product> Product { get; set; }
        public List<Category> Category { get; set; }
        public List<Category> SubCategory { get; set; }
        public List<Shelf> Shelf { get; set; }
        public List<Shelf> SubShelf { get; set; }
        public List<User> User { get; set; }
        public User SingleUser { get; set; }
        public List<Brand> Brand { get; set; }
        public Product SingleProduct { get; set; }
        public List<Warehouse> Warehouse { get; set; }


    }
}