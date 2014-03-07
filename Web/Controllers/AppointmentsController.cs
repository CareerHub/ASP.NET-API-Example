using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Code;

namespace Web.Controllers {
	public class AppointmentsController : OAuthController {

		public async Task<ActionResult> Index() {
            var api = new AppointmentBookingsApi(BaseUrl, this.Token);
			var result = await api.GetUpcomingAppointments();

            if (!result.Success) {
                return View("Error", result);
            }

			return View(result.Content);
		}
	}
}
