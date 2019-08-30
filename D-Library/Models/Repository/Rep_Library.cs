using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using D_Library.Models.Domins;

namespace D_Library.Models.Repository
{
    public class Rep_Library
    {
        ELEntities db = new ELEntities();

        public Rep_Library()
        {

        }
        public IEnumerable<SelectListItem> Get_AllLibrarySelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in db.Tbl_Library)
            {
                list.Add(new SelectListItem() { Value = item.Library_ID.ToString(), Text = item.Library_Name });
            }

            return list.AsEnumerable();
        }
    }
}