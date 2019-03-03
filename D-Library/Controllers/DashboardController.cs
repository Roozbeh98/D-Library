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
        public ActionResult BaseRoleList()
        {
            BaseRoleList tableMoodel = new BaseRoleList();
            tableMoodel.BaseRole = db.Tbl_BaseRole;

            return View(tableMoodel);
        }

        [HttpGet]
        public ActionResult NewBaseRole()
        {
            NewBaseRoleModel moodel = new NewBaseRoleModel();
            Rep_Role permission = new Rep_Role();
            moodel.FromList = permission.GetAllPermission();
            return View(moodel);
        }

        [HttpPost]
        public ActionResult NewBaseRole(NewBaseRoleModel moodel, string[] to)
        {
            Tbl_BaseRole baseRole = new Tbl_BaseRole();


            baseRole.BaseRole_Name = moodel.RoleName;
            baseRole.BaseRole_Titel = moodel.TitelName;
            db.Tbl_BaseRole.Add(baseRole);


            foreach (var item in to)
            {
                Tbl_BaseRolesPermission rolesPermission = new Tbl_BaseRolesPermission();
                rolesPermission.Tbl_BaseRole = baseRole;

                rolesPermission.BRP_PermissionID = Convert.ToInt32(item);
                db.Tbl_BaseRolesPermission.Add(rolesPermission);
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

        #region book

        [HttpGet]
        public ActionResult NewBook(int id)
        {
            Rep_Book rep = new Rep_Book();
            Rep_Professor professor = new Rep_Professor();
            NewBookModel model = new NewBookModel();
            model.DetailsNav = rep.Get_BookDetailsListByBookType(id);
            model.ID = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult NewBook(NewBookModel model)
        {
            Tbl_Book _book = new Tbl_Book();
            Tbl_BookDetails _details = new Tbl_BookDetails();
            Membership membership = new Membership();
            Rep_Book rep = new Rep_Book();

            model.DetailsNav = rep.Get_BookDetailsListByBookType(model.ID);

            _book.Book_Code = model.Book.Book_Code;
            _book.Book_Name = model.Book.Book_Name;
            _book.Book_BookTypeID = model.ID;
            _book.Book_UploaderUserID = membership.Get_IDByUserName(User.Identity.Name);
            _book.Book_GuestSearchEnabel = model.Book.Book_GuestSearchEnabel;
            _book.Book_Publish = false;

            _details.BD_DigitalVersionAvailable = model.Details.BD_DigitalVersionAvailable;

            _details.BD_PhysicalVersionAvailable = model.Details.BD_PhysicalVersionAvailable;

            _details.BD_PageCount = model.Details.BD_PageCount;




            foreach (var item in model.DetailsNav)
            {
                switch (item)
                {
                    case "Titel":
                        _details.BD_Titel = model.Details.BD_Titel;
                        break;
                    case "Abstract":
                        _details.BD_Abstract = model.Details.BD_Abstract;
                        break;
                    case "Student":
                        _details.BD_StudentID = model.Details.BD_StudentID;
                        break;
                    case "Master":
                        _details.BD_MasterID = model.Details.BD_MasterID;
                        break;
                    case "ISBN":
                        _details.BD_ISBN = model.Details.BD_ISBN;
                        break;
                    case "Group":
                        _details.BD_GroupID = model.Details.BD_GroupID;
                        break;
                    case "Branch":
                        _details.BD_BranchID = model.Details.BD_BranchID;
                        break;
                    case "Publishers":
                        _details.BD_Publishers = model.Details.BD_Publishers;
                        break;
                    case "Subject":
                        _details.BD_Subject = model.Details.BD_Subject;
                        break;
                    case "Description":
                        _details.BD_Description = model.Details.BD_Description;
                        break;
                    case "WriterName":
                        _details.BD_WriterName = model.Details.BD_WriterName;
                        break;
                    case "ReleaseCount":
                        _details.BD_ReleaseCount = model.Details.BD_ReleaseCount;
                        break;
                    case "Translator":
                        _details.BD_Translator = model.Details.BD_Translator;
                        break;
                    default:
                        break;
                }
            }

            db.Tbl_BookDetails.Add(_details);

            _book.Tbl_BookDetails = _details;

            db.Tbl_Book.Add(_book);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                ViewBag.Message = "عملبات با موفقیت انجام شده!";
                ViewBag.State = "Sucsse";
                if (_details.BD_DigitalVersionAvailable)
                {
                    return RedirectToAction("BookUplaod", "Dashboard", new { id = _book.Book_ID });
                }
                else
                {
                    return RedirectToAction("BookList", "Dashboard");
                }

            }
            else
            {
                ViewBag.Message = "عملبات با موفقیت انجام نشده!";
                ViewBag.State = "Error";
                return RedirectToAction("NewBook", "Dashboard", new { id = model.ID });
            }
        }

        [HttpGet]
        public ActionResult NewBookTypeSelector()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewBookTypeSelector(int BookTypeSelection)
        {
            return RedirectToAction("NewBook", "Dashboard", new { id = BookTypeSelection });
        }

        [HttpGet]
        public ActionResult BookType()
        {
            BookTypeTableModel tableMoodel = new BookTypeTableModel();
            tableMoodel.BookType = db.Tbl_BookType;

            return View(tableMoodel);
        }

        [HttpGet]
        public ActionResult NewBookType()
        {
            NewBookTypeModel moodel = new NewBookTypeModel();
            Rep_Book book = new Rep_Book();
            moodel.FromList = book.Get_BookDetailsListAll();
            return View(moodel);
        }

        [HttpPost]
        public ActionResult NewBookType(NewBookTypeModel moodel, string[] to)
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

        [HttpGet]
        public ActionResult BookUplaod(int id)
        {
            BookUplaodModel model = new BookUplaodModel();

            model.ID = id;

            return View(model);
        }

        [HttpPost]
        public ActionResult BookUplaod(BookUplaodModel model)
        {


            return View();
        }


        [HttpGet]
        public ActionResult BookList()
        {
            BookListModel moodel = new BookListModel();
            moodel.Books = db.Tbl_Book;
            return View(moodel);
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