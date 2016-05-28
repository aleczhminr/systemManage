using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Sys_TagInfo
    /// <summary>
    /// 标签
    /// </summary>	
    [Serializable]
    public partial class Sys_TagInfo
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>		
        public string t_Name { get; set; }
        /// <summary>
        /// 标签显示颜色
        /// </summary>		
        public string t_Color { get; set; }
        /// <summary>
        /// 标签显示背景色
        /// </summary>		
        public string t_BgColor { get; set; }
        /// <summary>
        /// 组别
        /// </summary>		
        public int t_group { get; set; }
        /// <summary>
        /// 店铺数
        /// </summary>		
        public int acc_Count { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>		
        public string insertName { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime insertTime { get; set; }
        /// <summary>
        /// 排序
        /// </summary>		
        public int t_order { get; set; }
        

    }
    /// <summary>
    /// 标签基本信息
    /// </summary>
    public partial class Sys_TagInfoBasic
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>		
        public string t_Name { get; set; }
        /// <summary>
        /// 标签显示颜色
        /// </summary>		
        public string t_Color { get; set; }
        /// <summary>
        /// 标签显示背景色
        /// </summary>		
        public string t_BgColor { get; set; }
        /// <summary>
        /// 排序
        /// </summary>		
        public int t_order { get; set; }
        /// <summary>
        /// 标签类别
        /// </summary>
        public string tagType { get; set; }
        /// <summary>
        /// 标签类别ID
        /// </summary>
        public string tagTypeid { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string insertName { get;set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public string insertTime { get; set; }
        /// <summary>
        /// 标签状态
        /// </summary>
        public int tagStatus { get; set; }
        /// <summary>
        /// 判断是否可以删除的时间间隔
        /// </summary>
        public string timediff { get; set; }
        /// <summary>
        /// 拥有该标签的店铺数
        /// </summary>
        public int AccidCount { get; set; }
        /// <summary>
        ///  有标签的店铺列表Json
        /// </summary>
        //public string AccidList { get; set; }

    }

    /// <summary>
    /// 标签类别基本
    /// </summary>
    public partial class SysTagTypeBasic
    {
        public SysTagTypeBasic()
        {
            itemList = new List<Sys_TagInfoBasic>();
        }
        /// <summary>
        /// 标签类别名称
        /// </summary>
        public string tagTypeName { get; set; }
        /// <summary>
        /// 标签类别ID
        /// </summary>
        public string tagTypeId { get; set; }
        /// <summary>
        /// 小类别信息
        /// </summary>
        public List<Sys_TagInfoBasic> itemList { get; set; }
    }
}

