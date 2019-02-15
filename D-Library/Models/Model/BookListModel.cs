using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;

namespace D_Library.Models.Model
{
    public class BookListModel
    {
        [Display(Name = "کتاب ها")]
        public IEnumerable<Tbl_Book> Books { get; set; }
    }
}