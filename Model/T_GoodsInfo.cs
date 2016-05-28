using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //T_GoodsInfo
    /// <summary>
    /// T_GoodsInfo
    /// </summary>	
    [Serializable]
    public partial class T_GoodsInfo
    {

        /// <summary>
        /// 商品ID
        /// </summary>		
        public int gid { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>		
        public int accID { get; set; }
        /// <summary>
        /// 是否为服务类型
        /// </summary>		
        public int isService { get; set; }
        /// <summary>
        /// 是否已下架
        /// </summary>		
        public int isDown { get; set; }
        /// <summary>
        /// 下架时间
        /// </summary>		
        public DateTime isDownTime { get; set; }
        /// <summary>
        /// 大分类名称
        /// </summary>		
        public string gMaxName { get; set; }
        /// <summary>
        /// 大分类ID
        /// </summary>		
        public int gMaxID { get; set; }
        /// <summary>
        /// 小分类名称
        /// </summary>		
        public string gMinName { get; set; }
        /// <summary>
        /// 小分类ID
        /// </summary>		
        public int gMinID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>		
        public string gName { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>		
        public decimal gQuantity { get; set; }
        /// <summary>
        /// 商品折扣
        /// </summary>		
        public decimal gDiscount { get; set; }
        /// <summary>
        /// 商品规格
        /// </summary>		
        public string gSpecification { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>		
        public decimal gPrice { get; set; }
        /// <summary>
        /// 商品进价
        /// </summary>		
        public decimal gCostPrice { get; set; }
        /// <summary>
        /// 商品添加时间
        /// </summary>		
        public DateTime gAddTime { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>		
        public string gBarcode { get; set; }
        /// <summary>
        /// 商品备注
        /// </summary>		
        public string gRemark { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>		
        public string gPicUrl { get; set; }
        /// <summary>
        /// 拼音首字母
        /// </summary>		
        public string gPY { get; set; }
        /// <summary>
        /// 拼音全拼
        /// </summary>		
        public string gPinYin { get; set; }
        /// <summary>
        /// 联合查询值(拼音,编码)
        /// </summary>		
        public string gUnionKey { get; set; }
        /// <summary>
        /// 记录写入时间
        /// </summary>		
        public DateTime gInsertDate { get; set; }
        /// <summary>
        /// 操作人员ID
        /// </summary>		
        public int gOperatorID { get; set; }
        /// <summary>
        /// 盘点时间
        /// </summary>		
        public DateTime gCheckDate { get; set; }
        /// <summary>
        /// IsExtend
        /// </summary>		
        public int IsExtend { get; set; }

    }

    /// <summary>
    /// 商品基本信息
    /// </summary>
    public partial class T_GoodsInfoBasic
    {
        public int gid { get; set; }
        /// <summary>
        /// 大分类ID
        /// </summary>		
        public int gMaxID { get; set; }
        /// <summary>
        /// 大分类名称
        /// </summary>		
        public string gMaxName { get; set; }
        /// <summary>
        /// 小分类ID
        /// </summary>		
        public int gMinID { get; set; }
        /// <summary>
        /// 小分类名称
        /// </summary>		
        public string gMinName { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>		
        public string gName { get; set; }
        /// <summary>
        /// 商品规格
        /// </summary>		
        public string gSpecification { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>		
        public decimal gPrice { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>		
        public decimal gQuantity { get; set; }
        /// <summary>
        /// 是否下架
        /// </summary>		
        public int isDown { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>		
        public List<T_GoodsInfoBasic> gPicUrls { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string gBarcode { get; set; }
    }
}

