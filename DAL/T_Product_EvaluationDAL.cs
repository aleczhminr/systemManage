using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class T_Product_EvaluationDAL
    {
        /// <summary>
        /// 获取总行数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetPageCount(string strWhere)
        {
            int count = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from T_Product_Evaluation");

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
        /// 获取记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Column"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<EvaluationModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            List<EvaluationModel> listitem = new List<EvaluationModel>();
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
                Column = "id,accId,dbo.GetAccountName(accId) accountName,productID,productType,content,isDisplay,isDelete,operatorIP,operatorUserId,createTime,editVale,remark,score";
            }
            strSql.Append(" ," + Column + " ");
            strSql.Append(" from T_Product_Evaluation  ");
            if (strWhere.Length > 0)
            {
                strSql.Append(" where ");
            }
            strSql.Append(strWhere);
            strSql.Append(" ) t ");
            strSql.Append(" where t.rowNumber between @bgNumber and @edNumber; ");
            try
            {
                listitem = HelperForFrontend.Query<EvaluationModel>(strSql.ToString(), new
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

        public bool UpdateEvaluation(int evaluationid, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update T_Product_Evaluation ");
            strSql.Append(" set isDisplay=@status ");
            strSql.Append(" where id=@evaluationid ");
            int rows = HelperForFrontend.Execute(strSql.ToString(), new
            {
                evaluationid = evaluationid,
                status = status
            });
            if (rows > 0)
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
