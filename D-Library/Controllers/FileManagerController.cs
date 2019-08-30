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

    }
}