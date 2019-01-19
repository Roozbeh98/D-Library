using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D_Library.Models.UserManagement
{
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {


        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    if (!httpContext.User.Identity.IsAuthenticated)
        //    {
        //        return false;
        //    }


        //    return true;
        //}

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            
            
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Errors/Error_401.cshtml",
            };
        }

    }
}