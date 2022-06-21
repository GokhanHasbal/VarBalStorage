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
    public class ContacUsController : Controller
    {
        // GET: ContacUs
        DataContext db = new DataContext();
        public AboutModel model()
        {
            AboutModel model = new AboutModel()
            {
                OurWorkers = db.OurWorker.Where(x => x.Delete == false).ToList(),
                VizionAndMissions = db.VizionAndMission.ToList(),
                AboutFeature = db.AboutFeature.ToList(),
                Abouts = db.About.Where(x => x.Delete == false).ToList()

            };
            return model;
        }
        public ActionResult Index()
        {
            return View(model());
        }
        [HttpGet]
        public ActionResult MainEdit(int Id)
        {
            var about = db.About.Find(Id);
            if (about != null)
            {
                return View(about);
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult MainEdit(About about, HttpPostedFileBase Image)
        {
            var editabout = db.About.Find(about.Id);
            editabout.Title = about.Title;
            editabout.Description = about.Description;
            editabout.State = about.State;
            string imagepath = "";
            string imagename = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/AboutImage"), imagename);
                    Image.SaveAs(imagepath);
                    editabout.Image = imagename;
                }
                ViewBag.mesaj = "Hakkımızda Düzenleme Başarılı";
            }
            catch
            {
                ViewBag.mesaj = "Unexpected Error";
            }
            db.SaveChanges();
            return View(editabout);
        }
        [HttpGet]
        public ActionResult MissionVisionEdit(int Id)
        {
            var about = db.VizionAndMission.Find(Id);
            if (about != null)
            {
                return View(about);
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult MissionVisionEdit(VizionAndMission about, HttpPostedFileBase Image)
        {
            var editabout = db.VizionAndMission.Find(about.Id);
            editabout.Title = about.Title;
            editabout.Description = about.Description;
            string imagepath = "";
            string imagename = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/AboutImage"), imagename);
                    Image.SaveAs(imagepath);
                    editabout.Image = imagename;
                }
                ViewBag.mesaj = "Hakkımızda Düzenleme Başarılı";
            }
            catch
            {
                ViewBag.mesaj = "Unexpected Error";
            }
            db.SaveChanges();
            return View(editabout);
        }
        [HttpGet]
        public ActionResult FeatureEdit(int Id)
        {
            var about = db.AboutFeature.Find(Id);
            if (about != null)
            {
                return View(about);
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult FeatureEdit(AboutFeature about, HttpPostedFileBase Image)
        {
            var editabout = db.AboutFeature.Find(about.Id);
            editabout.Title = about.Title;
            editabout.Description = about.Description;
            string imagepath = "";
            string imagename = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/AboutImage"), imagename);
                    Image.SaveAs(imagepath);
                    editabout.Image = imagename;
                }
                ViewBag.mesaj = "Hakkımızda Düzenleme Başarılı";
            }
            catch
            {
                ViewBag.mesaj = "Unexpected Error";
            }
            db.SaveChanges();
            return View(editabout);
        }
        [HttpGet]
        public ActionResult WorkerEdit(int Id)
        {
            var about = db.OurWorker.Find(Id);
            if (about != null)
            {
                return View(about);
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult WorkerEdit(OurWorker about, HttpPostedFileBase Image)
        {
            var editabout = db.OurWorker.Find(about.Id);
            editabout.NameSurname = about.NameSurname;
            editabout.Description = about.Description;
            editabout.Title = about.Title;
            editabout.Status = about.Status;
            string imagepath = "";
            string imagename = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/AboutImage"), imagename);
                    Image.SaveAs(imagepath);
                    editabout.Image = imagename;
                }
                ViewBag.mesaj = "Hakkımızda Düzenleme Başarılı";
            }
            catch
            {
                ViewBag.mesaj = "Unexpected Error";
            }
            db.SaveChanges();
            return View(editabout);
        }
        public ActionResult DeleteWorker(int Id)
        {
            var worker = db.OurWorker.Find(Id);
            worker.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult WorkerStatusEdit(int Id)
        {
            var worker = db.OurWorker.Find(Id);
            worker.Status = !worker.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult WorkerAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WorkerAdd(OurWorker worker)
        {
            db.OurWorker.Add(worker);
            db.SaveChanges();
            ViewBag.mesaj = "Çalışan Ekleme Başarılı";
            return View();
        }


    }
}