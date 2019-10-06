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
    public class SearchController : Controller
    {

        ELEntities db = new ELEntities();


        public ActionResult SearchResult(string looking)
        {
            if (!string.IsNullOrWhiteSpace(looking))
            {
                var model = (from a in db.Tbl_Book
                             where (a.Book_Name.Contains(looking) ||
                                    a.Tbl_BookDetails.BD_ISBN.Contains(looking) ||
                                    a.Tbl_BookDetails.BD_LCC.Contains(looking) ||
                                    a.Tbl_BookDetails.BD_Publishers.Contains(looking) ||
                                    a.Tbl_BookDetails.BD_Subject.Contains(looking) ||
                                    a.Tbl_BookDetails.BD_Titel.Contains(looking) ||
                                    a.Tbl_BookDetails.BD_Translator.Contains(looking) ||
                                    a.Tbl_BookTag.Any(b => b.Tbl_Tag.Tag_Name.Contains(looking)) ||
                                    a.Tbl_BookDetails.BD_WriterName.Contains(looking))
                             select a).ToList();

                SearchResultModel Result = new SearchResultModel();

                List<SearchResultItemModel> items = new List<SearchResultItemModel>();

                foreach (var item in model)
                {
                    items.Add(new SearchResultItemModel(item.Book_ID, item.Book_Name));
                }
                Result.looking = looking;
                Result.Items = items;

                return View(Result);
            }
            return View(new SearchResultModel() { looking = "", Items = null });
        }

        public ActionResult AdvancedSearch()
        {



            return View();
        }



    }
}