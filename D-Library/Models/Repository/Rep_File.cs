using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;

namespace D_Library.Models.Repository
{
    public class Rep_File
    {
        ELEntities db = new ELEntities();
        public Rep_File()
        {

        }

        public IEnumerable<Tbl_Files> FilesByBookID(int id)
        {

            var q = from a in db.Tbl_Files where (a.File_BookID == id) select a;

            return q;
        }
    }
}