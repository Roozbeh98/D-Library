using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;

namespace D_Library.Models.Model
{
    public class BaseRoleListModel
    {
        [Display(Name = "نام نقش")]
        public IEnumerable<Tbl_BaseRole> BaseRole { get; set; }
    }
}