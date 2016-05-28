using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Annotations;
using Model;
namespace DAL
{
   /// <summary>
   /// 商品信息
   /// </summary>
   public  class T_GoodsInfoDAL:Base.T_GoodsInfoBaseDAL
    {
       public const double EARTH_RADIUS = 6378.137;//地球半径
       /// <summary>
       /// 得到基本列表信息
       /// </summary>
       /// <param name="pageIndex">页号</param>
       /// <param name="pageSize">每页面显示数</param>
       /// <param name="sqlWhere">条件</param>
       /// <param name="filedOrder">排序</param>
       /// <returns></returns>
       public List<T_GoodsInfoBasic> GetBaseList(int pageIndex, int pageSize, List<DapperWhere> sqlWhere, string filedOrder)
       {
           string columnName = "gid, gMaxID,gMaxName,gMinID,gMinName,gName,gSpecification,gPrice,gQuantity,isDown,gPicUrl,gBarcode";

           return GetList<T_GoodsInfoBasic>(pageIndex, pageSize, columnName, sqlWhere, filedOrder);
       }

       /// <summary>
       /// 得到店铺地址地图列表
       /// </summary>
       /// <param name="DBwhere"></param>
       /// <returns></returns>
       public List<dynamic> GetGoodsAccountAddressList(string gName)
       {
           //List<saleMapItemList> mapList = new List<saleMapItemList>();
           string where = "";

           if (gName.Length > 0)
           {
               where = " where gName like '%'+ @gName +'%'";
           }

           StringBuilder strSql = new StringBuilder();
           strSql.Append(" select top 500 id,gName into #list from OTwoO.dbo.GoodsInfo " + where + " ;");
           strSql.Append(" select * into #info from #list inner join ( ");
           strSql.Append(" select  gid,accid,accountName,maxClass,minClass,goodsNum,price from OTwoO.dbo.GoodsList where  ");
           strSql.Append(" gid in(select id from #list)) a on a.gid=#list.id; ");
           strSql.Append(" select * from #info inner join( ");
           strSql.Append(" select accountid,TrueConty from i200.dbo.T_Account_User where  ");
           strSql.Append(" accountid in(select accid from #info) and grade='管理员' and TrueConty is not null) a ");
           strSql.Append(" on a.accountid=#info.accid; ");
           strSql.Append(" drop table #list; ");
           strSql.Append(" drop table #info; ");

           List<dynamic> list = DapperHelper.Query<dynamic>(strSql.ToString(), new {gName = gName}).ToList();

           return list;
       }

       public List<MobileMapModel> GetGoodsAccountAddressListEx(string gName)
       {
           
           string where = "";

           if (!string.IsNullOrEmpty(gName))
           {
               where = " where gName like '%'+ @gName +'%'";
           }

           StringBuilder strSql = new StringBuilder();
           strSql.Append(" select top 500 id,gName into #list from OTwoO.dbo.GoodsInfo " + where + " ;");
           strSql.Append(" select * into #info from #list inner join ( ");
           strSql.Append(" select  gid,accid,accountName,maxClass,minClass,goodsNum,price from OTwoO.dbo.GoodsList where  ");
           strSql.Append(" gid in(select id from #list)) a on a.gid=#list.id; ");

           strSql.Append(
               " select * into #all from #info left join (select picUrl,gid as goodsId from OTwoO.dbo.goodsPic) g on g.goodsId=#info.gid;");

           strSql.Append(" select * from #all inner join( ");
           strSql.Append(" select accountid,TrueConty from i200.dbo.T_Account_User where  ");
           strSql.Append(" accountid in(select accid from #info) and grade='管理员' and TrueConty is not null) a ");
           strSql.Append(" on a.accountid=#all.accid left join ( select companyaddress,phonenumber,id from i200.dbo.T_Account ) t on t.ID=#all.accid left join ( select accid aid,aa_ShortUrl as ShortUrl from i200.dbo.t_App_Au where appName='HTML' ) s on #all.accid=s.aid;");
           strSql.Append(" drop table #list; ");
           strSql.Append(" drop table #info; ");
           strSql.Append(" drop table #all; ");

           return DapperHelper.Query<MobileMapModel>(strSql.ToString(), new { gName = gName }).ToList().OrderByDescending(x => x.picUrl).ToList();
       }

