using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using EDI.Models;

using Omu.AwesomeMvc;

namespace EDI.Controllers.Awesome
{
    public class DataController : Controller
    {
        public ActionResult GetAllMeals()
        {
            var items = Db.Meals.Select(o => new KeyContent(o.Id, o.Name));
            return Json(items);
        }
    }
}