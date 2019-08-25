using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class LibraryAddModel
    {
        public int ID { get; set; }
        [Display(Name = "نام کتابخانه")]
        [StringLength(100, ErrorMessage = "مقدار وارد شده بیش 100 کارکتراست")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        //  [Remote("UserNameValid", "Account", HttpMethod = "Post", ErrorMessage = "نام کاربری تکراری است")]
        public string Name { get; set; }
    }
}