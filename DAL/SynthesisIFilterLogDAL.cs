using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    ///检索日志	
    /// </summary>
    public partial class SynthesisIFilterLogDAL
    {

        public int Add(SynthesisIFilterLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into I200_Log.dbo.SynthesisIFilterLog(");
            strSql.Append("strSql,inserTime,inserName,resultData,verification,userid");
            strSql.Append(") values (");
            strSql.Append("@strSql,@inserTime,@inserName,@resultData,@verification,@userid");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            object obj = DapperHelper.ExecuteScalar(strSql.ToString(), model);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }
        }

        /// <summary>
        /// 得到统计中的 会员
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="verif"></param>
        /// <returns></returns>
        public string GetAccountList(int uid, string verif)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select top 1 resultData from I200_Log.dbo.SynthesisIFilterLog where ");
            strSql.Append(" verification=@verfica and userid=@userid  ");
            strSql.Append("  and DATEDIFF(day,inserTime,GETDATE())=0 order by id desc ; ");

            object r = DapperHelper.ExecuteScalar(strSql.ToString(), new
            {
                userid = uid,
                verfica = verif
            });
            string dataAccount = "";
            if (r != null)
            {
                dataAccount = r.ToString();
            }

            return dataAccount;
        }
    }
}
