using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace SQEstimationAndBillingSystem.FilterAttributes
{
    public class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session != null)
            {
                if (context.Session.IsNewSession)
                {
                    string sessionCookie = context.Request.Headers["Cookie"];

                    if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        FormsAuthentication.SignOut();
                        string redirectTo = "~/Login/Index";
                        if (!string.IsNullOrEmpty(context.Request.RawUrl))
                        {
                            //redirectTo = string.Format("~/Login/Index?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                            //redirectTo = string.Format("~/Login/Index?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                            //filterContext.Result = new RedirectResult(redirectTo);

                            // filterContext.Result =new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Login" },{ "action", "Index" }});

                            context.Response.Redirect("~/Login/Index");
                        }

                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}