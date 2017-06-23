using MyGuide.Domain.Services;
using MyGuide.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyGuide.Controllers
{
    public class PlanController : BaseController
    {
        private const string RESULTKEY = "RESULTKEY";
        private const string CHOSENITEMSKEY = "CHOSENITEMSKEY";
        private readonly INomenclatureService nomenclatureService;
        private readonly IPlanService planService;

        public List<Destination> LastSearchResult
        {
            get
            {
                var lastResult = Session[RESULTKEY] as List<Destination>;

                if (lastResult == null)
                {
                    Session[RESULTKEY] = new List<Destination>();
                    lastResult = Session[RESULTKEY] as List<Destination>;
                }


                return lastResult;
            }

            set
            {
                Session[RESULTKEY] = value;
            }
        }

        public List<Destination> ChosenItems
        {
            get
            {
                var chosenItems = Session[CHOSENITEMSKEY] as List<Destination>;

                if (chosenItems == null)
                {
                    Session[CHOSENITEMSKEY] = new List<Destination>();
                    chosenItems = Session[CHOSENITEMSKEY] as List<Destination>;
                }


                return chosenItems;
            }

            set
            {
                Session[CHOSENITEMSKEY] = value;
            }
        }

        public PlanController(
            INomenclatureService nomenclatureService,
            IPlanService planService)
        {
            this.nomenclatureService = nomenclatureService;
            this.planService = planService;
        }

        // GET: Plan
        public ActionResult Index()
        {
            var settlements = nomenclatureService.GetSettlements();
            ViewBag.Towns = new SelectList(settlements, "Id", "Name", null);

            var categories = nomenclatureService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", null);

            ViewBag.ChosenDestinatins = ChosenItems;

            return View();
        }

        public ActionResult AddItem(long itemId)
        {
            Destination item;

            if (LastSearchResult == null || LastSearchResult.Count == 0 || ChosenItems.Any(i => i.Id == itemId))
            {
                return PartialView("ChosenDestinations", ChosenItems);
            }

            item = LastSearchResult.FirstOrDefault(r => r.Id == itemId);
            if (item != null)
            {
                ChosenItems.Add(item);
            }

            return PartialView("ChosenDestinations", ChosenItems);
        }

        public ActionResult RemoveItem(long itemId)
        {
            var item = ChosenItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                ChosenItems.Remove(item);
            }

            return PartialView("ChosenDestinations", ChosenItems);
        }

        public ActionResult GetDestinationInfo(long itemId)
        {
            var item = ChosenItems.FirstOrDefault(i => i.Id == itemId);

            if(item == null)
            {
                item = LastSearchResult.FirstOrDefault(i => i.Id == itemId);
            }

            if (item != null)
            {
                return PartialView("DestinationInfo", item);
            }

            return Json(null);
        }

        public ActionResult Search(SearchQueryModel model)
        {
            if (ModelState.IsValid)
            {
                LastSearchResult = planService.SearchDestinations(model.PlaceId, model.CategoryId, model.Keywords);


                return PartialView(
                    "SearchResult",
                    LastSearchResult != null && LastSearchResult.Count > 0 ? LastSearchResult : null);
            }

            return new EmptyResult();
        }
    }
}