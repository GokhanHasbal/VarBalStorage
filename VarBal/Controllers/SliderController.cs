using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using System.IO;


namespace VarBal.Controllers
{
    public class SliderController : Controller
    {
        // GET: Slider
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var slider = db.Slider.Where(x => x.Delete == false).ToList();
            return View(slider);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(HttpPostedFileBase Image, string Title, string Description, string Link, bool State)
        {
            Slider slider = new Slider();
            string resimyolu = "";
            string resimadi = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    resimadi = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName);
                    resimyolu = Path.Combine(Server.MapPath("~/Content/images/SliderImage"), resimadi);
                    Image.SaveAs(resimyolu);
                    slider.Image = resimadi;
                }
            }
            catch
            {

            }
            slider.Title = Title;
            slider.Link = Link;
            slider.Status = State;
            db.Slider.Add(slider);
            db.SaveChanges();

            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var picture = db.Slider.Find(Id);
            return View(picture);
        }
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase Image, Slider slider)
        {

            var picture = db.Slider.Find(slider.Id);

            string resimyolu = "";
            string resimadi = "";
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    resimadi = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName);
                    resimyolu = Path.Combine(Server.MapPath("~/Content/images/SliderImage"), resimadi);
                    Image.SaveAs(resimyolu);
                    picture.Image = resimadi;
                }
            }
            catch
            {
            }
            picture.Description = slider.Description;
            picture.Title = slider.Title;
            picture.Link = slider.Link;
            picture.Status = slider.Status;
            db.SaveChanges();
            return View(picture);
        }
        public ActionResult Delete(int Id)
        {
            var slider = db.Slider.Find(Id);
            slider.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult SliderStatusEdit(int Id)
        {
            var slider = db.Slider.Find(Id);
            slider.Status = !slider.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}