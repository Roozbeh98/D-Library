using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Domins
{
    internal class MetaData_Book
    {
        public int Book_ID { get; set; }
        [Display(Name = "کد کتاب خانه")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        [DataType(DataType.Text)]
        public Nullable<int> Book_Code { get; set; }
        [Display(Name = "نام کتاب")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        [StringLength(100, ErrorMessage = "مقدار وارد شده بیش 100 کارکتراست")]
        public string Book_Name { get; set; }
        public int Book_BookTypeID { get; set; }
        public Nullable<int> Book_BDID { get; set; }
        public Nullable<int> Book_BAID { get; set; }
        public int Book_UploaderUserID { get; set; }
       // [Display(Name = "نمایش در جستجو میهمان ها")]
        [Display(Name = " ")]
        public bool Book_Publish { get; set; }
    }

    [MetadataType(typeof(MetaData_Book))]
    public partial class Tbl_Book
    {

    }
}