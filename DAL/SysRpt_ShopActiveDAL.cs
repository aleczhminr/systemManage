using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Model;
using Dapper;
using Utility;

namespace DAL
{
    /// <summary>
    /// 店铺活跃
    /// </summary>
    public class SysRpt_ShopActiveDAL : Base.SysRpt_ShopActiveBaseDAL
    {

        /// <summary>
        /// 得到某些天中 每天新增的活跃用户数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<dynamic> GetNewActiveUser(DateTime startTime, DateTime endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select startTime dayDate,COUNT(*) activeNum from SysRpt_ShopActive where startTime>=@startTime and startTime<=@endTime  and active>4 and maxactive<5  group by startTime; ");
            return DapperHelper.Query(strSql.ToString(), new { startTime = startTime, endTime = endTime }).ToList();
        }
        /// <summary>
        /// 根据条件查看活跃信息数据（返回一些汇总信息）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="orderBy"></param>
        public SysShopActiveList GetListContainSummary(int pageIndex, int pageSize, List<DapperWhere> sqlWhere)
        {
            SysShopActiveList activeList = new SysShopActiveList();

            pageIndex = pageIndex > 0 ? pageIndex : 1;
            StringBuilder sqlStr = new StringBuilder();



            string whereSt = "";
            Dictionary<string, object> parm = new Dictionary<string, object>();
            foreach (DapperWhere item in sqlWhere)
            {
                if (whereSt.Length > 0)
                {
                    whereSt += " and ";
                }
                whereSt += item.Where;
                parm[item.ColumnName] = item.Value;
            }


            sqlStr.Append(" select DISTINCT accid into #activelist from SysRpt_ShopActive ");

            if (whereSt.Length > 0)
            {
                sqlStr.Append(" where " + whereSt + " ");
            }
            sqlStr.Append(";");
            sqlStr.Append(" declare @maxCount int;  ");
            sqlStr.Append(" select @maxCount=COUNT(*) from #activelist ;   ");
            sqlStr.Append(" select top (" + pageSize.ToString() + ") accid from #activelist  where accid not in( ");
            sqlStr.Append(" select top " + (pageSize * (pageIndex - 1)).ToString() + " accid from #activelist order by accid desc ");
            sqlStr.Append(" ) order by accid desc; ");
            sqlStr.Append(" select COUNT(*) countNum from #activelist;  ");
            sqlStr.Append(" drop table #activelist;  ");
            var data = DapperHelper.QueryMultiple(sqlStr.ToString(), parm);



            activeList.rowCount = 0;

            if (data.Count > 0)
            {
                List<int> accountIdList = new List<int>();
                foreach (dynamic item in data[0].ToList())
                {
                    accountIdList.Add(Convert.ToInt32(item.accid));
                }
                SysRpt_ShopInfoDAL shopDal = new SysRpt_ShopInfoDAL();
                activeList.shopList = shopDal.GetAccountSummarize(accountIdList.ToArray());
            }
            if (data.Count > 1)
            {
                activeList.rowCount = Convert.ToInt32(data[1].ToList().First().countNum);
            }

            return activeList;
        }


