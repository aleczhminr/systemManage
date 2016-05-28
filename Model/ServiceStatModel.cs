using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ServiceStatModel
    {
        public ServiceStatModel()
        {
            usrList = new List<VisitUserType>();
            channelPer = new List<VisitChannel>();
            servPersonPer = new List<ServPerson>();
        }

        //每日新老用户列表
        public List<VisitUserType> usrList { get; set; }
        //渠道比例
        public List<VisitChannel> channelPer { get; set; }
        //服务人员比例
        public List<ServPerson> servPersonPer { get; set; }
    }

    /// <summary>
    /// 新老用户比例
    /// </summary>
    public class VisitUserType
    {
        public VisitUserType()
        {
            date = DateTime.Now;
            oldUsr = 0;
            newUsr = 0;
        }

        public DateTime date { get; set; }
        public int oldUsr { get; set; }
        public int newUsr { get; set; }
        public string XLable { get; set; }
    }

    /// <summary>
    /// 渠道
    /// </summary>
    public class VisitChannel
    {
        public VisitChannel()
        {
            key = string.Empty;
            value = 0;
        }

        public string key { get; set; }
        public int value { get; set; }
    }

    public class ServPerson
    {
        public ServPerson()
        {
            key = string.Empty;
            value = 0;
        }

        public string key { get; set; }
        public int value { get; set; }
    }
}
