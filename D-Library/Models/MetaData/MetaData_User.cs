using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D_Library.Models.Domins
{
    internal class MetaData_User
    {

        public int User_ID { get; set; }
        public string User_Name { get; set; }
        public string User_Family { get; set; }
        public string User_Email { get; set; }
        public string User_Mobile { get; set; }
        public System.DateTime User_Date { get; set; }
        public Nullable<int> User_BranchID { get; set; }
        public bool User_SABAlloow { get; set; }
    }
    [MetadataType(typeof(MetaData_User))]
    public partial class Tbl_User
    {

    }

}