using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D_Library.Models.Domins;

namespace D_Library.Models.Repository
{
    public class Rep_GroupBranch
    {
        ELEntities db = new ELEntities();

        public Rep_GroupBranch()
        {

        }

        public List<DropDownModel> Get()
        {
            List<DropDownModel> t = new List<DropDownModel>();

            //var q = db.Tbl_Group.OrderBy(a => a.Group_ID).ToList();

            //foreach (var item in q)
            //{
            //    var qb = (from a in db.Tbl_branch where a.branch_GroupID == item.Group_ID select a).ToList();
            //    foreach (var itemqb in qb)
            //    {
            //        t.Add(new DropDownModel(itemqb.branch_ID, string.Format("{0} - {1}", item.Group_Name, itemqb.branch_Name)));
            //    }

            //}

           

            foreach (var itemG in db.Tbl_Group)
            {
                foreach (var itemB in itemG.Tbl_branch)
                {
                    t.Add(new DropDownModel(itemB.branch_ID, string.Format("{0} - {1}", itemG.Group_Name, itemB.branch_Name)));
                }

            }

            return t;
        }


    }


}