using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace D_Library.Models.Repository
{
    public class DropDownModel
    {
        public DropDownModel()
        {


        }

        public DropDownModel(int ID, string Name)
        {
            this.id = ID;
            this.name = Name;


        }


        public int id { get; set; }
        public string name { get; set; }
    }
}