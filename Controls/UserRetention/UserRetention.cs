using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;
using Utility;

namespace Controls.UserRetention
{
    public static class UserRetention
    {        public static string GetUserRetention(string dateType, DateTime bgTime, DateTime edTime, string usrType, string regSource, string agent)
        {
            UserRetentionModel usrRetentionModel = new UserRetentionModel();

            #region 留存数据添加Redis缓存
            var cacheKey = "RetentionStatus:" + bgTime.ToString("yyyyMMdd") + edTime.ToString("yyyyMMdd") + usrType + regSource + agent + dateType;
            var cacheResult = "";

            try
            {
                //Cache Redis
                cacheResult = RedisHelper.GetKey(cacheKey);
                if (cacheResult != null && cacheResult != "")
                {
                    usrRetentionModel = CommonLib.Helper.JsonDeserializeObject<UserRetentionModel>(cacheResult);
                }
                else
                {
                    usrRetentionModel = UserRetentionBLL.GetUserRetention(dateType, bgTime, edTime, usrType, regSource, agent);
                    if (usrRetentionModel.dataList != null && usrRetentionModel.dataList.Count > 0)
                    {
                        var cacheExpire = 60 * 60 * 12;
                        //var item = usrRetentionModel.dataList[0];
                        //TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(item.Date));
                        //if (ts.TotalDays < 1 && item.NewActive == 0)
                        //{
                        //    cacheExpire = 60 * 60 * 3;
                        //}
                        RedisHelper.SetKey(cacheKey, CommonLib.Helper.JsonSerializeObject(usrRetentionModel), cacheExpire);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("留存数据获取错误", ex);
            }
            #endregion
            //usrRetentionModel = UserRetentionBLL.GetUserRetention(dateType, bgTime, edTime, usrType, regSource, agent);

            return CommonLib.Helper.JsonSerializeObject(usrRetentionModel,"");
        }

        public static string GetUserRetentionEx(string dateType, DateTime bgTime, DateTime edTime, string usrType, string regSource)
        {
            UserRetentionModel usrRetentionModel = new UserRetentionModel();

            usrRetentionModel = UserRetentionBLL.GetUserRetentionEx(dateType, bgTime, edTime, usrType, regSource);

            return CommonLib.Helper.JsonSerializeObject(usrRetentionModel, "");
        }

        public static string GetActiveStatus(DateTime stDate, DateTime edDate)
        {
            ActiveChangeModel activeModel = new ActiveChangeModel();
            var cacheKey = "IndexActiveStatus:" + stDate.ToString("yyyyMMdd") + edDate.ToString("yyyyMMdd");
            var cacheResult = "";
            try
            {
                //Cache Redis
                cacheResult = RedisHelper.GetKey(cacheKey);
                if (cacheResult != null && cacheResult != "")
                {
                    activeModel = CommonLib.Helper.JsonDeserializeObject<ActiveChangeModel>(cacheResult);
                }
                else
                {
                    activeModel = UserRetentionBLL.GetActiveStatus(stDate, edDate);
                    if (activeModel.dataList != null && activeModel.dataList.Count > 0)
                    {
                        var cacheExpire = 60 * 60 * 12;
                        var item = activeModel.dataList[0];
                        TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(item.Date));
                        if (ts.TotalDays < 1 && item.NewActive == 0)
                        {
                            cacheExpire = 60 * 60 * 3;
                        }
                        RedisHelper.SetKey(cacheKey, CommonLib.Helper.JsonSerializeObject(activeModel), cacheExpire);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("活跃列表获取错误", ex);
            }

            return CommonLib.Helper.JsonSerializeObject(activeModel, "MM-dd");
        }

        /// <summary>
        /// 平均留存率测试方法
        /// </summary>
        /// <param name="dateType"></param>
        /// <param name="bgTime"></param>
        /// <param name="edTime"></param>
        /// <param name="usrType"></param>
        /// <param name="regSource"></param>
        /// <returns></returns>
        public static string GetUserRetentionTest(string dateType, DateTime bgTime, DateTime edTime, string usrType, string regSource)
        {
            UserRetentionModel usrRetentionModel = new UserRetentionModel();

            usrRetentionModel = UserRetentionBLL.GetUserRetentionTest(dateType, bgTime, edTime, usrType, regSource);

            return CommonLib.Helper.JsonSerializeObject(usrRetentionModel, "");
        }
    }
}
