using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Controllers {
    [Authorize]
    public class OAuthController : BaseController {

        protected string Token { get; private set; }

        protected override void OnAuthorization(AuthorizationContext filterContext) {

            // This is just a easy way to do this.
            // Would be better storing in a cookie
            this.Token = filterContext.HttpContext.Session["token"] as string;
            if (String.IsNullOrWhiteSpace(this.Token)) {
                filterContext.Result = new RedirectToRouteResult("OAuth", new RouteValueDictionary());
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}