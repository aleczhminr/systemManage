using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.O2O
{
    //NewGoodsList
    /// <summary>
    /// NewGoodsList
    /// </summary>	
    [Serializable]
    public partial class NewGoodsList
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// gid
        /// </summary>		
        public int gid { get; set; }
        /// <summary>
        /// accid
        /// </summary>		
        public int accid { get; set; }
        /// <summary>
        /// ygid
        /// </summary>		
        public int ygid { get; set; }
        /// <summary>
        /// accountName
        /// </summary>		
        public string accountName { get; set; }
        /// <summary>
        /// gname
        /// </summary>		
        public string gname { get; set; }
        /// <summary>
        /// maxClass
        /// </summary>		
        public string maxClass { get; set; }
        /// <summary>
        /// minClass
        /// </summary>		
        public string minClass { get; set; }
        /// <summary>
        /// goodsNum
        /// </summary>		
        public decimal goodsNum { get; set; }
        /// <summary>
        /// price
        /// </summary>		
        public decimal price { get; set; }
        /// <summary>
        /// lastSalePrice
        /// </summary>		
        public decimal lastSalePrice { get; set; }
        /// <summary>
        /// cosPrice
        /// </summary>		
        public decimal cosPrice { get; set; }
        /// <summary>
        /// saleNum
        /// </summary>		
        public int saleNum { get; set; }
        /// <summary>
        /// saleMoney
        /// </summary>		
        public decimal saleMoney { get; set; }
        /// <summary>
        /// userNum
        /// </summary>		
        public int userNum { get; set; }
        /// <summary>
        /// retailNum
        /// </summary>		
        public int retailNum { get; set; }
        /// <summary>
        /// isPic
        /// </summary>		
        public int isPic { get; set; }

    }
}
