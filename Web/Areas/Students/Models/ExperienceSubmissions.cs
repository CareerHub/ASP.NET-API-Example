using CareerHub.Client.API.Students.Experiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Students.Models {
    public class ExperienceSubmission : IExperienceSubmissionModel {

        public string Title { get; set; }
        public string Organisation { get; set; }
        public string Description { get; set; }
        
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public int TypeID { get; set; }
        public int? HoursID { get; set; }
    }
}