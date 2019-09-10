using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using D_Library.Models.Repository;

namespace D_Library.Models.Model
{
    public class BookTypeAddModel
    {
        public int ID { get; set; }

        [Display(Name = "انتخاب گروه")]
        public int Category { get; set; }

        [Display(Name = "نام زیر گروه")]
        [StringLength(100, ErrorMessage = "مقدار وارد شده بیش 100 کارکتراست")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
      //  [Remote("UserNameValid", "Account", HttpMethod = "Post", ErrorMessage = "نام کاربری تکراری است")]
        public string TypeName { get; set; }

        public string [] from { get; set; }
        public string [] to { get; set; }

    }
}