using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D_Library.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Error_401()
        {
            return View();
        }
        public ActionResult Error_404()
        {
            return View();
        }

        public ActionResult Error_500()
        {
            return View();
        }
    }
}