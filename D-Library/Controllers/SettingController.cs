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
            //ViewBag.TosterState = "success";
            //ViewBag.TosterType = TosterType.WithTitel;
            //ViewBag.TosterTitel = "عملیات";
            //ViewBag.TosterMassage = "موفقیت امیز بود";
            return View();
        }

        #region General

        #region Library

        [HttpGet]
        public ActionResult LibraryList()
        {
            return View(db.Tbl_Library);
        }
        [HttpGet]
        public ActionResult LibraryAdd()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult LibraryAdd(Tbl_Library model)
        {

            db.Tbl_Library.Add(model);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("LibraryList", "Setting");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult LibraryEdit(int id)
        {
            Tbl_Library model = new Tbl_Library();
            model = db.Tbl_Library.Where(a => a.Library_ID == id).SingleOrDefault();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult LibraryEdit(Tbl_Library model)
        {
            Tbl_Library q = new Tbl_Library();
            q = db.Tbl_Library.Where(a => a.Library_ID == model.Library_ID).SingleOrDefault();
            q.Library_Name = model.Library_Name;

            db.Entry(q).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("LibraryList", "Setting");
            }
            else
            {
                return View();
            }

        }

        #endregion

        #endregion

        #region Book

        #region Booktype

        [HttpGet]
        public ActionResult BookTypeList()
        {
            BookTypeListModel tableMoodel = new BookTypeListModel();
            tableMoodel.BookType = db.Tbl_BookType;

            return View(tableMoodel);
        }

        [HttpGet]
        public ActionResult BookTypeAdd()
        {
            BookTypeAddModel moodel = new BookTypeAddModel();
            Rep_Book book = new Rep_Book();
            moodel.FromList = book.Get_BookDetailsListAll();
            return View(moodel);
        }

        [HttpPost]
        public ActionResult BookTypeAdd(BookTypeAddModel moodel, string[] to)
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

        #region Language

        [HttpGet]
        public ActionResult LanguageList()
        {
            return View(db.Tbl_Language);
        }
        [HttpGet]
        public ActionResult LanguageAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LanguageAdd(LanguageAddModel model)
        {
            Tbl_Language q = new Tbl_Language();
            q.Language_Name = model.Name;
            q.Language_Titel = model.Titel;

            db.Tbl_Language.Add(q);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("LanguageList", "Setting");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult LanguageEdit(int id)
        {
            Tbl_Language q = new Tbl_Language();
            q = db.Tbl_Language.Where(a => a.Language_ID == id).SingleOrDefault();
            LanguageAddModel model = new LanguageAddModel();
            model.ID = q.Language_ID;
            model.Name = q.Language_Name;
            model.Titel = q.Language_Titel;
            return View(model);
        }

        [HttpPost]
        public ActionResult LanguageEdit(LanguageAddModel model)
        {
            Tbl_Language q = new Tbl_Language();
            q = db.Tbl_Language.Where(a => a.Language_ID == model.ID).SingleOrDefault();
            q.Language_Name = model.Name;
            q.Language_Titel = model.Titel;

            db.Entry(q).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("LanguageList", "Setting");
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region Tags
        [HttpGet]
        public ActionResult TagList()
        {
            return View(db.Tbl_Tag);
        }
        [HttpGet]
        public ActionResult TagAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TagAdd(TagAddModel model)
        {
            Tbl_Tag q = new Tbl_Tag();
            q.Tag_Name = model.Name;

            db.Tbl_Tag.Add(q);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("TagList", "Setting");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult TagEdit(int id)
        {
            Tbl_Tag q = new Tbl_Tag();
            q = db.Tbl_Tag.Where(a => a.Tag_ID == id).SingleOrDefault();
            TagAddModel model = new TagAddModel();
            model.ID = q.Tag_ID;
            model.Name = q.Tag_Name;
            return View(model);
        }

        [HttpPost]
        public ActionResult TagEdit(TagAddModel model)
        {
            Tbl_Tag q = new Tbl_Tag();
            q = db.Tbl_Tag.Where(a => a.Tag_ID == model.ID).SingleOrDefault();
            q.Tag_Name = model.Name;

            db.Entry(q).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("TagList", "Setting");
            }
            else
            {
                return View();
            }

        }
        #endregion

        #endregion

        #region Users

        #region BaseRole
        [HttpGet]
        public ActionResult BaseRoleList()
        {
            BaseRoleListModel tableMoodel = new BaseRoleListModel();
            tableMoodel.BaseRole = db.Tbl_BaseRole;

            return View(tableMoodel);
        }

        [HttpGet]
        public ActionResult BaseRoleAdd()
        {
            BaseRoleAddModel moodel = new BaseRoleAddModel();
            Rep_Role permission = new Rep_Role();
            moodel.FromList = permission.GetAllPermission();
            return View(moodel);
        }

        [HttpPost]
        public ActionResult BaseRoleAdd(BaseRoleAddModel moodel, string[] to)
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

        #region Group
        [HttpGet]
        public ActionResult GroupList()
        {
            return View(db.Tbl_Group);
        }

        [HttpGet]
        public ActionResult GroupAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GroupAdd(GroupAddModel model)
        {
            Tbl_Group q = new Tbl_Group();
            q.Group_Name = model.Name;

            db.Tbl_Group.Add(q);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("GroupList", "Setting");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult GroupEdit(int id)
        {
            Tbl_Group q = new Tbl_Group();
            q = db.Tbl_Group.Where(a => a.Group_ID == id).SingleOrDefault();
            GroupAddModel model = new GroupAddModel();
            model.ID = q.Group_ID;
            model.Name = q.Group_Name;
 
            return View(model);
        }

        [HttpPost]
        public ActionResult GroupEdit(LanguageAddModel model)
        {
            Tbl_Group q = new Tbl_Group();
            q = db.Tbl_Group.Where(a => a.Group_ID == model.ID).SingleOrDefault();
            q.Group_Name = model.Name;


            db.Entry(q).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("GroupList", "Setting");
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region Branch

        [HttpGet]
        public ActionResult BranchList()
        {
            return View(db.Tbl_branch);
        }
        [HttpGet]
        public ActionResult BranchAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BranchAdd(BranchAddModel model)
        {
            Tbl_branch q = new Tbl_branch();
            q.branch_GroupID = model.Group_ID;
            q.branch_Name = model.Name;

            db.Tbl_branch.Add(q);

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("BranchList", "Setting");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult BranchEdit(int id)
        {
            Tbl_branch q = new Tbl_branch();
            q = db.Tbl_branch.Where(a => a.branch_ID == id).SingleOrDefault();
            BranchAddModel model = new BranchAddModel();
            model.ID = q.branch_ID;
            model.Name = q.branch_Name;
            model.Group_ID = q.branch_GroupID;
            return View(model);
        }

        [HttpPost]
        public ActionResult BranchEdit(BranchAddModel model)
        {
            Tbl_branch q = new Tbl_branch();
            q = db.Tbl_branch.Where(a => a.branch_ID == model.ID).SingleOrDefault();
            q.branch_Name = model.Name;
            q.branch_GroupID = model.Group_ID;

            db.Entry(q).State = System.Data.Entity.EntityState.Modified;

            if (Convert.ToBoolean(db.SaveChanges() > 0))
            {
                return RedirectToAction("BranchList", "Setting");
            }
            else
            {
                return View();
            }

        }
        #endregion

        #endregion


    }
}