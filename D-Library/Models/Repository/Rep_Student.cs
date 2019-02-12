using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;
using System.Web.Mvc;

namespace D_Library.Models.Repository
{
    public class Rep_Student
    {
        ELEntities db = new ELEntities();

        public Rep_Student()
        {

        }

        public IEnumerable<SelectListItem> Get_AllSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var q = from a in db.Tbl_BaseRolesPermission where (a.Tbl_Permission.Permission_Name == "Student") select a;
            //db.Tbl_Login.Select(a => a.Tbl_BaseRole.Tbl_BaseRolesPermission);

            foreach (var item in q)
            {
                var p = from b in db.Tbl_Login where (b.Login_BaseRoleID == item.BRP_BaseRoleID) select b;

                if (p != null)
                {
                    foreach (var Student in p)
                    {
                        if (!Student.Login_RegisterActive)
                        {
                            list.Add(new SelectListItem() { Value = Student.Login_UserID.ToString(), Text = Student.Tbl_User.User_Name + " " + Student.Tbl_User.User_Family });

                        }
                    }

                }

            }

            return list.AsEnumerable();
        }
    }
}