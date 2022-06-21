using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using VarBal.AdminModel;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace VarBal.Controllers
{
    public class UserController : Controller
    {
        DataContext db = new DataContext();
        public UserModel model()
        {
            UserModel model = new UserModel()
            {
                User = db.User.Where(x => x.Delete == false).ToList(),
                Permission = db.Permission.Where(x => x.Delete == false).ToList(),
            };
            return model;
        }
        public ActionResult Index()
        {
            return View(model());
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View(model());
        }
        [HttpPost]
        public ActionResult Add(User user)
        {
            var emailcontrol = db.User.FirstOrDefault(m => m.Email == user.Email);

            if (emailcontrol == null)
            {
                var fromAddress = new MailAddress("iskuryazilim@gmail.com");
                var toAddress = new MailAddress(user.Email);
                var subject = "VarBal | Yeni Kayıt Bilgilendirme";
                var code = "Varbal-" + Guid.NewGuid().ToString().Substring(0, 5);
                User newuser = new User();
                newuser = user;
                newuser.Password = code;
                db.User.Add(newuser);
                db.SaveChanges();
                //try
                //{
                //    using (var smtp = new SmtpClient
                //    {
                //        Host = "smtp.gmail.com",
                //        Port = 587,//Port=25,
                //        EnableSsl = true,
                //        DeliveryMethod = SmtpDeliveryMethod.Network,
                //        UseDefaultCredentials = false,
                //        Credentials = new NetworkCredential(fromAddress.Address, "iskur123."),
                //        Timeout = 30000
                //    })
                //    {
                //        using (var message = new MailMessage(fromAddress, toAddress)
                //        {
                //            Subject = subject,
                //            Body = "Merhaba " + user.Email + ", kullanıcı kaydınız başarılı bir şekilde gerçekleşti <br> Şifreniz=" + code + "<br>Giriş yapmak için <a href='https://localhost:44353/Admin/Login'>tıklayınız</a><br>Gökhan Hasbal <br><img ' src ='https://blog.decathlon.com.tr/wp-content/uploads/2021/03/Screenshot.png'><br>VarBal Genel Müdür",
                //            IsBodyHtml = true
                //        })
                //        {
                //            smtp.Send(message);
                //        }
                //    }
                //}
                //catch
                //{
                //    ViewBag.mesaj = "Beklenmedik Bir Hata Gerçekleşti. Lütfen Tekrar Deneyiniz.";
                //    return View(model());
                //}
            }
            else
            {
                ViewBag.mesaj = emailcontrol.Email + "  mail adresi sistemde mevcut";
                return View(model());
            }
            var lastuser = db.User.ToList().LastOrDefault();
            return Redirect("~/User/Edit?id=" + lastuser.Id);
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var user = db.User.Find(Id);

            if (user != null)
            {
                ViewBag.permission = model().Permission.ToList();
                if (user.View == false)
                {
                    user.View = true;
                    db.SaveChanges();
                }
                return View(user);
            }
            else
            {
                return Redirect("~/Admin");
            }
        }
        [HttpPost]
        public ActionResult Edit(User user, HttpPostedFileBase Image)
        {
            var useredit = db.User.Find(user.Id);
            string imagepath = "";
            string imagename = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/UserImage"), imagename);
                    Image.SaveAs(imagepath);
                    useredit.Image = imagename;
                }
                useredit.Email = user.Email;
                useredit.Username = user.Username;
                useredit.Name = user.Name;
                useredit.Surname = user.Surname;
                useredit.Password = user.Phone;
                useredit.PermissionId = user.PermissionId;
                useredit.Status = user.Status;
                useredit.Password = user.Password;
                db.SaveChanges();
                ViewBag.mesaj = "Bilgiler başarılı bir şekilde değiştirildi";
            }
            catch
            {
                ViewBag.mesaj = "Unexpected Error";
            }
            return Redirect("~/User/Edit?id=" + user.Id);
        }
        public ActionResult Delete(int Id)
        {
            var user = db.User.Find(Id);
            user.Delete = true;
            user.Status = false;
            db.SaveChanges();
            return Redirect("~/User");
        }

        [Authorize]
        public ActionResult MyAccount()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            if (id > 0)
            {
                var usercontrol = db.User.FirstOrDefault(x => x.Id == id);
                if (usercontrol != null)
                {
                    ViewBag.yetki = db.Permission.Find(usercontrol.PermissionId).Name;
                   
                    return View(usercontrol);
                }
                else
                {
                    return RedirectToAction("login","Home");
                }
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult UpdateImage(HttpPostedFileBase Image)
        {
            int id = Convert.ToInt32(User.Identity.Name);
            if (id > 0)
            {
                var user = db.User.FirstOrDefault(x => x.Id == id);

                string imagepath = "";
                string imagename = "";

                try
                {
                    if (Image != null && Image.ContentLength > 0)
                    {
                        imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                        imagepath = Path.Combine(Server.MapPath("~/Content/images/UserImage"), imagename);
                        Image.SaveAs(imagepath);

                        user.Image = imagename;
                        db.SaveChanges();
                        ViewBag.mesaj = "Resim Güncelleme Başarılı";
                    }
                }
                catch
                {
                    ViewBag.mesaj = "Beklenmedik Bir Hata Oluştu";
                }

                return RedirectToAction("MyAccount");
            }
            else
            {
                return RedirectToAction("login", "Home");
            }

        }
        [HttpGet]
        [Authorize]
        public ActionResult UpdateMyAccount()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            if (id > 0)
            {
                var usercontrol = db.User.FirstOrDefault(x => x.Id == id);
                if (usercontrol != null)
                {
                    ViewBag.yetki = db.Permission.Find(usercontrol.PermissionId).Name;
                   
                    return View(usercontrol);
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

        [HttpPost]

        [Authorize]

        public ActionResult UpdateMyAccount(User user)
        {
            int id = Convert.ToInt32(User.Identity.Name);
            if (id > 0)
            {
                var usercontrol = db.User.FirstOrDefault(x => x.Id == id);
                if (usercontrol != null)
                {
                    usercontrol.Username = user.Username;
                    usercontrol.Name = user.Name;
                    usercontrol.Surname = user.Surname;
                    usercontrol.Phone = user.Phone;

                    usercontrol.Email = user.Email;


                    db.SaveChanges();

                    ViewBag.yetki = db.Permission.Find(usercontrol.PermissionId).Name;

                    ViewBag.mesaj = "Bilgiler Başarılı Bir Şekilde Güncellendi";

                    return View(usercontrol);
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


        [HttpGet]
        [Authorize]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ResetPassword(string Password, string NewPassword, string RepeatPassword)
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var passwordcontrol = db.User.FirstOrDefault(x => x.Password == Password && x.Id == id);
            if (passwordcontrol != null)
            {
                if (Password != NewPassword)
                {
                    if (NewPassword == RepeatPassword)
                    {
                        passwordcontrol.Password = NewPassword;
                        db.SaveChanges();
                        ViewBag.mesaj = "Şifre Değiştirme Başarılı";
                    }
                    else
                    {
                        ViewBag.mesaj = "Şifreler Uyuşmuyor";
                    }
                }
                else
                {
                    ViewBag.mesaj = "Yeni Şifre Eski Şifre Ile Aynı Olamaz";
                }
            }
            else
            {
                ViewBag.mesaj = "Şifre Doğru Değil";

            }
            return View();
        }



    }
}