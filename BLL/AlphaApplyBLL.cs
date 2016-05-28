using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class AlphaApplyBLL
    {
        public static int GetPageCount(string strWhere)
        {
            AlphaApplyDAL dal = new AlphaApplyDAL();
            return dal.GetPageCount(strWhere);
        }
        public static List<AlphaApplyModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            AlphaApplyDAL dal = new AlphaApplyDAL();
            var listitem = dal.GetPage(pageIndex, pageSize, Column, strWhere);
            foreach (AlphaApplyModel item in listitem)
            {
                item.statusName = Enum.GetName(typeof(Model.Enum.AlphaApplyEnum.AlphaStatus), item.status);
               // item.alphaVersion = item.alphaVersion.Replace(",", "和");
            }
            return listitem;
        }
        public static bool UpdateAlphaApplyStatus(int id, int status, string operatorIP, int operatorUserId)
        {
            AlphaApplyDAL dal = new AlphaApplyDAL();
            return dal.UpdateAlphaApplyStatus(id, status, operatorIP, operatorUserId);
        }
    }
}
