using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class UserMassageModel
    {
        public int SanderID { get; set; }
        public int ResiverID { get; set; }
        [Display(Name = "موضوع")]
        [Required(ErrorMessage = ("موضوع باید وارد شود"))]
        public string Subject { get; set; }
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = ("پیام باید وارد شود"))]
        [DataType(DataType.MultilineText)]
        public string Massage { get; set; }
    }
}