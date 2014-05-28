using CareerHub.Client.API.Students.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Students.Controllers {
	public class JobsController : OAuthSecuredController {
         
		public async Task<ActionResult> Index() {
            var factory = await GetFactory();
            var api = factory.GetJobsApi();
            var model = await api.GetJobs();

            return View("List", model);
		}

        public async Task<ActionResult> Search(string text) {
            var factory = await GetFactory();
            var api = factory.GetJobsApi();

            IEnumerable<JobModel> model = null;

            if (String.IsNullOrWhiteSpace(text)) {
                model = await api.GetJobs();
            } else {
                model = await api.SearchJobs(text);
            }

            return View("List", model);
        }

        public async Task<ActionResult> Detail(int id) {
            var factory = await GetFactory();
            var api = factory.GetJobsApi();
            var model = await api.GetJob(id);

            return View(model);
        }
	}
}
