using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;


namespace D_Library.Models.Model
{
    public class UserListMoodel
    {
        [Display(Name = "نام گروه")]
        public IEnumerable<Tbl_User> Users { get; set; }
        public int CarentPage { get; set; }
        public int PageCount { get; set; }
    }
}