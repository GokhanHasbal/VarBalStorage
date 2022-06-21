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
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var model = db.Warehouse.Where(x => x.Delete == false).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Warehouse warehouse)
        {
            db.Warehouse.Add(warehouse);
            db.SaveChanges();
            ViewBag.mesaj = "Depo Ekleme Başarılı";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var warehouse = db.Warehouse.Find(Id);
            if (warehouse != null)
            {
                return View(warehouse);
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Warehouse warehouse, HttpPostedFileBase Image)
        {
            var editwarehouse = db.Warehouse.Find(warehouse.Id);
            editwarehouse.Name = warehouse.Name;
            editwarehouse.Description = warehouse.Description;
            editwarehouse.Status = warehouse.Status;
            string imagepath = "";
            string imagename = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/WarehouseImage"), imagename);
                    Image.SaveAs(imagepath);
                    editwarehouse.Image = imagename;
                }
                ViewBag.mesaj = "Depo Düzenleme Başarılı";
            }
            catch
            {
                ViewBag.mesaj = "Unexpected Error";
            }
            db.SaveChanges();
            return View(editwarehouse);
            //return Redirect("~/Warehouse/Edit?id=" + warehouse.Id);
        }
        public ActionResult Delete(int Id)
        {
            var warehouse = db.Warehouse.Find(Id);
            warehouse.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult WarehouseStatusEdit(int Id)
        {
            var warehouse = db.Warehouse.Find(Id);
            warehouse.Status = !warehouse.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }



    }
}