        /// <summary>
        /// 分组取交集
        /// </summary>
        /// <param name="mainSqlWhere"></param>
        /// <param name="followSqlWhere"></param>
        /// <returns></returns>
        public SysShopActiveList GetGroupListContainSummary(int pageIndex, int pageSize, List<DapperWhere> mainSqlWhere, List<DapperWhere> followSqlWhere)
        {
            SysShopActiveList activeList = new SysShopActiveList();

            pageIndex = pageIndex > 0 ? pageIndex : 1;
            Dictionary<string, object> sqlParm = new Dictionary<string, object>();
            #region 主要条件

            string mainWhereSt = "";
            foreach (DapperWhere item in mainSqlWhere)
            {
                if (mainWhereSt.Length > 0)
                {
                    mainWhereSt += " and ";
                }
                mainWhereSt += item.Where;
                sqlParm[item.ColumnName] = item.Value;
            }

            #endregion

            #region 次要条件
            string followWhereSt = "";
            foreach (DapperWhere item in followSqlWhere)
            {
                if (followWhereSt.Length > 0)
                {
                    followWhereSt += " and ";
                }
                followWhereSt += item.Where;
                sqlParm[item.ColumnName] = item.Value;
            }

            #endregion



            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select DISTINCT accid into #main from SysRpt_ShopActive ");
            if (mainWhereSt.Length > 0)
            {
                strSql.Append(" where " + mainWhereSt);
            }
            strSql.Append(" ; ");

            strSql.Append(" select DISTINCT accid into #follow from SysRpt_ShopActive  ");
            if (followWhereSt.Length > 0)
            {
                strSql.Append(" where " + followWhereSt);
            }
            strSql.Append(" ; ");
            strSql.Append(" delete #main where #main.accid not in(select accid from #follow); ");
            strSql.Append(" drop table #follow; ");
            strSql.Append(" select top " + pageSize.ToString() + " accid from #main where accid not in(select top " + (pageSize * (pageIndex - 1)) + " accid from #main order by accid desc)order by accid desc; ");
            strSql.Append(" select COUNT(*) countNum from #main;  ");
            strSql.Append(" drop table #main; ");

            var dsJson = DapperHelper.QueryMultiple(strSql.ToString(), sqlParm);

            activeList.rowCount = 0;

            if (dsJson.Count > 0)
            {
                List<int> accountIdList = new List<int>();
                foreach (dynamic item in dsJson[0].ToList())
                {
                    accountIdList.Add(Convert.ToInt32(item.accid));
                }
                SysRpt_ShopInfoDAL shopDal = new SysRpt_ShopInfoDAL();
                activeList.shopList = shopDal.GetAccountSummarize(accountIdList.ToArray());
            }
            if (dsJson.Count > 1)
            {
                activeList.rowCount = Convert.ToInt32(dsJson[1].ToList().First().countNum);
            }

            return activeList;
        }

        /// <summary>
        /// 获取某天店铺状态
        /// </summary>
        /// <param name="dayDate"></param>
        /// <returns></returns>
        public ActiveStatus GetdailyStatus(DateTime dayDate)
        {
            ActiveStatus model = new ActiveStatus();
            DateTime dt = DateTime.Now;
            DateTime fDt = DateTime.Now.AddDays(-1);

            bool isToday = dayDate.ToShortDateString() == dt.ToShortDateString() ? true : false;
            bool isYesterday = dayDate.AddDays(-1).ToShortDateString() == fDt.ToShortDateString() ? true : false;

            if (CheckShopActive(dayDate) == 0 || isToday || isYesterday)
            {
                model = GenerateActiveModel(dayDate);
            }
            else
            {
                model = GetShopActive(dayDate);
            }


            return model;
        }

