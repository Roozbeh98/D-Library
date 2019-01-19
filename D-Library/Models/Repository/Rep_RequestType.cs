using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;

namespace D_Library.Models.Repository
{
    public class Rep_RequestType
    {
        ELEntities db = new ELEntities();

        public Rep_RequestType()
        {

        }

        public List<DropDownModel> Get_BookValidType()
        {
            List<DropDownModel> t = new List<DropDownModel>();

            var q = db.Tbl_RequestType.Where(a => a.RequestType_RequestKindID == 1);

            foreach (var item in q)
            {

                t.Add(new DropDownModel(item.RequestType_ID, item.RequestType_Titel));
            }


            return t;
        }

    }
}