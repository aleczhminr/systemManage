using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MaterialGoodsBaseModel
    {
        /// <summary>
        /// GoodsId
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// ImagePath
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName { get; set; }
    }
    public class T_MaterialGoodsModel : MaterialGoodsBaseModel
    {
        /// <summary>
        /// 重量
        /// </summary>
        public string GoodsWeight { get; set; }
        /// <summary>
        /// 上下架信息
        /// </summary>
        public int GoodsStatus { get; set; }
        /// <summary>
        /// 品牌名
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string ProductArea { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string Barcode { get; set; }
        /// <summary>
        /// 包装类型
        /// </summary>
        public string SaleUnit { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 产品信息
        /// </summary>
        public string GoodsParam { get; set; }
        /// <summary>
        /// 包装内容
        /// </summary>
        public string PackingList { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime InsertDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 滚动图片
        /// </summary>
        public string CarouselImgs { get; set; }
        /// <summary>
        /// 排序 1最前
        /// </summary>
        public int RankNumber { get; set; }
        /// <summary>
        /// 来源 1-自营 2-京东
        /// </summary>
        public int SourceOfGoods { get; set; }
    }

}
