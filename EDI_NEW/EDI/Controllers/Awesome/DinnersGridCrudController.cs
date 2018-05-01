using System;
using System.Linq;
using System.Web.Mvc;

using EDI.Models;
using EDI.Models.Bussines;
using EDI.Provider;
using EDI.Structure;
using EDI.ViewModels.Input;
//using EDI.ViewModels.Input;

using Omu.AwesomeMvc;

namespace EDI.Controllers
{
    /// <summary>
    /// Note: If your Key Property is not "Id", you need to change the Key = "MyId" in GridGetItems and it in the view
    /// change the Bind = "MyId" (if you're showing the id column), 
    /// and for the action columns you can either specify the MyId property (e.g. GridUtils.EditFormatForGrid("DinnersGrid", "MyId"));
    /// or in MapToGridModel additionally to o.MyId add another property Id = o.MyId
    /// parameters for Edit, Delete need to remain called "id", that's how they are set in GridUtils.cs (params:{{ id: );
    /// Edit and Delete post actions must return an object with property "Id" - in utils.js itemEdited and itemDeleted funcs expect it this way;
    /// </summary>
    public class DinnersGridCrudController : Controller
    {
        private static object MapToGridModel(HeaderDetailInformation o)
        {
            return
                 //new
                 //{
                 //    o.Id,
                 //    o.Name,
                 //    Date = o.Date.ToShortDateString(),
                 //    ChefName = o.Chef.FirstName + " " + o.Chef.LastName,
                 //    Meals = string.Join(", ", o.Meals.Select(m => m.Name))
                 //};
                 new
                 {
                     o.HeaderKey,
                     o.Company,
                     o.TradingPartner,
                     o.DocumentType,
                     o.DocumentNumber,
                     o.AlternateDocument,
                     o.StoreNumber,
                     o.Amount,
                     o.DateRecieved,
                     o.DateAcknowledgement
                 };
        }
        private static object MapToGridModelOutbox(HeaderDetailInformation o)
        {
            return
                
                 new
                 {
                     o.Company,
                     o.TradingPartner,
                     o.DocumentType,
                     o.DocumentNumber,
                     o.AlternateDocument,
                     o.StoreNumber,
                     o.Amount,
                     o.DateChanged,
                 };
        }

        //public ActionResult GridGetItems(GridParams g, string search)
        //{
        //    search = (search ?? "").ToLower();
        //    var items = Db.Dinners.Where(o => o.Name.ToLower().Contains(search)).AsQueryable();

        //    return Json(new GridModelBuilder<Dinner>(items, g)
        //    {
        //        Key = "Id", // needed for api select, update, tree, nesting, EF
        //        GetItem = () => Db.Get<Dinner>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
        //        Map = MapToGridModel
        //    }.Build());
        //}

        //public ActionResult GridGetItems(GridParams g, string search)
        //{
        //    search = (search ?? "").ToLower();
        //    HeaderDetailInformationBussines objHeaderDetailInformationBussines = new HeaderDetailInformationBussines();
        //   // var items = Db.Dinners.Where(o => o.Name.ToLower().Contains(search)).AsQueryable();
        //    var items=objHeaderDetailInformationBussines.GetInboxDetails().AsQueryable();
        //    return Json(new GridModelBuilder<Header_Details_Information>(items, g)
        //    {
        //        Key = "Id", // needed for api select, update, tree, nesting, EF             
        //        GetItem = () => Db.Get<Dinner>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func ) 
        //        GetItem = () => Db.Get<Header_Details_Information>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
        //        Map = MapToGridModel
        //    }.Build());
        //}


        public ActionResult GridGetItems(GridParams g, string search)
        {
            HeaderDetailInformationBussines objHeaderDetailInformationBussines = new HeaderDetailInformationBussines();
            search = (search ?? "").ToLower();
          //  var items1 = Db.Dinners.Where(o => o.Name.ToLower().Contains(search)).AsQueryable();
            var items = objHeaderDetailInformationBussines.GetInboxDetails().AsQueryable();
            return Json(new GridModelBuilder<HeaderDetailInformation>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
               // GetItem = () => Db.Get<HeaderDetailInformation>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel
            }.Build());
        }
        public ActionResult getOutboxItems(GridParams g, string search)
        {
            HeaderDetailInformationBussines objHeaderDetailInformationBussines = new HeaderDetailInformationBussines();
            search = (search ?? "").ToLower();
            var items = objHeaderDetailInformationBussines.getOutboxItems().AsQueryable();
            return Json(new GridModelBuilder<HeaderDetailInformation>(items, g)
            {
                Key = "Id",
                Map = MapToGridModelOutbox
            }.Build());
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(DinnerInput input)
        {
            //if (!ModelState.IsValid) return PartialView(input);

            //var dinner = Db.Insert(new Dinner
            //{
            //    Name = input.Name,
            //    Date = input.Date.Value,
            //    Chef = Db.Get<Chef>(input.Chef),
            //    Meals = Db.Meals.Where(o => input.Meals.Contains(o.Id)),
            //    BonusMeal = Db.Get<Meal>(input.BonusMealId)
            //});

            //return Json(MapToGridModel(dinner)); // returning grid model, used in grid.api.renderRow
            return View();
        }

        public ActionResult Edit(int id)
        {
            var dinner = Db.Get<Dinner>(id);

            var input = new DinnerInput
            {
                Id = dinner.Id,
                Name = dinner.Name,
                Chef = dinner.Chef.Id,
                Date = dinner.Date,
                Meals = dinner.Meals.Select(o => o.Id),
                BonusMealId = dinner.BonusMeal.Id
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(DinnerInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var dinner = Db.Get<Dinner>(input.Id);

            dinner.Name = input.Name;
            dinner.Date = input.Date.Value;
            dinner.Chef = Db.Get<Chef>(input.Chef);
            dinner.Meals = Db.Meals.Where(m => input.Meals.Contains(m.Id));
            dinner.BonusMeal = Db.Get<Meal>(input.BonusMealId);
            Db.Update(dinner);

            // returning the key to call grid.api.update
            return Json(new { Id = dinner.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var dinner = Db.Get<Dinner>(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete dinner <b>{0}</b> ?", dinner.Name)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            Db.Delete<Dinner>(input.Id);
            return Json(new { Id = input.Id });
        }
    }
}