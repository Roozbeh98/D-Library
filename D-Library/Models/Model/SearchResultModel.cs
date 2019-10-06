using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class SearchResultModel
    {
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایید")]
        [MinLength(3, ErrorMessage = "حداقل 3 حرف را وارد نمایید")]
        public string looking { get; set; }
        public IEnumerable<SearchResultItemModel> Items { get; set; }
    }
}