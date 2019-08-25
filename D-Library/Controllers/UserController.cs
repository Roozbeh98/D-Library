using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using D_Library.Models.Domins;
using D_Library.Models.Model;
using D_Library.Models.Repository;
using D_Library.Models.UserManagement;

namespace D_Library.Controllers
{
    public class UserController : Controller
    {
        ELEntities db = new ELEntities();
        #region users


        [HttpGet]
        public ActionResult RegisterCodeBuilder()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult RegisterCodeBuilder(RegisterCodeModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");

            }
            if (!ModelState.IsValid)
            {
                ViewBag.State = "Error";

                return View("BuildRegisterCode", model);
            }

            var q = db.Tbl_Login.Where(a => a.Login_UserName == model.Username).SingleOrDefault();
            int Code;
            if (q != null)
            {
                if (!q.Login_RegisterActive)
                {
                    ViewBag.TosterState = "error";
                    ViewBag.TosterType = TosterType.Maseage;
                    ViewBag.TosterMassage = "عملبات با موفقیت انجام نشده!";
                    return View();
                }
                else
                {
                    if (q.Tbl_RegisterCode.RegisterCode_Date.AddDays(-5) < DateTime.Now)
                    {
                        ViewBag.State = "Sucsse";
                        ViewBag.TosterState = "success";
                        ViewBag.TosterType = TosterType.Maseage;
                        ViewBag.TosterMassage = "عملبات با موفقیت انجام شده!";
                        ViewBag.Code = q.Tbl_RegisterCode.RegisterCode_Code;
                        ViewBag.Username = model.Username;
                        return View();
                    }
                    else
                    {
                        Random rnd = new Random();

                        Code = rnd.Next(100000, 999999);

                        q.Tbl_RegisterCode.RegisterCode_Code = Code.ToString();
                        q.Tbl_RegisterCode.RegisterCode_Date = DateTime.Now;
                        q.Login_BaseRoleID = model.SelectRole;

                        db.Entry(q.Tbl_RegisterCode).State = System.Data.Entity.EntityState.Modified;



                    }
                }
            }
            else
            {
                Tbl_Login L = new Tbl_Login();
                L.Login_UserName = model.Username;
                L.Login_RegisterActive = true;
                L.Login_BaseRoleID = model.SelectRole;
                L.Login_CustomRole = false;
                L.Login_UserActive = true;


                Tbl_RegisterCode r = new Tbl_RegisterCode();
                Random rnd = new Random();

                Code = rnd.Next(100000, 999999);

                r.RegisterCode_Code = Code.ToString();
                r.RegisterCode_Date = DateTime.Now;


                L.Tbl_RegisterCode = r;

                db.Tbl_RegisterCode.Add(r);
                db.Tbl_Login.Add(L);
            }



            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                ViewBag.State = "Sucsse";
                ViewBag.TosterState = "success";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام شده!";
                ViewBag.Username = model.Username;
                ViewBag.Code = Code;

