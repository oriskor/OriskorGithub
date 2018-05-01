using EDI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private EDIEntities db = new EDIEntities();

        //[ChildActionOnly]
        //public PartialViewResult GetMenu()
        //{
        //    List<trading> menuStructuredModel = new List<trading>();
        //    if (menuStructuredModel != null)
        //    {

        //        menuStructuredModel = db.menu_master.Select(x => new trading { HeaderKey = x.m_id }).ToList();
        //    }
        //    return PartialView("~/Views/Shared/_MasterPage.cshtml", menuStructuredModel);
        //}
        public PartialViewResult GetMenu()
        {
            Menu objMenu = new Menu();
            objMenu.MenuStructuredModel  = db.tradingPartnerSetups.ToList();

            return PartialView("~/Views/Shared/_SideMenu.cshtml", db.tradingPartnerSetups.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}