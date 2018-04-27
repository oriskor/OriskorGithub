using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EDI.Models;

namespace EDI.Controllers
{
    public class TradingPartnerController : Controller
    {
        EDIEntities db = new EDIEntities();


        public PartialViewResult BindGridPartnerTestECIdentifier(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/BindGridPartnerTestECIdentifier.cshtml", TradingPartnerSetup);
        }
        public PartialViewResult BindgridTransactionGroupLelelControlNumbers(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/BindgridTransactionGroupLelelControlNumbers.cshtml", TradingPartnerSetup);
        }
        public PartialViewResult ItemSetup(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/ItemSetup.cshtml", TradingPartnerSetup);
        }
        public PartialViewResult Tamplate(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/Tamplate.cshtml", TradingPartnerSetup);
        }

        public PartialViewResult Transaction(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/Transaction.cshtml", TradingPartnerSetup);
        }

        public PartialViewResult LabelSetup(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/LabelSetup.cshtml", TradingPartnerSetup);
        }

        public PartialViewResult GLAccounts(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/GLAccounts.cshtml", TradingPartnerSetup);
        }
        public PartialViewResult IntegrationSetup(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/IntegrationSetup.cshtml", TradingPartnerSetup);
        }
        public PartialViewResult PartnerSetup(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/PartnerSetup.cshtml", TradingPartnerSetup);
        }
        public PartialViewResult LookupTables(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/LookupTables.cshtml", TradingPartnerSetup);
        }
        public PartialViewResult Address(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/Address.cshtml", TradingPartnerSetup);
        }

        public PartialViewResult TradingPartnerDetail(int id)
        {
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            return PartialView("~/Views/TradingPartnerDetails/TradingPartnerMain.cshtml", TradingPartnerSetup);
        }


        // GET: TradingPartner
        public ActionResult Index()
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
            tradingPartnerSetup TradingPartnerSetup = db.tradingPartnerSetups.Find(id);
            if (TradingPartnerSetup == null)
            {
                return HttpNotFound();
            }
            return View(TradingPartnerSetup);
        }

    }
}