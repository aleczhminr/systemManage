using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
using Model.Model4View;

namespace Controls.Sms
{
    public class SmsChannelTest
    {
        public static string GetChannelTest(string strContent, string phoneNum, int userCnt, int channelId, int smsFlag)
        {
            SmsChannelBLL channelBll = new SmsChannelBLL();
            return channelBll.GetChannelTest(strContent, phoneNum, userCnt, channelId, smsFlag);
        }
    }
}
