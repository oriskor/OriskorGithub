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
    public class RolesAddEditController : Controller
    {
        private EDIEntities db = new EDIEntities();

        // GET: /RolesAddEdit/
        public ActionResult Index()
        {
            return View(db.role_master.ToList());
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            return View(db.role_master.ToList());
        }

        // GET: /RolesAddEdit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            role_master role_master = db.role_master.Find(id);
            if (role_master == null)
            {
                return HttpNotFound();
            }
            return View(role_master);
        }

        // GET: /RolesAddEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /RolesAddEdit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="roll_name")] role_master role_master)
        {
            if (ModelState.IsValid)
            {
                int RoleID = db.role_master.Select(x => x.role_id).Max();
                role_master.role_id = RoleID + 10;
                db.role_master.Add(role_master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role_master);
        }

        // GET: /RolesAddEdit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            role_master role_master = db.role_master.Find(id);
            if (role_master == null)
            {
                return HttpNotFound();
            }
            return View(role_master);
        }

        // POST: /RolesAddEdit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="role_id,roll_name")] role_master role_master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role_master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role_master);
        }

        // GET: /RolesAddEdit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            role_master role_master = db.role_master.Find(id);
            if (role_master == null)
            {
                return HttpNotFound();
            }
            return View(role_master);
        }

        // POST: /RolesAddEdit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            role_master role_master = db.role_master.Find(id);
            db.role_master.Remove(role_master);
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
