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
using Web.Controllers;
using Web.Models;

namespace Web.Areas.Students.Controllers {

    public class SecureController : BaseController {

        private readonly string baseUrl;
        private readonly AuthorizationApi api;

        public SecureController() {
            baseUrl = CareerHubApiInfo.BaseUrl;
            api = new AuthorizationApi(baseUrl, "api.demo.public", "abcdefghijklmnop");
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

        public ActionResult PublicOAuth(string returnUrl) {
            var scopes = GetPublicScopes();
            var result = api.GetApiClientAccessToken(scopes);

            StoreTokens(result);

            return RedirectToReturnUrl(returnUrl);
        }

        public ActionResult StudentsOAuth(string returnUrl) {
            var scopes = GetStudentScopes();

            // Callback is case sensitive
            var callback = Url.AbsAction("FinishOAuth").ToLower();

			return api.StartOAuth(callback, scopes).AsActionResultMvc5();
		}

        public async Task<ActionResult> FinishOAuth(string returnUrl) {
            var scopes = GetStudentScopes();
            var result = await api.FinishOAuth(Request, scopes);

            StoreTokens(result);

            // issue with FormsAuth SetAuthCookie in async methods
            Response.SetCookie(FormsAuthentication.GetAuthCookie(result.User, false));
            return RedirectToReturnUrl(returnUrl);
        }

        private ActionResult RedirectToReturnUrl(string returnUrl) {

            if (!String.IsNullOrWhiteSpace(returnUrl)) {
                return Redirect(returnUrl);
            } else {
                return Redirect("~/");
            }
        }

        private void StoreTokens(FinishedAuthorizedModel result) {
            Session["accesstoken"] = result.AccessToken;
            Session["refreshtoken"] = result.RefreshToken;
        }

        private static string[] GetPublicScopes() {
            return new string[] {
				"Public.Events",
				"Public.Jobs",
                "Public.News",
                "Public.Resources"
			};
        }

        private static string[] GetStudentScopes() {
            return new string[] {
				"JobSeeker.Appointments",
				"JobSeeker.Appointments",
				"JobSeeker.Events",
				"JobSeeker.Jobs",
				"JobSeeker.News",
                "JobSeeker.Profile",
				"JobSeeker.Questions",
				"JobSeeker.Resources"
			};;
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
