using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 店铺基本配置信息
    /// </summary>
    public class T_BusinessDAL : Base.T_BusinessBaseDAL
    {


        /// <summary>
        /// 得到短信配置
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public T_AccountConfig.SmsBillingConfig GetSmsBillingConfig(int accid)
        {
            T_AccountConfig.SmsBillingConfig model = new T_AccountConfig.SmsBillingConfig();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select SmsBilling from i200.dbo.T_Business where  accountid=@accid; ");

            string smsBilling = (string)DapperHelper.ExecuteScalar(strSql.ToString(), new { accid = accid });

            if (smsBilling.Length > 0)
            {
                try
                {
                    model = CommonLib.Helper.JsonDeserializeObject<T_AccountConfig.SmsBillingConfig>(smsBilling);
                }
                catch (Exception)
                {
                   
                }
            }
            return model;
        }
    }
}
