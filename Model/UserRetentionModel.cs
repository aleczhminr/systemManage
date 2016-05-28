using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserRetentionModel
    {
        public UserRetentionModel()
        {
            avgRatio = new List<decimal>();
            dataList = new List<RetentionList>();
        }
        /// <summary>
        /// 平均留存率
        /// </summary>
        public List<decimal> avgRatio { get; set; }

        public List<RetentionList> dataList { get; set; }
    }

    public class RetentionList
    {
        public RetentionList()
        {
            DataList = new List<DailyRetention>();
        }

        public RetentionList(string rowMark, string date, int initCount)
        {
            RowMark = rowMark;
            Date = date;
            InitialCount = initCount;

            InitialAccId = new List<int>();
            DataList = new List<DailyRetention>();
        }

        /// <summary>
        /// 行对象标记
        /// </summary>
        public string RowMark { get; set; }

        /// <summary>
        /// 留存率分析的日期
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 初始时间的AccId集合
        /// </summary>
        public List<int> InitialAccId { get; set; }
        /// <summary>
        /// 初始计数
        /// </summary>
        public int InitialCount { get; set; }

        /// <summary>
        /// 当日留存率在一段时间内的变化
        /// </summary>
        public List<DailyRetention> DataList { get; set; }
    }

    public class DailyRetention
    {
        public DailyRetention()
        {
            DayNum = 0;
            NowActive = 0;
            NoActive = 0;
            NewActive = 0;
            Ratio = 0;
        }

        public DailyRetention(int DayNum, int NowActive, int Ratio, string DayAccids, string ColumnMark)
        {
            this.DayNum = DayNum;
            this.NowActive = NowActive;
            this.Ratio = Ratio;
            this.DayAccids = DayAccids;
            this.ColumnMark = ColumnMark;
        }

        public DailyRetention(int DayNum, int NowActive, int Ratio, string DayAccids, string ColumnMark,string DayLost)
        {
            this.DayNum = DayNum;
            this.NowActive = NowActive;
            this.Ratio = Ratio;
            this.DayAccids = DayAccids;
            this.ColumnMark = ColumnMark;
            this.DayLostAccids = DayLost;
        }

        /// <summary>
        /// 当前日期的序号
        /// </summary>
        public int DayNum { get; set; }
        /// <summary>
        /// 列标识符
        /// </summary>
        public string ColumnMark { get; set; }

        /// <summary>
        /// 当日Accid集合
        /// </summary>
        public string DayAccids { get; set; }
        /// <summary>
        /// 当日流失Accid集合
        /// </summary>
        public string DayLostAccids { get; set; }

        /// <summary>
        /// 当前活跃数据
        /// </summary>
        public int NowActive { get; set; }
        /// <summary>
        /// 离开活跃
        /// </summary>
        public int NoActive { get; set; }
        /// <summary>
        /// 新活跃
        /// </summary>
        public int NewActive { get; set; }
        /// <summary>
        /// 留存率
        /// </summary>
        public int Ratio { get; set; }
    }

    public class TimePeriodModel
    {
        public TimePeriodModel()
        {
            TimePeriod = "";
            StDate = DateTime.MinValue;
            EdDate = DateTime.MinValue;
        }

        public TimePeriodModel(string time,DateTime dt1,DateTime dt2)
        {
            TimePeriod = time;
            StDate = dt1;
            EdDate = dt2;
        }

        public string TimePeriod { get; set; }
        public DateTime StDate { get; set; }
        public DateTime EdDate { get; set; }
    }

    /// <summary>
    /// 留存统计聚合Model
    /// </summary>
    public class RetentionGroup
    {
        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime R_Date { get; set; }
        /// <summary>
        /// 第几日标识
        /// </summary>
        public int DateCount { get; set; }
        /// <summary>
        /// 留存用户数
        /// </summary>
        public int Accounts { get; set; }
    }
}
