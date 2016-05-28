using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReportModel
    {
    }

    public class NewAccountModel
    {
        public NewAccountModel()
        {
            ItemGroupList = new List<ItemGroup>();
        }
        public List<ItemGroup> ItemGroupList { get; set; }        
    }

    public class UgcList
    {
        public UgcList()
        {
            DataList = new List<UgcGroup>();
        }

        public List<UgcGroup> DataList { get; set; }
    }

    public class ItemGroup
    {

        public ItemGroup()
        {
            DataList = new List<NewAccountItem>();
        }

        public ItemGroup(string groupName, int rowSpan, int colSpan)
        {
            DataList = new List<NewAccountItem>();
            GroupName = groupName;
            RowSpan = rowSpan;
            ColSpan = colSpan;
        }

        public ItemGroup(string groupName, int rowSpan, int colSpan, NewAccountItem item)
        {
            DataList = new List<NewAccountItem>();
            GroupName = groupName;
            RowSpan = rowSpan;
            ColSpan = colSpan;

            DataList.Add(item);
        }

        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 行位
        /// </summary>
        public int RowSpan { get; set; }
        /// <summary>
        /// 列位
        /// </summary>
        public int ColSpan { get; set; }
        /// <summary>
        /// 数据数组
        /// </summary>
        public List<NewAccountItem> DataList { get; set; }        
    }

    public class UgcGroup
    {
        public UgcGroup()
        {
            DataList = new List<ItemGroup>();
        }

        public UgcGroup(string name)
        {
            DataList = new List<ItemGroup>();
            Title = name;
        } 

        public string Title { get; set; }
        public List<ItemGroup> DataList { get; set; }
    }

    public class NewAccountItem
    {
        public NewAccountItem()
        {
        }

        public NewAccountItem(string sourceName, decimal tVal, decimal lVal)
        {
            SourceName = sourceName;
            ThisWeekVal = tVal;
            LastWeekVal = lVal;
        }

        public NewAccountItem(string sourceName, decimal tVal, decimal lVal, string per)
        {
            SourceName = sourceName;
            ThisWeekVal = tVal;
            LastWeekVal = lVal;
            Percent = per;
        }

        public string SourceName { get; set; }
        public decimal ThisWeekVal { get; set; }
        public decimal LastWeekVal { get; set; }
        public string Percent { get; set; }
    }

    public class ConversionSource
    {
        public string Source { get; set; }
        public int Reg { get; set; }
        public int Login { get; set; }
        public int DataInput { get; set; }
        public int SecRetention { get; set; }
        public int Paid { get; set; }
        public int Active { get; set; }
    }

    public class ConversionModel
    {
        public ConversionModel()
        {
        }

        public ConversionModel(string name)
        {
            NameStr = name;
        }

        public string NameStr { get; set; }
        public int iPhoneNum { get; set; }
        public int iPadNum { get; set; }
        public int ZhuzhanNum { get; set; }
        public int QihuNum { get; set; }
        public int TengxunNum { get; set; }
        public int XiaomiNum { get; set; }
        public int HuaweiNum { get; set; }
        public int BaiduNum { get; set; }
        public int MeizuNum { get; set; }
        public int OppoNum { get; set; }
        public int AnzhiNum { get; set; }
        public int WandoujiaNum { get; set; }
        public int Other { get; set; }
        public int PcClientNum { get; set; }
        public int WebNum { get; set; }
    }

    public class ConversionList
    {
        public ConversionList()
        {
            DataList = new List<ConversionModel>();
        }

        public List<ConversionModel> DataList { get; set; }
    }
}
