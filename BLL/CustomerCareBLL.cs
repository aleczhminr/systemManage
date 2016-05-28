using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class CustomerCareBLL
    {
        /// <summary>
        /// 获取客服数据汇总
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static Dictionary<string, List<dynamic>> GetServStatChartData(DateTime startTime, DateTime endTime)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.GetServStatChartData(startTime, endTime);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dayCnt"></param>
        /// <param name="type"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public static List<dynamic> getOrderInfo(DateTime startTime, DateTime endTime, int dayCnt, int type,
            string person)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.getOrderInfo(startTime, endTime, dayCnt, type, person);
        }

        /// <summary>
        /// 获取客服回访信息
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public static List<dynamic> getVisitInfo_customer()
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.getVisitInfo_customer();
        }

        public static List<dynamic> getVisitInfo_software()
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.getVisitInfo_software();
        }

        public static int getVisitInfo_count()
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.getVisitInfo_count();
        }

        public static List<dynamic> getVisitDetail(string where)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.getVisitDetail(where);
        }

        public static UserRetentionModel GetCareRetention(DateTime stDate, DateTime edDate, string dateType, string usrName)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.GetCareRetention(stDate, edDate, dateType, usrName);
        }

        public static CarePercentModel GetCarePartitionPer(DateTime stDate, DateTime edDate, string usrName, int partIndex)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.GetCarePartitionPer(stDate, edDate, usrName, partIndex);
        }

        public static List<OrderRenewalModel> GetOrderRenewal(DateTime stDate, DateTime edDate, string type)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.GetOrderRenewalModels(stDate, edDate, type);
        }

        public static int GetPageCount(string strWhere)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.GetPageCount(strWhere);
        }
        public static List<WithdrawalRecordModel> GetPage(int pageIndex, int pageSize, string Column, string strWhere)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            var listitem = dal.GetPage(pageIndex, pageSize, Column, strWhere);
            foreach (WithdrawalRecordModel item in listitem)
            {
                item.statusname = Enum.GetName(typeof(Model.Enum.WithdrawalEnum.WithdrawalStatus), item.status);
            }
            return listitem;
        }
        public static bool UpdateWithdrawalStatus(int withdrawalRecordId, int status, string operatorIP, int operatorUserId)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.UpdateWithdrawalStatus(withdrawalRecordId, status, operatorIP, operatorUserId);
        }

        public static decimal GetWithdrawalMoneyByWithdrawalRecordId(int withdrawalRecordId)
        {
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.GetWithdrawalMoneyByWithdrawalRecordId(withdrawalRecordId);
        }

        public static int GetVisitCount(int index,string insertName, DateTime stDate,DateTime edDate)
        {            
            CustomerCareDAL dal = new CustomerCareDAL();
            return dal.GetVisitCount(index,insertName, stDate, edDate);
        }
    }
}
