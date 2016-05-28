using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public static class T_PreFixVerBLL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<Model.T_PreFixVer> GetModelList(string strWhere)
        {
            var dal = new T_PreFixVerDAL();   
            return dal.GetList(strWhere);
        }
    }
}
