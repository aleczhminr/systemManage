using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 销售信息
    /// </summary>
   public class T_SaleInfoDAL:Base.T_SaleInfoBaseDAL
    {

       /// <summary>
       /// 得到销售地图销售信息
       /// </summary>
       /// <param name="oid"></param>
       /// <returns></returns>
       public List<saleMapItemList> GetSaleMapSaleList(int oid)
       {
           List<saleMapItemList> mapList = new List<saleMapItemList>();
           StringBuilder sql = new StringBuilder();
           sql.Append("create table #temp (orderid int,ShopNum nvarchar(30), ShopName nvarchar(300),ShopAddress nvarchar(100),SellMoney money,DetailAddress nvarchar(200),SellTime datetime,accountid int ); ");
           if (oid > 0)
           {
               sql.Append("insert into #temp(orderid,accountid,ShopNum,SellMoney,SellTime) ");
               sql.Append("select top 20 saleID,accID,saleNo,RealMoney,insertTime from i200.dbo.T_SaleInfo where saleID>'" + oid.ToString() + "' order by saleID asc; ");
           }
           else
           {
               sql.Append("insert into #temp(orderid,accountid,ShopNum,SellMoney,SellTime) ");
               sql.Append("select top 30 saleID,accID,saleNo,RealMoney,insertTime from i200.dbo.T_SaleInfo order by saleID desc; ");
           }
           sql.Append(" update #temp set ShopAddress=TrueConty from #temp inner join i200.dbo.t_account_user on t_account_user.accountid=#temp.accountid and grade='管理员'; ");
           sql.Append(" update #temp set ShopName=CompanyName,DetailAddress=CompanyAddress from #temp inner join i200.dbo.T_Account on #temp.accountid=T_Account.ID; ");
           sql.Append(" select * from #temp order by orderid asc ; ");
           sql.Append(" drop table #temp; ");

           List<dynamic> list = DapperHelper.Query(sql.ToString()).ToList();
            

           if (list != null && list.Count > 0)
           {
               foreach (dynamic item in list)
               {
                   saleMapItemList itemObj = new saleMapItemList();

                   itemObj.ShopCar = item.ShopNum.ToString();
                   itemObj.ShopName = item.ShopName.ToString();
                   itemObj.Address = item.ShopAddress.ToString();
                   itemObj.id = item.orderid.ToString();
                   if (string.IsNullOrEmpty(itemObj.Address) || itemObj.Address == "not find")
                   {
                       itemObj.Address = "乌鲁木齐";
                   }
                   else
                   {
                       try
                       {
                           itemObj.Address = itemObj.Address.Split('.')[0];
                       }
                       catch
                       {
                           itemObj.Address = "乌鲁木齐";
                       }
                   }
                   itemObj.SellMoney = string.Format("{0:C}", Convert.ToDouble(item.SellMoney.ToString()));
                   if (item.DetailAddress != null)
                   {
                       itemObj.DetailAddress = item.DetailAddress.ToString();
                   }
                   itemObj.SellTime = (int)System.DateTime.Now.Subtract(Convert.ToDateTime(item.SellTime.ToString())).TotalMinutes;

                   mapList.Add(itemObj);
               }
           }
           return mapList;
       }
    }
}
