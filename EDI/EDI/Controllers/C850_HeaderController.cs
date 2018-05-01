using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EDI.Models;
using EDI.Helpers.Awesome;


namespace EDI.Controllers
{
    public class C850_HeaderController : Controller
    {
        private EDIEntities db = new EDIEntities();

        // GET: C850_Header
        public ActionResult Index()
        {
            IEnumerable<C850_Header> a = db.C850_Header.ToList();
            return View(a);
        }

        public ActionResult mainGrid()
        {
            return View();
        }
        // GET: C850_Header/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C850_Header c850_Header = db.C850_Header.Find(id);
            if (c850_Header == null)
            {
                return HttpNotFound();
            }
            return View(c850_Header);
        }

        // GET: C850_Header/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: C850_Header/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HeaderKey,FunctionalGroupKey,FGKeySave,ST01_TranSetIdfrCode,ST02_TranSetControlNo,BEG01_TransactionSetPurposeCode,BEG02_PurchaseOrderTypeCode,BEG03_PurchaseOrderNumber,BEG05_PODate,CUR02_CurrencyCode,REF02_FreeFormText,REF02_InternalVendorNo,REF02_ProductGroup,PER02_ContactPersonName,DTM02_DeliveryRequestedDate,DTM02_RequestedPickupDate,N104_ShipFromID,N401_ShipFromCity,N402_ShipFromState,N403_ShipFromPostalCode,N404_ShipFromCountryCode,N104_ShipToID,N401_ShipToCity,N402_ShipToState,N403_ShipToPostalCode,N404_ShipToCountryCode,CTT01_NumberOfPO1Segments")] C850_Header c850_Header)
        {
            if (ModelState.IsValid)
            {
                db.C850_Header.Add(c850_Header);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c850_Header);
        }

        // GET: C850_Header/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C850_Header c850_Header = db.C850_Header.Find(id);
            if (c850_Header == null)
            {
                return HttpNotFound();
            }
            return View(c850_Header);
        }

        // POST: C850_Header/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HeaderKey,FunctionalGroupKey,FGKeySave,ST01_TranSetIdfrCode,ST02_TranSetControlNo,BEG01_TransactionSetPurposeCode,BEG02_PurchaseOrderTypeCode,BEG03_PurchaseOrderNumber,BEG05_PODate,CUR02_CurrencyCode,REF02_FreeFormText,REF02_InternalVendorNo,REF02_ProductGroup,PER02_ContactPersonName,DTM02_DeliveryRequestedDate,DTM02_RequestedPickupDate,N104_ShipFromID,N401_ShipFromCity,N402_ShipFromState,N403_ShipFromPostalCode,N404_ShipFromCountryCode,N104_ShipToID,N401_ShipToCity,N402_ShipToState,N403_ShipToPostalCode,N404_ShipToCountryCode,CTT01_NumberOfPO1Segments")] C850_Header c850_Header)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c850_Header).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c850_Header);
        }

        // GET: C850_Header/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C850_Header c850_Header = db.C850_Header.Find(id);
            if (c850_Header == null)
            {
                return HttpNotFound();
            }
            return View(c850_Header);
        }

        // POST: C850_Header/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C850_Header c850_Header = db.C850_Header.Find(id);
            db.C850_Header.Remove(c850_Header);
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
