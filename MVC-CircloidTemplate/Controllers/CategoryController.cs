using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
    public class CategoryController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Category

        public CategoryController()
        {
            ViewBag.CategorySelected = "selected";
        }
        public ActionResult Index()
        {
             List<Category> ctgList = ctx.Categories.ToList();

            return View(ctgList);
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory([Bind(Include = "CategoryName, Description")] Category cat, HttpPostedFileBase Picture)
        {
            if (Picture == null)
                return View();

            cat.Picture = ConvertToBytes(Picture);

            ctx.Categories.Add(cat);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        // Category Picture nesnesi Database'de byte[] şeklinde tutulduğu için seçilen resmi byte[]'e çevrilmesini sağlayan method.
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes(image.ContentLength);
            byte[] bytes = new byte[imageBytes.Length + 78];
            Array.Copy(imageBytes, 0, bytes, 78, imageBytes.Length);
            return bytes;
        }
        public ActionResult UpdateCategory(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Category ktg = ctx.Categories.Find(id);

            if (ktg == null)
                return HttpNotFound();

            return View(ktg);
        }

        [HttpPost]
        public ActionResult UpdateCategory([Bind(Include = "CategoryID, CategoryName, Description")] Category ktg, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                Category k = ctx.Categories.Find(ktg.CategoryID);

                k.CategoryName = ktg.CategoryName;
                k.Description = ktg.Description;

                if (Picture != null)
                {
                    k.Picture = ConvertToBytes(Picture);
                }
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //public ActionResult DeleteCategory(int catID)
        //{
        //    Category cat = ctx.Categories.FirstOrDefault(x => x.CategoryID == catID);
        //    return View(cat);
        //}

        [HttpPost]
        public ActionResult DeleteCategory(int? id)
        {
            Category cat = ctx.Categories.FirstOrDefault(x => x.CategoryID == id);
            ctx.Categories.Remove(cat);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}