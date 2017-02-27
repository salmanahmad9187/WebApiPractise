using AttributeRouting.Web.Http;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WebApi1.Filters;

namespace WebApi1.Controllers
{
    [ApiAuthenticationFilter]
    public class AuthenticateController : ApiController
    {
        private readonly ITokenServices _tokenServices;

        public AuthenticateController(ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
        }
        [POST("login")]
        [POST("authenticate")]
        [POST("get/token")]
        public HttpResponseMessage Authenticate()
        {
            if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;
                    return GetAuthToken(userId);
                }
            }
            return null;
        }

        private HttpResponseMessage GetAuthToken(int userId)
        {
            var token = _tokenServices.GenerateToken(userId);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            return response;
        }
    }
}
