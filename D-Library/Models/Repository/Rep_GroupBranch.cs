using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            foreach (var itemG in db.Tbl_Group)
            {
                foreach (var itemB in itemG.Tbl_branch)
                {
                    t.Add(new DropDownModel(itemB.branch_ID, string.Format("{0} - {1}", itemG.Group_Name, itemB.branch_Name)));
                }

            }

            return t;
        }

        public IEnumerable<SelectListItem> Get_AllBranchSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in db.Tbl_branch)
            {
                list.Add(new SelectListItem() { Value = item.branch_ID.ToString(), Text = item.branch_Name });
            }

            return list.AsEnumerable();
        }

        public IEnumerable<SelectListItem> Get_AllGroupSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in db.Tbl_Group)
            {
                list.Add(new SelectListItem() { Value = item.Group_ID.ToString(), Text = item.Group_Name });
            }

            return list.AsEnumerable();

        }
    }


}