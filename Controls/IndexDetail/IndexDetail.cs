using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;

namespace Controls.IndexDetail
{
    public static class IndexDetail
    {
        /// <summary>
        /// 返回当日登录用户ID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="index"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public static string GetLoginAccountId(int pageIndex, int index, string cnt, string order="")
        {
            string active = string.Empty;
            IndexDetailModel detailModel = new IndexDetailModel();

            switch (index)
            {
                case 1:
                    active = ">=5";
                    break;
                case 2:
                    active = "<0";
                    break;
                case 3:
                    active = "in (0,1)";
                    break;
            }

            int[] accId = DashBoardAnalyzeBLL.GetLoginAccountId(pageIndex,active);

            detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accId, order, pageIndex);
            detailModel.rowCount = int.Parse(cnt);
            detailModel.maxPage = detailModel.rowCount/15 + 1;

            return CommonLib.Helper.JsonSerializeObject(detailModel,"");
        }

        /// <summary>
        /// 返回活跃用户ID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="index"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public static string GetActiveAccountId(int pageIndex, int index, string cnt, string order = "")
        {
            IndexDetailModel detailModel = new IndexDetailModel();
            int[] accId = DashBoardAnalyzeBLL.GetActiveAccountId(pageIndex, index);

            detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accId, order, pageIndex);
            detailModel.rowCount = int.Parse(cnt);
            detailModel.maxPage = detailModel.rowCount / 15 + 1;

            return CommonLib.Helper.JsonSerializeObject(detailModel,"");
        }

        /// <summary>
        /// 返回今日所有付费用户ID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="index"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public static string GetPaidAllAccountId(int pageIndex, int index, string cnt, string order = "")
        {
            IndexDetailModel detailModel = new IndexDetailModel();
            int[] accId = DashBoardAnalyzeBLL.GetPaidAllAccountId(pageIndex, index);

            detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accId, order, pageIndex);
            detailModel.rowCount = int.Parse(cnt);
            detailModel.maxPage = detailModel.rowCount / 15 + 1;

            return CommonLib.Helper.JsonSerializeObject(detailModel, "");
        }

        /// <summary>
        /// 获取某个区域用户列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetLocationUsr(int pageIndex, string location, string order = "")
        {
            IndexDetailModel detailModel = new IndexDetailModel();
            int[] accId = DashBoardAnalyzeBLL.GetLocationAccId(pageIndex, location);

            //int[] accId = CommonLib.Helper.JsonDeserializeObject<List<int>>(dic["accidList"]).ToArray();

            detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accId, order, pageIndex);
            detailModel.rowCount = accId.Length;
                //int.Parse(dic["rowCount"]);
            detailModel.maxPage = accId.Length % 15 == 0 ? accId.Length / 15 : (accId.Length / 15 + 1);
                //int.Parse(dic["maxPage"]);

            return CommonLib.Helper.JsonSerializeObject(detailModel, "");
        }

        public static string GetFilterUsr(int pageIndex, string verif, string order,int uid)
        {
            IndexDetailModel detailModel = new IndexDetailModel();
            List<string> accIdStr = SynthesisIFilterLogBLL.GetAccountList(uid, verif).Split(',').ToList();
            accIdStr.RemoveAt(0);

            string[] accIdStrTemp = accIdStr.ToArray();

            int[] accId = Array.ConvertAll<string, int>(accIdStrTemp, delegate(string s) { return int.Parse(s); });

            detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accId, order, pageIndex);
            detailModel.rowCount = accId.Length;
            //int.Parse(dic["rowCount"]);
            detailModel.maxPage = accId.Length % 15 == 0 ? accId.Length / 15 : (accId.Length / 15 + 1);
            //int.Parse(dic["maxPage"]);

            return CommonLib.Helper.JsonSerializeObject(detailModel, "yyyy-MM-dd HH:mm:ss");

        }


        public static string GetRetentionUserDetail(int pageIndex, int index, string cnt, string date, string day, string order)
        {
            IndexDetailModel detailModel = new IndexDetailModel();
            int[] accId = DashBoardAnalyzeBLL.GetRetentionUserDetail(pageIndex, index, date, day);

            detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accId, order, pageIndex);
            detailModel.rowCount = int.Parse(cnt);
            detailModel.maxPage = detailModel.rowCount / 15 + 1;

            return CommonLib.Helper.JsonSerializeObject(detailModel, "");
        }

        /// <summary>
        /// 获取漏斗详情用户
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="keyword"></param>
        /// <param name="activeList"></param>
        /// <returns></returns>
        public static string GetFunnelDetail(int pageIndex, string keyword, string activeList,string cnt)
        {
            IndexDetailModel detailModel = new IndexDetailModel();

            int[] accId = DashBoardAnalyzeBLL.GetFunnelDetailUser(pageIndex, keyword, activeList);

            detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accId, "", pageIndex);
            detailModel.rowCount = int.Parse(cnt);
            detailModel.maxPage = detailModel.rowCount / 15 + 1;

            return CommonLib.Helper.JsonSerializeObject(detailModel, "");
        }

        public static string GetLocationUsrStatus(string location)
        {
            int[] accId = DashBoardAnalyzeBLL.GetLocationAccId(1, location);

            return CommonLib.Helper.JsonSerializeObject(DashBoardAnalyzeBLL.GetLocationUsrStatus(accId));
        }

        /// <summary>
        /// 返回不同活跃类型用户Id的通用方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public static string GetGeneralAccountId(int pageIndex,string type, int index,string cnt,string order="")
        {
            IndexDetailModel detailModel = new IndexDetailModel();
            int[] accId = DashBoardAnalyzeBLL.GetGeneralAccountId(pageIndex, type, index);
            int count = 0;

            if (type!="Paid")
            {
                count = DashBoardAnalyzeBLL.GetDashBoardUsrCount(type, index);
            }
            else
            {
                count = int.Parse(cnt);
            }

            detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accId, order, pageIndex);
            detailModel.rowCount = count;
            detailModel.maxPage = detailModel.rowCount / 15 + 1;

            return CommonLib.Helper.JsonSerializeObject(detailModel,"");
        }

        /// <summary>
        /// 返回不同留存条件下的用户详情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="type"></param>
        /// <param name="accids"></param>
        /// <returns></returns>
        public static string GetGeneralAccountId(int pageIndex, string type, List<int> accids)
        {
            IndexDetailModel detailModel = new IndexDetailModel();

            int bgNumber = ((pageIndex - 1) * 15);
            int count = accids.Count;

            if (accids.Count - bgNumber > 15)
            {
                accids = accids.GetRange(bgNumber, 15);
            }
            else if (accids.Count > bgNumber && accids.Count < (bgNumber + 15))
            {
                accids = accids.GetRange(bgNumber, accids.Count - bgNumber);
            }
            
            
            int[] accId = accids.ToArray();

            detailModel.listData = DashBoardAnalyzeBLL.GetGeneralList(accId);
            detailModel.rowCount = count;
            detailModel.maxPage = detailModel.rowCount / 15 + 1;

            return CommonLib.Helper.JsonSerializeObject(detailModel, "");
        }
    }
}
