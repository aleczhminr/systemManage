using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 数据筛选器
    /// </summary>
    public static class SynthesisIFilterBLL
    {
        /// <summary>
        /// 得到筛选数据列表
        /// </summary>
        /// <param name="topNum">前面几行</param>
        /// <param name="whereModel">条件信息</param>
        /// <param name="orderWhere">排序</param>
        /// <param name="userId">查询人ID</param>
        /// <param name="verification">验证</param>
        /// <param name="userName">筛选人名称</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetIFilterList(int topNum, List<SynthesisIFilter> whereModels, string orderWhere, int userId, string verification, string userName)
        {
            SynthesisIFilterDAL dal = new SynthesisIFilterDAL();
            return dal.GetIFilterList(topNum, whereModels, orderWhere, userId, verification, userName);
        }

        /// <summary>
        /// 获取筛选结果的规则优化版本
        /// </summary>
        /// <param name="topNum"></param>
        /// <param name="where"></param>
        /// <param name="orderWhere"></param>
        /// <param name="userId"></param>
        /// <param name="verification"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetFilterDataByRule(int topNum, string where, string orderWhere, int userId, string verification, string userName)
        {
            SynthesisIFilterDAL dal = new SynthesisIFilterDAL();
            return dal.GetFilterDataByRule(topNum, where, orderWhere, userId, verification, userName);
        }

        /// <summary>
        /// 得到分析数据
        /// </summary>
        /// <param name="AccountList"></param>
        /// <returns></returns>
        public static List<dynamic> GetSummarizingData(string AccountList)
        {
            SynthesisIFilterDAL dal = new SynthesisIFilterDAL();
            return dal.GetSummarizingData(AccountList);
        } 
    }
}
