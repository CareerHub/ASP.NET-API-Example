using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Code;

namespace Web.Controllers {
	public class AppointmentsController : BaseController {

		[Authorize]
		public ActionResult Index() {
			var token = Session["token"] as string;
			if (token == null) {
				return RedirectToAction("oauth", "home"); // CASE SENSITIVE!!!!
			}

			var api = new AppointmentsApi(BaseUrl, token);
			var data = api.GetAppointments();
			
			return View(data);
		}
	}
}
