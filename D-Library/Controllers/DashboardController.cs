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

            _details.BD_LanguageID = model.Details.BD_LanguageID;




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
        public ActionResult BookUplaod(int id)
        {
            BookUplaodModel model = new BookUplaodModel();

            model.ID = id;

            ViewBag.ID = id;

            return View(model);
        }

        [HttpPost]
        public ActionResult BookUplaod(BookUplaodModel model)
        {
            int s = model.ID;

            return View();
        }

        [HttpGet]
        public ActionResult BookPublish(int id)
        {
            BookPublishModel model = new BookPublishModel();
            model.ID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult BookPublish(BookPublishModel model)
        {
            int s = model.ID;

            return View();
        }

        [HttpGet]
        public ActionResult BookList()
        {
            BookListModel model = new BookListModel();
            model.Books = db.Tbl_Book;
            return View(model);
        }


        [HttpGet]
        public ActionResult BookShow(int id)
        {
            BookShowModel model = new BookShowModel();
            Rep_Book rep_b = new Rep_Book();
            Rep_File rep_f = new Rep_File();
            model.Book = db.Tbl_Book.Where(a => a.Book_ID == id).SingleOrDefault();

            model.DetailsNav = rep_b.Get_BookDetailsListByBookType(model.Book.Book_BookTypeID);

            model.Details = model.Book.Tbl_BookDetails;

            if (model.Details.BD_FileEnabel)
            {
                model.Files = rep_f.FilesByBookID(id);
            }
            model.Tags = new List<string>();
            model.Tags = rep_b.Get_TagsByBookID(id);

            return View(model);
        }

        [HttpGet]
        public ActionResult test(int id)
        {
            Tbl_Book book = new Tbl_Book();
            Tbl_BookDetails model = new Tbl_BookDetails();

            book = db.Tbl_Book.Where(a => a.Book_ID == id).SingleOrDefault();
            model = book.Tbl_BookDetails;

            return View(model);
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