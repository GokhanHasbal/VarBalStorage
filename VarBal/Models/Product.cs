using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VarBal.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public double Tax { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }        
        public int ShelfId { get; set; }
        public Shelf Shelf { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public bool Status { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }

    }
}