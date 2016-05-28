using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    /// <summary>
    /// 外链类别
    /// </summary>
    public static class T_OutLinkTypeBLL
    {
        /// <summary>
        /// 得到列表
        /// </summary>
        /// <returns></returns>
        public static List<OutLinkType> GetList()
        {
            T_OutLinkTypeDAL dal = new T_OutLinkTypeDAL();
            return dal.GetList();
        }

        public static int ChangeStatus(string status, string id)
        {
            T_OutLinkTypeDAL dal = new T_OutLinkTypeDAL();
            return dal.ChangeStatus(status, id);
        }
    }
}
