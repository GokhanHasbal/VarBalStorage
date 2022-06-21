using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;


namespace VarBal.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Vehicle
        DataContext db = new DataContext();

        public ActionResult Index()
        {

            var vehicle = db.Vehicle.Where(x => x.Delete == false).ToList();
            return View(vehicle);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Vehicle vehicle)
        {
            Vehicle newvehicle = new Vehicle();
            newvehicle.Name = vehicle.Name;
            newvehicle.LicensePlate = vehicle.LicensePlate;
            newvehicle.Type = vehicle.Type;
            newvehicle.Status = vehicle.Status;
            db.Vehicle.Add(newvehicle);
            db.SaveChanges();
            ViewBag.mesaj = "Araç Ekleme Başarılı";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var vehicle = db.Vehicle.Find(Id);
            if (vehicle != null)
            {
               
                return View(vehicle);
            }
            else
            {
                //return Redirect("~/Shelf");
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Vehicle vehicle)
        {
            var editvehicle = db.Vehicle.Find(vehicle.Id);
            editvehicle.Name = vehicle.Name;
            editvehicle.LicensePlate = vehicle.LicensePlate;
            editvehicle.Type = vehicle.Type;
            editvehicle.Status = vehicle.Status;
            db.SaveChanges();
            return Redirect("~/Vehicle/Edit?id=" + vehicle.Id);
        }
        public ActionResult Delete(int Id)
        {
            var vehicle = db.Vehicle.Find(Id);
            vehicle.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult VehicleStatusEdit(int Id)
        {
            var vehicle = db.Vehicle.Find(Id);
            vehicle.Status = !vehicle.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}