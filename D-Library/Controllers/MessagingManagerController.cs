using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using D_Library.Models.Domins;
using D_Library.Models.Model;
using D_Library.Models.UserManagement;

namespace D_Library.Controllers
{
    public class MessagingManagerController : Controller
    {
        ELEntities db = new ELEntities();

        [HttpGet]
        public ActionResult UserMassageFromManager(int id)
        {
            UserMassageModel model = new UserMassageModel();
            Models.UserManagement.Membership membership = new Models.UserManagement.Membership();

            var q = db.Tbl_Login.Where(a => a.Login_UserName == User.Identity.Name).SingleOrDefault();

            model.SanderID = q.Tbl_User.User_ID;

            model.ResiverID = id;


            return PartialView(model);

        }

        [HttpPost]
        public ActionResult UserMassageFromManager(UserMassageModel  model)
        {


            return PartialView();

        }
    }
}