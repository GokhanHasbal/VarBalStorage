using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarBal.Models;
using VarBal.AdminModel;

namespace VarBal.Controllers
{
    public class ShipmentController : Controller
    {
        // GET: Shipment
        DataContext db = new DataContext();

        public ShipmentModel model()
        {
            ShipmentModel model = new ShipmentModel()
            {
                Users = db.User.Where(x => x.Delete == false && x.PermissionId==6).ToList(),
                Warehouses = db.Warehouse.Where(x => x.Delete == false).ToList(),
                Products = db.Product.Where(x => x.Delete == false).ToList(),
                Vehicles = db.Vehicle.Where(x => x.Delete == false).ToList(),
                Shipments=db.Shipment.ToList()
            };
            return model;
        }
        public ActionResult Index()
        {
            return View(model());

        }
      
        public ActionResult Delete(int Id)
        {
            var shipment = db.Shipment.Find(Id);
            shipment.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult ShipmentStatusEdit(int Id)
        {
            var shipment = db.Shipment.Find(Id);
            shipment.Status = !shipment.Status;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(model());
        }  
        [HttpPost]
        public ActionResult Create(Shipment shipment)
        {
            var stoksil= shipment.Stock.Value;
            var urunid= shipment.ProductId; 

            Shipment newshipment = new Shipment();
            newshipment.VehicleId = shipment.VehicleId;
            newshipment.Description = shipment.Description;
            newshipment.ProductId = shipment.ProductId;
            newshipment.UserId = shipment.UserId;
            newshipment.SenderId = shipment.SenderId;
            newshipment.ReceiverId = shipment.ReceiverId;
            newshipment.Stock = shipment.Stock;
            newshipment.SendTime = DateTime.Now;
            //newshipment.ReceiveTime = shipment.ReceiveTime;
            newshipment.Status = false;
            //newshipment.Delete = false;
            //newshipment.View = false;
            newshipment.Process = shipment.Process;
            db.Shipment.Add(newshipment);

            var editproduct = db.Product.Find(urunid);
            editproduct.Stock -= stoksil;
            db.SaveChanges();



            return View(model());
        }
        [HttpGet]
        public ActionResult Detail (int id)
        {
            ShipmentModel model = new ShipmentModel();

            var shipment = db.Shipment.Find(id);
            model.SingleShipment = shipment;

            var productid = shipment.ProductId;
            var userid = shipment.UserId;
            var vehicid = shipment.VehicleId;
            var warsendid = shipment.SenderId;
            var warrecid = shipment.ReceiverId;


            var product = db.Product.Find(productid);
            model.SingleProduct = product;

            var user = db.User.Find(userid);
            model.SingleUser = user;

            var vehicle = db.Vehicle.Find(vehicid);
            model.SingleVehicle = vehicle;

            var wareShouse = db.Warehouse.Find(warsendid);
            model.SingleSenderWareHouse = wareShouse;
            
            var wareRhouse = db.Warehouse.Find(warrecid);
            model.SingleReceiverWareHouse = wareRhouse;

            if (shipment.View == false)
            {
                shipment.View = true;
                db.SaveChanges();
                 return View(model);
            }
            else
            {
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Detail(Shipment shipment, Product product)
        {
            var editshipment = db.Shipment.Find(shipment.Id);
            editshipment.Status = true;
            editshipment.ReceiveTime = DateTime.Now;
            editshipment.Process = shipment.Process;

            var stokekle = editshipment.Stock.Value;        // böyle herhalde

            var urun = db.Product.Where(x => x.Id == editshipment.ProductId).FirstOrDefault();

            //var depo = db.Shipment.Find(shipment.ReceiverId);
            var depo = db.Product.Where(x=>x.WarehouseId== editshipment.ReceiverId).FirstOrDefault();


            Product newproduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                RegisterDate=DateTime.Now,
                Barcode = product.Barcode,
                Price = product.Price,
                Tax = product.Tax,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                ShelfId = product.ShelfId,
                WarehouseId = product.WarehouseId,
                Status = product.Status,
                Delete = product.Delete,
                View = product.View,
                Image = product.Image,
                Stock = product.Stock

            };

            if (depo==null)
            {
                db.Product.Add(newproduct);
                db.SaveChanges();
            }
            else
            {
                var urunguncelle = db.Product.Where(x=>x.Barcode== urun.Barcode && x.WarehouseId==editshipment.ReceiverId).FirstOrDefault();
                urunguncelle.Stock += stokekle;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
