using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using OrangeApartments.Core.Domain;
using OrangeApartments.Helpers;
using OrangeApartments.Core.Domain;

namespace OrangeApartments.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public string Role { get; set; }


        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            Role role = Core.Domain.Role.User;
            if (!string.IsNullOrEmpty(Role))
            {
                role = (Role)Enum.Parse(typeof(Role), Role, true);
            }

            var isTokenHeaderExist = actionContext.Request.Headers.Contains("Token");

            if (!isTokenHeaderExist)
            {
                var response = new HttpResponseMessage
                {
                    Content =
                        new StringContent("Header \"Token\" is required!"),
                    StatusCode = HttpStatusCode.BadRequest
                };
                actionContext.Response = response;
                return;
            }

            var accessTokenValues = actionContext.Request.Headers.GetValues("Token");


            if (accessTokenValues != null)
            {
                string tokenValue = accessTokenValues.FirstOrDefault();
                bool validToken = false;
                if (!string.IsNullOrEmpty(tokenValue))
                    validToken = SessionHelper.GetSession(tokenValue) > 0;

                if (!validToken)
                {
                    var response = new HttpResponseMessage
                    {
                        Content =
                            new StringContent("This token is not valid, please refresh token or obtain valid token!"),
                        StatusCode = HttpStatusCode.Unauthorized
                    };
                    actionContext.Response = response;
                    return;
                }
            }
            else
            {
                var response = new HttpResponseMessage
                {
                    Content =
                        new StringContent("You must supply valid token to access method!"),
                    StatusCode = HttpStatusCode.Unauthorized
                };
                actionContext.Response = response;
                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
