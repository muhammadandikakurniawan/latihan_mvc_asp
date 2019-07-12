using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Threading;
using System.Security.Principal;
using System.Net.Http;
using Api.Models;
using System.Text;

namespace Api.Filters
{
    public class AuthenticationFilter : AuthorizationFilterAttribute //
    {
        public override void  OnAuthorization(HttpActionContext actionContext)
        {
            string token = this.GetToken(actionContext);


            if (token != null)
            {
                string encode = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                string[] creds = encode.Split(':');
                string username = creds[0];
                string password = creds[1];

                HR_Entities hr = new HR_Entities();

                COPY_EMP emp = hr.COPY_EMP.Where(e => e.FIRST_NAME == username).FirstOrDefault();
                if (emp != null && password == emp.LAST_NAME)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }

            //if(token != null)
            //{
            //    string[] creds = token.Split(':');
            //    string username = creds[0];
            //    string password = creds[1];
            //    if (password.Equals("123456"))
            //    {
            //        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
            //    }
            //    else
            //    {
            //        actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            //    }
            //}

            //if(token == "123456")
            //{
            //    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("user"), null);
            //}
            //else
            //{
            //    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            //}
            base.OnAuthorization(actionContext);
        }

        private string GetToken(HttpActionContext actionContext)
        {
            string token = null;
            var authRequest = actionContext.Request.Headers.Authorization;
            if(authRequest != null && !String.IsNullOrEmpty(authRequest.Parameter) && authRequest.Scheme == "Basic")
            {
                token = authRequest.Parameter;
            }

            return token;
        }
    }
}