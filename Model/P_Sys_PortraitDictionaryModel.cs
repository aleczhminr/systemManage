using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class P_Sys_PortraitDictionaryModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 字典项类型
        /// 1-行业大分类
        /// 2-行业小分类
        /// 3-用户来源
        /// 4-用户来源关键词
        /// 5-选择我们的理由
        /// 6-潜在需求
        /// 7-对我们的态度
        /// </summary>
        public int ItemType { get; set; }
        /// <summary>
        /// 字典项
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExtContent { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 字典项排序
        /// </summary>
        public int OrderBy { get; set; }
    }

    public class PortaritDicList
    {
        public PortaritDicList()
        {
            DataList = new List<P_Sys_PortraitDictionaryModel>();
        }

        public List<P_Sys_PortraitDictionaryModel> DataList { get; set; }
    }
}
