using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 地图信息
    /// </summary>
    public class Sys_SysAreaData4EchartsDAL : Base.Sys_SysAreaData4EchartsBaseDAL
    {
        /// <summary>
        /// 获取市及其对应省份
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> getHash()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT PC_Id,Json_Name from [sys_i200].[dbo].[Sys_ProvinceCityList] where id>35");

            List<dynamic> dataList = DapperHelper.Query(strSql.ToString()).ToList();


            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (dynamic dr in dataList)
            {
                dict.Add(dr.Json_Name.ToString(), Convert.ToInt32(dr.PC_Id));
            }
            return dict;
        }

        /// <summary>
        /// 获取省份及省份ID
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> getProvince()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT id,PC_Name from [sys_i200].[dbo].[Sys_ProvinceCityList] where id<=35");
            List<dynamic> dataList = DapperHelper.Query(strSql.ToString()).ToList();

            Dictionary<int, string> dict = new Dictionary<int, string>();
            foreach (dynamic dr in dataList)
            {
                dict.Add(Convert.ToInt32(dr.id), dr.PC_Name.ToString());
            }
            return dict;

        }


        /// <summary>
        /// 获得区域统计数据
        /// </summary>
        /// <param name="areaKey">统计关键值</param>
        /// <param name="sClass">统计类型</param>
        /// <param name="recDate">开始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <param name="sAreaName">省份名称 none-全部</param>
        /// <returns></returns>
        public Sys_AreaDate4EchartsMapList GetAreaDataMap(string areaKey, string sClass, DateTime recDate, DateTime endDate,string order)
        {
            Sys_AreaDate4EchartsMapList AreaDateMapList = new Sys_AreaDate4EchartsMapList();
            List<Sys_AreaData4EchartsMap> AreaDataList = new List<Sys_AreaData4EchartsMap>();

            if (sClass == "single")
            {
                //按单个日期统计之前所有数据

                string Column = "recDate, areaID, areaName, regCnt, MemberCnt, saleCnt, payCnt, smsCnt, orderSum, UserActiveCnt, UserLoginCnt, UserSleepCnt, UserLostCnt,prov_name";
                string orderSql = "";

                //判断关键值(排序处理,防止拼接Sql)
                string[] NameList = { "regCnt", "MemberCnt", "saleCnt", "payCnt", "smsCnt", "orderSum", "UserActiveCnt", "UserLoginCnt", "UserSleepCnt", "UserLostCnt", "prov_name" };
                if (NameList.Contains(areaKey))
                {
                    orderSql=(" order by ") + (areaKey) + (" desc;");
                    Column += "," + areaKey + " keyAreaValue";
                }
                //全国省市
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(Column);
                strSql.Append(" FROM Sys_SysAreaData4Echarts ");
                strSql.Append(" where datediff(day,recDate,@recDate)=0");
                strSql.Append(orderSql);
                AreaDataList = DapperHelper.Query<Sys_AreaData4EchartsMap>(strSql.ToString(), new { recDate = recDate }).ToList();

            }
            else
            {
                //按日期区间范围统计新增数据
                StringBuilder strSql = new StringBuilder();


                string Column = "areaID,areaName, sum(regCnt) as regCnt, sum(MemberCnt) as MemberCnt,sum(saleCnt) as saleCnt,sum(payCnt) as payCnt,sum(smsCnt) as smsCnt,sum(orderSum) as orderSum,sum(UserActiveCnt) as UserActiveCnt,sum(UserLoginCnt) as UserLoginCnt,sum(UserSleepCnt) as UserSleepCnt,sum(UserLostCnt) as UserLostCnt,'' prov_name";
                string orderSql = "";

                //判断关键值(排序处理,防止拼接Sql)
                string[] NameList = { "regCnt", "MemberCnt", "saleCnt", "payCnt", "smsCnt", "orderSum", "UserActiveCnt", "UserLoginCnt", "UserSleepCnt", "UserLostCnt", "prov_name" };
                if (NameList.Contains(areaKey))
                {
                    orderSql = (" order by ") + (areaKey) + (" desc ");
                    if (areaKey != "prov_name")
                    {
                        Column += ",sum(" + areaKey + ") keyAreaValue";
                    }
                    else
                    {
                        Column += ",0 keyAreaValue";
                    }
                }


                strSql.Append("SELECT ");
                strSql.Append(Column);
                strSql.Append(" into #temp");
                strSql.Append(" FROM Sys_SysAreaData4Echarts ");
                strSql.Append(" where recDate>@bgDate and recDate<@edDate group by areaName,areaID ");
                strSql.Append(orderSql + ";");
                strSql.Append("  alter table #temp alter column prov_name varchar(100); ");
                strSql.Append(" update #temp set prov_name=(select top 1 prov_name from Sys_SysAreaData4Echarts where #temp.areaID=Sys_SysAreaData4Echarts.areaID order by id desc); ");
                strSql.Append(" select * from #temp; ");
                strSql.Append(" drop table #temp; ");

                AreaDataList = DapperHelper.Query<Sys_AreaData4EchartsMap>(strSql.ToString(), new { bgDate = recDate, edDate = endDate.Date.Add(new TimeSpan(23, 59, 59)) }).ToList();

            }

            if (AreaDataList != null && AreaDataList.Count()>0)
            {
                decimal maxValue = 0;   //最大值

                Dictionary<string, int> city = getHash();
                Dictionary<int, string> province = getProvince();
                Dictionary<string, string> relation = new Dictionary<string, string>();

                foreach (var item in city)
                {
                    foreach (var prov in province)
                    {
                        if (item.Value == prov.Key)
                        {
                            relation.Add(item.Key, prov.Value);
                        }
                    }
                }

                foreach (var item in province)
                {
                    Sys_AreaData4EchartsMap AtraData = new Sys_AreaData4EchartsMap();
                    AtraData.areaName = item.Value;
                    AtraData.keyAreaValue = 0;
                    AtraData.prov_name = "prov";
                    AreaDataList.Add(AtraData);
                }

                //bool exist = false;

                foreach (var data in AreaDataList)
                {
                    if (relation.ContainsKey(data.areaName))
                    {


                        //exist = true;
                        foreach (var prov in AreaDataList)
                        {
                            if (prov.areaName == relation[data.areaName])
                            {
                                prov.keyAreaValue += data.keyAreaValue;
                                prov.MemberCnt += data.MemberCnt;
                                prov.orderSum += data.orderSum;
                                prov.payCnt += data.payCnt;
                                prov.regCnt += data.regCnt;
                                prov.saleCnt += data.saleCnt;
                                prov.smsCnt += data.smsCnt;
                                prov.UserActiveCnt += data.UserActiveCnt;
                                prov.UserLoginCnt += data.UserLoginCnt;
                                prov.UserLostCnt += data.UserLostCnt;
                                prov.UserSleepCnt += data.UserSleepCnt;
                            }
                        }
                    }
                }

                AreaDateMapList.areaDataList = AreaDataList;
                AreaDateMapList.areaCount = AreaDataList.Count;

                decimal minValue = AreaDataList[0].keyAreaValue;

                foreach (Sys_AreaData4EchartsMap AtraData in AreaDataList)
                {
                    //检测最大值
                    if (AtraData.keyAreaValue > 0)
                    {
                        if (Convert.ToDecimal(AtraData.keyAreaValue) > maxValue)
                        {
                            if (AtraData.areaName.IndexOf("其他") < 0)
                            {
                                maxValue = Convert.ToDecimal(AtraData.keyAreaValue);
                            }

                        }
                    }
                    if (AtraData.keyAreaValue > 0)
                    {
                        if (Convert.ToDecimal(AtraData.keyAreaValue) < minValue)
                        {
                            if (AtraData.areaName.IndexOf("其他") < 0)
                            {
                                minValue = Convert.ToDecimal(AtraData.keyAreaValue);
                            }

                        }
                    }
                }
                AreaDateMapList.areaMaxValue = maxValue;
                AreaDateMapList.areaMinValue = minValue;
            }

            //if (order != "")
            //{
            //    switch (order)
            //    {
            //        case "regNum":
            //            AreaDateMapList.areaDataList.OrderByDescending(x => x.regCnt);
            //            break;
            //        case "usrNum":
            //            AreaDateMapList.areaDataList.OrderByDescending(x => x.MemberCnt);
            //            break;
            //        case "saleNum":
            //            AreaDateMapList.areaDataList.OrderByDescending(x => x.saleCnt);
            //            break;
            //        case "orderNum":
            //            AreaDateMapList.areaDataList.OrderByDescending(x => x.orderSum);
            //            break;
            //        case "actNum":
            //            AreaDateMapList.areaDataList.OrderByDescending(x => x.UserActiveCnt);
            //            break;
            //        case "logNum":
            //            AreaDateMapList.areaDataList.OrderByDescending(x => x.UserLoginCnt);
            //            break;
            //    }
                
            //}

            return AreaDateMapList;
        }

        /// <summary>
        /// 获得归属地店铺信息
        /// </summary>
        /// <param name="areaName">归属地名称</param>
        /// <param name="bgDate">开始日期</param>
        /// <param name="edDate">结束日期</param>
        /// <param name="iPage">页数</param>
        /// <returns></returns>
        public Dictionary<string, object> GetAreaShopInfoEx(string areaName, DateTime bgDate, DateTime edDate, int iPage, int pageSize)
        {

            Dictionary<string, object> list = new Dictionary<string, object>(); 
            areaName = areaName.Trim('市');
            areaName = areaName.Trim('省');


            if (pageSize < 1)
            {
                pageSize = 20;
            }
            if (iPage < 1)
            {
                iPage = 1;
            }



            List<AreaShopInfoEx> areaShopInfoList = new List<AreaShopInfoEx>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select accid into #list from Sys_Account where accid in(select id from i200.dbo.T_Account where   ");
            strSql.Append(" RegTime between @bgDate and @edDate and State=1) and  sysAddress like '%'+ @AreaName +'%';             ");
            strSql.Append(" select  count(distinct accid) as accNum from #list; ");
            strSql.Append(" select * from (select row_number() over(order by accountid desc) as rowNumer,accountid,I200.dbo.T_Account.RegTime,I200.dbo.T_Account.CompanyName, ");
            strSql.Append(" max(allLoginNum) as LoginNum,max(lastLoginTime) as lastLoginTime ");
            strSql.Append(" from SysRpt_ShopDayInfo left outer join I200.dbo.T_Account on I200.dbo.T_Account.ID=SysRpt_ShopDayInfo.accountid ");
            strSql.Append(" where  SysRpt_ShopDayInfo.accountid in(select accid from #list) group by accountid,I200.dbo.T_Account.RegTime,I200.dbo.T_Account.CompanyName ");
            strSql.Append(" ) List ");
            strSql.Append(" where List.rowNumer between @bgNumber and @edNumber ");
            strSql.Append(" order by LoginNum desc ");
            strSql.Append(" drop table #list  ");

            //页数计算
            int bgNumber = ((iPage - 1) * pageSize) + 1;
            int edNumber = (iPage) * pageSize;

            var dataList = DapperHelper.QueryMultiple(strSql.ToString(), new {
                AreaName = areaName,
                bgDate = bgDate,
                edDate = edDate,
                bgNumber = bgNumber,
                edNumber = edNumber
            });




            if (dataList != null && dataList.Count > 0)
            {
                var maxCntList = dataList[0].ToList();
                int rowCount = Convert.ToInt32(maxCntList[0].accNum);
                 
                int maxPage = 0;
                if (rowCount > 0)
                {
                    maxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rowCount) / pageSize));
                }

                 

                foreach (dynamic dr in dataList[1].ToList())
                {
                    AreaShopInfoEx areaShopInfo = new AreaShopInfoEx();
                    areaShopInfo.accID = int.Parse(dr.accountid.ToString());
                    areaShopInfo.accName = dr.CompanyName.ToString();
                    areaShopInfo.regTime = DateTime.Parse(dr.RegTime.ToString());
                    areaShopInfo.loginTimes = int.Parse(dr.LoginNum.ToString());
                    areaShopInfo.lastLoginTime = DateTime.Parse(dr.lastLoginTime.ToString());
                     
                    areaShopInfoList.Add(areaShopInfo);
                }
                list["rowCount"] = rowCount;
                list["maxPage"] = maxPage;
                list["pageIndex"] = iPage;
                list["listData"] = areaShopInfoList;
            }

            return list;
        }
    }
}
