using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Model;

namespace DAL
{
    public class Sys_Manage_UserDAL
    {
        /// <summary>
        /// 用户登录判断
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">密码(MD5)</param>
        /// <param name="sIP">浏览器IP</param>
        /// <param name="sBrowser">浏览器类型</param>
        /// <returns></returns>
        public ManageUserModel Login(string userName, string userPwd, string sIP, string sBrowser)
        {
            Sys_Manage_User manageUser = new Sys_Manage_User();
            ManageUserModel AccModel = new ManageUserModel();
            AccModel.LoginStatus = false;
            string sPassWord = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top(1) Id,UserName,P_session,LoginCounter,phone,name,PassWord,MenuPermission from Sys_Manage_User where [state]=1 and UserName=@userName;");

            manageUser = DapperHelper.GetModel<Sys_Manage_User>(strSql.ToString(), new { userName = userName });

            if (manageUser != null)
            {
                AccModel.UserID = manageUser.Id;
                AccModel.UserName = manageUser.UserName;
                AccModel.PowerSession = Convert.ToInt32(manageUser.P_session);
                AccModel.LoginCnt = Convert.ToInt32(manageUser.LoginCounter);
                AccModel.Phone = manageUser.phone;
                AccModel.Name = manageUser.name;
                AccModel.MenuPermission = manageUser.MenuPermission;
                sPassWord = manageUser.PassWord;
                //判断是否一致
                if (sPassWord == userPwd)
                {
                    //登录成功
                    AccModel.LoginStatus = true;
                    Sys_ManageLogDAL logDal = new Sys_ManageLogDAL();
                    logDal.LoginLog(AccModel.UserID, sIP, sBrowser);
                }
            }
            return AccModel;

        }

        /// <summary>
        /// 更新密码
        /// </summary>
        public int UpdatePassWordEmployId(int id, string oldPass, string newPass)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" if(EXISTS(select * from Sys_Manage_User where Id=@id and PassWord=@oldPass)) ");

            strSql.Append(" begin ");
            strSql.Append(" update Sys_Manage_User set PassWord=@newPass where Id=@id; ");
            strSql.Append(" select 2; ");
            strSql.Append(" end ");
            strSql.Append(" else ");
            strSql.Append(" select 1;");

            int result = 0;
            try
            {
                result = DapperHelper.Execute(strSql.ToString(), new { id = id, oldPass = oldPass, newPass = newPass });

            }
            catch (Exception)
            {

                return 0;
            }

            return result;
        }

        /// <summary>
        /// 根据人员ID 得到名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetManageUserNameById(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select name from Sys_Manage_User where Id=@id");
            object robj = DapperHelper.ExecuteScalar(strSql.ToString(), new { id = id });
            if (robj != null)
            {
                return robj.ToString();
            }
            else
            {
                return "";
            }
        }

        public List<int> GetIdByName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            //DapperWhere dw = new DapperWhere("name", name, "name like '%'+ @name +'%'");
            strSql.Append("select Id from Sys_Manage_User where name like '%'+ @name +'%'");
            List<int> idList = DapperHelper.Query<int>(strSql.ToString(), new { name = name }).ToList();

            return idList;
        }

        /// <summary>
        /// 得到相关 操作人员信息
        /// </summary>
        /// <param name="column"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<dynamic> GetList(string column, string strWhere)
        {
            List<dynamic> dynamicList = new List<dynamic>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select @column from Sys_Manage_User ");
            if (strWhere.Length > 0)
            {
                strSql.Append(" where @where");
            }

            try
            {
                dynamicList = HelperForFrontend.Query<dynamic>(strSql.ToString(), new
                {
                    column = column,
                    where = strWhere
                }).ToList();
            }
            catch (Exception ex)
            {
                dynamicList = null;
            }

            return dynamicList;
        }

        public List<RemindUsr> GetAllMgrUsr()
        {
            List<RemindUsr> list = new List<RemindUsr>();
            Dictionary<int, string> dic = new Dictionary<int, string>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,name from Sys_Manage_User where state=1;");

            list = DapperHelper.Query<RemindUsr>(strSql.ToString()).ToList();

            return list;
        }
        /// <summary>
        /// 关闭账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CloseAccount(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_Manage_User set state=0,PassWord='og08qcgkmmndh70sfrq22t0059' where id=@uid;");
            int r = DapperHelper.Execute(strSql.ToString(), new { uid = id });
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个账号信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ManageUserModel GetManageUserModel(int uid)
        {

            Sys_Manage_User manageUser = new Sys_Manage_User();
            ManageUserModel AccModel = new ManageUserModel();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top(1) Id,UserName,P_session,LoginCounter,phone,name,PassWord,MenuPermission from Sys_Manage_User where [state]=1 and Id=@uid;");

            manageUser = DapperHelper.GetModel<Sys_Manage_User>(strSql.ToString(), new { uid = uid });

            if (manageUser != null)
            {
                AccModel.UserID = manageUser.Id;
                AccModel.UserName = manageUser.UserName;
                AccModel.PowerSession = Convert.ToInt32(manageUser.P_session);
                AccModel.LoginCnt = Convert.ToInt32(manageUser.LoginCounter);
                AccModel.Phone = manageUser.phone;
                AccModel.Name = manageUser.name;
                AccModel.MenuPermission = manageUser.MenuPermission;
            }
            return AccModel;
        }

        /// <summary>
        /// 更新菜单 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public bool UpdateUserMenu(int uid,string menuIds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_Manage_User set MenuPermission=@menu where id=@id;");
            int r = DapperHelper.Execute(strSql.ToString(), new { id = uid, menu = menuIds });
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
