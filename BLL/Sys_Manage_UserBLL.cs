using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class Sys_Manage_UserBLL
    {
        /// <summary>
        /// 用户登录判断
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">密码(MD5)</param>
        /// <param name="sIP">浏览器IP</param>
        /// <param name="sBrowser">浏览器类型</param>
        /// <returns></returns>
        public static ManageUserModel Login(string userName, string userPwd, string sIP, string sBrowser)
        {
            Sys_Manage_UserDAL dal = new Sys_Manage_UserDAL();
            return dal.Login(userName, userPwd, sIP, sBrowser);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        public static int UpdatePassWordEmployId(int id, string oldPass, string newPass)
        {
            Sys_Manage_UserDAL dal = new Sys_Manage_UserDAL();
            return dal.UpdatePassWordEmployId(id, oldPass, newPass);
        }
       /// <summary>
       /// 根据人员ID 得到名称
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public static string GetManageUserNameById(int id)
        {
            Sys_Manage_UserDAL dal = new Sys_Manage_UserDAL();
            return dal.GetManageUserNameById(id);
        }

        public static List<int> GetIdByName(string name)
        {
            Sys_Manage_UserDAL dal = new Sys_Manage_UserDAL();
            return dal.GetIdByName(name);
        }
        /// <summary>
        /// 得到所有的人员
        /// </summary>
        /// <returns></returns>
        public static List<RemindUsr> GetRemindUsr()
        {
            Sys_Manage_UserDAL ManageUserDal = new Sys_Manage_UserDAL();

            return ManageUserDal.GetAllMgrUsr();
        }
        /// <summary>
        /// 关闭账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CloseAccount(int id)
        {
            Sys_Manage_UserDAL dal = new Sys_Manage_UserDAL();
            return dal.CloseAccount(id);
        }
        /// <summary>
        /// 得到一个账号信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static ManageUserModel GetManageUserModel(int uid)
        {
            Sys_Manage_UserDAL dal = new Sys_Manage_UserDAL();
            return dal.GetManageUserModel(uid);
        }
        /// <summary>
        /// 更新菜单 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public static bool UpdateUserMenu(int uid, string menuIds)
        {
            Sys_Manage_UserDAL dal = new Sys_Manage_UserDAL();
            return dal.UpdateUserMenu(uid, menuIds);
        }
    }
}
