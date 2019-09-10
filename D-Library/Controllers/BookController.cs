using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;
using D_Library.Models.Domins;
using D_Library.Models.Model;
using D_Library.Models.Repository;
using D_Library.Models.UserManagement;
using D_Library.Models.Plugins;
using Newtonsoft.Json;

namespace D_Library.Controllers
{
    public class BookController : Controller
    {
        ELEntities db = new ELEntities();

        #region book

        [HttpGet]
        public ActionResult BookNew(int id)
        {
            Rep_Book rep = new Rep_Book();
            BookNewModel model = new BookNewModel();
            model.DetailsNav = rep.Get_BookDetailsListByBookType(id);
            model.ID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult BookNew(BookNewModel model)
        {
            Tbl_Book _book = new Tbl_Book();
            Tbl_BookDetails _details = new Tbl_BookDetails();
            Tbl_BookAcsses _acsses = new Tbl_BookAcsses();

            Membership membership = new Membership();
            Rep_Book rep = new Rep_Book();

            model.DetailsNav = rep.Get_BookDetailsListByBookType(model.ID);

            _book.Book_Code = model.Book.Book_Code;
            _book.Book_Name = model.Book.Book_Name;
            _book.Book_BookTypeID = model.ID;
            _book.Book_UploaderUserID = membership.Get_IDByUserName(User.Identity.Name);
            _book.Book_Publish = false;

            _details.BD_DigitalVersionAvailable = model.Details.BD_DigitalVersionAvailable;

            if (model.Details.BD_DigitalVersionAvailable)
            {
                _details.BD_FileEnabel = true;
            }


            _details.BD_PhysicalVersionAvailable = model.Details.BD_PhysicalVersionAvailable;



            if (model.Details.BD_PhysicalVersionAvailable)
            {
                _details.BD_LibraryID = model.Details.BD_LibraryID;
            }

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

            _acsses.BookAcsses_Guest = false;
            _acsses.BookAcsses_Global = false;
            _acsses.BookAcsses_Local = false;
            _acsses.BookAcsses_Custom = false;

            db.Tbl_BookDetails.Add(_details);
            db.Tbl_BookAcsses.Add(_acsses);

            _book.Tbl_BookDetails = _details;
            _book.Tbl_BookAcsses = _acsses;

            db.Tbl_Book.Add(_book);

            foreach (var item in model.Tag)
            {
                Tbl_BookTag _booktag = new Tbl_BookTag();

                int id = 0;

                if (Int32.TryParse(item, out id))
                {
                    Tbl_Tag tag = db.Tbl_Tag.Where(a => a.Tag_ID == id).SingleOrDefault();

                    if (tag == null)
                    {
                        Tbl_Tag _tag = new Tbl_Tag();
                        _tag.Tag_Name = item;
                        db.Tbl_Tag.Add(_tag);

                        _booktag.Tbl_Tag = _tag;
                        _booktag.Tbl_Book = _book;
                    }
                    else
                    {
                        _booktag.Tbl_Tag = tag;
                        _booktag.Tbl_Book = _book;
                    }
                }
                else
                {
                    Tbl_Tag _tag = new Tbl_Tag();
                    _tag.Tag_Name = item;
                    db.Tbl_Tag.Add(_tag);

                    _booktag.Tbl_Tag = _tag;
                    _booktag.Tbl_Book = _book;
                }


                db.Tbl_BookTag.Add(_booktag);
            }

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                ViewBag.Message = "عملبات با موفقیت انجام شده!";
                ViewBag.State = "Sucsse";
                if (_details.BD_DigitalVersionAvailable)
                {
                    return RedirectToAction("BookUplaod", "Book", new { id = _book.Book_ID });
                }
                else
                {
                    if (User.IsInRole("Publish"))
                    {
                        return RedirectToAction("BookPublish", "Book", new { id = _book.Book_ID });
                    }
                    return RedirectToAction("BookList", "Book");
                }

            }
            else
            {
                ViewBag.Message = "عملبات با موفقیت انجام نشده!";
                ViewBag.State = "Error";
                return RedirectToAction("BookNew", "Book", new { id = model.ID });
            }
        }

        [HttpGet]
        public ActionResult BookEdit(int id)
        {
            var q = db.Tbl_Book.Where(a => a.Book_ID == id).SingleOrDefault();


            Rep_Book rep = new Rep_Book();
            BookEditModel model = new BookEditModel();
            model.DetailsNav = rep.Get_BookDetailsListByBookType(q.Tbl_BookType.BookType_ID);
            model.ID = q.Book_ID;
            model.Details = q.Tbl_BookDetails;
            model.Book = q;



            return View(model);
        }

