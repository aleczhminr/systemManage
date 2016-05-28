using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 图表数据对像
    /// </summary>
    public class chartDataModel
    {
        public chartDataModel()
        {
            DataList = new Dictionary<string, charDataList>();
            NowDay = DateTime.Now.Day;
            NowMonth = DateTime.Now.Month;
            NowYear = DateTime.Now.Year;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string captionTitle { get; set; }
        /// <summary>
        /// 列表数据
        /// <para>LEY X轴的信息，Value 相关数据列表</para>
        /// </summary>
        public Dictionary<string,charDataList> DataList { get; set; }
        /// <summary>
        /// 当前天
        /// </summary>
        public int NowDay { get; set; }
        /// <summary>
        /// 当前月
        /// </summary>
        public int NowMonth { get; set; }
        /// <summary>
        /// 当前年
        /// </summary>
        public int NowYear { get; set; }
        /// <summary>
        /// 系列数（数据对比用）
        /// </summary>
        public string SeriesCount { get; set; }
        /// <summary>
        /// 来源字符串（数据对比用）
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// 关于时间的图标 设置值
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="dataList">数据信息</param>
        /// <param name="ColumnList">列名 和 汉字 说明 键值对 <para> "NewAccNum", "店铺"</para></param>
        /// <param name="captionTitle">图标的标题</param>
        /// <param name="XlableFormat">X轴格式化</param>
        /// <returns></returns>
        public chartDataModel SetDataAboutDataTime(DateTime startTime, DateTime endTime, List<dynamic> dataList, Dictionary<string, string> ColumnList, string captionTitle = "图表", string XlableFormat = "yyyy-MM-dd")
        {

            chartDataModel chartModel = new chartDataModel();
            chartModel.captionTitle = captionTitle;

            DateTime forDateTime = startTime;
            while (forDateTime <= endTime)
            {
                if (captionTitle == "活跃度分析")
                {
                    if (forDateTime <= DateTime.Now.Date)
                    {
                        #region 得到所有X轴
                        charDataList charItemList = new charDataList();

                        charItemList.XLable = forDateTime.ToString(XlableFormat);
                        charItemList.weekend = (int)forDateTime.DayOfWeek;

                        foreach (KeyValuePair<string, string> item in ColumnList)
                        {
                            charItemList.ItemList[item.Key] = new charDataItemList(0)
                            {
                                series = item.Value
                            };
                        }
                        chartModel.DataList[charItemList.XLable] = charItemList;
                        #endregion
                    }
                }
                else
                {
                    if (forDateTime < DateTime.Now.Date)
                    {
                        #region 得到所有X轴
                        charDataList charItemList = new charDataList();

                        charItemList.XLable = forDateTime.ToString(XlableFormat);
                        charItemList.weekend = (int)forDateTime.DayOfWeek;

                        foreach (KeyValuePair<string, string> item in ColumnList)
                        {
                            charItemList.ItemList[item.Key] = new charDataItemList(0)
                            {
                                series = item.Value
                            };
                        }
                        chartModel.DataList[charItemList.XLable] = charItemList;
                        #endregion
                    }
                }
                
                forDateTime = forDateTime.AddDays(1);
            }


            if (dataList != null && dataList.Count() > 0)
            {
                foreach (dynamic item in dataList)
                {
                    #region 给X轴增加值
                    var XLable = Convert.ToDateTime(item.dayDate).ToString(XlableFormat);
                    Dictionary<string, charDataItemList> dataitemList = chartModel.DataList[XLable].ItemList;

                    foreach (KeyValuePair<string, object> keyValue in item)
                    {
                        if (dataitemList.ContainsKey(keyValue.Key))
                        {
                            dataitemList[keyValue.Key].Values = keyValue.Value;
                        }
                    }
                    #endregion
                }
            }
            return chartModel;
        }
    }
    /// <summary>
    /// 图标数据
    /// </summary>
    public class charDataList
    {
        public charDataList()
        {
            ItemList = new Dictionary<string, charDataItemList>();

            thisMonth = 0;
            weekend = 1;
        }
        /// <summary>
        /// X轴名称
        /// </summary>
        /// <param name="xLable"></param>
        public charDataList(string xLable)
        {
            ItemList = new Dictionary<string, charDataItemList>();

            thisMonth = 0;

            XLable = xLable;
            weekend = 1;
        }
        /// <summary>
        /// X标签值
        /// </summary>
        public string XLable { get; set; }
        /// <summary>
        /// 列表数据
        /// <para>KEY 为 系列表标示，Value 为列数据</para>
        /// </summary>
        public Dictionary<string, charDataItemList> ItemList { get; set; }
        /// <summary>
        /// 总系列数
        /// </summary>
        public int count {
            get { return ItemList.Count; }
        }
        /// <summary>
        /// 是否为本月
        /// </summary>
        public int thisMonth { get; set; }

        /// <summary>
        /// 周几
        /// </summary>
        public int weekend { get; set; }

    }
    /// <summary>
    /// 每系列值
    /// </summary>
    public class charDataItemList
    {
        public charDataItemList()
        {
            Values = null;
        }
        /// <summary>
        /// 初始化默认值
        /// </summary>
        /// <param name="_Values"></param>
        public charDataItemList(object _Values)
        {
            Values = _Values;
        }
        /// <summary>
        /// 值 
        /// </summary>
        public object Values { get; set; }
        /// <summary>
        /// 自定义提示
        /// </summary>
        public string toolText { get; set; }
        /// <summary>
        /// 显示的颜色
        /// </summary>
        public string Colors { get; set; }
        /// <summary>
        /// 系列名称
        /// </summary>
        public string series { get; set; }
    }

    public class hourChartModel:chartDataModel
    {
        /// <summary>
        /// 当前小时
        /// </summary>
        public string nowHour { get; set; }
    }

    /// <summary>
    /// 销售地图列表
    /// </summary>
    public class saleMapItemList
    {
        public string id = "";
        public string Address = "";
        public string ShopName = "";
        public string SellMoney = "";
        public string DetailAddress = "";
        public int SellTime = 0;
        public string ShopCar = "";
    }
}
