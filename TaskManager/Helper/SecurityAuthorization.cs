using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Helper
{
    public class SecurityAuthorization : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
                {
                    filterContext.HttpContext.Response.Redirect("task");
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("authorization/login"); 
            }
        }
    }
}