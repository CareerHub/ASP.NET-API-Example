﻿using CareerHub.Client.API;
using CareerHub.Client.API.Students;
using CareerHub.Client.API.Students.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Students.Controllers {
	public class EventsController : OAuthSecuredController {
         
		public async Task<ActionResult> Index() {
            var factory = await GetFactory();
            var api = factory.GetEventsApi();
            var model = await api.GetEvents();

            return View("List", model);
		}

        public async Task<ActionResult> Search(string text) {
            var factory = await GetFactory();
            var api = factory.GetEventsApi();

            IEnumerable<EventModel> model = null;

            if (String.IsNullOrWhiteSpace(text)) {
                model = await api.GetEvents();
            } else {
                model = await api.SearchEvents(text);
            }

            return View("List", model);
        }

        public async Task<ActionResult> Detail(int id) {
            var factory = await GetFactory();
            var api = factory.GetEventsApi();
            var model = await api.GetEvent(id);

            return View(model);
        }

        public async Task<ActionResult> Bookings() {
            var factory = await GetFactory();
            var api = factory.GetEventBookingsApi();
            var model = await api.GetUpcomingEvents();

            return View("List", model);
        }

        [HttpPost]
        public async Task<ActionResult> Book(int id) {
            var factory = await GetFactory();
            var api = factory.GetEventBookingsApi();

            var model = await api.BookEvent(id);

            return RedirectToAction("bookings");
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(int id) {
            var factory = await GetFactory();
            var api = factory.GetEventBookingsApi();

            await api.CancelBooking(id);

            return RedirectToAction("bookings");
        }
	}
}
