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
    public class InspectionDetailsController : Controller
    {
        private GLaccountsModel db = new GLaccountsModel();

        // GET: InspectionDetails
        public ActionResult Index()
        {

            var inspectionDetails = db.InspectionDetails.Include(i => i.NationalPremisesRegister);
            return View(inspectionDetails.ToList());
        }

        // GET: InspectionDetails/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inspectionDetail = db.InspectionDetails.Find(id);
            InspectionDetails inspectionDetails = db.InspectionDetails.Find(id);
            if (inspectionDetails == null)
            {
                return HttpNotFound();
            }
            return View(inspectionDetails);
        }

        // GET: InspectionDetails/Create
        public ActionResult Create()
        {
            ViewBag.PremisesRef = new SelectList(db.NationalPremisesRegister, "Reference", "Name");
            return View();
        }

        // POST: InspectionDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Date,MLRQ1, MLRQ2, MLRQ3, MLRQ4, MLRQ5,PremisesRef,Officer,GeneralHygieneStatus")] InspectionDetails inspectionDetails)
        {
            if (ModelState.IsValid)
            {
                db.InspectionDetails.Add(inspectionDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inspectionDetails);
        }

        // GET: InspectionDetails/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InspectionDetails inspectionDetails = db.InspectionDetails.Find(id);
            if (inspectionDetails == null)
            {
                return HttpNotFound();
            }
            return View(inspectionDetails);
        }

        // POST: InspectionDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InspectionNo,Date,PremisesRef,Officer,GeneralHygieneStatus")] InspectionDetails inspectionDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspectionDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inspectionDetails);
        }

        // GET: InspectionDetails/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InspectionDetails inspectionDetails = db.InspectionDetails.Find(id);
            if (inspectionDetails == null)
            {
                return HttpNotFound();
            }
            return View(inspectionDetails);
        }

        // POST: InspectionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InspectionDetails inspectionDetails = db.InspectionDetails.Find(id);
            db.InspectionDetails.Remove(inspectionDetails);
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
