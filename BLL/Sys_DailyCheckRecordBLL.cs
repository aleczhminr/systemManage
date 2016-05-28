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
    /// 日检记录
    /// </summary>
    public static class Sys_DailyCheckRecordBLL
    {
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add(Sys_DailyCheckRecord model)
        {
            Sys_DailyCheckRecordDAL dal = new Sys_DailyCheckRecordDAL();
            return dal.Add(model);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="dapperWheres">查询条件列表</param>
        public static List<Sys_DailyCheckRecord> GetList(List<DapperWhere> dapperWheres)
        {
            Sys_DailyCheckRecordDAL dal = new Sys_DailyCheckRecordDAL();
            return dal.GetList(dapperWheres);
        }
        /// <summary>
        /// 得到总数
        /// </summary>
        /// <param name="dapperWheres">条件列表</param>
        /// <returns></returns>
        public static int GetCount(List<DapperWhere> dapperWheres)
        {
            Sys_DailyCheckRecordDAL dal = new Sys_DailyCheckRecordDAL();
            return dal.GetCount(dapperWheres);
        }
        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public static List<Sys_DailyCheckRecord> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
        {
            Sys_DailyCheckRecordDAL dal = new Sys_DailyCheckRecordDAL();
            return dal.GetList(pageIndex, pageSize, dapperWheres, filedOrder);
        }
    }
}
