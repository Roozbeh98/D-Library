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
    public class AccountController : Controller
    {
        ELEntities db = new ELEntities();
        // GET: Account

        #region log in/out

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Dashboard");

            }

            //if (Session.Count > 0)
            //{
            //    try
            //    {
            //        if (Session["User"].ToString() != null)
            //        {
            //            return RedirectToAction("Dashboard", "Dashboard");

            //        }
            //    }
            //    catch
            //    {

            //    }

            //}

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            //if (Session.Count > 0)
            //{
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Dashboard");

            }

            //if (Session["User"] != null)
            //{
            //    return RedirectToAction("Dashboard", "Dashboard");
            //}
            //}

            if (!ModelState.IsValid)
            {

                ViewBag.State = "Error";

                return View("Login", model);
            }


            var qlogin = (from a in db.Tbl_Login where a.Login_UserName == model.Username select a).SingleOrDefault();
            if (qlogin != null)
            {
                if (qlogin.Login_UserActive)
                {
                    if (qlogin.Login_RegisterActive)
                    {

                        if (qlogin.Tbl_RegisterCode.RegisterCode_Code == model.Password)
                        {
                            if (qlogin.Tbl_RegisterCode.RegisterCode_Date.AddDays(-5) < DateTime.Now)
                            {
                                Session["User"] = qlogin.Login_UserName;
                                Session["Role"] = qlogin.Login_BaseRoleID;
                                Session["Register"] = "Active";

                                return RedirectToAction("Register", "Account");

                            }
                            else
                            {
                                //err
                                ViewBag.Message = "کد ثبت نام منقضی شده است ! لطفا برای دریافت کد جدید به کتاب خونه مراجعه کنید";
                                ViewBag.State = "Error";
                                return View();
                            }
                        }

                        else
                        {
                            //err
                            ViewBag.Message = "کد ثبت نام نادرست است!";
                            ViewBag.State = "Error";
                            return View();


                        }
                    }
                    else
                    {

                        var SaltPassword = model.Password + qlogin.Login_PasswordSalt;
                        var SaltPasswordBytes = Encoding.UTF8.GetBytes(SaltPassword);
                        var SaltPasswordHush = Convert.ToBase64String(SHA512.Create().ComputeHash(SaltPasswordBytes));


                        if (qlogin.Login_PasswordHush == SaltPasswordHush)
                        {
                            string s = string.Empty;

                            Models.UserManagement.Membership Role = new Models.UserManagement.Membership();

                            var r = Role.GetRoles(model.Username);

                            if (r.Count > 0)
                            {
                                s = string.Join(",", r);
                            }



                            var Ticket = new FormsAuthenticationTicket(0, model.Username, DateTime.Now, model.RemenberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddDays(1), true, s);
                            var EncryptedTicket = FormsAuthentication.Encrypt(Ticket);
                            var Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptedTicket)
                            {
                                Expires = Ticket.Expiration
                                // Domain =

                            };
                            Response.Cookies.Add(Cookie);
                            return RedirectToAction("Dashboard", "Dashboard");
                        }
                        else
                        {
                            //err
                            ViewBag.Message = "پسورد نادرست است !";
                            ViewBag.State = "Error";
                            return View();

                        }
                    }
                }
                else
                {
                    ViewBag.Message = "این کاربر غیر فعال است ! لطفا به کتابخوانه مراجعه فرمایید";
                    ViewBag.State = "Error";
                    return View();
                }

            }
            else
            {
                //err
                ViewBag.Message = "نام کاربری نادرست است !";
                ViewBag.State = "Error";
                return View();

            }

        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            var Cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            Response.Cookies.Add(Cookie);
            Session.RemoveAll();

            return RedirectToAction("Login", "Account");

        }


        #endregion

        #region Register

        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Dashboard");

            }
            else
            {
                try
                {
                    if (Session["Register"] != null)
                    {
                        if (Session["Register"].ToString() != "Active")
                        {

                            return RedirectToAction("Login", "Account");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }


                }
                catch
                {
                    return RedirectToAction("Login", "Account");

                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model, int SelectBranch)
        {
            //if (Password != MatchPassword)
            //{
            //    return View();
            //}

            if (!ModelState.IsValid)
            {

                ViewBag.State = "Error";

                return View("Register", model);
            }

            Tbl_User ur = new Tbl_User();
            ur.User_Name = model.Name;
            ur.User_Family = model.Family;
            ur.User_Email = model.Email;
            ur.User_Mobile = model.Mobile;
            ur.User_Date = DateTime.Now;
            ur.User_BranchID = SelectBranch;
            ur.User_SABAlloow = false;

            db.Tbl_User.Add(ur);

            Tbl_Login login = new Tbl_Login();
            string s = Session["User"].ToString();
            login = db.Tbl_Login.Where(a => a.Login_UserName == s).SingleOrDefault();
            login.Login_UserID = ur.User_ID;

            var Salt = Guid.NewGuid().ToString("N");

            var SaltPassword = model.Password + Salt;
            var SaltPasswordBytes = Encoding.UTF8.GetBytes(SaltPassword);
            var SaltPasswordHush = Convert.ToBase64String(SHA512.Create().ComputeHash(SaltPasswordBytes));

            login.Login_PasswordHush = SaltPasswordHush;
            login.Login_PasswordSalt = Salt;
            login.Login_RegisterActive = false;
            db.Entry(login).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {

                Session["Register"] = "Deactive";
                return RedirectToAction("Login", "Account");


            }
            else
            {

            }


            return View();
        }

        #endregion

        #region validtor

        [HttpPost]
        public JsonResult EamilValid(string Email)
        {
            try
            {

                var q = db.Tbl_User.Where(a => a.User_Email == Email).SingleOrDefault();

                if (q == null)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }

            }
            catch
            {

                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }
        [HttpPost]
        public JsonResult PasswordMatch(string Password, string PasswordVerify)
        {
            try
            {



                if (Password.Equals(PasswordVerify))
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }

            }
            catch
            {

                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public JsonResult UserNameValid(string Username)
        {
            try
            {

                var q = db.Tbl_Login.Where(a => a.Login_UserName == Username).SingleOrDefault();

                if (q == null)
                {
                    return Json(true, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }

            }
            catch
            {

                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion

    }

}