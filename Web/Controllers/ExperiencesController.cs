using CareerHub.Client.API;
using CareerHub.Client.API.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers {
    public class ExperiencesController : OAuthController {

		public async Task<ActionResult> Index() {
            var info = await CareerHubApiInfo.GetStudentsInfo();
            var factory = new StudentsApiFactory(info, this.Token);

            var api = factory.GetExperiencesApi();
			var model = await api.GetExperiences();
            
            return View(model);
		}
	}
}
