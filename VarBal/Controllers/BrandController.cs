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
    public class BrandController : Controller
    {
        // GET: Brand
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var model = db.Brand.Where(x => x.Delete == false).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Brand brand)
        {
            db.Brand.Add(brand);
            db.SaveChanges();
            ViewBag.mesaj = "Marka Ekleme Başarılı";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var brand = db.Brand.Find(Id);
            if (brand != null)
            {
                return View(brand);
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Brand brand, HttpPostedFileBase Image)
        {
            var editbrand = db.Brand.Find(brand.Id);
            editbrand.Name = brand.Name;
            editbrand.Description = brand.Description;
            editbrand.Status = brand.Status;
            string imagepath = "";
            string imagename = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/BrandImage"), imagename);
                    Image.SaveAs(imagepath);
                    editbrand.Image = imagename;
                }
                ViewBag.mesaj = "Marka Düzenleme Başarılı";
            }
            catch
            {
                ViewBag.mesaj = "Unexpected Error";
            }
            db.SaveChanges();
            return View(editbrand);
            //return Redirect("~/Brand/Edit?id=" + brand.Id);
        }
        public ActionResult Delete(int Id)
        {
            var brand = db.Brand.Find(Id);
            brand.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult BrandStatusEdit(int Id)
        {
            var brand = db.Brand.Find(Id);
            brand.Status = !brand.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}