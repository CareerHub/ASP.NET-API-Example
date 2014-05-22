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

    public class SecureController : BaseController {

        private readonly string baseUrl;
        private readonly AuthorizationApi api;
        private readonly string[] scopes;

        public SecureController() {
            baseUrl = CareerHubApiInfo.BaseUrl;
            api = new AuthorizationApi(baseUrl, "api.demo.public", "abcdefghijklmnop");
            scopes = new string[] {
				"JobSeeker.Appointments",
				"JobSeeker.Events",
                "JobSeeker.Profile"
			};
        }

        public ActionResult RefreshToken() {
            string refreshToken = this.Session["refreshtoken"] as string;
            if(String.IsNullOrWhiteSpace(refreshToken)) {
                return RedirectToAction("oauth");
            }

            var result = api.GetRefreshedTokens(refreshToken);

            StoreTokens(result);

            return RedirectToAction("index", "home");
        }


        public async Task<ActionResult> OAuth(string returnUrl) {		

			if (string.IsNullOrEmpty(Request.QueryString["code"])) {
				var callback = Request.Url.AbsoluteUri;
                callback = RemoveQueryStringFromUri(callback);

				return api.StartOAuth(callback, scopes).AsActionResultMvc5();
			} else {
				var result = await api.FinishOAuth(Request, scopes);

                StoreTokens(result);

                // issue with FormsAuth SetAuthCookie in async methods
                Response.SetCookie(FormsAuthentication.GetAuthCookie(result.User, false));

				if (!String.IsNullOrWhiteSpace(returnUrl)) {
					return Redirect(returnUrl);
				} else {
					return Redirect("~/");
				}
			}
		}

        private void StoreTokens(FinishedAuthorizedModel result) {
            Session["accesstoken"] = result.AccessToken;
            Session["refreshtoken"] = result.RefreshToken;
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
