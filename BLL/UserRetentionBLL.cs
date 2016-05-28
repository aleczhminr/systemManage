using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public static class UserRetentionBLL
    {
        public static UserRetentionModel GetUserRetention(string dateType, DateTime bgTime, DateTime edTime, string usrType, string regSource, string agent)
        {
            UserRetentionDAL dal = new UserRetentionDAL();
            return dal.GetUserRetention(dateType, bgTime, edTime, usrType, regSource, agent);
        }

        public static UserRetentionModel GetUserRetentionEx(string dateType, DateTime bgTime, DateTime edTime, string usrType, string regSource)
        {
            UserRetentionDAL dal = new UserRetentionDAL();
            return dal.GetUserRetentionEx(dateType, bgTime, edTime, usrType, regSource);
        }

        public static ActiveChangeModel GetActiveStatus(DateTime stDate, DateTime edDate)
        {
            UserRetentionDAL dal = new UserRetentionDAL();
            return dal.GetActiveChangeData(stDate, edDate);
        }

        /// <summary>
        /// 平均留存率测试方法
        /// </summary>
        /// <param name="dateType"></param>
        /// <param name="bgTime"></param>
        /// <param name="edTime"></param>
        /// <param name="usrType"></param>
        /// <param name="regSource"></param>
        /// <returns></returns>
        public static UserRetentionModel GetUserRetentionTest(string dateType, DateTime bgTime, DateTime edTime,
            string usrType, string regSource)
        {
            UserRetentionDAL dal = new UserRetentionDAL();
            return dal.GetUserRetentionTest(dateType, bgTime, edTime, usrType, regSource);
        }
    }
}
