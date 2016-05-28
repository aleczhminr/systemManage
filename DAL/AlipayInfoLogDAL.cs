using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AlipayInfoLogDAL
    {
        #region  AddAlipayInfoLog     添加日志

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddAlipayInfoLogBase(T_AlipayInfoLogModel model)
        {
            bool bResult = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_AlipayInfoLog(");
            strSql.Append("accId,createTime,columnName,oldValue,nowValue,lgUserId,lgUserIp");
            strSql.Append(") values (");
            strSql.Append("@accId,@createTime,@columnName,@oldValue,@nowValue,@lgUserId,@lgUserIp");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            object ro = HelperForFrontend.ExecuteScalar(strSql.ToString(), model);
            if (ro != null)
            {
                bResult = true;
            }
            return bResult;
        }

        #endregion
    }
}
