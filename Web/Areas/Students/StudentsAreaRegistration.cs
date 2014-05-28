using System.Web.Mvc;

namespace Web.Areas.Students
{
    public class StudentsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Students";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Students_default",
                "students/{controller}/{action}/{id}",
                new { controller = "home", action = "index", id = UrlParameter.Optional },
                new string[] { "Web.Areas.Students.Controllers" }
            );
        }
    }
}