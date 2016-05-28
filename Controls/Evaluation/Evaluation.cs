using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Evaluation
{
    public static class Evaluation
    {
        public static string getEvaluationRecord(int pageIndex, int productType, int displayType, string busId, string evaluationID, string accId, string start, string end)
        {
            string Column = string.Empty;
            string strWhere = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"RowCount",""},
                {"PageCount",""},
                {"list",""}
            };
            if (Convert.ToDateTime(end) > Convert.ToDateTime(start))
            {
                if (start != "")
                {
                    DateTime stTime = Convert.ToDateTime(start);
                    strWhere += " createTime >='" + stTime.ToString("yyyy-MM-dd") + "' and";
                }
                if (end != "")
                {
                    DateTime edTime = Convert.ToDateTime(end);
                    strWhere += " createTime <'" + edTime.AddDays(1).Date.ToString("yyyy-MM-dd") + "' and";
                }
            }

            if (productType != -99)
            {
                strWhere += " productType=" + productType.ToString() + " and ";
            }
            if (displayType != -99)
            {
                strWhere += " isDisplay=" + displayType + " and ";
            }
            if (busId != "")
            {
                strWhere += " productID=" + busId + " and ";
            }
            if (accId != "")
            {
                strWhere += " accId=" + accId + " and ";
            }
            if (evaluationID != "")
            {
                strWhere += " id=" + evaluationID + " and ";
            }
            if (strWhere.Length > 0)
            {
                strWhere = strWhere.Substring(0, strWhere.LastIndexOf('a'));
            }

            int pageCount = T_Product_EvaluationBLL.GetPageCount(strWhere);
            int pageSize = 15;
            if (pageCount != 0)
            {
                dic["RowCount"] = pageCount.ToString();
            }
            else
            {
                dic["RowCount"] = "0";
            }
            if (pageCount > 0)
            {
                dic["PageCount"] = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(pageCount) / Convert.ToDecimal(pageSize))).ToString();
            }
            else
            {
                dic["PageCount"] = "0";
            }

            List<EvaluationModel> listitem = T_Product_EvaluationBLL.GetPage(pageIndex, pageSize, Column, strWhere);
            dic["list"] = CommonLib.Helper.JsonSerializeObject(listitem, "yyyy-MM-dd HH:mm:ss");
            return CommonLib.Helper.JsonSerializeObject(dic);
        }

        public static string UpdateEvaluation(int evaluationid, int status)
        {
            string iResult = "0";
            bool isSaved = T_Product_EvaluationBLL.UpdateEvaluation(evaluationid, status);
            if (isSaved)
            {
                iResult = "1";
            }
            return iResult;
        }
    }



}
