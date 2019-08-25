using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D_Library.Models.Model
{
    public class UserInfoEditModel
    {
        public int ID { get; set; }
        [Display(Name = "نام")]
        [StringLength(100, ErrorMessage = "مقدار وارد شده بیش 100 کارکتراست")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public string name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [StringLength(100, ErrorMessage = "مقدار وارد شده بیش 100 کارکتراست")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        public string Family { get; set; }
        [Display(Name = "گروه")]
        public int Group { get; set; }  
        [Display(Name = "رشته")]
        public int Branch { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        [StringLength(200, ErrorMessage = "مقدار وارد شده بیش 200 کارکتراست")]
        [EmailAddress(ErrorMessage = "ایمیل را به درستی وارد نمایید")]
        public string Email { get; set; }
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        [StringLength(11, ErrorMessage = "مقدار وارد شده بیش 11 کارکتراست")]
        public string Mobile { get; set; }
    }
}