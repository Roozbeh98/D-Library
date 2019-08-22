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
            SortedDictionary<int, string> order = new SortedDictionary<int, string>();


            foreach (var item in q)
            {
                order.Add(item.Tbl_BookDetailsFeatures.BDF_Priority, item.Tbl_BookDetailsFeatures.BDF_Name);
            }

            list.AddRange(order.Values.ToList());

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

        public string Get_BookTypeNameByID(int id)
        {
            string name = db.Tbl_BookType.Where(a => a.BookType_ID == id).SingleOrDefault().BookType_Name;

            return name;
        }

        public List<string> Get_TagsByBookID(int id)
        {
            var list = db.Tbl_Book.Where(a => a.Book_ID == id).SingleOrDefault().Tbl_BookTag.ToList();
            List<string> tags = new List<string>();
            foreach (var item in list)
            {
                tags.Add(item.Tbl_Tag.Tag_Name);
            }

            return tags;
        }


    }
}