using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class Sys_DailyCheckBLL
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="dailyModel"></param>
        /// <returns></returns>
        public static int AddTask(Sys_DailyCheck dailyModel)
        {
            Sys_DailyCheckDAL dal = new Sys_DailyCheckDAL();
            return dal.AddTask(dailyModel);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">前几行</param>
        /// <param name="columnName">需要获取的列名<para>为了方便不在处理，列与列用逗号分开，参照SQL写法</para></param>
        /// <param name="dapperWheres">查询条件列表</param>
        /// <param name="filedOrder">排序</param>
        public static List<T> GetList<T>(int top, string columnName, List<DapperWhere> dapperWheres, string filedOrder)
        {
            Sys_DailyCheckDAL dal = new Sys_DailyCheckDAL();
            return dal.GetList<T>(top, columnName, dapperWheres, filedOrder);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            Sys_DailyCheckDAL dal = new Sys_DailyCheckDAL();
            return dal.Delete(id);
        }
        
    }
}
