using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;


namespace D_Library.Models.Model
{
    public class UserListModel
    {
        [Display(Name = "نام گروه")]
        public IEnumerable<Tbl_User> Users { get; set; }
    }
}