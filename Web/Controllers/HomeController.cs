using CareerHub.Client.API;
using CareerHub.Client.API.Authorization;
using CareerHub.Client.API.Students;
using DotNetOpenAuth.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers {

	public class HomeController : BaseController {
		
		[Authorize]
		public ActionResult Index() {
			return View();
		}

        public async Task<ActionResult> OAuth(string returnUrl) {
            string baseUrl = CareerHubApiInfo.BaseUrl;
            var api = new AuthorizationApi(baseUrl, "api.demo.public", "abcdefghijklmnop");

			var scopes = new string[] {
				"JobSeeker.Appointments",
				"JobSeeker.Events",
                "JobSeeker.Profile"
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
