using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class T_PreFixVerDAL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.T_PreFixVer> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,preName,agentId,displayName,remark,operatorId,createDate ");
            strSql.Append(" FROM i200.dbo.T_PreFixVer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DapperHelper.Query<Model.T_PreFixVer>(strSql.ToString()).ToList();
        }
    }
}