        /// <summary>
        /// 检查是否有当日数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int CheckShopActive(DateTime dt)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("if exists(select * from Sys_TempActiveStatus where DateDiff(day,ShowDate,@date)=0)" +
                          " select 1;" +
                          " else " +
                          " select 0;");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { date = dt });
        }

        /// <summary>
        /// 插入当日数据
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int InsertShopActive(ActiveStatus model)
        {
            if (CheckShopActive(model.ShowDate)==1)
            {
                UpdateTodayActive(model);
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sys_TempActiveStatus " +
                          "(ShowDate,AllUsr,NewReg,UnknownUsr,RegAttention,Attention,LoginUsr," +
                          "RegUsr,ActiveUsr,FaithUsr,SleepUsr,OutUsr,Time) " +
                          "Values(@ShowDate,@AllUsr,@NewReg,@UnknownUsr," +
                          "@RegAttention,@Attention,@LoginUsr,@RegUsr,@ActiveUsr,@FaithUsr,@SleepUsr,@OutUsr,@Time);");

            return DapperHelper.Execute(strSql.ToString(), new
            {
                ShowDate = model.ShowDate,
                AllUsr = model.AllUsr,
                NewReg = model.NewReg,
                UnknownUsr = model.UnknownUsr,
                RegAttention = model.RegAttention,
                Attention = model.Attention,
                LoginUsr = model.LoginUsr,
                RegUsr = model.RegUsr,
                ActiveUsr = model.ActiveUsr,
                FaithUsr = model.FaithUsr,
                SleepUsr = model.SleepUsr,
                OutUsr = model.OutUsr,
                Time = model.Time
            });
        }

        /// <summary>
        /// 获取店铺状态
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public ActiveStatus GetShopActive(DateTime dt)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select top 1 * from Sys_TempActiveStatus where DateDiff(day,ShowDate,@date)=0;");

            return DapperHelper.GetModel<ActiveStatus>(strSql.ToString(), new { date = dt });
        }

        /// <summary>
        /// 根据当日时间生成新的活跃数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public ActiveStatus GenerateActiveModel(DateTime dt)
        {
            ActiveStatus model = new ActiveStatus();
            StringBuilder strSql = new StringBuilder();

            model.ShowDate = dt;
            model.Time = dt;

            strSql.Append("select active,COUNT(distinct accid) cnt from Sys_I200.dbo.SysRpt_ShopActive " +
                      "where startTime<=@dayDate and updatetime>=@dayDate " +
                      "group by active");

            List<ActiveModel> modelData =
                DapperHelper.Query<ActiveModel>(strSql.ToString(), new { dayDate = dt }).ToList();

            foreach (var item in modelData)
            {
                switch (item.active)
                {
                    case -3:
                        //流失用户
                        model.OutUsr = item.cnt;
                        break;
                    case -1:
                        //休眠用户
                        model.SleepUsr = item.cnt;
                        break;
                    case 1:
                        //新注册用户
                        model.NewReg = item.cnt;
                        break;
                    case 3:
                        //需关怀用户
                        model.RegAttention = item.cnt;
                        break;
                    case 4:
                        //流失需关怀用户
                        model.Attention = item.cnt;
                        break;
                    case 5:
                        //活跃用户
                        model.ActiveUsr = item.cnt;
                        break;
                    case 7:
                        //忠诚用户
                        model.FaithUsr = item.cnt;
                        break;
                }
            }

            //获取当日注册人数
            strSql.Clear();
            strSql.Append("select count(*) from i200.dbo.T_Account where datediff(day,regtime,@dayDate)=0 and state=1;");

            model.RegUsr = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { dayDate = dt });

            //获取当日登录人数
            strSql.Clear();
            strSql.Append(
                "select COUNT(distinct Accountid) from i200.dbo.T_LOG where DATEDIFF(DAY,OperDate,@dayDate)=0;");

            model.LoginUsr = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { dayDate = dt });

            strSql.Clear();
            strSql.Append(
                "select COUNT(distinct ID) from i200.dbo.T_Account where state=1 and regtime<=@dayDate;");

            model.AllUsr = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { dayDate = dt });

            if (CheckShopActive(dt) == 0)
            {
                InsertShopActive(model);
            }
            else
            {
                UpdateTodayActive(model);                                
            }

            return model;
        }

        /// <summary>
        /// 更新今日活跃状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateTodayActive(ActiveStatus model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update Sys_TempActiveStatus set " +
                          "AllUsr=@AllUsr," +
                          "NewReg=@NewReg," +
                          "UnknownUsr=@UnknownUsr," +
                          "RegAttention=@RegAttention," +
                          "Attention=@Attention," +
                          "LoginUsr=@LoginUsr," +
                          "RegUsr=@RegUsr," +
                          "ActiveUsr=@ActiveUsr," +
                          "FaithUsr=@FaithUsr," +
                          "SleepUsr=@SleepUsr," +
                          "OutUsr=@OutUsr," +
                          "Time=@Time " +
                          "where datediff(day,ShowDate,@date)=0;");

            return DapperHelper.Execute(strSql.ToString(), new
            {
                date = model.ShowDate,
                AllUsr = model.AllUsr,
                NewReg = model.NewReg,
                UnknownUsr = model.UnknownUsr,
                RegAttention = model.RegAttention,
                Attention = model.Attention,
                LoginUsr = model.LoginUsr,
                RegUsr = model.RegUsr,
                ActiveUsr = model.ActiveUsr,
                FaithUsr = model.FaithUsr,
                SleepUsr = model.SleepUsr,
                OutUsr = model.OutUsr,
                Time = model.Time
            });
        }

        /// <summary>
        /// 获取店铺活跃图表的列数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public dynamic GetActiveStatusList(DateTime dt, string col)
        {
            StringBuilder strSql = new StringBuilder();

            GenerateActiveModel(dt);
            strSql.Append("select " + col + " from Sys_TempActiveStatus where DateDiff(day,ShowDate,@date)=0;");

            //if (CheckShopActive(dt) == 1)
            //{                
                
            //}
            //else
            //{
            //    GenerateActiveModel(dt);
            //    strSql.Append("select " + col + " from Sys_TempActiveStatus where DateDiff(day,ShowDate,@date)=0;");
            //}

            try
            {
                return DapperHelper.GetModel<dynamic>(strSql.ToString(), new { date = dt });
            }
            catch (Exception ex)
            {
                Logger.Error("获取活跃记录出错", ex);
                return null;
            }
            
        }
    }
}
