using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using D_Library.Models.Domins;

namespace D_Library.Models.Repository
{
    public class Rep_Language
    {
        ELEntities db = new ELEntities();

        public Rep_Language()
        {

        }

        public IEnumerable<SelectListItem> Get_AllLanguageSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in db.Tbl_Language)
            {
                list.Add(new SelectListItem() { Value = item.Language_ID.ToString(), Text = item.Language_Name + " - " + item.Language_Titel });
            }

            return list.AsEnumerable();
        }
    }
}