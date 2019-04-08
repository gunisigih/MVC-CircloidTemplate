using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
    public class SupplierController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();

        public SupplierController()
        {
            ViewBag.SupplierSelected = "selected";
        }

        // GET: Supplier
        public ActionResult Index()
        {
            List<Supplier> ktg = ctx.Suppliers.ToList();
            return View(ktg);
        }

        // Bu methodun içinde oluşan hata Ajax'ı etkilemez. Ajax için success Ajax'ın doğru bir şekilde action'a ulaşmış olmasıyla ilgilidir. Bu methodda veritabanındaki ilişkilerden dolayı kayıt silinemez ve benzeri hatalar Ajax'ı ilgilendirmez. Bu yüzden bu method içinde oluşan hatalarla ilgili Ajax tarafına bilgi göndermeliyiz. 
        [HttpPost]
        public string DeleteSupplier(int id)
        {
            try
            {
                Supplier s = ctx.Suppliers.Find(id);
                ctx.Suppliers.Remove(s);
                ctx.SaveChanges();

                return "OK";
            }
            catch (Exception)
            {
                return "ERROR";
            }
        }
        public ActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSupplier([Bind(Include = "SupplierID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                ctx.Suppliers.Add(supplier);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("AddSupplier");
        }

        public ActionResult UpdateSupplier(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Supplier supplier = ctx.Suppliers.Find(id);

            if (supplier == null)
                return HttpNotFound();

            return View(supplier);
        }

        [HttpPost]
        public ActionResult UpdateSupplier([Bind(Include = "SupplierID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                ctx.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("UpdateSupplier", new { id = supplier.SupplierID });
        }
    }
}