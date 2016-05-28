using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationPlatform.Controllers
{
    public class UserMenuController : Controller
    {
        //
        // GET: /UserMenu/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserMenu()
        {
            Utility.Menu.UserMenu um = new Utility.Menu.UserMenu();
            if (Session["logUser"] != null)
            {
                ManageUserModel user = (ManageUserModel)Session["logUser"];
                if (Session["userMenu"] != null)
                {
                    um = (Utility.Menu.UserMenu)Session["userMenu"];
                }
                else
                {
                    Utility.Menu.MenuControls menuControls = new Utility.Menu.MenuControls();
                    string menuPermission = user.MenuPermission;
                    if (menuPermission != null && menuPermission.Length > 0)
                    {
                        List<int> menuIdList = new List<int>();
                        foreach (string m in menuPermission.Trim(',').Split(','))
                        {
                            int mid = 0;
                            if (int.TryParse(m, out mid))
                            {
                                menuIdList.Add(mid);
                            }
                        }
                        um = menuControls.GetMenu(menuIdList.ToArray());
                    }
                    else
                    {
                        List<int> menuIdList = menuControls.GetDepartmentMenuId(user.PowerSession);
                        um = menuControls.GetMenu(menuIdList.ToArray());
                    }
                }
            }

            return PartialView(um);
        }
    }
}