using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using System.IO;

namespace VarBal.Controllers
{
    public class ContactPageController : Controller
    {
        // GET: ContactPage
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var contact = db.Contact.Where(x => x.Delete == false).ToList();
            return View(contact);
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var contact = db.Contact.Find(Id);
            return View(contact);
        }
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase Image, Contact contact)
        {

            var contactp = db.Contact.Find(contact.Id);

            string resimyolu = "";
            string resimadi = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    resimadi = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName);
                    resimyolu = Path.Combine(Server.MapPath("~/Content/images/ContactPageImage"), resimadi);
                    Image.SaveAs(resimyolu);
                    contactp.Image = resimadi;
                }
            }
            catch
            {
            }
            contactp.Adress1 = contact.Adress1;
            contactp.Adress2 = contact.Adress2;
            contactp.Phone1 = contact.Phone1;
            contactp.Phone2 = contact.Phone2;
            contactp.Mail1 = contact.Mail1;
            contactp.Mail2 = contact.Mail2;
            contactp.Description = contact.Description;
            contactp.Facebook = contact.Facebook;
            contactp.Twitter = contact.Twitter;
            contactp.Instagram = contact.Instagram;
            contactp.Youtube = contact.Youtube;
            contactp.Linkedin = contact.Linkedin;
            contactp.Linkedin = contact.Linkedin;
            contactp.Pinterest = contact.Pinterest;
            contactp.GoogleMaps = contact.GoogleMaps;
            contactp.Status = contact.Status;
            db.SaveChanges();
            return View(contactp);
        }
        public ActionResult ContactStatusEdit(int Id)
        {
            var contact = db.Contact.Find(Id);
            contact.Status = !contact.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}