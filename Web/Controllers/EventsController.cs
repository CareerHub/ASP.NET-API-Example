using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Code;

namespace Web.Controllers {
	public class EventsController : BaseController {
		[Authorize]
		public ActionResult Index() {
			var token = Session["token"] as string;
			if (token == null) {
				return RedirectToAction("oauth", "home"); // CASE SENSITIVE!!!!
			}

			var api = new EventsApi(BaseUrl, token);
			var data = api.GetEvents();

			return View(data);
		}
	}
}
