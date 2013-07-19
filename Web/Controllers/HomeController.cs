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
using System.Configuration;

namespace Web.Controllers {

	public class HomeController : BaseController {
		
		[Authorize]
		public ActionResult Index() {
			return View();
		}

		public ActionResult OAuth(string returnUrl) {
			var api = new AuthorizationApi(BaseUrl, "api.demo.public", "abcdefghijklmnop");

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
		
		private static string RemoveQueryStringFromUri(string uri) {
			int index = uri.IndexOf('?');
			if (index > -1) {
				uri = uri.Substring(0, index);
			}
			return uri;
		}
	}
}