        [HttpPost]
        public ActionResult BookEdit(BookEditModel model)
        {
            Tbl_Book _book = new Tbl_Book();
            Tbl_BookDetails _details = new Tbl_BookDetails();
            Tbl_BookTag _tag = new Tbl_BookTag();
            Membership membership = new Membership();
            Rep_Book rep = new Rep_Book();

            var q = db.Tbl_Book.Where(a => a.Book_ID == model.ID).SingleOrDefault();


            _book = q;
            _details = q.Tbl_BookDetails;

            model.DetailsNav = rep.Get_BookDetailsListByBookType(q.Tbl_BookType.BookType_ID);

            _book.Book_Code = model.Book.Book_Code;
            _book.Book_Name = model.Book.Book_Name;
            _book.Book_UploaderUserID = membership.Get_IDByUserName(User.Identity.Name);

            _details.BD_DigitalVersionAvailable = model.Details.BD_DigitalVersionAvailable;
            if (model.Details.BD_DigitalVersionAvailable)
            {
                _details.BD_FileEnabel = true;
            }

            _details.BD_PhysicalVersionAvailable = model.Details.BD_PhysicalVersionAvailable;
            if (model.Details.BD_PhysicalVersionAvailable)
            {
                _details.BD_LibraryID = model.Details.BD_LibraryID;
            }

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

            db.Entry(_details).State = System.Data.Entity.EntityState.Modified;
            db.Entry(_book).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("BookShow", "Book", new { id = _book.Book_ID });
            }
            else
            {
                ViewBag.Message = "عملبات با موفقیت انجام نشده!";
                ViewBag.State = "Error";
                return RedirectToAction("BookShow", "Book", new { id = _book.Book_ID });
            }
        }

        [HttpGet]
        public ActionResult BookTagEdit(int id)
        {
            BookTagsModel model = new BookTagsModel();

            model.ID = id;

            model.TagCollection = db.Tbl_Tag;

            model.Tags = db.Tbl_BookTag.Where(a => a.BT_BookID == id).Select(a => a.Tbl_Tag.Tag_ID.ToString()).ToArray();


            return View(model);
        }

