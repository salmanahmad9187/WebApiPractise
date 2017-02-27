using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi1.Filters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string Token = "Token";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ITokenServices)) as ITokenServices;
            if (actionContext.Request.Headers.Contains(Token))
            {
                var tokenValue = actionContext.Request.Headers.GetValues(Token).First();
                if (provider != null && !provider.ValidateToken(tokenValue))
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = "Invalid Request"
                    };
                    actionContext.Response = responseMessage;
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}