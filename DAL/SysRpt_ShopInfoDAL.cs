using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
    /// <summary>
    /// 后台店铺统计信息
    /// </summary>
    public class SysRpt_ShopInfoDAL : Base.SysRpt_ShopInfoBaseDAL
    {

        /// <summary>
        /// 得到店铺的操作汇总信息
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public T_AccountSummarize.Summarize GetAccountSummarize(int accid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 accountid,saleNum,saleMoney,userNum,goodsNum,integral,smsNum,freeSmsNum,outlayNum,outlayMoney,orderNum orderNum,orderMoney orderMoney,acc_Rep loginLong,Ratings behaveIndex from SysRpt_ShopInfo where accountid=@accid;");

            return DapperHelper.GetModel<T_AccountSummarize.Summarize>(strSql.ToString(), new { accid = accid });
        }


        #region GetAllMax获取所有变量的最大值
        /// <summary>
        /// 数据筛选专用
        /// </summary>
        /// <returns></returns>
        public FiltrateData.AllMax GetAllMax()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select max(userNum) MaxUserNum,max(saleNum) MaxSaleNum,DATEDIFF(day,min(lastLoginTime),GETDATE()) MaxLastLoginTime,max(orderMoney) MaxOrderMoney, ");
            strSql.Append(" max(orderNum) MaxOrderNum,max(smsNum) MaxSmsNum,max(goodsNum) MaxGoodsNum,max(outlayNum) MaxOutlayNum,max(allLoginNum) MaxAllLoginNum ");
            strSql.Append(" from SysRpt_ShopInfo; ");

            return DapperHelper.GetModel<FiltrateData.AllMax>(strSql.ToString());
        }
        #endregion

        /// <summary>
        /// 获取用户登陆端
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public List<LogClientDic> GetLogClient(int accId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select Count(*) Cnt,cast(LogMode as varchar(10)) Source from i200.dbo.T_LOG where Accountid=" + accId +
                          " group by LogMode " +
                          "union all " +
                          "select count(*) Cnt,AppKey Source from i200.dbo.T_Token_Api where accId=" + accId + " group by AppKey");

            return DapperHelper.Query<LogClientDic>(strSql.ToString()).ToList();
        }

        public string GetLatestLogClient(int accId)
        {
            string client = "";
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select top 1 LogMode,OperDate from i200.dbo.T_LOG  where Accountid=" + accId +
                          " order by OperDate desc");

            dynamic Log = DapperHelper.GetModel<dynamic>(strSql.ToString());
            strSql.Clear();

            strSql.Append("select top 1 AppKey,createTime from i200.dbo.T_Token_Api  where accId=" + accId +
                          " order by createTime desc");
            dynamic App = DapperHelper.GetModel<dynamic>(strSql.ToString());

            if (App != null && Log!=null)
            {
                if (Convert.ToDateTime(Log.OperDate) > Convert.ToDateTime(App.createTime))
                {
                    client = Log.LogMode.ToString();
                    if (client=="4")
                    {
                        client = App.AppKey.ToString();
                    }
                }
                else
                {
                    client = App.AppKey.ToString();
                }
            }
            else if (App==null)
            {
                client = Log.LogMode.ToString();
            }
            else
            {
                client = App.AppKey.ToString();
            }
            

            if (client == "0" || client == "1")
            {
                return "0";
            }
            else if (client.IndexOf('3') == 0)
            {
                return "8";
            }
            else if (client.IndexOf("Android") >= 0)
            {
                return "11";
            }
            else if (client.IndexOf("iPad") >= 0)
            {
                return "13";
            }
            else if (client.IndexOf("iPhone") >= 0)
            {
                return "10";
            }

            return "";
        }


        #region 根据店铺ID 得到 汇总信息 通用  不能和数据筛选器一起用

        /// <summary>
        /// 根据ID得到店铺汇总信息  通用版本
        /// </summary>
        /// <param name="accids"></param>
        public List<SysShopSummarizeInfo> GetAccountSummarize(int[] accids, string order = "", int pageIndex = 1)
        {
            List<SysShopSummarizeInfo> shopInfo = new List<SysShopSummarizeInfo>();

            string accidStr = "";
            foreach (int a in accids)
            {
                accidStr += "," + a.ToString();
            }
            if (accidStr.Length > 0)
            {
                StringBuilder strSql = new StringBuilder();

                if (string.IsNullOrEmpty(order))
                {
                    strSql.Append(" create table #accountList(accid int); ");
                    strSql.Append(" insert into #accountList(accid) ");
                    strSql.Append(" select id from I200.dbo.T_Account  where ID in(0" + accidStr + ") and State=1 ");
                    strSql.Append("  select  ");
                    strSql.Append("  a.ID,a.CompanyName,a.RegTime,c.active,b.aotjb  ");
                    strSql.Append(" ,b.endtime aotjbEndTime,b.dxunity, ");
                    strSql.Append(" c.userNum,c.goodsNum,c.saleNum ,c.smsNum ,c.outlayNum , ");
                    strSql.Append(" d.allCount ,d.userCount ,a.LoginTimeWeb ,LoginTimeLast,c.orderMoney ");
                    strSql.Append("  from i200.dbo.T_Account a left outer join i200.dbo.T_Business b on a.ID=b.accountid  ");
                    strSql.Append(" left outer join SysRpt_ShopInfo c on a.ID=c.accountid left outer join ( ");
                    strSql.Append(" select toAccId,COUNT(id) allCount,sum(case when useAccId IS null then 0 else 1 end) userCount  ");
                    strSql.Append("  from i200.dbo.T_Order_CouponList where toAccId in(select accid from #accountList) group by toAccId) d on a.ID=d.toAccId ");
                    strSql.Append(" where a.ID in(select accid from #accountList) order by a.RegTime desc");
                    strSql.Append(" drop table #accountList; ");

                    shopInfo = DapperHelper.Query<SysShopSummarizeInfo>(strSql.ToString()).ToList();
                }
                else
                {

                    int bgNumber = ((pageIndex - 1) * 15) + 1;
                    int edNumber = (pageIndex) * 15;

                    if (order=="active")
                    {
                        order = "c." + order;
                    }

                    strSql.Append(" create table #accountList(accid int); ");
                    strSql.Append(" insert into #accountList(accid) ");
                    strSql.Append(" select id from I200.dbo.T_Account  where ID in(0" + accidStr + ") and State=1 ");
                    strSql.Append(" select * from (");
                    strSql.Append("  select  row_number() over (order by " + order + " desc) rowNumber,");
                    strSql.Append("  a.ID,a.CompanyName,a.RegTime,c.active,b.aotjb  ");
                    strSql.Append(" ,b.endtime aotjbEndTime,b.dxunity, ");
                    strSql.Append(" c.userNum,c.goodsNum,c.saleNum ,c.smsNum ,c.outlayNum , ");
                    strSql.Append(" d.allCount ,d.userCount ,a.LoginTimeWeb ,LoginTimeLast,c.orderMoney ");
                    strSql.Append("  from i200.dbo.T_Account a left outer join i200.dbo.T_Business b on a.ID=b.accountid  ");
                    strSql.Append(" left outer join SysRpt_ShopInfo c on a.ID=c.accountid left outer join ( ");
                    strSql.Append(" select toAccId,COUNT(id) allCount,sum(case when useAccId IS null then 0 else 1 end) userCount  ");
                    strSql.Append("  from i200.dbo.T_Order_CouponList where toAccId in(select accid from #accountList) group by toAccId) d on a.ID=d.toAccId ");
                    strSql.Append(" where a.ID in(select accid from #accountList)) t where t.rowNumber between @bgNumber and @edNumber order by t.rowNumber;");
                    strSql.Append(" drop table #accountList; ");

                    shopInfo =
                        DapperHelper.Query<SysShopSummarizeInfo>(strSql.ToString(),
                            new {bgNumber = bgNumber, edNumber = edNumber}).ToList();
                }

                foreach (SysShopSummarizeInfo model in shopInfo)
                {
                    model.aotjbName = Enum.GetName(typeof(Model.Enum.AccountEnum.StoreVer), model.aotjb);
                    model.activeName = Enum.GetName(typeof(Model.Enum.AccountEnum.ActiveStatus), model.active);

                    strSql.Clear();
                    strSql.Append(
                        "select COUNT(*) from SysRpt_ShopActive where active in (select active from SysRpt_ShopActive where accid=@AccId and stateVal=0 and active=5) and accid=@AccId;");
                    int activeCnt = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { AccId = model.ID });

                    strSql.Clear();
                    strSql.Append(
                        "select COUNT(*) from SysRpt_ShopActive where active in (select active from SysRpt_ShopActive where accid=@AccId and stateVal=0 and active=-3) and accid=@AccId;");
                    int lostCnt = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { AccId = model.ID });

                    if (activeCnt==1)
                    {
                        model.FirstActive = true;
                    }
                    else
                    {
                        model.FirstActive = false;
                    }

                    if (lostCnt == 1)
                    {
                        model.FirstLost = true;
                    }
                    else
                    {
                        model.FirstLost = false;
                    }
                }
            }

            //shopInfo.OrderByDescending(x => x.RegTime);

            return shopInfo;
        }

        /// <summary>
        /// 获取地区用户曾经活跃和当前活跃人数 
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public LocationUsrStatus GetLocationUsrStatus(int[] accId)
        {
            LocationUsrStatus model=new LocationUsrStatus();

            string accidStr = "";
            foreach (int a in accId)
            {
                accidStr += "," + a.ToString();
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(distinct accid) FormerActive from SysRpt_ShopActive where maxactive>=5 and accid in (0" + accidStr + ") and stateVal=1;");

            model.FormerActive = DapperHelper.ExecuteScalar<int>(strSql.ToString());
            strSql.Clear();

            strSql.Append("select count(distinct accountid) NowActive from i200.dbo.T_Business where active>=5 and accountid in (0" + accidStr + ");");
            model.NowActive = DapperHelper.ExecuteScalar<int>(strSql.ToString());

            return model;
        }

        #endregion
    }
}
