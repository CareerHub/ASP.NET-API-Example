using CareerHub.Client.API;
using CareerHub.Client.API.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Students.Controllers {
	public class AppointmentsController : OAuthSecuredController {

        public async Task<ActionResult> Index() {
            var factory = await GetFactory();

            var api = factory.GetAppointmentBookingsApi();
			var model = await api.GetUpcomingAppointments();

            return View(model);
		}
	}
}
