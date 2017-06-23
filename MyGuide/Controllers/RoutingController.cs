using MyGuide.Domain.Services;
using MyGuide.Models;
using Resources.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyGuide.Controllers
{
    public class RoutingController : BaseController
    {
        private readonly IRoutingService routingService;
        private readonly ITspService tspService;
        private const string CHOSENITEMSKEY = "CHOSENITEMSKEY";
        private const string LASTOLKEY = "LASTOLKEY";
        private const string ROUTESKEY = "ROUTESKEY";
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

        public List<Destination> LastOrderedList
        {
            get
            {
                var lastOrderedList = Session[LASTOLKEY] as List<Destination>;

                if (lastOrderedList == null)
                {
                    Session[LASTOLKEY] = new List<Destination>();
                    lastOrderedList = Session[LASTOLKEY] as List<Destination>;
                }


                return lastOrderedList;
            }

            set
            {
                Session[LASTOLKEY] = value;
            }
        }

        public List<Route> UserRoutes
        {
            get
            {
                var routes = Session[ROUTESKEY] as List<Route>;

                if (routes == null)
                {
                    Session[ROUTESKEY] = new List<Route>();
                    routes = Session[ROUTESKEY] as List<Route>;
                }


                return routes;
            }

            set
            {
                Session[ROUTESKEY] = value;
            }
        }

        public RoutingController(IRoutingService routingService, ITspService tspService)
        {
            this.routingService = routingService;
            this.tspService = tspService;
        }

        // GET: Routing
        public ActionResult Index(long townId)
        {
            var homeTown = routingService.GetHomeCoordinates(townId);

            // insert the home town for calculations:
            ChosenItems.Insert(0, homeTown);
            
            LastOrderedList = tspService.SolveTsp(ChosenItems);
            
            // Remove hometown after calculations are done:
            ChosenItems.Remove(homeTown);
            return View(LastOrderedList);
        }

        public ActionResult RemoveItem(long itemId)
        {
            var item = ChosenItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                ChosenItems.Remove(item);
            }

            return PartialView("~/Views/Plan/ChosenDestinations.cshtml", ChosenItems);
        }

        public ActionResult GetDestinationInfo(long itemId)
        {
            var item = ChosenItems.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                UserRoutes.ForEach(r =>
                {
                    item = r.Destinations?.FirstOrDefault(d => d.Id == itemId) ?? item;
                });
            }

            if (item != null)
            {
                return PartialView("~/Views/Plan/DestinationInfo.cshtml", item);
            }

            return Json(null);
        }

        public ActionResult Preview()
        {
            if (User == null)
            {
                return new RedirectResult("~/Account/Login?returnUrl=~/Routing/Preview", false);
            }

            UserRoutes = routingService.GetRoutes(User.Id);

            ViewBag.Dates = UserRoutes.Select(r => string.Format("{0:F}", r.Date)).ToList();
            //ViewBag.Dates = new List<string>
            //{
            //    "12.06.2017 13:45",
            //    "18.08.2016 16:26",
            //    "22.03.2017 11:17",
            //    "01.11.2016 19:50"
            //};

            return View();
        }

        public ActionResult Save()
        {
            if(User == null)
            {
                return new RedirectResult("~/Account/Login?returnUrl=~/Routing/Save", false);
            }
            
            var route = new Route
            {
                Date = DateTime.Now,
                UserId = User.Id,
                HomeId = LastOrderedList[0].Id,
                Destinations = LastOrderedList
            };

            // Remove hometown before saving:
            LastOrderedList.RemoveAt(0);

            routingService.SaveRoute(route);

            // Null collections after save:
            LastOrderedList = new List<Destination>();
            
            return new RedirectResult("~/", true);
        }

        public ActionResult ShowRoute(string date)
        {
            if(date == null)
            {
                return new EmptyResult();
            }

            var route = UserRoutes.FirstOrDefault(r => string.Format("{0:F}", r.Date) == date);

            route.Destinations = routingService.GetDestinationsByRouteId(route.Id);

            var homeTown = routingService.GetHomeCoordinates(route.HomeId);

            route.Destinations.Insert(0, homeTown);

            return View(route.Destinations);
        }
    }
}