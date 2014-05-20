using CareerHub.Client.API;
using CareerHub.Client.API.Students;
using CareerHub.Client.API.Students.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers {
	public class EventsController : OAuthController {
         


		public async Task<ActionResult> Index() {
            var factory = await GetFactory();
            var api = factory.GetEventsApi();

            var result = await api.GetEvents();

            if (!result.Success) {
                return View("Error", result);
            }

            return View("List", result.Content);
		}

        public async Task<ActionResult> Search(string text) {
            var factory = await GetFactory();
            var api = factory.GetEventsApi();

            var result = await api.SearchEvents(text);

            if (!result.Success) {
                return View("Error", result);
            }

            return View("List", result.Content);
        }

        public async Task<ActionResult> Detail(int id) {
            var factory = await GetFactory();
            var api = factory.GetEventsApi();

            var result = await api.GetEvent(id);

            if (!result.Success) {
                return View("Error", result);
            }

            return View(result.Content);
        }

        public async Task<ActionResult> Bookings() {
            var factory = await GetFactory();
            var api = factory.GetEventBookingsApi();

            var result = await api.GetUpcomingEvents();

            if (!result.Success) {
                return View("Error", result);
            }

            return View("List", result.Content);
        }

        [HttpPost]
        public async Task<ActionResult> Book(int id) {
            var factory = await GetFactory();
            var api = factory.GetEventBookingsApi();

            var result = await api.BookEvent(id);

            if (!result.Success) {
                return View("Error", result);
            }

            return RedirectToAction("bookings");
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(int id) {
            var factory = await GetFactory();
            var api = factory.GetEventBookingsApi();

            var result = await api.CancelBooking(id);

            if (!result.Success) {
                return View("Error", result);
            }

            return RedirectToAction("bookings");
        }

        private async Task<StudentsApiFactory> GetFactory() {
            var info = await CareerHubApiInfo.GetStudentsInfo();
            var factory = new StudentsApiFactory(info, this.Token);

            return factory;
        }
	}
}
