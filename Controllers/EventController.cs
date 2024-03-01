using EPBLL;
using EPEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            EventService es = new EventService();
            var events = es.GetEvents();

            return View(events);
        }

        public ActionResult CreateEventView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEventView(Events events)
        {
            EventService es = new EventService();

            if (es.AddEventServices(events))
            {
                ViewBag.Message = "Event is added successfully";
            }
            return View();
        }

        /// <summary>
        /// EditEventView is used to return view with records based on EventID
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public ActionResult EditEventView(int EventID)
        {
            EventService es = new EventService();

            var events = es.GetEvents().Find(x => x.EventID == EventID);


            return View(events);

        }

        /// <summary>
        /// This is used to update the record and pass that to Service and Repository
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditEventView(Events events)
        {
            EventService es = new EventService();

            if (es.UpdateEventServices(events))
            {
                ViewBag.Message = "Event is updated successfully";
            }
            return View();
        }

        public ActionResult DeleteEvent(int EventID)
        {
            EventService es = new EventService();

            if (es.DeleteEventServices(EventID))
            {
                ViewBag.Message = "Event is deleted successfully";
            }
            return RedirectToAction("Index");

        }
    }
}