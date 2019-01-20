using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D_Library.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public void UploadFiles()
        {
            if (Request.Files?.Count > 0)
            {
                var filesCount = Request.Files.Count;
                for (int i = 0; i < filesCount; i++)
                {
                    var file = Request.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);

                    file.SaveAs(path);
                }
            }
        }

        public ActionResult Uploader()
        {

            return View();
        }

    }

}