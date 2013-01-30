using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.Messaging;
using Web.Code;
using DotNetOpenAuth.OAuth.ChannelElements;

namespace Web.Controllers {

	public class HomeController : Controller {
		private const string BaseUrl = "http://students.careerhub.com.au/";

		public ActionResult Index() {
			return Content("Hello World");
		}

		public ActionResult OAuth(string returnUrl) {
			var api = new AuthorizationApi(BaseUrl, "api.demo", "abcdefghijklmnop");

			var scopes = new string[] {
				"Students.JobSeeker",
				"Students.Appointments"
			};

			if (string.IsNullOrEmpty(Request.QueryString["code"])) {
				var callback = Request.Url.AbsoluteUri;
                callback = RemoveQueryStringFromUri(callback);

				return api.StartOAuth(callback, scopes).AsActionResult();
			} else {
				var result = api.FinishOAuth(Request, scopes);
				
				Session["token"] = result.AccessToken;
				
				FormsAuthentication.SetAuthCookie(result.User, false);

				if (!String.IsNullOrWhiteSpace(returnUrl)) {
					return Redirect(returnUrl);
				} else {
					return Redirect("~/");
				}
			}
		}

		[Authorize]
		public ActionResult Appointments() {
			var token = Session["token"] as string;
			if (token == null) {
				return RedirectToAction("oauth"); // CASE SENSITIVE!!!!
			}

			var api = new AppointmentsApi(BaseUrl, token);
			var data = api.GetAppointments();

			return Json(data, JsonRequestBehavior.AllowGet);
		}

		private static string RemoveQueryStringFromUri(string uri) {
			int index = uri.IndexOf('?');
			if (index > -1) {
				uri = uri.Substring(0, index);
			}
			return uri;
		}
	}
}
