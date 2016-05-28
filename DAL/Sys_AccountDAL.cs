using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 店铺收集信息
    /// </summary>
   public  class Sys_AccountDAL:Base.Sys_AccountBaseDAL
    {

        /// <summary>
        /// 根据列名更新信息
        /// </summary>
        /// <param name="accid">店铺ID</param>
        /// <param name="Column">列名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool UpdateColumn(int accid, string Column, object value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" if(exists(select * from Sys_Account where accid=@accid)) ");
            strSql.Append(" begin ");
            strSql.Append(" 	update Sys_Account set " + Column + "=@value where accid=@accid; ");
            strSql.Append(" 	select 1; ");
            strSql.Append(" end ");
            strSql.Append(" else ");
            strSql.Append(" begin ");
            strSql.Append(" 	insert into Sys_Account (accid," + Column + ")values(@accid,@value); ");
            strSql.Append(" 	select 1; ");
            strSql.Append(" end ");

            object rid = DapperHelper.ExecuteScalar(strSql.ToString(), new { value = value, accid = accid });
            if (rid != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       /// <summary>
       /// 得到一个店铺收集信息
       /// </summary>
       /// <param name="accid"></param>
       /// <returns></returns>
        public SysAccountInfo GetAccountInfo(int accid)
        {
            string column = "accid,a_QQ sys_qq,a_WeiXin sys_weixin,a_Tel sys_tel,a_ShopSize sys_shopsize,a_Operate sys_operate,a_Address sys_address,a_Industry sys_industry,a_Name sys_name,a_IdentityNumber sys_indentity,a_Duration sys_duration,a_OtherSoftware sys_software,a_Remark sys_remark,feedbackTel feedbackTel,feedbackQQ feedbackQQ,sysAddress aboutAddress";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ");
            strSql.Append(column);
            strSql.Append(" from Sys_Account where accid=@accid");
            return DapperHelper.GetModel<SysAccountInfo>(strSql.ToString(), new { accid = accid });

        }


    }
}
