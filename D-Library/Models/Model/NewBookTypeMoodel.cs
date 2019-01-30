using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using D_Library.Models.Repository;

namespace D_Library.Models.Model
{
    public class NewBookTypeMoodel
    {
        [Display(Name = "نام گروه")]
        [StringLength(100, ErrorMessage = "مقدار وارد شده بیش 100 کارکتراست")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
      //  [Remote("UserNameValid", "Account", HttpMethod = "Post", ErrorMessage = "نام کاربری تکراری است")]
        public string TypeName { get; set; }

        public List<DropDownModel> FromList { get; set; }

    }
}