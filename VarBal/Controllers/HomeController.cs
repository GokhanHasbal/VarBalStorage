using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VarBal.Models;
using VarBal.AdminModel;
using System.Net.Mail;
using System.Net;

namespace VarBal.Controllers
{

    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            HomeModel home = new HomeModel()
            {
                Sliders = db.Slider.ToList()
            };

            return View(home);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var usercontrol = db.User.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (usercontrol != null)
            {
                FormsAuthentication.SetAuthCookie(usercontrol.Id.ToString(), false); //adım2

                ViewBag.mesaj = "Giriş Başarılı";
                return RedirectToAction("Booking");
            }
            else
            {
                ViewBag.mesaj = "Giriş Bilgileri Uyuşmamaktadır";
                return View();
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Home/Login");
        }
        [HttpGet]
        public ActionResult About()
        {
            AboutModel model = new AboutModel();
            model.Abouts = db.About.ToList();
            model.OurWorkers = db.OurWorker.ToList();
            model.VizionAndMissions = db.VizionAndMission.ToList();
            model.AboutFeature = db.AboutFeature.ToList();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Booking()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            BookingModel newbooking = new BookingModel();
            newbooking.Warehouse = db.Warehouse.ToList();
            var Users = db.User.Find(id);
            newbooking.SingleUser = Users;
            newbooking.SingleAdres = db.Address.Where(x => x.UserId == Users.Id).FirstOrDefault();


            ////int id = Convert.ToInt32(User.Identity.Name);//cookiedeki giren kişinin Id si
            //var userc = db.User.FirstOrDefault(x => x.Id == id);//user tablosunda giriş yapanı bulduk userc attık
            //var adres = db.Address.FirstOrDefault(x => x.UserId == id).FullAddress;//adres tablosunda o id ye ait kişini adresini bulduk
            //var zip = db.Address.FirstOrDefault(x => x.UserId == id).PostCode;
            //ViewBag.adres = adres;
            //ViewBag.zipcode = zip;
            //ViewBag.warehouse = db.Warehouse.ToList();


            if (Users != null)//cookie deki Id null değilse
            {
                if (newbooking.SingleAdres != null)
                {
                    return View(newbooking);//bu kişinin bilgilerinin getir
                }
                else
                {
                    return RedirectToAction("login");//değilse logine at
                }
            }
            else
            {
                return RedirectToAction("login");//değilse logine at
            }
        }
        [HttpPost]
        public ActionResult Booking(Booking booking)
        {
            int id = Convert.ToInt32(User.Identity.Name);
            BookingModel newbooking2 = new BookingModel();
            newbooking2.Warehouse = db.Warehouse.ToList();
            var Users = db.User.Find(id);
            newbooking2.SingleUser = Users;
            newbooking2.SingleAdres = db.Address.Where(x => x.UserId == Users.Id).FirstOrDefault();

            Booking newbooking = new Booking();
            newbooking.NameSurname = booking.NameSurname;
            newbooking.Phone = booking.Phone;
            newbooking.Email = booking.Email;
            newbooking.Address = booking.Address;
            newbooking.PostCode = booking.PostCode;
            newbooking.WarehouseId = booking.WarehouseId;
            newbooking.Size = booking.Size;
            newbooking.Note = booking.Note;
            newbooking.BookTime = booking.BookTime;
            newbooking.Status = booking.Status;

            db.Booking.Add(newbooking);
            db.SaveChanges();


            return View(newbooking2);

        }

