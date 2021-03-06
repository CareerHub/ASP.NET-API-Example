﻿using CareerHub.Client.API;
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
		
		public ActionResult Index() {
			return View();
		}
	}
}
