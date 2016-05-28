using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.O2O
{
    //NewGoodsInfo
    /// <summary>
    /// NewGoodsInfo
    /// </summary>	
    [Serializable]
    public partial class NewGoodsInfo
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// cMaxId
        /// </summary>		
        public int cMaxId { get; set; }
        /// <summary>
        /// cMaxName
        /// </summary>		
        public string cMaxName { get; set; }
        /// <summary>
        /// cMinId
        /// </summary>		
        public int cMinId { get; set; }
        /// <summary>
        /// cMinName
        /// </summary>		
        public string cMinName { get; set; }
        /// <summary>
        /// gName
        /// </summary>		
        public string gName { get; set; }
        /// <summary>
        /// gQuantity
        /// </summary>		
        public int gQuantity { get; set; }
        /// <summary>
        /// averagePrice
        /// </summary>		
        public decimal averagePrice { get; set; }
        /// <summary>
        /// averageCosPrice
        /// </summary>		
        public decimal averageCosPrice { get; set; }
        /// <summary>
        /// saleNum
        /// </summary>		
        public decimal saleNum { get; set; }
        /// <summary>
        /// saleMoney
        /// </summary>		
        public decimal saleMoney { get; set; }
        /// <summary>
        /// accountNum
        /// </summary>		
        public int accountNum { get; set; }
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
