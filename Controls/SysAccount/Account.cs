using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using CommonLib;
using System.Web;
using BLL;
using Model.Model4View;
using Utility.Menu;

namespace Controls.SysAccount
{
    public class Account
    {
        #region 登录
        public static string GetPassword(string ip)
        {
            string md5 = CommonLib.Helper.Md5Hash(ip);
            return CommonLib.Helper.Md5Hash(md5.Substring(3, 20));
        }

        //public static int UsrLogin()
        //{

        //}

        #endregion

        #region 修改密码

        public static string ChangePwd(string newPwd, string oldPwd, string id)
        {
            string newVal = CommonLib.Helper.Md5Hash(newPwd);
            string oldVal = CommonLib.Helper.Md5Hash(oldPwd);
            int uid = int.Parse(id);

            int result = Sys_Manage_UserBLL.UpdatePassWordEmployId(uid, oldVal, newVal);

            if (result == 1)
            {
                return "<script>$(document).ready(function(){alert('修改成功！')}); </script>";
            }
            else
            {
                return "<script>$(document).ready(function(){alert('修改失败！')}); </script>";
            }
        }

        #endregion

        #region 登录日志

        public static List<dynamic> GetLoginLogList(int pageIndex, int pageSize, string where)
        {
            return Sys_ManageLogBLL.GetLog(pageIndex, pageSize, where);
        }

        public static int GetLogListCount(string where)
        {
            return Sys_ManageLogBLL.GetLogListCount(where);
        }

        #endregion

        #region 店铺审核

        /// <summary>
        /// 返回未审核的店铺列表
        /// </summary>
        /// <returns></returns>
        public static List<UncheckedShopModel> GetUncheckedShopList(DateTime regTime)
        {
            return T_AccountBLL.GetUncheckedShopList(regTime);
        }

        /// <summary>
        /// 审核店铺信息
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="type">0-邮件|1-手机</param>
        /// <returns></returns>
        public static int CheckShop(int accid, int type)
        {
            return T_AccountBLL.CheckShop(accid, type);
        }

        /// <summary>
        ///  在审核店铺时删除店铺
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static int DeleteShopDuringChecking(int accid)
        {
            return T_AccountBLL.DeleteShopDuringChecking(accid);
        }

        #endregion


        #region 账号管理
        /// <summary>
        /// 得到所有账号
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="uName"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetManageUserList(int pageIndex, int pageSize,string uName)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();

            List<DapperWhere> sqlWhere = new List<DapperWhere>();

            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.Sys_Manage_UserBaseBLL.GetCount(sqlWhere);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }


            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
          
            List<SysManageUserBase> baseList = BLL.Base.Sys_Manage_UserBaseBLL.GetList<SysManageUserBase>(pageIndex, pageSize, "Id,UserName,P_session,LoginCounter,LastLoginTime,phone,name,[state],WeixinOpenid", sqlWhere, " id desc");

            foreach (SysManageUserBase itemList in baseList)
            {
                itemList.P_sesionName = Enum.GetName(typeof(Model.Enum.ManageUserEnum.Session), itemList.P_session);
            }

            list["listData"] = baseList;
            return list;
        }

        /// <summary>
        /// 关闭账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CloseAccount(int id)
        {
            return Sys_Manage_UserBLL.CloseAccount(id);
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
        public static int AddManageUser(string account, string pw, string name, string phone, int session)
        {
            Sys_Manage_User model = new Sys_Manage_User();
            model.UserName = account;
            model.PassWord = CommonLib.Helper.Md5Hash(pw); 
            model.P_session = session;
            model.LoginCounter = 0;
            model.LastLoginTime = DateTime.Now;
            model.phone = phone;
            model.name = name;
            model.simcard = "";
            model.state = 1;
            model.WeiXinType = 0;
            model.Id = BLL.Base.Sys_Manage_UserBaseBLL.Add(model);
            return model.Id;
        }

        #endregion


        #region 修改权限

        /// <summary>
        /// 得到所有菜单权限
        /// </summary>
        /// <returns></returns>
        public static UserMenu GetAllMenu()
        {

            MenuDataList mDL = new MenuDataList();
            Dictionary<int, MenuList> AllMenu = mDL.GetAllList();

            Dictionary<string, UserMenuItem> LeftMenu = new Dictionary<string, UserMenuItem>();
            Dictionary<string, UserMenuItem> FunctionMap = new Dictionary<string, UserMenuItem>();







            foreach (KeyValuePair<int, MenuList> itemKey in AllMenu)
            {
                MenuList item = itemKey.Value;

                UserMenuItem umI = new UserMenuItem();
                umI.MenuId = item.menuId;
                umI.MenuTitle = item.menuName;
                umI.MenuUrl = "javascript:void(0)";

                if (item.menuType == 1)
                {
                    if (!LeftMenu.ContainsKey(item.superiorMenu))
                    {
                        UserMenuItem superiorMenu = new UserMenuItem();
                        superiorMenu.ItemList = new List<UserMenuItem>();
                        superiorMenu.MenuTitle = item.superiorMenu;
                        superiorMenu.MenuUrl = "javascript:void(0)";
                        superiorMenu.MenuIcon = mDL.GetIco(item.superiorMenu);
                        LeftMenu[item.superiorMenu] = superiorMenu;
                    }
                    LeftMenu[item.superiorMenu].ItemList.Add(umI);
                }
                else
                {
                    if (!FunctionMap.ContainsKey(item.superiorMenu))
                    {
                        UserMenuItem superiorMenu = new UserMenuItem();
                        superiorMenu.ItemList = new List<UserMenuItem>();
                        superiorMenu.MenuTitle = item.superiorMenu;
                        superiorMenu.MenuUrl = "javascript:void(0)";
                        superiorMenu.MenuIcon = mDL.GetIco(item.superiorMenu);
                        FunctionMap[item.superiorMenu] = superiorMenu;
                    }
                    FunctionMap[item.superiorMenu].ItemList.Add(umI);
                }

            }

            UserMenu um = new UserMenu();
            um.LeftMenu = LeftMenu.Values.ToList();
            um.FunctionMap = FunctionMap.Values.ToList();
            return um;
        }

        public static string GetUserMenuIds(int uid)
        {
            var muM = Sys_Manage_UserBLL.GetManageUserModel(uid);
           if (muM.MenuPermission != null)
           {
               return muM.MenuPermission;
           }
           else
           {
               MenuControls mc = new MenuControls();
               List<int> list = mc.GetDepartmentMenuId(muM.PowerSession);

               string l = "";
               foreach (int i in list)
               {
                   l += "," + i.ToString();
               }
               return l.Trim(',');
           }
        }
        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public static bool UpdateUserMenu(int uid, string menuIds)
        {
            return Sys_Manage_UserBLL.UpdateUserMenu(uid, menuIds);
        }

        #endregion
    }
}
