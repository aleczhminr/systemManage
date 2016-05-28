using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public static class SingleUserAnalyzeBLL
    {
        public static SingleUserAnalyze GetUsrAnalyze(int accId)
        {
            SingleUserAnalyzeDAL dal = new SingleUserAnalyzeDAL();
            return dal.GetUsrAnalyzeData(accId);
        }

        public static TableList GetMostSaleList(int accId)
        {           
            SingleUserAnalyzeDAL dal = new SingleUserAnalyzeDAL();
            TableList tableData = new TableList();
            tableData.tbList = dal.GetGoodsSaleList(accId, "Quantity");
            tableData.ListCount = tableData.tbList.Count;
            return tableData;
        }

        public static TableList GetMostProfitList(int accId)
        {
            SingleUserAnalyzeDAL dal = new SingleUserAnalyzeDAL();
            TableList tableData = new TableList();
            tableData.tbList = dal.GetGoodsSaleList(accId, "GProfit");
            tableData.ListCount = tableData.tbList.Count;
            return tableData;
        }

        public static List<DateTime> GetSaleDateInterval(int accId)
        {
            SingleUserAnalyzeDAL dal = new SingleUserAnalyzeDAL();
            return dal.SaleInterval(accId);
        }
    }
}
