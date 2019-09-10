using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class BookTypeSelectorModel
    {
        [Display(Name = "دسته")]
        public int Catgory { get; set; }
        [Display(Name = "زیردسته")]
        [Required(ErrorMessage = ("زیر دسته باید انتخاب گردد"))]
        public int SubCatgory { get; set; }
    }
}