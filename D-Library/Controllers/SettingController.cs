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
    public class SettingController : Controller
    {
        ELEntities db = new ELEntities();

        [HttpGet]
        public ActionResult Setting()
        {
            return View();
        }

        #region Library

        [HttpGet]
        public ActionResult LibraryList()
        {
            return View(db.Tbl_Library);
        }
        [HttpGet]
        public ActionResult LibraryAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LibraryAdd(Tbl_Library model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult LibraryDelete()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LibraryEdit()
        {
            return View();
        }

        #endregion

    }
}