using System.Web.Mvc;

namespace Web.Areas.Public
{
    public class PublicAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Public";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Public_default",
                "public/{controller}/{action}/{id}",
                new { controller = "home", action = "index", id = UrlParameter.Optional },
                new string[] { "Web.Areas.Public.Controllers" }
            );
        }
    }
}