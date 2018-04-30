using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EDI.Class_Structure;
using EDI.Models;

namespace EDI.Controllers
{
    public class ModuleRoleMappingController : Controller
    {
        private EDIEntities db = new EDIEntities();
        public string[] SelectedValues { get; set; }

        public MultiSelectList DropDownList { get; set; }
        [ChildActionOnly]
        public PartialViewResult GetMenu()
        {
            List<C850_Header> menuStructuredModel = db.C850_Header.ToList();

            return PartialView("~/Views/Shared/_MasterPage.cshtml", menuStructuredModel);
        }
        // GET: ModuleRoleMapping

        public ActionResult Index()
        {
            PermissionType permissionType = new PermissionType();
            ViewData["Items"] = new MultiSelectList(db.role_master.ToList(), "role_id", "roll_name");
            List<UserRole> ListUserRole = new List<UserRole>();
            ListUserRole = db.role_master.Select(x => new UserRole { RoleName = x.roll_name, RileID = x.role_id }).ToList();
            DropDownList = new MultiSelectList(ListUserRole.Select(x => new { RileID = x.RileID, RoleName = x.RoleName }).ToList(), "RileID", "RoleName");
            ViewBag.ddlRole = DropDownList;
            //  ViewBag.ddlRole = new SelectList(db.role_master.ToList(), "role_id", "roll_name");
            ViewBag.ddlMenu = new SelectList(db.menu_master.ToList(), "m_id", "m_name");
            ViewBag.ddlPermission = new SelectList(db.PermissionTypes.ToList(), "PermissionTypeID", "PermissionTypeDesc");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadPhysiansByDepartment(string deptId)
        {


            List<menu_master> objmenu_master = new List<menu_master>();
            objmenu_master = Comman.getAllMenuWhichNotAssignedToTheRole(Convert.ToInt32(deptId));
            //Your Code For Getting Physicans Goes Here
           // var phyList = this.GetPhysicans(Convert.ToInt32(deptId));


            var phyData = objmenu_master.Select(m => new SelectListItem()
            {
                Text = m.m_name,
                Value = m.m_id.ToString(),
            });
            return Json(phyData, JsonRequestBehavior.AllowGet);
        }
        //Action result for ajax call
        //[HttpPost]
        //public ActionResult GetCityByStaeId(int stateid)
        //{
        //    List<City> objcity = new List<City>();
        //    objcity = GetAllCity().Where(m => m.StateId == stateid).ToList();
        //    SelectList obgcity = new SelectList(objcity, "Id", "CityName", 0);
        //    return Json(obgcity);
        //}
        //Action result for ajax call
        [HttpPost]
        public ActionResult GetMenuByRoleId(int RoleId)
        {
            List<menu_master> objmenu_master = new List<menu_master>();
            objmenu_master =  Comman.getAllMenuWhichNotAssignedToTheRole(RoleId);
            SelectList obgcity = new SelectList(objmenu_master, "m_id", "m_name", 0);
            ViewBag.ddlMenu = new SelectList(obgcity, "m_id", "m_name");
            return Json(obgcity);
        }
        [HttpPost]
        public ActionResult Index(string model)
        {
            ViewBag.rowsPerPage = int.Parse(Request.Form["paging"].ToString());
            // return View(new StudentModel().ListStudent());

            return View();
        }

        // GET: ModuleRoleMapping/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionType permissionType = db.PermissionTypes.Find(id);
            
            if (permissionType == null)
            {
                return HttpNotFound();
            }
            return View(permissionType);
        }

        // GET: ModuleRoleMapping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModuleRoleMapping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermissionTypeID,PermissionTypeDesc")] PermissionType permissionType)
        {
            if (ModelState.IsValid)
            {
                db.PermissionTypes.Add(permissionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permissionType);
        }

        // GET: ModuleRoleMapping/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionType permissionType = db.PermissionTypes.Find(id);
            if (permissionType == null)
            {
                return HttpNotFound();
            }
            return View(permissionType);
        }

        // POST: ModuleRoleMapping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermissionTypeID,PermissionTypeDesc")] PermissionType permissionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permissionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permissionType);
        }

        // GET: ModuleRoleMapping/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionType permissionType = db.PermissionTypes.Find(id);
            if (permissionType == null)
            {
                return HttpNotFound();
            }
            return View(permissionType);
        }

        // POST: ModuleRoleMapping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PermissionType permissionType = db.PermissionTypes.Find(id);
            db.PermissionTypes.Remove(permissionType);
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
