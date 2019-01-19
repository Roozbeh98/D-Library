using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D_Library.Models.Domins
{
    internal class MetaData_Login
    {

        public int Login_ID { get; set; }
        public string Login_PasswordSalt { get; set; }
        public string Login_PasswordHush { get; set; }
        public Nullable<int> Login_UserID { get; set; }
        public string Login_UserName { get; set; }
        public bool Login_RegisterActive { get; set; }
        public Nullable<int> Login_RegisterID { get; set; }
        public int Login_BaseRoleID { get; set; }
        public bool Login_CustomRole { get; set; }
        public bool Login_UserActive { get; set; }
    }

    [MetadataType(typeof(MetaData_Login))]
    public partial class Tbl_Login
    {

    }
}