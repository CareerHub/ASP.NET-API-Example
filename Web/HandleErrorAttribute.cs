using CareerHub.Client.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web {
    public class HandleErrorAttribute : FilterAttribute, IExceptionFilter{

        public void OnException(ExceptionContext filterContext) {
            if (filterContext == null) throw new ArgumentNullException("filterContext");
            
            if (filterContext.IsChildAction || filterContext.ExceptionHandled) {
                return;
            }

            if (filterContext.Exception is CareerHubApiHttpException) {
                var err = filterContext.Exception as CareerHubApiHttpException;

                if (err.Status == HttpStatusCode.Forbidden) {
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                    filterContext.Result = new RedirectResult("~/");
                    return;
                }

            }
        }
    }
}