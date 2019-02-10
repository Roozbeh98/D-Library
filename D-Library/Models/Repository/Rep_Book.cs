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

        public List<DropDownModel> Get_BookTypeCreate()
        {
            List<DropDownModel> list = new List<DropDownModel>();
            foreach (var item in db.Tbl_BookType)
            {
                list.Add(new DropDownModel(item.BookType_ID ,item.BookType_Name));
            }

            return list;
        }

        public List<string> Get_BookDetailsListByBookType(int id)
        {
            List<string> list = new List<string>();

            var q = db.Tbl_BookDetailsNavigator.Where(a => a.Tbl_BookType.BookType_ID == id);

            foreach (var item in q)
            {
                list.Add(item.Tbl_BookDetailsFeatures.BDF_Name);
            }

            return list;
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