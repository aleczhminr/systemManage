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
    /// 
    /// </summary>
    public static class T_Goods_AttributeBLL
    {

        /// <summary>
        /// 新增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Add(T_Goods_Attribute model)
        {
            T_Goods_AttributeDAL dal = new T_Goods_AttributeDAL();
            return dal.Add(model);
        }

    }
}
