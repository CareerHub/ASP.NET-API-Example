using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Students.Models {
    public class ExperienceViewModel {

        public string Title { get; set; }
        public string Organisation { get; set; }
        public string Description { get; set; }
        
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public int? TypeID { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
    }
}