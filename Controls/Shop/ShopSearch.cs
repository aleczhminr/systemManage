using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace Controls.Shop
{
    public class ShopSearch
    {
        /// <summary>
        /// 返回店铺搜索分页列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> GetSearchList(int pageIndex, int pageSize, string companyName = "", string userRealName = "", string phoneNumber = "", string userEmail = "", int bbsUid = 0, int agentId = 0, string agentName = "", string serviceManager = "", string regSource = "all", DateTime? startRegTime = null, DateTime? endRegTime = null, string searchStr = "")
        {

            Dictionary<string, object> list = new Dictionary<string, object>();
            List<DapperWhere> dapperWhere = new List<DapperWhere>();


            if (string.IsNullOrEmpty(searchStr))
            {

                dapperWhere.Add(new DapperWhere("State", 1));

                if (companyName != "")
                {
                    dapperWhere.Add(new DapperWhere("CompanyName", companyName, " CompanyName like '%'+ @CompanyName +'%'"));
                }
                if (userRealName != "")
                {
                    dapperWhere.Add(new DapperWhere("UserRealName", userRealName, " UserRealName like '%'+ @UserRealName +'%'"));
                }
                if (phoneNumber != "")
                {
                    dapperWhere.Add(new DapperWhere("phoneNumber", phoneNumber, "ID in(select accountid from i200.dbo.T_Account_User where grade='管理员' and PhoneNumber like '%' + @phoneNumber + '%')"));
                }
                if (userEmail != "")
                {
                    dapperWhere.Add(new DapperWhere("UserEmail", userEmail, "ID in(select accountid from i200.dbo.T_Account_User where grade='管理员' and UserEmail like '%' + @UserEmail + '%')"));
                }
                if (bbsUid != 0)
                {
                    dapperWhere.Add(new DapperWhere("BBS_Uid", bbsUid));
                }

                if (startRegTime != null)
                {
                    dapperWhere.Add(new DapperWhere("startRegtime", startRegTime, " RegTime>=@startRegtime"));
                }
                if (endRegTime != null)
                {
                    DateTime end = Convert.ToDateTime(endRegTime).Date.Add(new TimeSpan(23, 59, 59));
                    dapperWhere.Add(new DapperWhere("endRegtime", end, "RegTime<=@endRegtime"));
                }

                if (agentId != 0)
                {
                    dapperWhere.Add(new DapperWhere("AgentId", agentId));
                }
                if (agentName != "")
                {
                    dapperWhere.Add(new DapperWhere("agentName", agentName, "AgentId in(select ID from Sys_agent_mess where AgentName=@agentName)"));
                }

                if (serviceManager != "")
                {
                    dapperWhere.Add(new DapperWhere("ServiceManager", serviceManager, " ServiceManager like '%+ @ServiceManager +%'"));
                }

                if (regSource != "all")
                {
                    switch (regSource)
                    {
                        case "0":
                            dapperWhere.Add(new DapperWhere("Remark", "0"));
                            break;
                        case "9":
                            dapperWhere.Add(new DapperWhere("Remark", "9"));
                            break;
                        case "8":
                            dapperWhere.Add(new DapperWhere("Remark", "8"));
                            break;
                        case "10":
                            dapperWhere.Add(new DapperWhere("Remark", "10"));
                            break;
                        case "11":
                            dapperWhere.Add(new DapperWhere("Remark", "11"));
                            break;
                    }
                }
            }

            if (pageSize < 1)
            {
                pageSize = 20;
            }

            int rowCount = 0;
            if (pageIndex == 1)
            {
                rowCount = BLL.Base.T_AccountBaseBLL.GetCount(dapperWhere, searchStr);
            }
            int maxPage = 0;
            if (rowCount > 0)
            {
                maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
            }

            //List<dynamic> dataList = T_AccountBLL.GetSearchList(pageIndex, 15, dapperWhere, " id desc");

            list["rowCount"] = rowCount;
            list["maxPage"] = maxPage;
            list["pageIndex"] = pageIndex;
            list["listData"] = T_AccountBLL.GetSearchList(pageIndex, 15, dapperWhere, " id desc", searchStr);

            //List<int> accidList = new List<int>();
            //foreach (dynamic item in dataList)
            //{
            //    accidList.Add(item.ID);
            //}

            //list["accidList"] = CommonLib.Helper.JsonSerializeObject(accidList);

            return list;
        }
    }
}
