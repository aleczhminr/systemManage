using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;       
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class T_AlipayInfoBLL
    {


        public static int GetPageCount(string strWhere)
        {
            T_AlipayInfoDAL dal = new T_AlipayInfoDAL();
            return dal.GetPageCount(strWhere);
        }
        public static AlipayInfoModel GetAlipayInfoModel(int id)
        {
            T_AlipayInfoDAL dal = new T_AlipayInfoDAL();
            var model= dal.GetAlipayInfoModel(id);
            model.statusDes = Enum.GetName(typeof(Model.Enum.AlipayInfoEnum.AlipayInfoStatusEnum), model.status);
            return model;
        }
        public static List<AlipayInfoModel> GetPageCount(int pageIndex, int pageSize, string Column, string strWhere)
        {
            T_AlipayInfoDAL dal = new T_AlipayInfoDAL();
            List<AlipayInfoModel> listitem = dal.GetPage(pageIndex, pageSize, Column, strWhere);
            foreach (AlipayInfoModel item in listitem)
            {
                item.statusDes=Enum.GetName(typeof(Model.Enum.AlipayInfoEnum.AlipayInfoStatusEnum), item.status);                
            }
            return listitem;
        }

        public static string UpdateStatus(int status, bool isGoNextStep, string remark, int alipayId)
        {
            string sResult = "";
            T_AlipayInfoDAL dal = new T_AlipayInfoDAL();
            if (isGoNextStep)
            {
                sResult = dal.UpdateStatusForSuccessStep(status,alipayId);                
            }
            else
            {
                sResult = dal.UpdateStatusForFailedStep(status, alipayId, remark);
            }
            return sResult;
        }

        

    }
}
