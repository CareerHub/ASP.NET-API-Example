using CareerHub.Client.API.Trusted.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Trusted.Models {
    public class ExperienceSubmission : IExperienceSubmissionModel {

        public string Title { get; set; }
        public string Organisation { get; set; }
        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
    }
}