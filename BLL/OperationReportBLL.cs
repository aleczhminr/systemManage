using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class OperationReportBLL
    {
        public static List<NewAccountItem> GetNewAccountModel(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetNewAccountModel(stDate, edDate);
        }

        public static List<ConversionSource> GetConversionList(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetConversionList(stDate, edDate);
        }

        public static List<NewAccountItem> GetDimensionLogin(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetDimensionLogin(stDate, edDate);
        }

        public static List<NewAccountItem> GetRetentionData(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetRetentionData(stDate, edDate);
        }

        #region 促活相关数据处理

        public static int GetIndependLog(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetIndependLog(stDate, edDate);
        }

        public static List<NewAccountItem> GetSalePart(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetSalePart(stDate, edDate);
        }

        public static List<NewAccountItem> GetGoodsPart(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetGoodsPart(stDate, edDate);
        }

        public static List<NewAccountItem> GetUserPart(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetUserPart(stDate, edDate);
        }

        public static List<NewAccountItem> GetPayPart(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetPayPart(stDate, edDate);
        }

        public static List<NewAccountItem> GetSmsPart(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetSmsPart(stDate, edDate);
        }

        public static List<NewAccountItem> GetShowPart(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetShowPart(stDate, edDate);
        }

        public static List<NewAccountItem> GetIncomePart(DateTime stDate, DateTime edDate)
        {
            OperationReportDAL dal = new OperationReportDAL();
            return dal.GetIncomePart(stDate, edDate);
        }

        

        #endregion
    }
}
