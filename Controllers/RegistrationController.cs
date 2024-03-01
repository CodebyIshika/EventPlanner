using EPBLL;
using EPEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPlanner.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index(int EventID)
        {
            TempData["EventID"] = EventID;
            ViewBag.EventID = EventID;
            RegService rs = new RegService();
            var reg = rs.GetParticipants(EventID);
            return View(reg);
            
        }

        public ActionResult CreateRegView()
        {
            EventService es = new EventService();
            var events = es.GetEvents();

            ViewBag.EventId = new SelectList(events, "EventID", "EventName");

            Registration registration = new Registration();

            return View(registration);
        }

        [HttpPost]
        public ActionResult CreateRegView(Registration registration, int EventID)
        {
            registration.EventID = EventID;

            EventService es = new EventService();
            var events = es.GetEvents();

            TempData["listOfEventsDropdown"] = events.Select(eventItem => new SelectListItem
            {
                Text = eventItem.EventName,
                Value = eventItem.EventID.ToString()
            }).ToList();

            RegService rs = new RegService();

            if (rs.AddRegServices(registration))
            {
                ViewBag.Message = "Your registration is successful";
            }

            return View(registration);
        }



        public ActionResult EditRegView(int EventID, int RegistrationID)
        {
            RegService rs = new RegService();

            var reg = rs.GetParticipants(EventID).Find(x => x.RegistrationID == RegistrationID);

            ViewBag.EventID = EventID;

            return View(reg);
        }

        /// <summary>
        /// This is used to update the record and pass that to Service and Repository
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRegView(Registration reg)
        {
            RegService rs = new RegService();

            if (rs.UpdateRegServices(reg))
            {
                ViewBag.Message = "Participant information is updated successfully";
            }
            else
            {
                ViewBag.Message = "Error updating participant information";
                return View(reg);
            }
            // return View(reg);
            
            return RedirectToAction("Index", new { EventID = reg.EventID });
        }

        public ActionResult DeleteRegistration(int RegistrationID)
        {
            RegService rs = new RegService();

            if (rs.DeleteRegServices(RegistrationID))
            {
                ViewBag.Message = "Registration is deleted successfully";
            }
            return RedirectToAction("Index", new {EventID = TempData["EventID"] });

        }

    }
}