using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using VarBal.AdminModel;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System.IO;



namespace VarBal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DataContext db = new DataContext();
        [Authorize]
        public ActionResult Index()
        {
            IndexModel model = new IndexModel()
            {
                User = db.User.ToList(),
                UserTotalCount = db.User.Where(x => x.Delete == false).Count(),
                UserActiveCount = db.User.Where(x => x.Status == true && x.Delete == false).Count(),
                UserPasiveCount = db.User.Where(x => x.Status == false && x.Delete == false).Count()
                
            };

            var Permision = db.Permission.ToList();
            var Shipment = db.Shipment.ToList();
            var ContactUs = db.ContactUs.ToList();
            var Contact = db.Contact.ToList();
            
            
            return View(model);
        }
        public ActionResult Sidebar()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name);
                if (id > 0)
                {
                    var usercontrol = db.User.FirstOrDefault(x => x.Id == id);
                    if (usercontrol != null)
                    {
                        ViewBag.yetki = db.Permission.Find(usercontrol.PermissionId).Name;
                        return PartialView(usercontrol);
                    }
                    else
                    {
                        return RedirectToAction("login", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("login", "Home");
                }
            }
            catch
            {
                return RedirectToAction("login", "Home");
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Home/Login");
        }

        public ActionResult Notification()
        {

            var NewsGet = db.NewsGet.Where(x => x.Delete == false && x.View == false).ToList();

            var User = db.User.Where(x => x.Delete == false && x.View == false).ToList();

            var Product = db.Product.Where(x => x.Delete == false && x.View == false).ToList();

            var Order = db.Order.Where(x => x.View == false).ToList();

            var Shipment = db.Shipment.Where(x => x.Delete == false && x.View == false).ToList();

            var ContactUs = db.ContactUs.Where(x => x.Delete == false && x.View == false).ToList();

            var Booking = db.Booking.Where(x => x.Delete == false && x.View == false).ToList();



            List<Notification> notifications = new List<Notification>();
            Notification newnotification;

            foreach (var item in NewsGet)
            {
                newnotification = new Notification();
                newnotification.Date = item.Date;
                newnotification.Icon = "mdi-email-edit";
                newnotification.NameSurname = item.Mail;
                newnotification.Content = item.Mail;
                newnotification.Color = "bg-primary";
                notifications.Add(newnotification);
            }
            foreach (var item in User)
            {
                newnotification = new Notification();
                newnotification.Date = item.RegistrationDate;
                newnotification.Icon = "mdi-account";
                newnotification.NameSurname = item.Name + " " + item.Surname;
                newnotification.Content = item.Email;
                newnotification.Color = "bg-success";
                newnotification.Link = "User/Edit?id=" + item.Id;
                notifications.Add(newnotification);
            }
            foreach (var item in Order)
            {
                newnotification = new Notification();
                newnotification.Date = item.OrderDate;
                newnotification.Icon = "mdi-archive";
                newnotification.NameSurname = db.User.FirstOrDefault(x => x.Id == item.UserId).Email;
                newnotification.Content = db.Product.FirstOrDefault(x => x.Id == item.PoductId).Name;
                newnotification.Color = "bg-dark";

                notifications.Add(newnotification);
            }
            foreach (var item in Product)
            {
                newnotification = new Notification();
                newnotification.Date = item.RegisterDate;
                newnotification.Icon = "mdi-archive";
                newnotification.NameSurname = item.Name;
                newnotification.Content = item.Name;
                newnotification.Link = "Product/Edit?id=" + item.Id;
                newnotification.Color = "bg-dark";

                notifications.Add(newnotification);
            }
            foreach (var item in Shipment)
            {
                newnotification = new Notification();
                newnotification.Date = item.SendTime;
                newnotification.Icon = "mdi-archive";
                newnotification.NameSurname = db.Product.FirstOrDefault(x => x.Id == item.ProductId).Name;
                newnotification.Content = item.Description;
                newnotification.Link = "Shipment/Detail?id=" + item.Id;
                newnotification.Color = "bg-dark";

                notifications.Add(newnotification);
            }
            foreach (var item in Booking)
            {
                newnotification = new Notification();
                newnotification.Icon = "mdi-archive";
                newnotification.NameSurname = item.NameSurname;
                newnotification.Content = item.Note;
                newnotification.Color = "bg-dark";
                newnotification.Link = "Booking/Detail?id=" + item.Id;
                notifications.Add(newnotification);
            }foreach (var item in ContactUs)
            {
                newnotification = new Notification();
                newnotification.Icon = "mdi-archive";
                newnotification.NameSurname = item.NameSurname;
                newnotification.Content = item.Subject;
                newnotification.Color = "bg-dark";
                newnotification.Link = "Contact/Detail?id=" + item.Id;
                notifications.Add(newnotification);
            }

            var model = notifications.OrderByDescending(x => x.Date).ToList();
            return PartialView(model);

        }
        public ActionResult ClearNotification()
        {
            db.NewsGet.Where(x => x.Delete == false && x.View == false).ToList().ForEach(x => x.View = true);
            db.User.Where(x => x.Delete == false && x.View == false).ToList().ForEach(x => x.View = true);
            db.Order.Where(x => x.View == false).ToList().ForEach(x => x.View = true);
            db.Product.Where(x => x.View == false).ToList().ForEach(x => x.View = true);
            db.Shipment.Where(x => x.View == false).ToList().ForEach(x => x.View = true);
            db.ContactUs.Where(x => x.View == false).ToList().ForEach(x => x.View = true);
            db.Booking.Where(x => x.View == false).ToList().ForEach(x => x.View = true);

            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        #region eski slider
        //[HttpGet]
        //public ActionResult SliderInsert()
        //{
        //    Slider slider = new Slider();

        //    return View(slider);
        //}

        //[HttpPost]
        //public ActionResult SliderInsert(HttpPostedFileBase Image, string Title, string Description, string Link, bool State)
        //{
        //    Slider slider = new Slider();
        //    //HttpPostedFileBase// nedir bak

        //    string resimyolu = "";
        //    string resimad = "";

        //    try
        //    {
        //        if (Image != null && Image.ContentLength > 0)
        //        {
        //            resimad = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName); // benzersiz bir ıd vercek

        //            resimyolu = Path.Combine(Server.MapPath("~/Content/img/slider"), resimad);

        //            Image.SaveAs(resimyolu);
        //            slider.Image = resimad;

        //        }

        //    }
        //    catch
        //    {

        //    }

        //    slider.Title = Title;
        //    slider.Description = Description;
        //    slider.Link = Link;
        //    slider.Status = State;

        //    db.Slider.Add(slider);
        //    db.SaveChanges();

        //    return Redirect("~/Home/Index");
        //}

        //[HttpGet]

        //public ActionResult SliderEdit(int Id)
        //{
        //    var slider = db.Slider.Find(Id);

        //    return View(slider);
        //    //power bi :))
        //}
        //[HttpPost]
        //public ActionResult SliderEdit(HttpPostedFileBase Image, Slider slider)
        //{
        //    var slaydeır1 = db.Slider.Find(slider.Id);

        //    slaydeır1.Title = slider.Title;
        //    slaydeır1.Description = slider.Description;
        //    slaydeır1.Link = slider.Link;
        //    slaydeır1.Status = slider.Status;

        //    string resimyolu = "";
        //    string resimad = "";

        //    try
        //    {
        //        if (Image != null && Image.ContentLength > 0)
        //        {
        //            resimad = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName); // benzersiz bir ıd vercek

        //            resimyolu = Path.Combine(Server.MapPath("~/Content/img/slider"), resimad);

        //            Image.SaveAs(resimyolu);
        //            slaydeır1.Image = resimad;

        //        }

        //    }
        //    catch { }


        //    db.SaveChanges();

        //    //return View(slaydeır1);
        //    return RedirectToAction("Index"); /*aynı yerde kalmasın slidera donsun*/
        //}
        //[HttpGet]
        //public ActionResult SliderDelete(int id)
        //{

        //    var deleteslider = db.Slider.Find(id);


        //    deleteslider.Status = false;
        //    deleteslider.Delete = true;

        //    db.SaveChanges();

        //    return Redirect("~/Home/Index");

        //}

        #endregion


    }
}