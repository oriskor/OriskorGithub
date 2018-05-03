using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDI.Models;

namespace EDI.Controllers
{
    public class TransactionInboxController : Controller
    {
        // GET: TransactionInbox
        public ActionResult Index()
        {
            return View();
        }
    }
}