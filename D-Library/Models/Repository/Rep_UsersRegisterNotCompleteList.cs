using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;
using D_Library.Models.Model;
namespace D_Library.Models.Repository
{
    public class Rep_UsersRegisterNotCompleteList
    {
        ELEntities db = new ELEntities();
        public Rep_UsersRegisterNotCompleteList()
        {

        }

        public List<UsersRegisterNotCompleteListModel> GetList()
        {

            List<UsersRegisterNotCompleteListModel> tmp = new List<UsersRegisterNotCompleteListModel>();

            foreach (var item in db.Tbl_Login)
            {
                if (item.Login_RegisterActive)
                {
                    bool exp = false;

                    if (item.Tbl_RegisterCode.RegisterCode_Date.AddDays(5) > DateTime.Now)
                    {
                        exp = true;
                    }

                    tmp.Add(new UsersRegisterNotCompleteListModel(
                            item.Login_ID,
                            item.Login_UserName,
                            item.Tbl_BaseRole.BaseRole_Titel,
                            exp,
                            item.Tbl_RegisterCode.RegisterCode_Date.AddDays(5),
                            item.Tbl_RegisterCode.RegisterCode_Code
                            ));

                }
            }

            return tmp;
        }

    }
}