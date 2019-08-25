using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class ProfileModel
    {
        public int ID { get; set; }
        [Display(Name = "نام")]
        public string name { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }
        [Display(Name = "نوع کاربری")]
        public string baseRole { get; set; }
        [Display(Name = "گروه")]
        public string Group { get; set; }
        [Display(Name = "رشته")]
        public string Branch { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        public bool Active { get; set; }

    }
}