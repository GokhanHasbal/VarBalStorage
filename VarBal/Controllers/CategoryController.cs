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
    public class CategoryController : Controller
    {
        // GET: Category
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var category = db.Category.Where(x => x.Delete == false).ToList();
            return View(category);
        }
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.category = db.Category.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Category category)
        {
            Category newcategory = new Category();
            newcategory.Name = category.Name;
            newcategory.Description = category.Description;
            newcategory.MainCategoryId = category.MainCategoryId;
            newcategory.Status = category.Status;
            db.Category.Add(newcategory);
            db.SaveChanges();
            ViewBag.category = db.Category.ToList();
            ViewBag.mesaj = "Kategori Ekleme Başarılı";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var category = db.Category.Find(Id);
            if (category != null)
            {
                ViewBag.category = db.Category.ToList();
                if (category.MainCategoryId != 0)
                {
                    ViewBag.maincategory = db.Category.FirstOrDefault(x => x.Id == category.MainCategoryId).Name;
                }
                return View(category);
            }
            else
            {
                //return Redirect("~/Category");
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase Image)
        {
            var editcategory = db.Category.Find(category.Id);
            editcategory.Name = category.Name;
            editcategory.Description = category.Description;
            editcategory.Status = category.Status;
            editcategory.MainCategoryId = category.MainCategoryId;
            string imagepath = "";
            string imagename = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/CategoryImage"), imagename);
                    Image.SaveAs(imagepath);
                    editcategory.Image = imagename;
                }
                ViewBag.mesaj = "Kategori Düzenleme Başarılı";
            }
            catch
            {
                ViewBag.mesaj = "Unexpected Error";
            }
            db.SaveChanges();
            return Redirect("~/Category/Edit?id=" + category.Id);
        }
        public ActionResult Delete(int Id)
        {
            var category = db.Category.Find(Id);
            category.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult CategoryStatusEdit(int Id)
        {
            var category = db.Category.Find(Id);
            category.Status = !category.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}