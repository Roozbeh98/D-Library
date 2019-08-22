using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D_Library.Models.Model
{
    public class LoginModel
    {
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage =("نام کاربری باید وارد شود"))]
        [StringLength(100, ErrorMessage = "مقدار وارد شده بیش 100 کارکتراست")]
        public string Username { get; set; }
        [Display(Name ="رمز عبور یا کد ثبت نام")]
        [Required(ErrorMessage = ("رمز عبور یا کد ثبت نام باید وارد شود"))]
        [StringLength(100, ErrorMessage = "مقدار وارد شده بیش 100 کارکتراست")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemenberMe { get; set; }
    }
}