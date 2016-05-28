using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class SourceDataDAL
    {
        public DxChartModel GetDataSource(DateTime stDate, DateTime edDate, string[] sourceType,
            string[] conditions)
        {
            StringBuilder strSql = new StringBuilder();
            List<dynamic> listSale = new List<dynamic>();
            List<dynamic> listReg = new List<dynamic>();
            List<dynamic> listGoods = new List<dynamic>();

            DxChartModel chartModel = new DxChartModel();
            //List<DxChartData> dataList = new List<DxChartData>();

            DateTime iter = stDate;

            while (iter < edDate)
            {
                chartModel.DataList.Add(new DxChartData(iter.ToShortDateString()));
                iter = iter.AddDays(1);
            }

            foreach (string conds in conditions)
            {
                strSql.Clear();
                switch (conds)
                {
                    case "销售":
                        strSql.Append(
                            "select CAST(saleTime as date) opDate,COUNT(*) cnt,saleFlag from [i200].[dbo].[T_SaleInfo] " +
                            "where saleTime between @stDate and @edDate group by CAST(saleTime as date),saleFlag;");
                        listSale =
                            DapperHelper.Query<dynamic>(strSql.ToString(), new {stDate = stDate, edDate = edDate})
                                .ToList();
                        break;
                    case "商品":
                        strSql.Append(
                            "select CAST(gAddTime as date) opDate,COUNT(*) cnt,gFlag from [i200].[dbo].[T_GoodsInfo] " +
                            "where gAddTime between @stDate and @edDate group by CAST(gAddTime as date),gFlag;");
                        listGoods =
                            DapperHelper.Query<dynamic>(strSql.ToString(), new {stDate = stDate, edDate = edDate})
                                .ToList();
                        break;
                    case "会员":
                        strSql.Append(
                            "select CAST(uRegTime as date) opDate,COUNT(*) cnt,uFlag from [i200].[dbo].[T_UserInfo] " +
                            "where uRegTime between @stDate and @edDate group by CAST(uRegTime as date),uFlag;");
                        listReg =
                            DapperHelper.Query<dynamic>(strSql.ToString(), new {stDate = stDate, edDate = edDate})
                                .ToList();
                        break;
                }
            }

            foreach (DxChartData day in chartModel.DataList)
            {
                List<dynamic> daySaleList = listSale.FindAll(x => x.opDate.ToShortDateString() == day.Date);
                List<dynamic> dayGoodsList = listGoods.FindAll(x => x.opDate.ToShortDateString() == day.Date);
                List<dynamic> dayRegList = listReg.FindAll(x => x.opDate.ToShortDateString() == day.Date);

                if (daySaleList.Count > 0)
                {
                    if (sourceType.Contains("IPHONE"))
                    {
                        day.Data.Add("IPHONE销售",
                        (daySaleList.Exists(x => x.saleFlag == 1)
                            ? daySaleList.Find(x => x.saleFlag == 1).cnt
                            : 0));
                    }

                    if (sourceType.Contains("Android"))
                    {
                        day.Data.Add("Android销售",
                        (daySaleList.Exists(x => x.saleFlag == 2)
                            ? daySaleList.Find(x => x.saleFlag == 2).cnt
                            : 0));
                    }

                    if (sourceType.Contains("Web"))
                    {
                        day.Data.Add("Web销售",
                        (daySaleList.Exists(x => x.saleFlag == 0)
                            ? daySaleList.Find(x => x.saleFlag == 0).cnt
                            : 0));
                    }

                    if (sourceType.Contains("iPad"))
                    {
                        day.Data.Add("iPad销售",
                        (daySaleList.Exists(x => x.saleFlag == 3)
                            ? daySaleList.Find(x => x.saleFlag == 3).cnt
                            : 0));
                    }
                    
                }
                else if (conditions.Contains("销售"))
                {
                    foreach (string str in sourceType)
                    {
                        day.Data.Add(str + "销售", 0);
                    }

                }
                

                if (dayGoodsList.Count > 0)
                {
                    if (sourceType.Contains("IPHONE"))
                    {
                        day.Data.Add("IPHONE商品",
                        (dayGoodsList.Exists(x => x.gFlag == 1)
                            ? dayGoodsList.Find(x => x.gFlag == 1).cnt
                            : 0));
                    }

                    if (sourceType.Contains("Android"))
                    {
                        day.Data.Add("Android商品",
                        (dayGoodsList.Exists(x => x.gFlag == 2)
                            ? dayGoodsList.Find(x => x.gFlag == 2).cnt
                            : 0));
                    }

                    if (sourceType.Contains("Web"))
                    {
                        day.Data.Add("Web商品",
                          (dayGoodsList.Exists(x => x.gFlag == 0)
                              ? dayGoodsList.Find(x => x.gFlag == 0).cnt
                              : 0));
                    }

                    if (sourceType.Contains("iPad"))
                    {
                        day.Data.Add("iPad商品",
                        (dayGoodsList.Exists(x => x.gFlag == 3)
                            ? dayGoodsList.Find(x => x.gFlag == 3).cnt
                            : 0));
                    }
                    
                }
                else if (conditions.Contains("商品"))
                {
                    foreach (string str in sourceType)
                    {
                        day.Data.Add(str + "商品", 0);
                    }

                }

                if (dayRegList.Count > 0)
                {
                    if (sourceType.Contains("IPHONE"))
                    {
                        day.Data.Add("IPHONE会员",
                        (dayRegList.Exists(x => x.uFlag == 1)
                            ? dayRegList.Find(x => x.uFlag == 1).cnt
                            : 0));
                    }

                    if (sourceType.Contains("Android"))
                    {
                        day.Data.Add("Android会员",
                        (dayRegList.Exists(x => x.uFlag == 2)
                            ? dayRegList.Find(x => x.uFlag == 2).cnt
                            : 0));
                    }

                    if (sourceType.Contains("Web"))
                    {
                        day.Data.Add("Web会员",
                        (dayRegList.Exists(x => x.uFlag == 0)
                            ? dayRegList.Find(x => x.uFlag == 0).cnt
                            : 0));
                    }

                    if (sourceType.Contains("iPad"))
                    {
                        day.Data.Add("iPad会员",
                        (dayRegList.Exists(x => x.uFlag == 3)
                            ? dayRegList.Find(x => x.uFlag == 3).cnt
                            : 0));
                    }
                    
                }
                else if (conditions.Contains("会员"))
                {
                    foreach (string str in sourceType)
                    {
                        day.Data.Add(str + "会员", 0);
                    }

                }

            }

            return chartModel;

        }
    }
}
