using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class BookTagsModel
    {
      
        public int ID { get; set; }

        [Display(Name = "برچسب")]
        public string Tags { get; set; }
    }
}