       public List<ShopLocationModel> GetShopAround(string lng, string lat)
       {
           StringBuilder strSql = new StringBuilder();

           double lngD = Convert.ToDouble(lng);
           double latD = Convert.ToDouble(lat);

           strSql.Append("select ap.AccId,ap.PredLng Longitude,ap.PredLat Latitude,ta.CompanyName,ta.UserRealName,ta.PhoneNumber,ta.CompanyAddress,tb.active as ActiveStatus from Sys_AddressPrecision ap " +
                         "left join i200.dbo.T_Account ta on ap.AccId=ta.ID " +
                         "left join i200.dbo.T_Business tb on ap.AccId=tb.accountid " +
                         "where ap.PredLat is not null " +
                         "and CAST(ap.PredLng as decimal(18,6))-@lngD<0.1 and CAST(ap.PredLng as decimal(18,6))-@lngD>-0.1 " +
                         "and CAST(ap.PredLat as decimal(18,6))-@latD<0.1 and CAST(ap.PredLat as decimal(18,6))-@latD>-0.1 " +
                         "and ta.LoginTimeWeb>5;");
           try
           {
               //获取符合条件的店铺
               List<ShopLocationModel> model =
               DapperHelper.Query<ShopLocationModel>(strSql.ToString(), new { lngD = lngD, latD = latD }).ToList();

               return model;
           }
           catch (Exception ex)
           {
               return null;               
           }
           
           //string where = "where ";
           //int count = 0;

           //string[] strList = location.Split(new char[] {'省', '市', '区', '路'});

           //foreach (string str in strList)
           //{
           //    if (str != "")
           //    {
           //        count++;
           //        if (location.Contains("北京") || location.Contains("天津") || location.Contains("上海") || location.Contains("重庆"))
           //        {
           //            if (count < 2)
           //            {
           //                where += " companyaddress like '%" + str + "%' or ";
           //            }
           //        }
           //        else
           //        {
           //            if (count < 3)
           //            {                         
           //               where += " companyaddress like '%" + str + "%' and "; 
           //            }
           //        }

           //    }
           //}

           //where = (location.Contains("北京") || location.Contains("天津") || location.Contains("上海") || location.Contains("重庆"))
           //    ? where.Substring(0, where.LastIndexOf('o'))
           //    : where.Substring(0, where.LastIndexOf('a'));

           //List<MobileMapModel> list = SeperateModels(where);
           //List<MobileMapModel> listEx = list.Distinct(new ModelComparer()).ToList();
           
           //return listEx.OrderByDescending(x=>x.picUrl).Take(15).ToList();
       }

       public List<ShopLocationModel> GetSpecShop(string shopName)
       {
           StringBuilder strSql = new StringBuilder();

           strSql.Append("select ap.AccId,ap.PredLng Longitude,ap.PredLat Latitude,ta.CompanyName,ta.UserRealName,ta.PhoneNumber,ta.CompanyAddress,tb.active as ActiveStatus from Sys_AddressPrecision ap " +
                         "left join i200.dbo.T_Account ta on ap.AccId=ta.ID " +
                         "left join i200.dbo.T_Business tb on ap.AccId=tb.accountid " +
                         "where ta.CompanyName like '%" + shopName + "%' ");
           try
           {
               //获取符合条件的店铺
               List<ShopLocationModel> model =
               DapperHelper.Query<ShopLocationModel>(strSql.ToString()).ToList();

               return model;
           }
           catch (Exception ex)
           {
               return null;
           }
       }

       public List<MobileMapModel> SeperateModels(string where)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(" select id,gName into #list from OTwoO.dbo.GoodsInfo;");
           strSql.Append(" select * into #info from #list inner join ( ");
           strSql.Append(" select  gid,accid,accountName,maxClass,minClass,goodsNum,price from OTwoO.dbo.GoodsList where  ");
           strSql.Append(" gid in(select id from #list)) a on a.gid=#list.id; ");

           strSql.Append(
               " select * into #all from #info left join (select picUrl,gid as goodsId from OTwoO.dbo.goodsPic) g on g.goodsId=#info.gid;");

           strSql.Append(" select top 50000 * from #all inner join( ");
           strSql.Append(" select accountid,TrueConty from i200.dbo.T_Account_User where  ");
           strSql.Append(" accountid in(select accid from #info) and grade='管理员' and TrueConty is not null) a ");
           strSql.Append(" on a.accountid=#all.accid left join ( select companyaddress,phonenumber,id from i200.dbo.T_Account " + where + "  ) t on t.ID=#all.accid  left join ( select accid aid,aa_ShortUrl as ShortUrl from i200.dbo.t_App_Au where appName='HTML' ) s on #all.accid=s.aid where  companyaddress is not null;");

           strSql.Append(" drop table #list; ");
           strSql.Append(" drop table #info; ");
           strSql.Append(" drop table #all; ");

           return DapperHelper.Query<MobileMapModel>(strSql.ToString()).ToList();
       }

       public double rad(double d)
       {
           return d * Math.PI / 180.0;
       }

       public double GetDistance(double lat1, double lng1, double lat2, double lng2)
       {
           double radLat1 = rad(lat1);
           double radLat2 = rad(lat2);
           double a = radLat1 - radLat2;
           double b = rad(lng1) - rad(lng2);

           double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
           Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
           s = s * EARTH_RADIUS;
           s = Math.Round(s * 10000) / 10000;
           return s;
       }
    }

   public class ModelComparer : IEqualityComparer<MobileMapModel>
   {
       public bool Equals(MobileMapModel x, MobileMapModel y)
       {
           return x.accid == y.accid;
       }
       public int GetHashCode(MobileMapModel obj)
       {
           return obj.accid.GetHashCode();
       }
   }

    
    
}
