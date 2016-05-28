using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controls.SysAccount;
using Utility.Menu;
using Model;
namespace OperationPlatform.Controllers
{
    [OperationPlatform.App_Start.LoginAuthentication]
    public class SysManageController : Controller
    {
        //
        // GET: /SysManage/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserList()
        {
            ManageUserModel muM = (ManageUserModel)Session["logUser"];
            if (muM.PowerSession == 0 && muM.MenuPermission.IndexOf("501") > -1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }
        public ActionResult SetPermission(int id)
        {
            ManageUserModel muM = (ManageUserModel)Session["logUser"];
            UserMenu um = new UserMenu();
            if (muM.PowerSession == 0 && muM.MenuPermission.IndexOf("501") > -1)
            {
                um = Controls.SysAccount.Account.GetAllMenu();
            }
            ViewBag.AccId = id;
            return View(um);
        }
        /// <summary>
        /// 得到账号列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="uname"></param>
        /// <returns></returns>
        public string GetUserList(int page, string uname)
        {
            var list = Account.GetManageUserList(page, 15, uname);
            return CommonLib.Helper.JsonSerializeObject(list, "yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 关闭账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string CloseAccount(int id)
        {
            if (Controls.SysAccount.Account.CloseAccount(id))
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }
        /// <summary>
        /// 增加账号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pw"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public int AddAccount(string account, string pw, string name, string phone, int session)
        {
            if (account.Length < 1)
            {
                return 1;
            }
            if (pw.Length < 1)
            {
                return 2;
            }
            if (name.Length < 1)
            {
                return 3;
            }
            if (phone.Length < 1)
            {
                return 4;
            }
            return Controls.SysAccount.Account.AddManageUser(account, pw, name, phone, session);
        }

        public string GetUserM(int id)
        {
            ManageUserModel muM = (ManageUserModel)Session["logUser"];
            UserMenu um = new UserMenu();
            if (muM.PowerSession == 0 && muM.MenuPermission.IndexOf("501") > -1)
            {
                return Controls.SysAccount.Account.GetUserMenuIds(id);
            }
            else
            {
                return "";
            }
        }

        public string SaveMenu(int uid, string idList)
        {
            ManageUserModel muM = (ManageUserModel)Session["logUser"];
            UserMenu um = new UserMenu();
            if (muM.PowerSession == 0 && muM.MenuPermission.IndexOf("501") > -1)
            {
                if (uid < 1)
                {
                    return "uid";
                }
                if (idList.Length < 1)
                {
                    return "list";
                }
                if (Controls.SysAccount.Account.UpdateUserMenu(uid, idList))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "log";
            }
        }

    }
}