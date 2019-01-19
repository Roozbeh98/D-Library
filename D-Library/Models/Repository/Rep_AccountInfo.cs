using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D_Library.Models.Model;
using D_Library.Models.Domins;

namespace D_Library.Models.Repository
{
    public class Rep_AccountInfo
    {
        ELEntities db = new ELEntities();

        public Rep_AccountInfo()
        {

        }

        public AccountInfoModel GetInfoForNavbar(string Username)
        {

            var q = (from a in db.Tbl_Login where (a.Login_UserName == Username) select a).SingleOrDefault();

            if (q != null)
            {
                AccountInfoModel infoModel = new AccountInfoModel();
                infoModel.Name = q.Tbl_User.User_Name + " " + q.Tbl_User.User_Family;
                infoModel.Role = q.Tbl_BaseRole.BaseRole_Titel;
                return infoModel;
            }
            else
            {
                return null;

            }

        }

    }
}