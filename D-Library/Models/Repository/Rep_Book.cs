using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;


namespace D_Library.Models.Repository
{
    public class Rep_Book
    {
        ELEntities db = new ELEntities();

        public Rep_Book()
        {

        }

        public List<DropDownModel> Get_BookDetailsListAll()
        {

            List<DropDownModel> list = new List<DropDownModel>();
            foreach (var item in db.Tbl_BookDetailsFeatures)
            {
                list.Add(new DropDownModel(item.BDF_ID, item.BDF_Titel));
            }

            return list;
        }
        public List<DropDownModel> Get_BookDetailsListByType(int? TypeId)
        {

            return null;
        }

    }
}