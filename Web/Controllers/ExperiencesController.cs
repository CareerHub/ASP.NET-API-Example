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

        public async Task<ActionResult> Detail(int id) {
            var info = await CareerHubApiInfo.GetStudentsInfo();
            var factory = new StudentsApiFactory(info, this.Token);

            var api = factory.GetExperiencesApi();
            var model = await api.GetExperience(id);
            
            return View(model);
        }

        public async Task<ActionResult> Edit(int id) {
            var info = await CareerHubApiInfo.GetStudentsInfo();
            var factory = new StudentsApiFactory(info, this.Token);

            var api = factory.GetExperiencesApi();
            var experience = await api.GetExperience(id);
            var model = new ExperienceSubmission {
                Title = experience.Title,
                Organisation = experience.Organisation,
                Description = experience.Description,
                StartDate = experience.Start,
                EndDate = experience.End,
                ContactName = experience.ContactName,
                ContactEmail = experience.ContactEmail,
                ContactPhone = experience.ContactPhone
            };

            return View(model);
        }

        public ActionResult Add() {
            return View(new ExperienceSubmission());
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ExperienceSubmission submission) {
            var info = await CareerHubApiInfo.GetStudentsInfo();
            var factory = new StudentsApiFactory(info, this.Token);

            var api = factory.GetExperiencesApi();
            var model = await api.UpdateExperience(id, submission);

            return RedirectToAction("detail", new { id = model.ID });
        }


        [HttpPost]
        public async Task<ActionResult> Add(ExperienceSubmission submission) {
            var info = await CareerHubApiInfo.GetStudentsInfo();
            var factory = new StudentsApiFactory(info, this.Token);

            var api = factory.GetExperiencesApi();
            var model = await api.CreateExperience(submission);

            return RedirectToAction("detail", new { id = model.ID });
        }
	}
}
