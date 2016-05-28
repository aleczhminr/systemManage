using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
namespace Controls.Filtrate
{
    /// <summary>
    /// 数据筛选
    /// </summary>
    public static class Filtrate
    {
        /// <summary>
        /// 数据筛选专用
        /// </summary>
        /// <returns></returns>
        public static Model.FiltrateData.AllMax GetAllMax()
        {
            return SysRpt_ShopInfoBLL.GetAllMax();
        }
        /// <summary>
        /// 得到标签的名称和ID号
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetTagList()
        {
            return BLL.Base.Sys_TagInfoBaseBLL.GetList<dynamic>(0, " id,t_Name", new List<DapperWhere>(), "t_order asc,id desc");
        }
        /// <summary>
        /// 得到产品信息
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetOrderProjectList()
        {
            List<DapperWhere> sqlWhere = new List<DapperWhere>();
            sqlWhere.Add(new DapperWhere("allowBuy", 1));
            return BLL.Base.T_Order_ProjectBaseBLL.GetList(0, "busId,displayName,normalMoney", sqlWhere, " busId desc");
        }
        /// <summary>
        /// 得到代理商的信息
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetAnentList()
        {
            return BLL.Base.Sys_agent_messBaseBLL.GetList<dynamic>(0, "ID,AgentName,AgentId,AgentGroup", new List<DapperWhere>(), " ID desc");
        }

        public static Dictionary<string, object> GetSelectData(string PostDataContent, int uid, string uname)
        {
            List<SynthesisIFilter> SynIfit = new List<SynthesisIFilter>();

            if (PostDataContent.Length > 3 && PostDataContent != "{}")
            {
                SynIfit = CommonLib.Helper.JsonDeserializeObject<List<SynthesisIFilter>>(PostDataContent);


                Random rm = new Random();
                string ver = DateTime.Now.ToString("HHmmss") + rm.Next(1000, 9999).ToString();

                #region 消息发送逻辑
                #endregion

                return SynthesisIFilterBLL.GetIFilterList(50, SynIfit, "", uid, ver, uname);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据规则获取筛选结果
        /// </summary>
        /// <param name="postJson"></param>
        /// <param name="uid"></param>
        /// <param name="nName"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetFilterData(string postJson, int uid, string uName)
        {
            List<FilterCondition> conList = new List<FilterCondition>();

            if (postJson.Length > 3 && postJson != "{}")
            {
                //获得条件组
                conList = CommonLib.Helper.JsonDeserializeObject<List<FilterCondition>>(postJson);

                Random rm = new Random();
                string ver = DateTime.Now.ToString("HHmmss") + rm.Next(1000, 9999).ToString();

                #region 生成新的规则并生成筛选SQL

                string where = RuleManage.RuleManage.GetWhereStrByCondition(conList, 1, ver);
                #endregion

                if (!string.IsNullOrEmpty(where))
                {
                    return SynthesisIFilterBLL.GetFilterDataByRule(50, where, "", uid, ver, uName);    
                }
                else
                {
                    return null;
                }
                
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到统计中的 会员
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="verif"></param>
        /// <returns></returns>
        public static string GetAccountList(int uid, string verif)
        {
            return SynthesisIFilterLogBLL.GetAccountList(uid, verif);
        }

        /// <summary>
        /// 得到分析数据
        /// </summary>
        /// <param name="AccountList"></param>
        /// <returns></returns>
        public static List<dynamic> GetSummarizingData(string AccountList)
        {
            return SynthesisIFilterBLL.GetSummarizingData(AccountList);
        }
    }
}
