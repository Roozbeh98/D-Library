using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class RequestTypeModel
    {
        [Display(Name = "نام استاد")]
        [Required(ErrorMessage = ("نام کاربری باید وارد شود"))]
        [StringLength(100, ErrorMessage = "مقدار وارد شده بیش 100 کارکتراست")]
        public string Master { get; set; }

        [Display(Name = "موضوع")]
        [Required(ErrorMessage = ("موضوع باید وارد شود"))]
        [StringLength(300, ErrorMessage = "مقدار وارد شده بیش 300 کارکتراست")]
        
        public string Titel { get; set; }

    }
}