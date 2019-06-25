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
    public class NationalPremisesRegistersController : Controller
    {
        private GLaccountsModel db = new GLaccountsModel();

        // GET: NationalPremisesRegisters
        public ActionResult Index()


        {
            var list= db.NationalPremisesRegister.ToList();

            return View(db.NationalPremisesRegister.ToList());
        }

        // GET: NationalPremisesRegisters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalPremisesRegister nationalPremisesRegister = db.NationalPremisesRegister.Find(id);
            if (nationalPremisesRegister == null)
            {
                return HttpNotFound();
            }
            return View(nationalPremisesRegister);
        }

        // GET: NationalPremisesRegisters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NationalPremisesRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Reference,Area,SubArea,Name,SubBuildingNumber,BuildingNumber,BuildingName,StreetName,Townland,Locality,CityTown,County,Postcode,FoodBusinessOperators,BusinessCategory,BusinessType,RiskCategory,Foodofficer")] NationalPremisesRegister nationalPremisesRegister)
        {
            if (ModelState.IsValid)
            {
                db.NationalPremisesRegister.Add(nationalPremisesRegister);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nationalPremisesRegister);
        }

        // GET: NationalPremisesRegisters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalPremisesRegister nationalPremisesRegister = db.NationalPremisesRegister.Find(id);
            if (nationalPremisesRegister == null)
            {
                return HttpNotFound();
            }
            return View(nationalPremisesRegister);
        }

        // POST: NationalPremisesRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Reference,Area,SubArea,Name,SubBuildingNumber,BuildingNumber,BuildingName,StreetName,Townland,Locality,CityTown,County,Postcode,FoodBusinessOperators,BusinessCategory,BusinessType,RiskCategory,Foodofficer")] NationalPremisesRegister nationalPremisesRegister)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nationalPremisesRegister).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nationalPremisesRegister);
        }

        // GET: NationalPremisesRegisters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalPremisesRegister nationalPremisesRegister = db.NationalPremisesRegister.Find(id);
            if (nationalPremisesRegister == null)
            {
                return HttpNotFound();
            }
            return View(nationalPremisesRegister);
        }

        // POST: NationalPremisesRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NationalPremisesRegister nationalPremisesRegister = db.NationalPremisesRegister.Find(id);
            db.NationalPremisesRegister.Remove(nationalPremisesRegister);
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
