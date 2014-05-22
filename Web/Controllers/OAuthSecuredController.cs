using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Controllers {
    [Authorize]
    public class OAuthSecuredController : BaseController {

        protected string AccessToken { get; private set; }
        protected string RefreshToken { get; private set; }

        protected override void OnAuthorization(AuthorizationContext filterContext) {

            // This is just a easy way to do this.
            // Would be better storing in a cookie
            this.AccessToken = filterContext.HttpContext.Session["accesstoken"] as string;
            this.RefreshToken = filterContext.HttpContext.Session["refreshtoken"] as string;

            if (String.IsNullOrWhiteSpace(this.AccessToken)) {
                var routeValues = new RouteValueDictionary();
                routeValues.Add("controller", "secure");
                routeValues.Add("action", "oauth");

                filterContext.Result = new RedirectToRouteResult("Default", routeValues);
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}