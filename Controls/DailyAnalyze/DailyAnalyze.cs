using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;

namespace Controls.DailyAnalyze
{
    public static class DailyAnalyze
    {
        public static string GetDailyAnalyzeList(int page, int source, int newReg, int noAction, DateTime dateTime, string orderWhere)
        {
            //page, source, column, whereStr, dateTime, orderWhere
            StringBuilder sqlCondition = new StringBuilder();
            DailyAnalyzeModel dailyNAnalyzeModel = new DailyAnalyzeModel();

            sqlCondition.Append(" datediff(day,dayDate,@nowDay)=0 and ");
            if (newReg==1)
            {
                sqlCondition.Append(" datediff(day,regTime,@nowDay)=0 and ");
            }
            if (noAction==1)
            {
                sqlCondition.Append(" userNum=0 and saleNum=0 and smsNum=0 and goodsNum=0 and outlayNum=0 and orderNum=0 and moodNum=0 and registration=0 and ");
            }

            string Column = " dayDate,accountid,dbo.GetAccountName(accountid) name,regTime,saleNum,saleMoney,saleGoodsNum,memSaleNum, memSaleMoney,retailSaleNum,retailSaleMoney, smsNum,orderNum, orderMoney , goodsNum , userNum , registration , moodNum ";

            string whereStr = sqlCondition.ToString();
            whereStr = whereStr.Substring(0, whereStr.LastIndexOf('a'));

            var modelData = DailyAnalyzeBLL.GetDailyAnalyzeList(page, source, Column, whereStr, dateTime, orderWhere);
            dailyNAnalyzeModel.RowCount = modelData[1].ToList()[0].num;

            if (dailyNAnalyzeModel.RowCount != 0)
            {
                foreach (dynamic item in modelData[0].ToList())
                {
                    DailyUsrModel usrModel = new DailyUsrModel();
                    usrModel.Id = item.accountid;
                    usrModel.AccountName = item.name;
                    if (item.regTime!=null)
                    {
                        usrModel.RegTime = item.regTime;
                    }
                    else
                    {
                        usrModel.RegTime = DateTime.MinValue;
                    }
                    usrModel.RegSource = item.t_Name;
                    usrModel.SaleNum = item.saleNum;
                    usrModel.MemberPaid = item.memSaleNum;
                    usrModel.Retail = item.retailSaleNum;
                    usrModel.SmsNum = item.smsNum;
                    usrModel.OrderNum = item.orderNum;
                    usrModel.GoodsNum = item.goodsNum;
                    usrModel.MemberNum = item.userNum;
                    usrModel.SignFlag = item.registration;
                    usrModel.MoodNum = item.moodNum;

                    dailyNAnalyzeModel.UsrList.Add(usrModel);
                }
            }

            return CommonLib.Helper.JsonSerializeObject(dailyNAnalyzeModel);
        }
    }
}
