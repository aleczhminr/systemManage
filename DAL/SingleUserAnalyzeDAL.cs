using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class SingleUserAnalyzeDAL
    {
        public SingleUserAnalyze GetUsrAnalyzeData(int accId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" declare  ");
            strSql.Append(" @shopName varchar(100) , ");// 店铺名称
            strSql.Append(" @active int , ");// 活跃状态
            strSql.Append(" @regTime datetime , ");// 注册时间
            strSql.Append(" @loginTime int , ");// 登录次数
            strSql.Append(" @regSource int , ");// 商品数
            strSql.Append(" @goodsNum int , ");// 短信数
            strSql.Append(" @goodsNumBar int , ");// 订单数
            strSql.Append(" @maxProfit float , ");// 短信数
            strSql.Append(" @minProfit float , ");// 订单数
            strSql.Append(" @maxSaleStr varchar(50); ");// 订单金额

            strSql.Append(" select @active=active from i200.dbo.T_Business where accountid=@AccId; ");
            strSql.Append(
                " select @shopName=CompanyName,@regTime=RegTime,@loginTime=LoginTimeWeb from i200.dbo.T_Account where ID=@AccId; ");
            strSql.Append(" select @regSource=tag_id from Sys_TagNexus where acc_id=@AccId; ");
            strSql.Append(" select @goodsNum=COUNT(gid) from i200.dbo.T_GoodsInfo where accID=@AccId; ");
            strSql.Append(
                " select @goodsNumBar=COUNT(gid) from i200.dbo.T_GoodsInfo where gBarcode is not null and accID=@AccId; ");
            strSql.Append(
                " select top 3 @maxSaleStr= isnull(@maxSaleStr,'') + ',' +CAST(DATEPART(HOUR,insertTime) as varchar(100))  from [i200].[dbo].[T_SaleInfo] where accID=@AccId and Datediff(Month,insertTime,getdate())<6 group by DATEPART(HOUR,insertTime) order by COUNT(saleNo) desc; ");
            strSql.Append(" declare @tempMaxProfit float, ");
            strSql.Append(" @tempMinProfit float, ");
            strSql.Append(" @tempMaxProfitSku float, ");
            strSql.Append(" @tempMinProfitSku float; ");
            strSql.Append(
                " select @tempMaxProfit=max(isnull(gPrice,0)-isnull(gCostPrice,0)),@tempMinProfit=min(isnull(gPrice,0)-isnull(gCostPrice,0)) from i200.dbo.T_GoodsInfo where accID=@AccId; ");
            strSql.Append(
                " select @tempMaxProfitSku=max(isnull(gsPrice,0)-isnull(gsCostPrice,0)),@tempMinProfitSku=min(isnull(gsPrice,0)-isnull(gsCostPrice,0)) from i200.dbo.T_Goods_Sku where accId=@AccId; ");
            strSql.Append(" if isnull(@tempMaxProfit,0)>isnull(@tempMaxProfitSku,0) ");
            strSql.Append(" begin ");
            strSql.Append(" select @maxProfit=@tempMaxProfit; ");
            strSql.Append(" end ");
            strSql.Append(" else ");
            strSql.Append(" begin ");
            strSql.Append(" select @maxProfit=@tempMaxProfitSku; ");
            strSql.Append(" end ");
            strSql.Append(" if @tempMinProfit>@tempMinProfitSku ");
            strSql.Append(" begin ");
            strSql.Append(" select @minProfit=@tempMinProfitSku; ");
            strSql.Append(" end ");
            strSql.Append(" else ");
            strSql.Append(" begin ");
            strSql.Append(" select @minProfit=@tempMinProfit; ");
            strSql.Append(" end ");
            strSql.Append(
                "select @active as ActiveStatus,@shopName as AccName,@regTime as RegTime,@loginTime as LoginTime,@regSource as Source,@goodsNum as GoodsNum," +
                "@goodsNumBar as GoodsNumWithBarCode,@maxSaleStr as MaxSaleTimeStr,@maxProfit as MaxProfit,@minProfit as MinProfit;");

            return DapperHelper.Query<SingleUserAnalyze>(strSql.ToString(), new {AccId = accId}).ToList()[0];

        }

        /// <summary>
        /// 获取用户面板的列表
        /// </summary>
        /// <param name="accId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<MostSaleList> GetGoodsSaleList(int accId,string order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select top 5 GoodsName GName,AVG(isnull(Price,0)) GPrice," +
                "AVG(isnull(Price,0)-ISNULL(costprice,0)) GProfit,COUNT(*) Quantity " +
                "from i200.dbo.T_Sale_List where accid=@accId group by GoodsName order by " + order + " desc");

            return DapperHelper.Query<MostSaleList>(strSql.ToString(), new {accId = accId}).ToList();
        }

        /// <summary>
        /// 获取最近三天销售成交间隔
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public List<DateTime> SaleInterval(int accId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select saleTime from i200.dbo.T_Sale_List where DATEDIFF(DAY,saleTime,GETDATE())<3 and accID=@accId;");

            return DapperHelper.Query<DateTime>(strSql.ToString(), new { accId = accId }).ToList();
        }
 
    }
}
