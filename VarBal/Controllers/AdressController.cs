using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using VarBal.AdminModel;

namespace VarBal.Controllers
{
    public class AdressController : Controller
    {
        // GET: Adress
        DataContext db = new DataContext();
        public AddressModel model()
        {
            AddressModel model = new AddressModel()
            {
                User = db.User.Where(x => x.Delete == false).ToList(),
                Permission = db.Permission.Where(x => x.Delete == false).ToList(),
                Address = db.Address.Where(x => x.Delete == false).ToList(),
            };
            return model;
        }
        public ActionResult Index(User user)
        {
            var model = db.Address.Where(x => x.Delete == false).ToList();
            return View(model);

        
        }
  
        public ActionResult MyAddress(User user)
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var usercontrol = db.User.FirstOrDefault(x => x.Id == id);
            ViewBag.username = usercontrol.Name;
            ViewBag.usersurname = usercontrol.Surname;
            return View(model());
        }
        [HttpGet]
        public ActionResult Select(int Id)
        {
            AddressModel model = new AddressModel();
            int userid = Convert.ToInt32(User.Identity.Name);
            model.Address = db.Address.Where(x => x.Status == true && x.Delete == false && x.UserId == userid).ToList();

            foreach (var item in model.Address)
            {
                item.Selection = false;
            }
            var address = db.Address.Find(Id);
            var address2 = model.Address.FirstOrDefault(x => x.Id == Id);  // bu daha hızlıymış
            address.Selection = true;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View(model());
        }
        [HttpPost]
        public ActionResult Add(Address address)
        {
            int id = Convert.ToInt32(User.Identity.Name);
            Address newaddress = new Address();
            newaddress = address;
            newaddress.UserId = id;
            newaddress.Status = true;
            newaddress.Selection = false;
            newaddress.Delete = false;
            db.Address.Add(newaddress);
            db.SaveChanges();
            ViewBag.mesaj = "Adres eklendi";
            return View(model());
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var address = db.Address.Find(Id);
            if (address != null)
            {
                return View(address);
            }
            else
            {
                return Redirect("~/Address");
            }
        }
        [HttpPost]
        public ActionResult Edit(Address address)
        {
            var addressedit = db.Address.Find(address.Id);
            addressedit.Title = address.Title;
            addressedit.Country = address.Country;
            addressedit.City = address.City;
            addressedit.District = address.District;
            addressedit.Street = address.Street;
            addressedit.PostCode = address.PostCode;
            addressedit.BuildingNumber = address.BuildingNumber;
            addressedit.FullAddress = address.FullAddress;
            addressedit.Status = address.Status;
            addressedit.Selection = address.Selection;
            db.SaveChanges();

            //return Redirect("~/Address/Edit?id=" + address.Id);
            ViewBag.bildirim = "Bilgiler başarılı bir şekilde değiştirildi";
            return View(addressedit);
        }
        public ActionResult Delete(int Id)
        {
            var address = db.Address.Find(Id);
            address.Delete = true;
            address.Status = false;
            db.SaveChanges();
            return Redirect("~/Address");
        }
        public ActionResult AddressStatusEdit(int Id)
        {
            var address = db.Address.Find(Id);
            address.Status = !address.Status;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}