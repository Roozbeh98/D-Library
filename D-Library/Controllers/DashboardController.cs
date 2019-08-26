using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using D_Library.Models.Domins;
using D_Library.Models.Model;
using D_Library.Models.Repository;
using D_Library.Models.UserManagement;


namespace D_Library.Controllers
{
    public class DashboardController : Controller
    {
        ELEntities db = new ELEntities();
        // GET: Dashboard


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Models.UserManagement.CustomAuthorize]
        public ActionResult Dashboard()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");


            }
            return View();
        }



        [HttpGet]
        public ActionResult RequestTypeSelector()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RequestTypeSelector(int x)
        {
            return View();
        }

        [HttpGet]
        public ActionResult RequestUploadFile()
        {
            return View();
        }
    }

}