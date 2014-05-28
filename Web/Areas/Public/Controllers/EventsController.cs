using CareerHub.Client.API.Public.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Public.Controllers {
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
	}
}
