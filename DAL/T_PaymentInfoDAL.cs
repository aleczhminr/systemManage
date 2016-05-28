using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DAL
{
    public class T_PaymentInfoDAL
    {
        public int Add(AlipayUserInfo model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("if(not exists(select accId from T_PaymentInfo where accId=@accId)) " +
                          "begin " +
                          "INSERT INTO T_PaymentInfo(accId, InfoStatus, alipayAccount, alipayPID, alipayKey, StoreName, StoreIndustry, StoreAdress, RealName, Phone, TelNumber, Email, Remark) " +
                          "VALUES (@accId,0,@aliAccount,@aliPid,@aliKey,@accName,'','','',@phoneNumber,'','',''); " +
                          "end " +
                          "if(not exists(select accId from t_App_Au where accid=@accId and appkey=10)) " +
                          "begin " +
                          "INSERT INTO t_App_Au(accid, appkey, appName, stattime, endtime, aa_time, aa_remark, aa_ShortUrl, aa_Status) " +
                          "VALUES (@accId,10,'支付宝收款',GETDATE(),'2015-12-31',GETDATE(),'','',1); " +
                          "end ");

            try
            {
                return HelperForFrontend.Execute(strSql.ToString(), new
                {
                    accId = model.AccId,
                    aliAccount = model.AliAccount,
                    aliPid = model.AliPid,
                    aliKey = model.AliKey,
                    accName = model.AccName,
                    phoneNumber = model.PhoneNum
                });
            }
            catch (Exception ex)
            {
                Logger.Error("添加用户支付宝收款信息失败", ex);
                return 0;
            }
        }
    }
}
