using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hidMy.Models;

namespace hidMy.Controllers
{
    public class BusinessCategoryTypesController : Controller
    {
        private GLaccountsModel db = new GLaccountsModel();

        // GET: BusinessCategoryTypes
        public ActionResult Index()
        {
            var businessCategoryTypes = from i in db.BusinessCategoryTypes
                                         orderby i.BusinessCategory ascending
                                         select i;
            return View(businessCategoryTypes.ToList());
        }

        // GET: BusinessCategoryTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessCategoryType businessCategoryType = db.BusinessCategoryTypes.Find(id);
            if (businessCategoryType == null)
            {
                return HttpNotFound();
            }
            return View(businessCategoryType);
        }

        // GET: BusinessCategoryTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessCategoryTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatID,BusinessType,BusinessCategory")] BusinessCategoryType businessCategoryType)
        {
            if (ModelState.IsValid)
            {
                db.BusinessCategoryTypes.Add(businessCategoryType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(businessCategoryType);
        }

        // GET: BusinessCategoryTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessCategoryType businessCategoryType = db.BusinessCategoryTypes.Find(id);
            if (businessCategoryType == null)
            {
                return HttpNotFound();
            }
            return View(businessCategoryType);
        }

        // POST: BusinessCategoryTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatID,BusinessType,BusinessCategory")] BusinessCategoryType businessCategoryType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessCategoryType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(businessCategoryType);
        }

        // GET: BusinessCategoryTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessCategoryType businessCategoryType = db.BusinessCategoryTypes.Find(id);
            if (businessCategoryType == null)
            {
                return HttpNotFound();
            }
            return View(businessCategoryType);
        }

        // POST: BusinessCategoryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessCategoryType businessCategoryType = db.BusinessCategoryTypes.Find(id);
            db.BusinessCategoryTypes.Remove(businessCategoryType);
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
