using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PetBuddy.Web.Controllers
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var role = httpContext.Session["Role"];

            if (role == null)
                return false;

            return role.ToString()
                       .Trim()
                       .Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result =
                new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" }
                    });
        }
    }
}
