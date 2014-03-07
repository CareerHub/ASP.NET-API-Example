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
using System.Threading.Tasks;

namespace Web.Controllers {

	public class HomeController : BaseController {
		
		[Authorize]
		public ActionResult Index() {
			return View();
		}

		public async Task<ActionResult> OAuth(string returnUrl) {
			var api = new AuthorizationApi(BaseUrl, "api.demo.public", "abcdefghijklmnop");

			var scopes = new string[] {
				"Students.JobSeeker",
				"Students.Appointments",
				"Students.Events"
			};

			if (string.IsNullOrEmpty(Request.QueryString["code"])) {
				var callback = Request.Url.AbsoluteUri;
                callback = RemoveQueryStringFromUri(callback);

				return api.StartOAuth(callback, scopes).AsActionResultMvc5();
			} else {
				var result = await api.FinishOAuth(Request, scopes);
				
				Session["token"] = result.AccessToken;

                // issue with FormsAuth SetAuthCookie in async methods
                Response.SetCookie(FormsAuthentication.GetAuthCookie(result.User, false));

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
