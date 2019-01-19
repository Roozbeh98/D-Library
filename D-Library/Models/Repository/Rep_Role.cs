using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}