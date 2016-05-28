using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class Sys_DailyIntegralExchangeBLL
    {
        public static bool IsExistDayRecord(DateTime dayDate)
        {
            Sys_DailyIntegralExchangeDAL dal = new Sys_DailyIntegralExchangeDAL();
            return dal.IsExistDayRecord(dayDate);
        }

        public static int AddNewRecord(IntegralExchangeModel model)
        {
            Sys_DailyIntegralExchangeDAL dal = new Sys_DailyIntegralExchangeDAL();
            return dal.AddNewRecord(model);
        }

        public static int ModifyRecord(DateTime dayDate, IntegralExchangeModel model)
        {
            Sys_DailyIntegralExchangeDAL dal = new Sys_DailyIntegralExchangeDAL();
            return dal.ModifyRecord(dayDate, model);
        }

        public static IntegralExchangeModel GetDailyExchangeModel(DateTime dayDate)
        {
            Sys_DailyIntegralExchangeDAL dal = new Sys_DailyIntegralExchangeDAL();
            return dal.GetDailyExchange(dayDate);
        }
    }
}   