        [HttpPost]
        public ActionResult BookTagEdit(BookTagsModel model)
        {
            var oldtags = db.Tbl_BookTag.Where(a => a.Tbl_Book.Book_ID == model.ID).ToList();
            List<Tbl_Tag> newtags = new List<Tbl_Tag>();
            var _book = db.Tbl_Book.Where(a => a.Book_ID == model.ID).SingleOrDefault();

            foreach (var item in model.Tags)
            {
                int id = 0;

                if (Int32.TryParse(item, out id))
                {
                    newtags.Add(db.Tbl_Tag.Where(a => a.Tag_ID == id).SingleOrDefault());
                }
            }

            foreach (var item in oldtags)
            {
                if (!newtags.Exists(a => a.Tag_ID == item.Tbl_Tag.Tag_ID ))
                {
                    db.Tbl_BookTag.Remove(item);
                }
            }

            foreach (var item in model.Tags)
            {
                Tbl_BookTag _booktag = new Tbl_BookTag();
                int id = 0;

                if (Int32.TryParse(item, out id))
                {
                    Tbl_Tag tag = db.Tbl_Tag.Where(a => a.Tag_ID == id).SingleOrDefault();

                    if (tag != null)
                    {
                        if (!oldtags.Exists(a => a.Tbl_Tag.Tag_ID == id))
                        {
                            _booktag.Tbl_Tag = tag;
                            _booktag.Tbl_Book = _book;
                        }
                    }
                    else
                    {
                        Tbl_Tag _tag = new Tbl_Tag();
                        _tag.Tag_Name = item;
                        db.Tbl_Tag.Add(_tag);

                        _booktag.Tbl_Tag = _tag;
                        _booktag.Tbl_Book = _book;
                    }


                }
                else
                {
                    Tbl_Tag _tag = new Tbl_Tag();
                    _tag.Tag_Name = item;
                    db.Tbl_Tag.Add(_tag);

                    _booktag.Tbl_Tag = _tag;
                    _booktag.Tbl_Book = _book;
                }

                db.Tbl_BookTag.Add(_booktag);
            }

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("BookShow", "Book", new { id = _book.Book_ID });
            }
            else
            {
                return RedirectToAction("BookShow", "Book", new { id = _book.Book_ID });
            }
        }


        [HttpGet]
        public ActionResult BookAddFile(int id)
        {
            ViewBag.ID = id;
            return View();
        }

        [HttpGet]
        public ActionResult BookTypeSelector()
        {
            BookTypeSelectorModel model = new BookTypeSelectorModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult BookTypeSelector(BookTypeSelectorModel model)
        {
            return RedirectToAction("BookNew", "Book", new { id = model.SubCatgory });
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

            var q = db.Tbl_Book.Where(a => a.Book_ID == id).SingleOrDefault();

            model.ID = id;
            model.Publish = q.Book_Publish;
            model.GusetSearch = q.Tbl_BookAcsses.BookAcsses_Guest;
            model.GlobalAcsses = q.Tbl_BookAcsses.BookAcsses_Global;
            model.LocalAcsses = q.Tbl_BookAcsses.BookAcsses_Local;
            model.Custom = q.Tbl_BookAcsses.BookAcsses_Custom;

            return View(model);
        }

        [HttpPost]
        public ActionResult BookPublish(BookPublishModel model)
        {
            Tbl_Book book = new Tbl_Book();

            book = db.Tbl_Book.Where(a => a.Book_ID == model.ID).SingleOrDefault();


            book.Tbl_BookAcsses.BookAcsses_Guest = model.GusetSearch;
            book.Tbl_BookAcsses.BookAcsses_Global = model.GlobalAcsses;
            book.Tbl_BookAcsses.BookAcsses_Local = model.LocalAcsses;
            book.Tbl_BookAcsses.BookAcsses_Custom = model.Custom;
            book.Book_Publish = model.Publish;

            db.Entry(book).State = System.Data.Entity.EntityState.Modified;
            db.Entry(book.Tbl_BookAcsses).State = System.Data.Entity.EntityState.Modified;


            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                ViewBag.Message = "عملبات با موفقیت انجام شده!";
                ViewBag.State = "Sucsse";
                return RedirectToAction("BookShow", "Book", new { id = model.ID });

            }
            else
            {
                ViewBag.Message = "عملبات با موفقیت انجام نشده!";
                ViewBag.State = "Error";
                return RedirectToAction("BookShow", "Book", new { id = model.ID });
            }


        }

        [HttpGet]
        public ActionResult BookPublishAdvanced(int id)
        {
            BookPublishModel model = new BookPublishModel();
            model.ID = id;

            //var q = db.Tbl_Book.Where(a=> a.)

            //model.GlobalAcsses = id;
            //model.GusetSearch = id;
            //model.LocalAcsses = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult BookPublishAdvanced(BookPublishModel model)
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
        public ActionResult BookDelete(int id)
        {
            var q = db.Tbl_Book.Where(a => a.Book_ID == id).SingleOrDefault();

            BookDeleteModel model = new BookDeleteModel();

            model.ID = q.Book_ID;
            model.Name = q.Book_Name;

            return View(model);
        }

        [HttpPost]
        public ActionResult BookDelete(BookDeleteModel model)
        {
            Tbl_Book _Book = db.Tbl_Book.Where(a => a.Book_ID == model.ID).SingleOrDefault();

            if (_Book.Tbl_BookDetails.BD_FileEnabel)
            {
                foreach (var item in _Book.Tbl_Files.ToList())
                {
                    string path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.File_Path);
                    string root = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.File_FolderName.ToString());
                    db.Tbl_Files.Remove(item);
                    FileManagement file = new FileManagement();
                    file.DeleteFileWithPath(path);
                    file.Dir_Empty(root);
                }
            }

            db.Tbl_BookDetails.Remove(_Book.Tbl_BookDetails);

            if (_Book.Tbl_BookAcsses.BookAcsses_Custom)
            {
                foreach (var item in db.Tbl_BookCustomAcsses.Where(a => a.Tbl_BookAcsses == _Book.Tbl_BookAcsses).ToList())
                {
                    db.Tbl_BookCustomAcsses.Remove(item);
                }
            }

            db.Tbl_BookAcsses.Remove(_Book.Tbl_BookAcsses);
            db.Tbl_Book.Remove(_Book);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("BookList", "Book");
            }
            else
            {
                return null;
            }

        }


        #endregion

        #region API

        public JsonResult Get_BookCatgoryList(string searchTerm)
        {
            var q = db.Tbl_BookCategory.ToList();
            if (searchTerm != null)
            {
                q = db.Tbl_BookCategory.Where(a => a.BC_Name.Contains(searchTerm)).ToList();
            }

            var md = q.Select(a => new { id = a.BC_ID, text = a.BC_Name });

            return Json(md, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Get_AllTagList(string searchTerm)
        {
            var q = db.Tbl_Tag.ToList();
            if (searchTerm != null)
            {
                q = db.Tbl_Tag.Where(a => a.Tag_Name.Contains(searchTerm)).ToList();
            }

            var md = q.Select(a => new { id = a.Tag_ID, text = a.Tag_Name });

            return Json(md, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Get_SubCatgoryList(int CatgoryID)
        {
            var q = db.Tbl_BookCategory.Where(a => a.BC_ID == CatgoryID).SingleOrDefault();

            var t = db.Tbl_BookType.Where(a => a.Tbl_BookCategory.BC_ID == q.BC_ID).ToList();

            var md = t.Select(a => new { id = a.BookType_ID, text = a.BookType_Name });

            return Json(md, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}