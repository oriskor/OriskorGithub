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
    public class LoginController : Controller
    {
        private EDIEntities db = new EDIEntities();

        // GET: /Login/
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: /Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_master user_master = db.user_master.Find(id);
            if (user_master == null)
            {
                return HttpNotFound();
            }
            return View(user_master);
        }

        // GET: /Login/Create
        public ActionResult Create()
        {
            ViewBag.c_id = new SelectList(db.company_master, "c_id", "c_name");
            ViewBag.role_id = new SelectList(db.role_master, "role_id", "roll_name");
            return View();
        }

        // POST: /Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="user_id,u_first_name,u_last_name,c_id,role_id,UserName,phone_no,email_id,Password")] user_master user_master)
        {
            if (ModelState.IsValid)
            {
                db.user_master.Add(user_master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.c_id = new SelectList(db.company_master, "c_id", "c_name", user_master.c_id);
            ViewBag.role_id = new SelectList(db.role_master, "role_id", "roll_name", user_master.role_id);
            return View(user_master);
        }

        // GET: /Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_master user_master = db.user_master.Find(id);
            if (user_master == null)
            {
                return HttpNotFound();
            }
            ViewBag.c_id = new SelectList(db.company_master, "c_id", "c_name", user_master.c_id);
            ViewBag.role_id = new SelectList(db.role_master, "role_id", "roll_name", user_master.role_id);
            return View(user_master);
        }

        // POST: /Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="user_id,u_first_name,u_last_name,c_id,role_id,UserName,phone_no,email_id,Password")] user_master user_master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.c_id = new SelectList(db.company_master, "c_id", "c_name", user_master.c_id);
            ViewBag.role_id = new SelectList(db.role_master, "role_id", "roll_name", user_master.role_id);
            return View(user_master);
        }

        // GET: /Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_master user_master = db.user_master.Find(id);
            if (user_master == null)
            {
                return HttpNotFound();
            }
            return View(user_master);
        }

        // POST: /Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_master user_master = db.user_master.Find(id);
            db.user_master.Remove(user_master);
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
