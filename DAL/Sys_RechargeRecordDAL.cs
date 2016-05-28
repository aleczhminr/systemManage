using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DAL
{
    public class Sys_RechargeRecordDAL
    {
        //public List<RechargeRecord> GetRecordList()
        //{
        //    StringBuilder strSql = new StringBuilder();

        //}

        /// <summary>
        /// 检查表中是否存在该订单号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public int CheckExist(int oid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("if(exists(select * from Sys_RechargeRecord where oid=@oid)) " +
                          "select 1 " +
                          "else " +
                          "select 0");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString(), new { oid = oid });
        }

        /// <summary>
        /// 获取充值记录表中最近的一个订单Id
        /// </summary>
        /// <returns></returns>
        public int GetLastOid()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select top 1 oid from Sys_RechargeRecord order by oid desc;");

            return DapperHelper.ExecuteScalar<int>(strSql.ToString());
        }

        /// <summary>
        /// 添加一条充值记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNewRecord(RechargeRecord model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "insert into Sys_RechargeRecord (AccId,OrderNo,State,CardNum,RealNum,PaidNum,GapNum,CardName,AddTime,Oid) Values " +
                "(@accId,@orderNo,@state,@cardNum,@realNum,@paidNum,@gapNum,@cardName,@addTime,@oid);");

            try
            {
                return DapperHelper.Execute(strSql.ToString(), new
                {
                    accId = model.AccId,
                    orderNo = model.OrderNo,
                    state = model.State,
                    cardNum = model.CardNum,
                    realNum = model.RealNum,
                    paidNum=model.PaidNum,
                    gapNum=model.GapNum,
                    cardName = model.CardName,
                    addTime = model.AddTime,
                    oid = model.Oid
                });
            }
            catch (Exception ex)
            {
                Logger.Error("添加充值记录出错", ex);
                return 0;
            }
        }

        /// <summary>
        /// 返回充值成功的摘要数据
        /// </summary>
        /// <returns></returns>
        public RecordSummary GetRecordSum()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(
                "select count(*) SuccessCnt,0 FailCnt,sum(PaidNum) UsrPaid,sum(RealNum) WePaid,sum(GapNum) GapSum,count(distinct AccId) AccidCount from Sys_RechargeRecord where state=1;");

            return DapperHelper.Query<RecordSummary>(strSql.ToString()).ToList()[0];
        }
    }
}
