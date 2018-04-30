using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDI.Models
{
    public class RoleModel
    {
        public int RoleId
        {
            get;
            set;
        }
        public string RoleName
        {
            get;
            set;
        }
        public double Salary
        {
            get;
            set;
        }
        public List<RoleModel> listRole
        {
            get;
            set;
        }
        //This property will be used to populate employee dropdownlist  
        public IEnumerable<SelectListItem> RoleListItems
        {
            get
            {
                return new SelectList(listRole, "RoleId", "RoleName");
            }
        }


        public int MenuId
        {
            get;
            set;
        }
        public string MenuName
        {
            get;
            set;
        }

        public List<RoleModel> listMenu
        {
            get;
            set;
        }
        //This property will be used to populate employee dropdownlist  
        public IEnumerable<SelectListItem> MenuListItems
        {
            get
            {
                return new SelectList(listMenu, "MenuId", "MenuName");
            }
        }

    }
}