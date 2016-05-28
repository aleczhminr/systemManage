using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    /// <summary>
    /// 推广链接
    /// </summary>
    public static class T_OutLinkBLL
    {
        /// <summary>
        /// 新增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add(T_OutLink model)
        {
            T_OutLinkDAL dal = new T_OutLinkDAL();
            return dal.Add(model);
        }
        /// <summary>
        /// 分页得到列表
        /// </summary>
        /// <param name="pageIndex">显示页号</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="dapperWheres">条件列表</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>返回列表</returns>
        public static Dictionary<string, string> GetList(int pageIndex, int pageSize, List<DapperWhere> dapperWheres, string filedOrder)
        {
            T_OutLinkDAL dal = new T_OutLinkDAL();
            return dal.GetList(pageIndex, pageSize, dapperWheres, filedOrder);
        }
    }
}
