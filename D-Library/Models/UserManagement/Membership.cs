using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;

namespace D_Library.Models.UserManagement
{
    public class Membership
    {
        ELEntities db = new ELEntities();

        public Membership()
        {
           
        }
        public string Get_FullNameByID(int id)
        {
            var user = db.Tbl_User.Where(a => a.User_ID == id).SingleOrDefault();
            string fullname = string.Format("{0} {1}", user.User_Name, user.User_Family);

            return fullname;
        }

        public int Get_IDByUserName(string Username)
        {
            var id = db.Tbl_Login.Where(a => a.Login_UserName == Username).SingleOrDefault().Login_UserID;
            return (int)id;
        }

        public List<string> GetRoles(string Username)
        {
            List<string> Roles = new List<string>();

            var q = db.Tbl_Login.Where(a => a.Login_UserName == Username).FirstOrDefault();

            if (q != null)
            {
                if (!q.Login_CustomRole)
                {
                    var r = q.Tbl_BaseRole.Tbl_BaseRolesPermission.Select(a => a.Tbl_Permission.Permission_Name).ToList();

                    if (r.Count > 0)
                    {
                        Roles.AddRange(r);
                    }

                }
                else
                {
                    var r = q.Tbl_User.Tbl_UsersPermission.Select(a => a.Tbl_Permission.Permission_Name).ToList();
                    if (r.Count > 0)
                    {
                        Roles.AddRange(r);
                    }
                }

                if (Roles.Count > 0)
                {
                    return Roles;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

    }
}