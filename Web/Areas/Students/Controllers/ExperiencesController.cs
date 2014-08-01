using CareerHub.Client.API;
using CareerHub.Client.API.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Students.Models;
using Web.Models;

namespace Web.Areas.Students.Controllers {
    public class ExperiencesController : OAuthSecuredController {

        public async Task<ActionResult> Index() {
            var factory = await GetFactory();

            var api = factory.GetExperiencesApi();
			var model = await api.GetExperiences();
            
            return View(model);
		}

        public async Task<ActionResult> Detail(int id) {
            var factory = await GetFactory();

            var api = factory.GetExperiencesApi();
            var model = await api.GetExperience(id);
            
            return View(model);
        }

        public async Task<ActionResult> Edit(int id) {
            var factory = await GetFactory();

            var typesApi = factory.GetExperienceTypesApi();
            var api = factory.GetExperiencesApi();

            var types = await typesApi.GetExperienceTypes();
            var experience = await api.GetExperience(id);

            var model = new ExperienceViewModel {
                Title = experience.Title,
                Organisation = experience.Organisation,
                Description = experience.Description,
                Start = experience.Start,
                End = experience.End,
                ContactName = experience.ContactName,
                ContactEmail = experience.ContactEmail,
                ContactPhone = experience.ContactPhone,
                //TypeID = experience.TypeID,
                Types = types.Select(t => new SelectListItem {
                    Text = t.Name,
                    Value = t.ID.ToString()
                })
            };

            return View(model);
        }

        public async Task<ActionResult> Add() {
            var factory = await GetFactory();
            var api = factory.GetExperienceTypesApi();
            var types = await api.GetExperienceTypes();

            var model = new ExperienceViewModel() {
                Types = types.Select(t => new SelectListItem {
                    Text = t.Name,
                    Value = t.ID.ToString()
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ExperienceSubmission submission) {
            var factory = await GetFactory();

            var api = factory.GetExperiencesApi();
            var model = await api.UpdateExperience(id, submission);

            return RedirectToAction("detail", new { id = model.ID });
        }

        [HttpPost]
        public async Task<ActionResult> Add(ExperienceSubmission submission) {
            var factory = await GetFactory();

            var api = factory.GetExperiencesApi();
            var model = await api.CreateExperience(submission);

            return RedirectToAction("detail", new { id = model.ID });
        }
	}
}
