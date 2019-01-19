using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;

namespace D_Library.Models.Repository
{
    public class Rep_Professor
    {
        ELEntities db = new ELEntities();

        public Rep_Professor()
        {

        }

        public List<DropDownModel> GetAllProfessor()
        {
            List<DropDownModel> t = new List<DropDownModel>();

            var q = from a in db.Tbl_BaseRolesPermission where (a.Tbl_Permission.Permission_Name == "Professor") select a ;
                //db.Tbl_Login.Select(a => a.Tbl_BaseRole.Tbl_BaseRolesPermission);

            foreach (var item in q)
            {
                var p =from b in db.Tbl_Login where(b.Login_BaseRoleID == item.BRP_BaseRoleID) select b;

                if (p != null)
                {
                    foreach (var master in p)
                    {
                        t.Add(new DropDownModel((int)master.Login_UserID, master.Tbl_User.User_Name +" "+ master.Tbl_User.User_Family));
                    }

                }
               
            }


            return t;
        }

    }
}