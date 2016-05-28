using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
using Utility;

namespace Controls.AlphaApply
{
    public static class AlphaApply
    {
        public static string GetAlphaApplyRecord(int pageIndex, int alphaStatus, string userPhone, string accId, string start, string end, string alphaVersion)
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

            if (alphaStatus != -99)
            {
                strWhere += " status=" + alphaStatus.ToString() + " and ";
            }
            if (userPhone != "")
            {
                strWhere += " userPhone=" + userPhone + " and ";
            }
            if (accId != "")
            {
                strWhere += " userAccId=" + accId + " and ";
            }
            if (alphaVersion != "")
            {
                strWhere += " alphaVersion=" + alphaVersion + " and ";
            }
            if (strWhere.Length > 0)
            {
                strWhere = strWhere.Substring(0, strWhere.LastIndexOf('a'));
            }

            int pageCount = AlphaApplyBLL.GetPageCount(strWhere);
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

            List<AlphaApplyModel> listitem = AlphaApplyBLL.GetPage(pageIndex, pageSize, Column, strWhere);
            dic["list"] = CommonLib.Helper.JsonSerializeObject(listitem, "yyyy-MM-dd HH:mm:ss");
            return CommonLib.Helper.JsonSerializeObject(dic);
        }
        public static string UpdateWithdrawalStatus(int id, int status, string operatorIP, int operatorUserId)
        {
            string iResult = "0";
            bool isSaved = AlphaApplyBLL.UpdateAlphaApplyStatus(id, status, operatorIP, operatorUserId);
            if (isSaved)
            {
                iResult = "1";
            }
            return iResult;
        }
    }
}
