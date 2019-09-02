using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using D_Library.Models.Domins;
using D_Library.Models.Model;
using D_Library.Models.Repository;
using D_Library.Models.UserManagement;
using D_Library.Models.Plugins;
using System.Runtime;

namespace D_Library.Controllers
{
    public class FileManagerController : Controller
    {
        ELEntities db = new ELEntities();

        #region upload

        [HttpPost]
        public void UploadFiles(int id)
        {
            if (db.Tbl_Book.Any(a => a.Book_ID == id))
            {
                if (Request.Files?.Count > 0)
                {
                    Guid folderName;
               
                    int index = 0;

                    if (db.Tbl_Files.Any(a => a.File_BookID == id))
                    {
                        folderName = db.Tbl_Files.FirstOrDefault(a => a.File_BookID == id).File_FolderName;
                        index = db.Tbl_Files.Select(a => a.File_BookID == id).Count();
                    }
                    else
                    {
                        folderName = Guid.NewGuid();
                        Directory.CreateDirectory(Path.Combine(Server.MapPath("~/App_Data/Upload/"), folderName.ToString()));
                    }

                    Membership membership = new Membership();

                    var filesCount = Request.Files.Count;

                    for (int i = 0; i < filesCount; i++)
                    {
                        Tbl_Files q = new Tbl_Files();
                        var file = Request.Files[i];
                        index++;
                        q.File_Index = index;

                        var fileName = Path.GetFileName(file.FileName);
                        q.File_Name = fileName;

                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), string.Format("{0}/", folderName.ToString()), fileName);
                        q.File_Path = path;

                        q.File_BookID = id;

                        q.File_UserUploaderID = membership.Get_IDByUserName(User.Identity.Name);

                        q.File_Date = DateTime.Now;

                        Guid key = Guid.NewGuid();
                        q.File_DownloadKey = key;

                        q.File_DownloadAcssesGlobalIP = true;
                        q.File_DownloadAcssesLocalIP = true;

                        q.File_FolderName = folderName;

                        db.Tbl_Files.Add(q);

                        Tbl_BookDetails _BookDetails = db.Tbl_Book.Where(a => a.Book_ID == id).SingleOrDefault().Tbl_BookDetails;



                        if (_BookDetails.BD_FileCount != null)
                        {
                            _BookDetails.BD_FileCount++;
                        }
                        else
                        {
                            _BookDetails.BD_FileCount = 1;
                        }

                        db.Entry(_BookDetails).State = System.Data.Entity.EntityState.Modified;

                        if (Convert.ToBoolean(db.SaveChanges() > 0))
                        {
                            file.SaveAs(path);
                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        [HttpDelete]
        public void RemoveUploadFiles(int id)
        {
            int x = 1;

        }
        #endregion

        #region Download
        
        [HttpGet]
        public FileResult Download(string Key)
        {
            Tbl_Files q = db.Tbl_Files.Where(a => a.File_DownloadKey.ToString() == Key).SingleOrDefault();

            if (q != null)
            {
                return File(q.File_Path, "*", q.File_Name);
            }
            else
            {
                return null;
            }
     
        }


        #endregion

        #region Manage

        [HttpGet]
        public ActionResult DeleteFile(int id)
        {
            Tbl_Files q = db.Tbl_Files.Where(a => a.File_ID == id).SingleOrDefault();

            FileDeleteModel model = new FileDeleteModel();


            model.ID = q.File_ID;
            model.Key = q.File_DownloadKey;
            model.name = q.File_Name;

            return View(model);


        }


        [HttpPost]
        public ActionResult DeleteFile(FileDeleteModel model)
        {
            Tbl_Files q = db.Tbl_Files.Where(a => a.File_DownloadKey == model.Key).SingleOrDefault();

            if (q != null)
            {

                Tbl_BookDetails _BookDetails = db.Tbl_Book.Where(a => a.Book_ID == q.File_BookID).SingleOrDefault().Tbl_BookDetails;

                if (_BookDetails.BD_FileCount > 1)
                {
                    _BookDetails.BD_FileCount--;   
                }
                else
                {
                    _BookDetails.BD_FileCount= null;
                }

                int book_id = q.File_BookID;

                string path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), q.File_Path);
                string root = Path.Combine(Server.MapPath("~/App_Data/Upload/"), q.File_FolderName.ToString());

                db.Tbl_Files.Remove(q);

                db.Entry(_BookDetails).State = System.Data.Entity.EntityState.Modified;

                if (Convert.ToBoolean(db.SaveChanges() > 0))
                {
                    FileManagement file = new FileManagement();

                    file.DeleteFileWithPath(path);

                    file.Dir_Empty(root);

                    return RedirectToAction("BookShow","Book",new { id = book_id });
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        #endregion

    }
}