        [HttpGet]
        public ActionResult Contact()
        {
            ContactModel model = new ContactModel();
            model.Contact = db.Contact.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Contact(ContactUs contactus)
        {
            var newcontacus = new ContactUs();

            newcontacus.NameSurname = contactus.NameSurname;
            newcontacus.Email = contactus.Email;
            newcontacus.Subject = contactus.Subject;
            newcontacus.Message = contactus.Message;
            newcontacus.Status = true;
            newcontacus.Delete = false;
            newcontacus.View = false;

            db.ContactUs.Add(newcontacus);
            db.SaveChanges();


            ContactModel model = new ContactModel();
            model.Contact = db.Contact.ToList();
            return View(model);
        }
        public ActionResult Facility()
        {
            FacilityModel facility = new FacilityModel();
            facility.Warehouse = db.Warehouse.ToList();
            facility.Address = db.Address.ToList();
            return View(facility);

        }
        public ActionResult FacilityDetail(int id)
        {
            FacilityModel model = new FacilityModel();

            var warehouse = db.Warehouse.Find(id);
            var address = db.Address.Find(id);

            //model.Warehouse = db.Warehouse.ToList();
            model.SingleWarehouse = warehouse;
            model.SingleAddress = address;

            model.Address = db.Address.ToList();

            return View(model);


        }
        //public ActionResult Slider()
        //{
        //    var slider = db.Slider.Where(x => x.Status == true).ToList();

        //    return View(slider);
        //}
        [HttpGet]

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, string Repeat)
        {
            var emailcontrol = db.User.FirstOrDefault(m => m.Username == user.Username);

            if (emailcontrol == null)
            {
                User newuser = new User();
                if (user.Password == Repeat)
                {
                    newuser = user;
                    newuser.PermissionId = 2;

                    db.User.Add(newuser);
                    db.SaveChanges();

                    ViewBag.mesaj = "Kayıt Başarılı";
                }
                else
                {
                    ViewBag.mesaj = "Şifre Uyuşmuyor";
                }
            }
            else
            {
                ViewBag.mesaj = user.Username + " adresi sistemde mevcut başka bir email adresi deneyiniz.";
            }
            return View();
        }


        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            var emailcontrol = db.User.FirstOrDefault(m => m.Email == Email);

            if (emailcontrol != null)
            {
                var fromAddress = new MailAddress("iskuryazilim@gmail.com");
                var toAddress = new MailAddress(Email);
                var subject = "VarBal | Şifre Hatılatma";
                var code = Guid.NewGuid().ToString().Substring(0, 8);

                try
                {
                    emailcontrol.Code = code;
                    db.SaveChanges();
                    using (var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 25,//Port=25, //587
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, "iskur123."),
                        Timeout = 30000
                    })
                    {
                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = "Merhaba " + emailcontrol.Email + " Şifre Yenileme Kodu=" + code + "<br><a href='https://localhost:44353/Home/NewPassword/" + code + "'>New Password</a>",
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(message);
                        }
                    }

                }
                catch
                {
                    //ViewBag.mesaj = "Beklenmedik Bir Hata Gerçekleşti. Lütfen Tekrar Deneyiniz.";

                    char str = emailcontrol.Password.ToCharArray()[0];
                    char str2 = emailcontrol.Password.ToCharArray()[emailcontrol.Password.Length - 1];


                    ViewBag.mesaj = "Şifren " + emailcontrol.Password.Count() + " karakterli. <br/> İlk karakteri " + str + "<br/>Son karakteri " + str2;
                }

            }
            else
            {
                ViewBag.mesaj = "Böyle Bir Kayıt Bulunamadı";
            }
            return View();
        }

        [Route("{Code}")]
        public ActionResult NewPassword(string code)
        {
            var codecontrol = db.User.FirstOrDefault(x => x.Code == code);
            if (codecontrol != null)
            {
                ViewBag.code = code;
            }
            else
            {
                ViewBag.mesaj = "Böyle Bir Code Bulunamadı";
            }
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(string Password, string Repeat, string Code)
        {
            if (Password == Repeat)
            {
                var codecontrol = db.User.FirstOrDefault(x => x.Code == Code);
                codecontrol.Password = Password;
                codecontrol.Code = null;
                db.SaveChanges();
                ViewBag.mesaj = "Şifre Değiştirme Başarılı";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.mesaj = "Şifre Aynı Değil Tekrar Deneyiniz";
                return View();
            }

        }
    }
}