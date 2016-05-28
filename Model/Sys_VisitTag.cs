using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Sys_VisitTag
    /// <summary>
    /// 回访标签
    /// </summary>	
    [Serializable]
    public partial class Sys_VisitTag
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>		
        public string tagName { get; set; }
        /// <summary>
        /// 标签类别ID
        /// </summary>		
        public int vTypeId { get; set; }
        /// <summary>
        /// 标签类别说明
        /// </summary>		
        public string vTypeName { get; set; }
        /// <summary>
        /// 标签状态
        /// </summary>		
        public int vtState { get; set; }
        /// <summary>
        /// 推荐
        /// </summary>		
        public int recommend { get; set; }

    }
    /// <summary>
    /// 系统回访标签列表
    /// </summary>
    public partial class SysVisitTagTypeInfo
    {
        public SysVisitTagTypeInfo()
        {
            ItemList = new List<SysVisitTagItem>();
        }
        public SysVisitTagTypeInfo(int _vTypeId, string _vTypeName)
        {
            vTypeId = _vTypeId;
            vTypeName = _vTypeName;
            ItemList = new List<SysVisitTagItem>();
        }
        public int vTypeId { get; set; }
        public string vTypeName { get; set; }
        public List<SysVisitTagItem> ItemList { get; set; }
    }
    /// <summary>
    /// 系统回访标签列表数据
    /// </summary>
    public partial class SysVisitTagItem
    {
        public SysVisitTagItem()
        {

        }
        public SysVisitTagItem(int _id, string _tagName)
        {
            id = _id;
            tagName = _tagName;
        }
        /// <summary>
        /// 标签ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string tagName { get; set; }
    }

}

