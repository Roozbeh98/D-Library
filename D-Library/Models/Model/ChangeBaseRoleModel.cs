using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class ChangeBaseRoleModel
    {
        public int ID { get; set; }
        [Display(Name = " نقش")]
        public int BaseRole { get; set; }
    }
}