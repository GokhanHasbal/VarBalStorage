using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace VarBal.Models
{
    public class DataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DataContext() : base("dataContext") { }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Shelf> Shelf { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<NewsGet> NewsGet { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<OurWorker> OurWorker { get; set; }
        public DbSet<VizionAndMission> VizionAndMission { get; set; }
        public DbSet<AboutFeature> AboutFeature { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Booking> Booking { get; set; }
        

    }
}