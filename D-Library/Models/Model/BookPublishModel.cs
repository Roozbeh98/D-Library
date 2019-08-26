using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class BookPublishModel
    {
        public int ID { get; set; }
        [Display(Name = " ")]
        public bool GusetSearch { get; set; }
        [Display(Name = " ")]
        public bool LocalAcsses { get; set; }
        [Display(Name = " ")]
        public bool GlobalAcsses { get; set; }
        public bool Publish { get; set; }

       
    }
}