using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class IntegralStatDAL
    {
        /// <summary>
        /// 获取兑换的各类商品的比例
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<IntPie> GetExRatio(DateTime stDate, DateTime edDate)
        {
            List<IntPie> dic = new List<IntPie>();
           

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select eProjectName Name, count(*) Value " +
                          "from [i200].[dbo].[T_ExchangeLog] " +
                          "where eInsertTime<@edDate and eInsertTime>@stDate group by eProjectName;");
            try
            {
                dic =
                    DapperHelper.Query<IntPie>(strSql.ToString(), new {stDate = stDate, edDate = edDate}).ToList();
            }
            catch (Exception ex)
            {

                return dic;
            }
            

            return dic;
        }
        
        /// <summary>
        /// 兑换的生意专家和实物商品的比例
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<IntPie> GetGoodsType(DateTime stDate, DateTime edDate)
        {
            List<IntPie> dic = new List<IntPie>();

            IntPie entity = new IntPie("实物产品", 0);
            IntPie notEntity = new IntPie("生意专家产品", 0);



            IntegralStore.StoreProject storeModel = new IntegralStore.StoreProject();
            List<string> entityProduct = new List<string>();

            for (int i = 1; i < 36; i++)
            {
                if (storeModel.GetModel(i) != null)
                {
                    if (storeModel.GetModel(i).isEntity==1)
                    {
                        entityProduct.Add(storeModel.GetModel(i).ProjectName);
                    }
                }
            }

            List<IntPie> rawData = GetExRatio(stDate, edDate);

            if (rawData != null)
            {
                foreach (IntPie item in rawData)
                {
                    if (entityProduct.Contains(item.Name))
                    {
                        entity.Value += item.Value;
                    }
                    else
                    {
                        notEntity.Value += item.Value;
                    }
                }

                dic.Add(entity);
                dic.Add(notEntity);

                return dic;
            }

            return null;

        }

        /// <summary>
        /// 每种商品的访客和兑换比
        /// </summary>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public List<DecimalPie> GetVisitRatio(DateTime stDate, DateTime edDate)
        {
            List<DecimalPie> dic = new List<DecimalPie>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(
                "select ProductName Name,sum(Ratio)/count(*) Value " +
                "from [Sys_I200].[dbo].[Sys_DailyIntegralExchange] " +
                "where Daydate between @stDate and @edDate group by ProductName;");

            try
            {
                dic =
                    DapperHelper.Query<DecimalPie>(strSql.ToString(), new {stDate = stDate, edDate = edDate}).ToList();
            }
            catch (Exception ex)
            {

                return dic;
            }

           
            return dic;
        }

        public Dictionary<string, List<DecimalPie>> GetPaidType(DateTime stDate, DateTime edDate)
        {
            Dictionary<string, List<DecimalPie>> dataDic = new Dictionary<string, List<DecimalPie>>();
            List<DecimalPie> dic = new List<DecimalPie>();
            List<DecimalPie> dicSec = new List<DecimalPie>();
            List<DecimalPie> dicAppend = new List<DecimalPie>();

            StringBuilder strSql = new StringBuilder();

            
            strSql.Append("select Remark Name," +
                          "sum( case when (cast(FinialVal as int)>cast(OriginalVal as int)) then cast(EditVal as int) else 0 end ) Value " +
                          "FROM [i200].[dbo].[T_LogInfo] where CreatTime>@stDate and CreatTime<@edDate and Keys='Integral' and LogType=17 group by Remark;");

            dicSec = DapperHelper.Query<DecimalPie>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();
            dicSec.RemoveAll(x => x.Value == 0);

            strSql.Clear();

            strSql.Append(
                "SELECT LogType Name," +
                "sum( case when (cast(FinialVal as int)>cast(OriginalVal as int)) then cast(EditVal as int) else 0 end ) Value " +
                "FROM [i200].[dbo].[T_LogInfo] where CreatTime>@stDate and CreatTime<@edDate and Keys='Integral' group by LogType;");
            try
            {
                dic =
                    DapperHelper.Query<DecimalPie>(strSql.ToString(), new { stDate = stDate, edDate = edDate }).ToList();

                foreach (DecimalPie item in dic)
                {
                    switch (item.Name)
                    {
                        case "1":
                            item.Name = "积分兑换";
                            break;  
                        case "2":
                            item.Name = "抽奖活动";
                            break;
                        case "4":
                            item.Name = "订单购买";
                            break;
                        case "5":
                            item.Name = "支持生意专家";
                            break;
                        case "6":
                            item.Name = "分享生意专家 完善资料  分享视频";
                            break;
                        case "7":
                            item.Name = "每日签到";
                            break;
                        case "8":
                            item.Name = "每日心情";
                            break;
                        case "9":
                            item.Name = "关注微信";
                            break;
                        case "10":
                            item.Name = "论坛金币兑换";
                            break;
                        case "11":
                            item.Name = "完成会员引导";
                            break;
                        case "12":
                            item.Name = "完成商品引导";
                            break;
                        case "13":
                            item.Name = "完成销售引导";
                            break;
                        case "14":
                            item.Name = "邀请注册";
                            break;
                        case "16":
                            item.Name = "新手任务";
                            break;
                        case "17":
                            item.Name = "每日行为";
                            break;
                    }

                }

                dic.RemoveAll(x => x.Value == 0);
            }
            catch (Exception ex)
            {

                return null;
            }

            foreach (DecimalPie item in dic)
            {
                dicAppend.Add(item);
            }
            dicAppend.RemoveAll(x => x.Name == "每日行为");

            foreach (DecimalPie item in dicSec)
            {
                dicAppend.Add(item);
            }


            dataDic.Add("fData", dic);
            dataDic.Add("sData", dicAppend);

            return dataDic;
        }

        
    }
}
