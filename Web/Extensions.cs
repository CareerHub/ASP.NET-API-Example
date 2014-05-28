using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web {
    public static class Extensions {

        public static string AbsAction(this UrlHelper url, string action) {
            var rightPart = url.Action(action);
            var leftPart = url.RequestContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
            return leftPart + rightPart;
        }
    }
}