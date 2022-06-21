using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using VarBal.AdminModel;

namespace VarBal.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var contact = db.ContactUs.Where(x => x.Delete == false).ToList();
            return View(contact);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var detail = db.ContactUs.Find(id);


            if (detail.View == false)
            {
                detail.View = true;
                db.SaveChanges();
            }
            return View(detail);
        }
        [HttpPost]
        public ActionResult Detail(ContactUs contact)
        {
            var detail = db.ContactUs.Find(contact.Id);
            detail.Status = contact.Status;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var contact = db.ContactUs.Find(id);
            contact.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}