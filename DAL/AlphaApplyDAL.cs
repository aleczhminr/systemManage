using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Utility;


namespace DAL
{
    public class AlphaApplyDAL
    {
        /// <summary>
        /// 获得数据总行数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetPageCount(string strWhere)
        {
            int count = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from T_AlphaApply ");

            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);
            try
            {
                count = HelperForFrontend.Query<int>(strSql.ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                count = 0;
            }
            return count;
        }
        /// <summary>
        /// 获取前几行数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Column"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<AlphaApplyModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            List<AlphaApplyModel> listitem = new List<AlphaApplyModel>();
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 20 : pageSize;
            //页数计算
            int bgNumber = ((pageIndex - 1) * pageSize) + 1;
            int edNumber = (pageIndex) * pageSize;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (");
            strSql.Append("select ROW_NUMBER() over (order by id desc) as rowNumber ");
            if (Column.Length == 0)
            {
                Column = "id,userAccId,userJob,userPhone,userAccName,userAlphaClient,createTime,status,alphaVersion";
            }
            strSql.Append(" ," + Column + " ");
            strSql.Append(" from T_AlphaApply ");
            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);
            strSql.Append(" ) t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber; ");
            try
            {
                listitem = HelperForFrontend.Query<AlphaApplyModel>(strSql.ToString(), new
                {
                    bgNumber = bgNumber,
                    edNumber = edNumber
                }).ToList();
            }
            catch (Exception ex)
            {
                listitem = null;
            }
            return listitem;
        }

        public bool UpdateAlphaApplyStatus(int id, int status, string operatorIP, int operatorUserId)
        {
            StringBuilder strSql = new StringBuilder();
            int rows = 0;
            bool iResult = false;
            strSql.Append(" update T_AlphaApply ");
            strSql.Append(" set status=@status ");
            strSql.Append(" ,operatorIP=@operatorIP ");
            strSql.Append(" ,operatorUserId=@operatorUserId ");
            strSql.Append(" where id=@id ");
            rows = HelperForFrontend.Execute(strSql.ToString(), new
            {
                id = id,
                status = status,
                operatorIP = operatorIP,
                operatorUserId = operatorUserId
            });
            if (rows > 0)
            {
                iResult = true;
            }
            return iResult;
        }
    }
}
