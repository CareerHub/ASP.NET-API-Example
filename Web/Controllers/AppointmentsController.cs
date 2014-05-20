using CareerHub.Client.API;
using CareerHub.Client.API.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers {
	public class AppointmentsController : OAuthController {

		public async Task<ActionResult> Index() {
            var info = await CareerHubApiInfo.GetStudentsInfo();
            var factory = new StudentsApiFactory(info, this.Token);

            var api = factory.GetAppointmentBookingsApi();
			var result = await api.GetUpcomingAppointments();

            if (!result.Success) {
                return View("Error", result);
            }

			return View(result.Content);
		}
	}
}
