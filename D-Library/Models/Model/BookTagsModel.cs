using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;
namespace D_Library.Models.Model
{
    public class BookTagsModel
    {
      
        public int ID { get; set; }

        public IEnumerable<Tbl_Tag> TagCollection { get; set; }

        [Display(Name = "برچسب")]
        public string[] Tags { get; set; }
    }
}