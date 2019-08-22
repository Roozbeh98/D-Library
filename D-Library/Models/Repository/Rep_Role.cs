using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using D_Library.Models.Domins;

namespace D_Library.Models.Repository
{
    public class Rep_Role
    {
        ELEntities db = new ELEntities();

        public Rep_Role()
        {

        }

        public List<DropDownModel> Get()
        {
            List<DropDownModel> t = new List<DropDownModel>();
            foreach (var item in db.Tbl_BaseRole)
            {
                t.Add(new DropDownModel(item.BaseRole_ID, item.BaseRole_Titel));
            }
            return t;
        }

        public List<DropDownModel> GetAllPermission()
        {
            List<DropDownModel> t = new List<DropDownModel>();
            foreach (var item in db.Tbl_Permission)
            {
                t.Add(new DropDownModel(item.Permission_ID, item.Permission_Titel));
            }
            return t;
        }


        public IEnumerable<SelectListItem> Get_AllBaseRole()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in db.Tbl_BaseRole)
            {
                list.Add(new SelectListItem() { Value = item.BaseRole_ID.ToString(), Text = item.BaseRole_Titel});
            }

            return list.AsEnumerable();
        }


    }
}