using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using VarBal.AdminModel;
using System.IO;

namespace VarBal.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            ProductModel pm = new ProductModel();
            pm.Product = db.Product.Where(x => x.Delete == false).ToList();
            pm.Category = db.Category.Where(x => x.Delete == false).ToList();
            pm.Brand = db.Brand.Where(x => x.Delete == false).ToList();
            pm.Shelf = db.Shelf.Where(x => x.Delete == false).ToList();
            pm.Warehouse = db.Warehouse.Where(x => x.Delete == false).ToList();

            return View(pm);
        }
        [HttpGet]
        public ActionResult Add()
        {
            ProductModel model = new ProductModel()
            {
                Category = db.Category.Where(x => x.MainCategoryId == 0 && x.Status == true && x.Delete == false).ToList(),
                SubCategory = db.Category.Where(x => x.MainCategoryId != 0 && x.Status == true && x.Delete == false).ToList(),
                Shelf = db.Shelf.Where(x => x.MainShelfId == 0 && x.Status == true && x.Delete == false).ToList(),
                SubShelf = db.Shelf.Where(x => x.MainShelfId != 0 && x.Status == true && x.Delete == false).ToList(),
                Brand = db.Brand.Where(x => x.Status == true && x.Delete == false).ToList(),
                Warehouse = db.Warehouse.Where(x => x.Status == true && x.Delete == false).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(Product product, string SubPrice)
        {
            string fiyat = product.Price + "," + SubPrice;
            Product newproduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                RegisterDate = DateTime.Now,
                Barcode = "VB-"+Guid.NewGuid().ToString().Substring(0, 8),
                Price = Convert.ToDouble(fiyat),
                Stock = product.Stock,
                Tax = product.Tax,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                ShelfId = product.ShelfId,
                WarehouseId = product.WarehouseId,
                Status = product.Status
            };
            db.Product.Add(newproduct);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductModel model = new ProductModel()
            {
                SingleProduct = db.Product.Find(id),
                Category = db.Category.Where(x => x.MainCategoryId == 0 && x.Status == true && x.Delete == false).ToList(),
                SubCategory = db.Category.Where(x => x.MainCategoryId != 0 && x.Status == true && x.Delete == false).ToList(),
                Shelf = db.Shelf.Where(x => x.MainShelfId == 0 && x.Status == true && x.Delete == false).ToList(),
                SubShelf = db.Shelf.Where(x => x.MainShelfId != 0 && x.Status == true && x.Delete == false).ToList(),
                Brand = db.Brand.Where(x => x.Status == true && x.Delete == false).ToList(),
                Warehouse = db.Warehouse.Where(x => x.Status == true && x.Delete == false).ToList(),
            };

            var product = db.Product.Find(id);

            if (product.View == false)
            {
                product.View = true;
                db.SaveChanges();
                return View(model);
            }
            else
            {

            return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase Image, string SubPrice)
        {
            var editproduct = db.Product.Find(product.Id);

            string ImagePath = "";
            string ImageName = "";

            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/ProductImage"), ImageName);
                    Image.SaveAs(ImagePath);
                    editproduct.Image = ImageName;
                }

                editproduct.Name = product.Name;
                editproduct.Description = product.Description;
                string fiyat = product.Price + "," + SubPrice;
                editproduct.Price = Convert.ToDouble(fiyat);
                editproduct.Stock = product.Stock;
                editproduct.Tax = product.Tax;
                editproduct.CategoryId = product.CategoryId;
                editproduct.BrandId = product.BrandId;
                editproduct.ShelfId = product.ShelfId;
                editproduct.WarehouseId = product.WarehouseId;
                editproduct.Status = product.Status;

              
                db.SaveChanges();

                return Redirect("~/Product/Edit?id=" + editproduct.Id);
            }
            catch
            {

            }

            return View();
        }
        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            product.Delete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ProductStatusEdit(int Id)
        {
            var product = db.Product.Find(Id);
            product.Status = !product.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }

    }
}