using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ConditionSettingModel
    {
        public class ConditionRecoveryModel
        {
            /// <summary>
            /// RuleListId
            /// </summary>
            public int Id { get; set; }
            public string ColName { get; set; }
            public string ConditionType { get; set; }
            public int ConditionGroup { get; set; }
            public string ColDesc { get; set; }
            /// <summary>
            /// RuleDetailId
            /// </summary>
            public int RuleId { get; set; }

            //条件范围组
            public string MaxValue { get; set; }
            public string MinValue { get; set; }
            public string RangeData { get; set; }

            /// <summary>
            /// 关于规则的描述
            /// </summary>
            public string Remark { get; set; }
        }

        public class ShowModelList
        {
            public ShowModelList()
            {
                DataList = new List<ShowModel>();
            }

            public List<ShowModel> DataList { get; set; }
        }

        /// <summary>
        /// 用于页面显示的Model
        /// </summary>
        public class ShowModel
        {
            public ShowModel()
            {
                GroupList = new List<ConditionRecoveryModel>();
            }

            public string GroupDesc { get; set; }
            /// <summary>
            /// 条件所属组别Id
            /// </summary>
            public int ConditionGroup { get; set; }
            public List<ConditionRecoveryModel> GroupList { get; set; }
        }

        /// <summary>
        /// 消息任务的设置信息Model
        /// </summary>
        public class SettingModel
        {
            public int Id { get; set; }
            /// <summary>
            /// 对应的筛选器条件标识
            /// </summary>
            public string Verification { get; set; }
            /// <summary>
            /// 发送类型
            /// 0-普通推送
            /// 1-定时推送
            /// 2-循环任务
            /// </summary>
            public int SendingType { get; set; }
            /// <summary>
            /// 循环任务对应的Sql条件组
            /// </summary>
            public string SqlSet { get; set; }
            /// <summary>
            /// 初始的AccId集合
            /// </summary>
            public string AccIdSet { get; set; }
            /// <summary>
            /// 初始的AccId计数
            /// </summary>
            public int AccIdCount { get; set; }
            /// <summary>
            /// 推送用户源
            /// 1-手填
            /// 2-筛选器
            /// 3-定时消息
            /// 4-循环消息
            /// </summary>
            public int SourceType { get; set; }

            //发送设置的相关内容
            public string MobileTitle { get; set; }
            public string PubTitle { get; set; }
            public string EmailTitle { get; set; }
            public string SmsContent { get; set; }
            public string MobileContent { get; set; }
            public string PubContent { get; set; }
            public string EmailContent { get; set; }
            public string Remark { get; set; }

            /// <summary>
            /// 任务激活状态
            /// </summary>
            public int ActiveStatus { get; set; }
            /// <summary>
            /// 定时任务的发送日期
            /// </summary>
            public DateTime SendingDate { get; set; }
            /// <summary>
            /// 循环任务的Cron表达式
            /// </summary>
            public string RecurringSendingCron { get; set; }
            /// <summary>
            /// 是否设定跟踪目标
            /// </summary>
            public int TargetMark { get; set; }
            /// <summary>
            /// 设定的跟踪目标ID
            /// </summary>
            public int TargetMarkId { get; set; }
            /// <summary>
            /// 操作者的ID
            /// </summary>
            public int Operator { get; set; }
            /// <summary>
            /// 最近一次发送的批次Id
            /// </summary>
            public string LatestBatchId { get; set; }
        }
    }
}
