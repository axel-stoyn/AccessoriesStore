using Domain.Abstract;
using Domain.Context;
using Domain.Organization;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        AccessoriesDbContext db = new AccessoriesDbContext();
        IAccessory repository;

        public AdminController (IAccessory repo)
        {
            repository = repo;
        }
        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.Accessories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Accessory accessory, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid /*&& upload != null*/)
            {
                //byte[] imageData = null;
                //using (var binaryReader = new BinaryReader(upload.InputStream))
                //{
                //    imageData = binaryReader.ReadBytes(upload.ContentLength);
                //}
                //accessory.ImageData = imageData;
                db.Accessories.Add(accessory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            ViewBag.Message = accessory.Name + " " + accessory.Cost + " " + accessory.Description + " " + accessory.Cathegory;
            return View(accessory);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accessory = db.Accessories.Find(id);
            if (accessory == null)
            {
                return HttpNotFound();
            }
            return View(accessory);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accessoriesToUpdate = db.Accessories.Find(id);
            if (TryUpdateModel(accessoriesToUpdate, "",
               new string[] { "Name", "Cost", "Cathegory", "Discription" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(accessoriesToUpdate);
        }

        public ActionResult Delete(int? id, bool? saveChangesError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again or inform to administrator";
            }
            var accessory = db.Accessories.Find(id);
            if (accessory == null)
            {
                return HttpNotFound();
            }
            return View(accessory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Accessory accessory = db.Accessories.Find(id);
                db.Accessories.Remove(accessory);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}