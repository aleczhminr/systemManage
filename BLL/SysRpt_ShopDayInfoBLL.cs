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
    /// 店铺每日信息汇总
    /// </summary>
    public static class SysRpt_ShopDayInfoBLL
    {
        /// <summary>
        /// 店铺成长图标
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static chartDataModel GetGrouwthList(int accid, string[] dataType, DateTime? start, DateTime? end)
        {

            DateTime startTime = DateTime.Now.Date;
            DateTime endTime = DateTime.Now.Date;




            Dictionary<string, string> columnList = new Dictionary<string, string>();
            columnList.Add("loginNum", "登录");
            columnList.Add("userNum", "会员数");
            columnList.Add("saleNum", "销售数");
            columnList.Add("saleMoney", "销售金额");
            columnList.Add("smsNum", "短信数");
            columnList.Add("goodsNum", "商品数");
            columnList.Add("orderMoney", "订单金额");
            columnList.Add("acc_Rep", "在线时长");
            columnList.Add("moodNum", "心情");
            chartDataModel chartModel = new chartDataModel();
            chartModel.captionTitle = "店铺成长";
            chartModel.DataList = new Dictionary<string, charDataList>();

            string columnName = "dayDate,regTime";
            foreach (string dataItemType in dataType)
            {
                columnName += "," + dataItemType;
            }

            List<DapperWhere> sqlWhere = new List<DapperWhere>();
            sqlWhere.Add(new DapperWhere("accountid", accid));
            if (start != null)
            {
                sqlWhere.Add(new DapperWhere("startTime", start, " dayDate>=@startTime "));
                startTime = Convert.ToDateTime(start);
            }
            else
            {
                startTime = new DateTime(1999, 1, 1);
            }
            if (end != null)
            {
                endTime = Convert.ToDateTime(end);
                sqlWhere.Add(new DapperWhere("endTime", end, " dayDate <=@endTime "));
            }

            List<dynamic> dataList = Base.SysRpt_ShopDayInfoBaseBLL.GetList<dynamic>(0, columnName, sqlWhere, " dayDate asc");

            Dictionary<string, charDataList> charItemList = new Dictionary<string, charDataList>();
            if (dataList != null && dataList.Count>0)
            {
                DateTime regTime = DateTime.Now;


                foreach (dynamic itemDataList in dataList)
                {
                    regTime = Convert.ToDateTime(itemDataList.regTime);
                    var XLable = Convert.ToDateTime(itemDataList.dayDate).ToString("yy-MM-dd");
                    charItemList[XLable] = new charDataList(XLable);
                    Dictionary<string, charDataItemList> charDataItem = new Dictionary<string, charDataItemList>();
                    #region 系列增值
                    if (dataType.Contains("loginNum"))
                    {
                        charDataItem[columnList["loginNum"]] = new charDataItemList() { Values = itemDataList.loginNum, series = columnList["loginNum"] };
                    }
                    if (dataType.Contains("userNum"))
                    {
                        charDataItem[columnList["userNum"]] = new charDataItemList() { Values = itemDataList.userNum, series = columnList["userNum"] };
                    }
                    if (dataType.Contains("saleNum"))
                    {
                        charDataItem[columnList["saleNum"]] = new charDataItemList() { Values = itemDataList.saleNum, series = columnList["saleNum"] };
                    }
                    if (dataType.Contains("saleMoney"))
                    {
                        charDataItem[columnList["saleMoney"]] = new charDataItemList() { Values = itemDataList.saleMoney, series = columnList["saleMoney"] };
                    }
                    if (dataType.Contains("smsNum"))
                    {
                        charDataItem[columnList["smsNum"]] = new charDataItemList() { Values = itemDataList.smsNum, series = columnList["smsNum"] };
                    }
                    if (dataType.Contains("goodsNum"))
                    {
                        charDataItem[columnList["goodsNum"]] = new charDataItemList() { Values = itemDataList.goodsNum, series = columnList["goodsNum"] };
                    }
                    if (dataType.Contains("orderMoney"))
                    {
                        charDataItem[columnList["orderMoney"]] = new charDataItemList() { Values = itemDataList.orderMoney, series = columnList["orderMoney"] };
                    }
                    if (dataType.Contains("acc_Rep"))
                    {
                        charDataItem[columnList["acc_Rep"]] = new charDataItemList() { Values = itemDataList.acc_Rep, series = columnList["acc_Rep"] };
                    }
                    if (dataType.Contains("moodNum"))
                    {
                        charDataItem[columnList["moodNum"]] = new charDataItemList() { Values = itemDataList.moodNum, series = columnList["moodNum"] };
                    }
                    #endregion

                    charItemList[XLable].ItemList = charDataItem;
                }
                //chartModel.DataList = charItemList;
                startTime = startTime > regTime ? startTime : regTime;
            }


            while (startTime <= endTime)
            {

                var XLable = startTime.ToString("yy-MM-dd");

                if (charItemList.ContainsKey(XLable))
                {
                    chartModel.DataList[XLable] = charItemList[XLable];

                }
                else
                {
                    chartModel.DataList[XLable] = new charDataList(XLable);

                    #region 系列增值

                    foreach (string itemType in dataType)
                    {
                        chartModel.DataList[XLable].ItemList[columnList[itemType]] = new charDataItemList() { Values = 0, series = columnList[itemType] };
                    }
                    #endregion

                }
                chartModel.DataList[XLable].weekend = (int)startTime.DayOfWeek;
                startTime = startTime.AddDays(1);
            }


            return chartModel;
        }

    }
}
