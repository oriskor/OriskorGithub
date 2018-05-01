using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EDI.Models;

namespace EDI.Controllers
{
    public class AdminDashboardController : Controller
    {
        private EDIEntities db = new EDIEntities();

        // GET: /AdminDashboard/
        public ActionResult Index()
        {
            return View(db.menu_master.ToList());
          
        }

        // GET: /AdminDashboard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu_master menu_master = db.menu_master.Find(id);
            if (menu_master == null)
            {
                return HttpNotFound();
            }
            return View(menu_master);
        }

        // GET: /AdminDashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AdminDashboard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="m_id,m_name")] menu_master menu_master)
        {
            if (ModelState.IsValid)
            {
                db.menu_master.Add(menu_master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu_master);
        }

        // GET: /AdminDashboard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu_master menu_master = db.menu_master.Find(id);
            if (menu_master == null)
            {
                return HttpNotFound();
            }
            return View(menu_master);
        }

        // POST: /AdminDashboard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="m_id,m_name")] menu_master menu_master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu_master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu_master);
        }

        // GET: /AdminDashboard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu_master menu_master = db.menu_master.Find(id);
            if (menu_master == null)
            {
                return HttpNotFound();
            }
            return View(menu_master);
        }

        // POST: /AdminDashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            menu_master menu_master = db.menu_master.Find(id);
            db.menu_master.Remove(menu_master);
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
