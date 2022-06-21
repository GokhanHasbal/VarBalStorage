using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using VarBal.AdminModel;


namespace VarBal.Controllers
{
    public class PermissionController : Controller
    {
        // GET: Permission
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var permission = db.Permission.Where(x => x.Delete == false).ToList();
            return View(permission);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Permission permission)
        {
            db.Permission.Add(permission);
            db.SaveChanges();
            ViewBag.mesaj = "Yetki Ekleme Başarılı";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var permission = db.Permission.Find(Id);
            return View(permission);
        }
        [HttpPost]
        public ActionResult Edit(Permission permission)
        {
            var editpermission = db.Permission.Find(permission.Id);
            editpermission.Name = permission.Name;
            editpermission.Status = permission.Status;
            db.SaveChanges();
            ViewBag.mesaj = "Yetki Düzenleme Başarılı";
            return View(editpermission);
        }
        public ActionResult Delete(int Id)
        {
            var permission = db.Permission.Find(Id);
            permission.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult PermissionStatusEdit(int Id)
        {
            var permission = db.Permission.Find(Id);
            permission.Status = !permission.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}