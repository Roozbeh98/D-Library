﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace D_Library.Models.Model
{
    public class SearchResultItemModel
    {
        public SearchResultItemModel(int id , string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public int ID { get; set; }
        public string Name { get; set; }

    }
}