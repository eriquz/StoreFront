using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class ShoeSizesController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: ShoeSizes
        public ActionResult Index()
        {
            return View(db.ShoeSizes.ToList());
        }

        // GET: ShoeSizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoeSize shoeSize = db.ShoeSizes.Find(id);
            if (shoeSize == null)
            {
                return HttpNotFound();
            }
            return View(shoeSize);
        }

        // GET: ShoeSizes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoeSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShoeSizeID,Size")] ShoeSize shoeSize)
        {
            if (ModelState.IsValid)
            {
                db.ShoeSizes.Add(shoeSize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoeSize);
        }

        // GET: ShoeSizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoeSize shoeSize = db.ShoeSizes.Find(id);
            if (shoeSize == null)
            {
                return HttpNotFound();
            }
            return View(shoeSize);
        }

        // POST: ShoeSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoeSizeID,Size")] ShoeSize shoeSize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoeSize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoeSize);
        }

        // GET: ShoeSizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoeSize shoeSize = db.ShoeSizes.Find(id);
            if (shoeSize == null)
            {
                return HttpNotFound();
            }
            return View(shoeSize);
        }

        // POST: ShoeSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoeSize shoeSize = db.ShoeSizes.Find(id);
            db.ShoeSizes.Remove(shoeSize);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
