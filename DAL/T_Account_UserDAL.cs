using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 店铺登录账号信息
    /// </summary>
    public class T_Account_UserDAL : Base.T_Account_UserBaseDAL
    {

        /// <summary>
        /// 获取店铺登录账号信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public List<T_Account_UserBasic> GetAccountUserBasic (int accid){

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select id,accountid,account,name,grade,PhoneNumber,UserEmail from i200.dbo.T_Account_User where accountid=@accid order by id desc; ");
            return DapperHelper.Query<T_Account_UserBasic>(strSql.ToString(), new { accid = accid }).ToList();
        }
         

    }
}
