using CareerHub.Client.API;
using CareerHub.Client.API.Trusted;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Trusted.Models;
using Web.Models;

namespace Web.Areas.Trusted.Controllers {
    public class ExperiencesController : OAuthSecuredController {

        public ActionResult Index() {
            return View();
        }

        public async Task<ActionResult> List(string studentid) {
            ViewBag.StudentID = studentid;

            var factory = await GetFactory();

            var api = factory.GetExperiencesApi();
			var model = await api.GetExperiences(studentid);
            
            return View(model);
		}

        public async Task<ActionResult> Detail(string studentid, int id) {
            ViewBag.StudentID = studentid;

            var factory = await GetFactory();

            var api = factory.GetExperiencesApi();
            var model = await api.GetExperience(studentid, id);
            
            return View(model);
        }

        public async Task<ActionResult> Edit(string studentid, int id) {
            ViewBag.StudentID = studentid;

            var factory = await GetFactory();

            var typesApi = factory.GetExperienceTypesApi();
            var api = factory.GetExperiencesApi();

            var types = await typesApi.GetExperienceTypes();
            var experience = await api.GetExperience(studentid, id);

            var model = new ExperienceViewModel {
                Title = experience.Title,
                Organisation = experience.Organisation,
                Description = experience.Description,
                Start = experience.Start,
                End = experience.End,
                ContactName = experience.ContactName,
                ContactEmail = experience.ContactEmail,
                ContactPhone = experience.ContactPhone,

                TypeID = experience.TypeID,
                Types = types.Select(t => new SelectListItem {
                    Text = t.Name,
                    Value = t.ID.ToString(),
                    Selected = t.ID == experience.TypeID
                })
            };

            return View(model);
        }

        public async Task<ActionResult> Add(string studentid) {
            ViewBag.StudentID = studentid;

            var factory = await GetFactory();
            var api = factory.GetExperienceTypesApi();
            var types = await api.GetExperienceTypes();

            var model = new ExperienceViewModel {                
                Types = types.Select(t => new SelectListItem {
                    Text = t.Name,
                    Value = t.ID.ToString()
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string studentid, int id, ExperienceSubmission submission) {
            ViewBag.StudentID = studentid;

            var factory = await GetFactory();

            var api = factory.GetExperiencesApi();
            var model = await api.UpdateExperience(studentid, id, submission);

            return RedirectToAction("detail", new { id = model.ID, studentid = studentid });
        }


        [HttpPost]
        public async Task<ActionResult> Add(string studentid, ExperienceSubmission submission) {
            ViewBag.StudentID = studentid;

            var factory = await GetFactory();

            var api = factory.GetExperiencesApi();
            var model = await api.CreateExperience(studentid, submission);

            return RedirectToAction("detail", new { id = model.ID, studentid = studentid });
        }
	}
}
