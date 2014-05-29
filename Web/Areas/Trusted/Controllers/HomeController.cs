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

namespace Web.Areas.Trusted.Controllers {

    public class HomeController : OAuthSecuredController {
		
		public ActionResult Index() {
			return View();
		}
	}
}