                return View();



            }
            else
            {
                ViewBag.TosterState = "error";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام نشده!";
                return View();
            }



        }

        
        [HttpGet]
        public ActionResult RegisterCodeDelete(int id)
        {
            RegisterCodeDeleteModel model = new RegisterCodeDeleteModel();

            var q = db.Tbl_Login.Where(a => a.Login_ID == id).SingleOrDefault();

            model.ID = q.Login_ID;
            model.Username = q.Login_UserName;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult RegisterCodeDelete(RegisterCodeDeleteModel model)
        {
            var q = db.Tbl_Login.Where(a => a.Login_ID == model.ID).SingleOrDefault();

            db.Entry(q).State = System.Data.Entity.EntityState.Deleted;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                ViewBag.State = "Sucsse";
                ViewBag.TosterState = "success";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام شده!";


                return RedirectToAction("UsersRegisterNotCompleteList", "User");



            }
            else
            {
                ViewBag.TosterState = "error";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام نشده!";
                return RedirectToAction("UsersRegisterNotCompleteList", "User");
            }
        }

        [HttpGet]
        public ActionResult UsersList()
        {
            UserListModel tableMoodel = new UserListModel();
            tableMoodel.Users = db.Tbl_User;

            return View(tableMoodel);
        }

        [HttpGet]
        public ActionResult UsersRegisterNotCompleteList()
        {
            return View();
        }


        [HttpGet]
        public ActionResult UserProfile(int id)
        {
            var q = db.Tbl_Login.Where(a => a.Tbl_User.User_ID == id).SingleOrDefault();

            ProfileModel model = new ProfileModel();

            model.name = q.Tbl_User.User_Name;
            model.Family = q.Tbl_User.User_Family;
            model.ID = q.Login_ID;
            model.Email = q.Tbl_User.User_Email;
            model.baseRole = q.Tbl_BaseRole.BaseRole_Titel;
            model.Group = q.Tbl_User.Tbl_branch.Tbl_Group.Group_Name;
            model.Branch = q.Tbl_User.Tbl_branch.branch_Name;
            model.Mobile = q.Tbl_User.User_Mobile;
            model.Active = q.Login_UserActive;

            return View(model);

        }

        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            var q = db.Tbl_Login.Where(a => a.Login_ID == id).SingleOrDefault();

            ChangePasswordModel model = new ChangePasswordModel();
            model.ID = id;


            return PartialView(model);

        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var q = db.Tbl_Login.Where(a => a.Login_ID == model.ID).SingleOrDefault();


            var SaltPassword = model.CurentPassword + q.Login_PasswordSalt;
            var SaltPasswordBytes = Encoding.UTF8.GetBytes(SaltPassword);
            var SaltPasswordHush = Convert.ToBase64String(SHA512.Create().ComputeHash(SaltPasswordBytes));


            if (q.Login_PasswordHush == SaltPasswordHush)
            {

                var NewSalt = Guid.NewGuid().ToString("N");

                var NewSaltPassword = model.NewPassword + NewSalt;
                var NewSaltPasswordBytes = Encoding.UTF8.GetBytes(NewSaltPassword);
                var NewSaltPasswordHush = Convert.ToBase64String(SHA512.Create().ComputeHash(NewSaltPasswordBytes));

                q.Login_PasswordHush = NewSaltPasswordHush;
                q.Login_PasswordSalt = NewSalt;

                db.Entry(q).State = System.Data.Entity.EntityState.Modified;

                if (Convert.ToBoolean(db.SaveChanges() > 0))
                {
                    ViewBag.State = "Sucsse";
                    ViewBag.TosterState = "success";
                    ViewBag.TosterType = TosterType.Maseage;
                    ViewBag.TosterMassage = "عملبات با موفقیت انجام شده!";


                    return RedirectToAction("UserProfile", "User", new { id = q.Tbl_User.User_ID });



                }
                else
                {
                    ViewBag.TosterState = "error";
                    ViewBag.TosterType = TosterType.Maseage;
                    ViewBag.TosterMassage = "عملبات با موفقیت انجام نشده!";
                    return RedirectToAction("UserProfile", "User", new { id = q.Tbl_User.User_ID });
                }


            }
            else
            {
                //err
                ViewBag.TosterState = "error";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "پسورد نادرست است!";
                return RedirectToAction("UserProfile", "User", new { id = q.Tbl_User.User_ID });

            }

        }

        [HttpGet]
        public ActionResult ChangeBaseRole(int id)
        {
            var q = db.Tbl_Login.Where(a => a.Login_ID == id).SingleOrDefault();

            ChangeBaseRoleModel model = new ChangeBaseRoleModel();
            model.ID = id;
            model.BaseRole = q.Login_BaseRoleID;


            return PartialView(model);

        }

        [HttpPost]
        public ActionResult ChangeBaseRole(ChangeBaseRoleModel model)
        {
            var q = db.Tbl_Login.Where(a => a.Login_ID == model.ID).SingleOrDefault();
            q.Login_BaseRoleID = model.BaseRole;

            db.Entry(q).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                ViewBag.State = "Sucsse";
                ViewBag.TosterState = "success";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام شده!";


                return RedirectToAction("UserProfile", "User", new { id = q.Tbl_User.User_ID });



            }
            else
            {
                ViewBag.TosterState = "error";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام نشده!";
                return RedirectToAction("UserProfile", "User", new { id = q.Tbl_User.User_ID });
            }


        }

        [HttpGet]
        public ActionResult ChangeUserInfo(int id)
        {
            var q = db.Tbl_Login.Where(a => a.Login_ID == id).SingleOrDefault();

            UserInfoEditModel model = new UserInfoEditModel();
            model.ID = id;
            model.name = q.Tbl_User.User_Name;
            model.Family = q.Tbl_User.User_Family;
            model.Email = q.Tbl_User.User_Email;
            model.Branch =(int)q.Tbl_User.User_BranchID;
            model.Group = q.Tbl_User.Tbl_branch.branch_GroupID;
            model.Mobile = q.Tbl_User.User_Mobile;


            return PartialView(model);

        }

        [HttpPost]
        public ActionResult ChangeUserInfo(UserInfoEditModel model)
        {

            var q = db.Tbl_Login.Where(a => a.Login_ID == model.ID).SingleOrDefault();
            Tbl_User user = new Tbl_User();

            user = q.Tbl_User;

            user.User_Name = model.name;
            user.User_Family = model.Family;
            user.User_Email = model.Email;
            user.User_Mobile = model.Mobile;
            user.User_BranchID = model.Branch;

            db.Entry(q).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                ViewBag.State = "Sucsse";
                ViewBag.TosterState = "success";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام شده!";


                return RedirectToAction("UserProfile", "User", new { id = q.Tbl_User.User_ID });



            }
            else
            {
                ViewBag.TosterState = "error";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام نشده!";
                return RedirectToAction("UserProfile", "User", new { id = q.Tbl_User.User_ID });
            }

        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            UserBanOrDeleteAccuont model = new UserBanOrDeleteAccuont();

            var q = db.Tbl_Login.Where(a => a.Login_ID == id).SingleOrDefault();

            model.ID = q.Login_ID;
            model.Username = q.Login_UserName;

            return PartialView(model);

        }

        [HttpPost]
        public ActionResult DeleteUser(UserBanOrDeleteAccuont model)
        {


            return View();

        }

        [HttpGet]
        public ActionResult BanUser(int id)
        {
            UserBanOrDeleteAccuont model = new UserBanOrDeleteAccuont();

            var q = db.Tbl_Login.Where(a => a.Login_ID == id).SingleOrDefault();

            model.ID = q.Login_ID;
            model.Username = q.Login_UserName;
                       
            return PartialView(model);

        }

        [HttpPost]
        public ActionResult BanUser(UserBanOrDeleteAccuont model)
        {
            var q = db.Tbl_Login.Where(a => a.Login_ID == model.ID).SingleOrDefault();


            if (q.Login_UserActive)
            {
                q.Login_UserActive = false;
            }
            else
            {
                q.Login_UserActive = true;
            }
          

            db.Entry(q).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                ViewBag.State = "Sucsse";
                ViewBag.TosterState = "success";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام شده!";

                return RedirectToAction("UserProfile", "User", new { id = q.Tbl_User.User_ID });



            }
            else
            {
                ViewBag.TosterState = "error";
                ViewBag.TosterType = TosterType.Maseage;
                ViewBag.TosterMassage = "عملبات با موفقیت انجام نشده!";
                return RedirectToAction("UserProfile", "User", new { id = q.Tbl_User.User_ID });

            }

        }
        #endregion
    }
}