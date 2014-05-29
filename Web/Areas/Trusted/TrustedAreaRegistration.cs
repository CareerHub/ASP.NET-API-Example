using System.Web.Mvc;

namespace Web.Areas.Trusted
{
    public class TrustedAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Trusted";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Trusted_default",
                "trusted/{controller}/{action}/{id}",
                new { controller = "home", action = "index", id = UrlParameter.Optional },
                new string[] { "Web.Areas.Trusted.Controllers" }
            );
        }
    }
}