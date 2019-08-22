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

            return View(model);
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





        #endregion
    }
}