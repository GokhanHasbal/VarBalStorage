using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using VarBal.AdminModel;

namespace VarBal.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var book = db.Booking.Where(x => x.Delete == false).ToList();
            return View(book);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var detail = db.Booking.Find(id);


            if (detail.View == false)
            {
                detail.View = true;
                db.SaveChanges();
            }
            return View(detail);
        }
        [HttpPost]
        public ActionResult Detail(Booking booking)
        {
            var detail = db.Booking.Find(booking.Id);
            detail.Status = booking.Status;
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var newsGet = db.Booking.Find(id);
            newsGet.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}