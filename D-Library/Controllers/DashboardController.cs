using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using D_Library.Models.Domins;
using D_Library.Models.Model;
using D_Library.Models.Repository;

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


        #region users


        [HttpGet]
        public ActionResult BuildRegisterCode()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");


            }
            return View();
        }
        [HttpPost]
        public ActionResult BuildRegisterCode(RegisterCodeModel model, int SelectRole)
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
                    ViewBag.Message = "عملبات با موفقیت انجام نشده!";
                    ViewBag.State = "Error";
                    return View();
                }
                else
                {
                    if (q.Tbl_RegisterCode.RegisterCode_Date.AddDays(-5) < DateTime.Now)
                    {
                        ViewBag.Message = "عملبات با موفقیت انجام شده!";
                        ViewBag.Code = q.Tbl_RegisterCode.RegisterCode_Code;
                        ViewBag.Username = model.Username;
                        ViewBag.State = "Sucsse";
                        return View();
                    }
                    else
                    {
                        Random rnd = new Random();

                        Code = rnd.Next(100000, 999999);

                        q.Tbl_RegisterCode.RegisterCode_Code = Code.ToString();
                        q.Tbl_RegisterCode.RegisterCode_Date = DateTime.Now;
                        q.Login_BaseRoleID = SelectRole;

                        db.Entry(q.Tbl_RegisterCode).State = System.Data.Entity.EntityState.Modified;



                    }
                }
            }
            else
            {
                Tbl_Login L = new Tbl_Login();
                L.Login_UserName = model.Username;
                L.Login_RegisterActive = true;
                L.Login_BaseRoleID = SelectRole;
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


                ViewBag.Message = "عملبات با موفقیت انجام شده!";
                ViewBag.Code = Code;
                ViewBag.Username = model.Username;
                ViewBag.State = "Sucsse";
                return View();



            }
            else
            {
                ViewBag.Message = "عملبات با موفقیت انجام نشده!";
                ViewBag.State = "Error";
                return View();
            }



        }

        public ActionResult UsersList(string Page)
        {
            UserListMoodel tableMoodel = new UserListMoodel();
            tableMoodel.Users = db.Tbl_User;
            int c = Convert.ToInt32(db.Tbl_BookType.Count() / 10);

            if (!string.IsNullOrEmpty(Page))
            {
                tableMoodel.CarentPage = Convert.ToInt32(Page);

            }
            else
            {
                tableMoodel.CarentPage = 1;

            }

            return View(tableMoodel);
        }

        [HttpPost]
        public ActionResult UsersList(NewBookTypeMoodel moodel, string[] to)
        {

            return View();




        }

        #endregion

        #region book

        public ActionResult BookType(string Page)
        {
            BookTypeTableMoodel tableMoodel = new BookTypeTableMoodel();
            tableMoodel.BookType = db.Tbl_BookType;
            int c = Convert.ToInt32(db.Tbl_BookType.Count() / 10);
           
            if (!string.IsNullOrEmpty(Page))
            {
                tableMoodel.CarentPage = Convert.ToInt32(Page);
           
            }
            else
            {
                tableMoodel.CarentPage = 1;

            }

            return View(tableMoodel);
        }

        [HttpPost]
        public ActionResult BookType(NewBookTypeMoodel moodel, string[] to)
        {

            return View();




        }


        [HttpGet]
        public ActionResult NewBookType()
        {
            NewBookTypeMoodel moodel = new NewBookTypeMoodel();
            Rep_Book book = new Rep_Book();
            moodel.FromList = book.Get_BookDetailsListAll();
            return View(moodel);
        }

        [HttpPost]
        public ActionResult NewBookType(NewBookTypeMoodel moodel, string[] to)
        {
            Tbl_BookType bookType = new Tbl_BookType();


            bookType.BookType_Name = moodel.TypeName;
            db.Tbl_BookType.Add(bookType);


            foreach (var item in to)
            {
                Tbl_BookDetailsNavigator detailsNavigator = new Tbl_BookDetailsNavigator();
                detailsNavigator.Tbl_BookType = bookType;
                detailsNavigator.BDN_BDFID = Convert.ToInt32(item);
                db.Tbl_BookDetailsNavigator.Add(detailsNavigator);
            }

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {


                ViewBag.Message = "عملبات با موفقیت انجام شده!";
                ViewBag.State = "Sucsse";
                return View();



            }
            else
            {
                ViewBag.Message = "عملبات با موفقیت انجام نشده!";
                ViewBag.State = "Error";
                return View();
            }



        }






        #endregion

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