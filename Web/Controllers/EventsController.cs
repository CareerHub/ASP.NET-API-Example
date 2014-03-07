using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Code;

namespace Web.Controllers {
	public class EventsController : OAuthController {

        private EventsApi eventsApi = null;
        private EventsApi EventsApi { 
            get {
                if (eventsApi == null) eventsApi = new EventsApi(BaseUrl, this.Token);
                return eventsApi; 
            }
        }

        private EventBookingsApi bookingsApi = null;
        private EventBookingsApi BookingsApi {
            get {
                if (bookingsApi == null) bookingsApi = new EventBookingsApi(BaseUrl, this.Token);
                return bookingsApi;
            }
        }
 
		public async Task<ActionResult> Index() {
            var result = await this.EventsApi.GetEvents();

            if (!result.Success) {
                return View("Error", result);
            }

            return View("List", result.Content);
		}

        public async Task<ActionResult> Search(string text) {
            var result = await this.EventsApi.SearchEvents(text);

            if (!result.Success) {
                return View("Error", result);
            }

            return View("List", result.Content);
        }

        public async Task<ActionResult> Detail(int id) {
            var result = await this.EventsApi.GetEvent(id);

            if (!result.Success) {
                return View("Error", result);
            }

            return View(result.Content);
        }

        public async Task<ActionResult> Bookings() {
            var result = await this.BookingsApi.GetUpcomingEvents();

            if (!result.Success) {
                return View("Error", result);
            }

            return View("List", result.Content);
        }

        [HttpPost]
        public async Task<ActionResult> Book(int id) {
            var result = await this.BookingsApi.BookEvent(id);

            if (!result.Success) {
                return View("Error", result);
            }

            return RedirectToAction("bookings");
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(int id) {
            var result = await this.BookingsApi.CancelBooking(id);

            if (!result.Success) {
                return View("Error", result);
            }

            return RedirectToAction("bookings");
        }
	}
}
