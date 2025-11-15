using PetrolPumpApp.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PetrolPumpApp.Filters
{
    public class JwtAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if Authorization header exists
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new { message = "Missing authorization header" });
                return;
            }

            // Get the token from Authorization header
            string token = actionContext.Request.Headers.Authorization.Parameter;

            if (string.IsNullOrEmpty(token))
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new { message = "Missing token" });
                return;
            }

            // Validate the token
            var principal = JwtTokenHelper.ValidateToken(token);

            if (principal == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new { message = "Invalid or expired token" });
                return;
            }

            // Set the current principal
            System.Threading.Thread.CurrentPrincipal = principal;
            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext.Current.User = principal;
            }
        }
    }
}
