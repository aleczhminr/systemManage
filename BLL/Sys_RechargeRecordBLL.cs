using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class Sys_RechargeRecordBLL
    {
        public static int CheckExist(int oid)
        {
            Sys_RechargeRecordDAL dal = new Sys_RechargeRecordDAL();
            return dal.CheckExist(oid);
        }

        public static int GetLastOid()
        {
            Sys_RechargeRecordDAL dal = new Sys_RechargeRecordDAL();
            return dal.GetLastOid();
        }

        public static int AddNewRecord(RechargeRecord model)
        {
            Sys_RechargeRecordDAL dal = new Sys_RechargeRecordDAL();
            return dal.AddNewRecord(model);
        }

        public static RecordSummary GetRecordSum()
        {
            Sys_RechargeRecordDAL dal = new Sys_RechargeRecordDAL();
            return dal.GetRecordSum();
        }

    }
}
