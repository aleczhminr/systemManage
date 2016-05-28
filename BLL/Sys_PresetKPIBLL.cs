using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    
    public static class Sys_PresetKPIBLL
    {
        /// <summary>
        /// 新增一条KPI预设数据
        /// </summary>
        public static int AddKpi(MonthlyKPI model)
        {
            Sys_PresetKPIDAL dal = new Sys_PresetKPIDAL();
            return dal.SetNewKPI(model);
        }

        /// <summary>
        /// 删除一条KPI数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteKpi(int id)
        {
            Sys_PresetKPIDAL dal = new Sys_PresetKPIDAL();
            return dal.DeleteKPI(id);
        }

        /// <summary>
        /// 获取KPI列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="condition"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<MonthlyKPI> GetList(int page,bool condition,DateTime dt)
        {
            Sys_PresetKPIDAL dal = new Sys_PresetKPIDAL();
            return dal.GetKPIList(page, condition, dt);
        }
    }

 
}
