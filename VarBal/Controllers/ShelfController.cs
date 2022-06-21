using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;

namespace VarBal.Controllers
{
    public class ShelfController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var shelf = db.Shelf.Where(x => x.Delete == false).ToList();
            return View(shelf);
        }
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.shelf = db.Shelf.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Shelf shelf)
        {
            Shelf newshelf = new Shelf();
            newshelf.Name = shelf.Name;
            newshelf.Description = shelf.Description;
            newshelf.MainShelfId = shelf.MainShelfId;
            newshelf.Status = shelf.Status;
            db.Shelf.Add(newshelf);
            db.SaveChanges();
            ViewBag.shelf = db.Shelf.ToList();
            ViewBag.mesaj = "Raf Ekleme Başarılı";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var shelf = db.Shelf.Find(Id);
            if (shelf != null)
            {
                ViewBag.shelf = db.Shelf.ToList();
                if (shelf.MainShelfId != 0)
                {
                    ViewBag.mainshelf = db.Shelf.FirstOrDefault(x => x.Id == shelf.MainShelfId).Name;
                }
                return View(shelf);
            }
            else
            {
                //return Redirect("~/Shelf");
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Shelf shelf, HttpPostedFileBase Image)
        {
            var editshelf = db.Shelf.Find(shelf.Id);
            editshelf.Name = shelf.Name;
            editshelf.Description = shelf.Description;
            editshelf.Status = shelf.Status;
            editshelf.MainShelfId = shelf.MainShelfId;
            string imagepath = "";
            string imagename = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/ShelfImage"), imagename);
                    Image.SaveAs(imagepath);
                    editshelf.Image = imagename;
                }
                ViewBag.mesaj = "Raf Düzenleme Başarılı";
            }
            catch
            {
                ViewBag.mesaj = "Unexpected Error";
            }
            db.SaveChanges();
            return Redirect("~/Shelf/Edit?id=" + shelf.Id);
        }
        public ActionResult Delete(int Id)
        {
            var shelf = db.Shelf.Find(Id);
            shelf.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult ShelfStatusEdit(int Id)
        {
            var shelf = db.Shelf.Find(Id);
            shelf.Status = !shelf.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}