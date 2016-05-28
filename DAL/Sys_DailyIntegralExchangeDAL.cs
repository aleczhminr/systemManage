using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class Sys_DailyIntegralExchangeDAL
    {
        /// <summary>
        /// 判断是否存在某日的积分信息
        /// </summary>
        /// <param name="dayDate"></param>
        /// <returns></returns>
        public bool IsExistDayRecord(DateTime dayDate)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(*) from Sys_DailyIntegralExchange where DateDiff(day,DayDate,@dayDate)=0;");
            int reVal = 0;

            try
            {
                reVal = Convert.ToInt32(DapperHelper.ExecuteScalar(strSql.ToString(), new {dayDate = dayDate}));
            }
            catch (Exception ex)
            {
                reVal = 0;
            }

            return (reVal == 0) ? false : true;

        }

        /// <summary>
        /// 批量新增某日的积分统计
        /// </summary>
        /// <param name="models"></param>
        /// <returns>
        /// 1-已经存在当日记录
        /// 0-更新失败
        /// 2-更新成功
        /// 3-没有传入参数
        /// </returns>
        public int AddNewRecord(IntegralExchangeModel model)
        {
            if (IsExistDayRecord(model.DayTime))
            {
                return 1;
            }

            StringBuilder strSql = new StringBuilder();
            int reVal = 0;

            if (model.DataList != null && model.DataList.Count > 0)
            {
                foreach (Model.Sys_DailyIntegralExchange item in model.DataList)
                {
                    strSql.Clear();

                    strSql.Append("insert into Sys_DailyIntegralExchange (DayDate,ProductName,VisitNum,ExchangeNum,Ratio,ProductId) " +
                                  "values(@dayDate,@productName,@visitNum,@exchangeNum,@ratio,@productId);");

                    try
                    {
                        reVal = DapperHelper.Execute(strSql.ToString(), new
                        {
                            dayDate = item.DayDate,
                            productName = item.ProductName,
                            visitNum = item.VisitNum,
                            exchangeNum = item.ExchangeNum,
                            ratio = item.Ratio,
                            productId = item.ProductId
                        });

                        if (reVal <= 0)
                        {
                            return 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }

                return 2;
            }
            else
            {
                return 3;
            }
        }

        /// <summary>
        /// 修改积分商城统计数据
        /// 更新方式为全部更新
        /// </summary>
        /// <param name="dayDate"></param>
        /// <param name="model"></param>
        /// <returns>
        /// 0-没有找到对应日期的对象
        /// 1-更新出错
        /// 2-更新成功
        /// </returns>
        public int ModifyRecord(DateTime dayDate, IntegralExchangeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_DailyIntegralExchange where DayDate=@dayDate;");

            Model.Sys_DailyIntegralExchange modelForUpdate = new Model.Sys_DailyIntegralExchange();
            int reVal = 0;

            List<Model.Sys_DailyIntegralExchange> models =
                DapperHelper.Query<Model.Sys_DailyIntegralExchange>(strSql.ToString(), new {dayDate = dayDate}).ToList();

            strSql.Clear();

            if (models != null && models.Count > 0)
            {
                foreach (Model.Sys_DailyIntegralExchange item in models)
                {
                    strSql.Clear();
                    
                    modelForUpdate = model.DataList.Find(x => x.ProductName == item.ProductName);
                    strSql.Append(
                        "update Sys_DailyIntegralExchange set VisitNum=@visitNum,ExchangeNum=@exchangeNum,Ratio=@ratio " +
                        "where DayDate=@dayDate and ProductName=@productName;");

                    try
                    {
                        reVal = DapperHelper.Execute(strSql.ToString(), new
                        {
                            visitNum = modelForUpdate.VisitNum,
                            exchangeNum = modelForUpdate.ExchangeNum,
                            ratio = modelForUpdate.Ratio,
                            dayDate = dayDate,
                            productName = item.ProductName
                        });

                        if (reVal <= 0)
                        {
                            return 1;
                        }
                    }
                    catch (Exception ex)
                    {

                        return 1;
                    }
                    
                }

                return 2;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取查询日期的积分商城统计信息
        /// </summary>
        /// <param name="dayDate"></param>
        /// <returns></returns>
        public IntegralExchangeModel GetDailyExchange(DateTime dayDate)
        {
            StringBuilder strSql = new StringBuilder();
            IntegralExchangeModel model = new IntegralExchangeModel();

            model.DayTime = dayDate;

            List<Model.Sys_DailyIntegralExchange> models = new List<Sys_DailyIntegralExchange>();

            strSql.Append("select * from Sys_DailyIntegralExchange where DayDate=@dayDate;");

            try
            {
                models =
                    DapperHelper.Query<Model.Sys_DailyIntegralExchange>(strSql.ToString(), new {dayDate = dayDate})
                        .ToList();

                model.DataList = models;
            }
            catch (Exception ex)
            {
                return null;
            }

            if (models == null || models.Count <= 0)
            {
                if (AddNewRecord(GenerateNewDailyModel(dayDate))==2)
                {
                    return GetDailyExchange(dayDate);
                }
                else
                {
                    return null;
                }
            }

            else
            {
                return model;
            }

        }

        /// <summary>
        /// 生成新的积分商城统计信息
        /// </summary>
        /// <param name="dayDate"></param>
        /// <returns></returns>
        public IntegralExchangeModel GenerateNewDailyModel(DateTime dayDate)
        {
            StringBuilder strSql = new StringBuilder();
            
            IntegralExchangeModel exchangeModel = new IntegralExchangeModel();
            exchangeModel.DayTime = dayDate;

            IntegralStore.StoreProject storeModel = new IntegralStore.StoreProject();
            //Sys_DailyIntegralExchange updateModel = new Sys_DailyIntegralExchange();
            int productCount = 0;

            for (int i = 1; i < 36; i++)
            {
                strSql.Clear();
                Sys_DailyIntegralExchange updateModel = new Sys_DailyIntegralExchange();

                if (storeModel.GetModel(i) != null)
                {
                    updateModel.DayDate = dayDate;
                    updateModel.ProductName = storeModel.GetModel(i).ProjectName;
                    strSql.Append(
                        "select count(*) from i200.dbo.T_ExchangeLog where DateDiff(day,eInsertTime,@dayDate)=0 and eProjectName=@productName;");

                    productCount = DapperHelper.ExecuteScalar<int>(strSql.ToString(), new
                    {
                        dayDate = dayDate,
                        productName = updateModel.ProductName
                    });

                    updateModel.ExchangeNum = productCount;
                    updateModel.VisitNum = 0;
                    updateModel.Ratio = 0;
                    updateModel.ProductId = i;

                    exchangeModel.DataList.Add(updateModel);
                }
            }

            return exchangeModel;
        }
    }
}
