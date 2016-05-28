using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//SysRpt_WebDayInfo
			/// <summary>
		/// SysRpt_WebDayInfo
        /// </summary>	
    [Serializable]
	public partial class SysRpt_WebDayInfo
	{
        /// <summary>
        /// 添加索引器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        public dynamic this[string name]
        {
            get
            {
                decimal reNum = 0;
                switch (name)
                {
                    case "NewAccNum":
                        reNum = NewAccNum;
                        break;
                    case "userNum":
                        reNum = userNum;
                        break;
                    case "saleNum":
                        reNum = saleNum;
                        break;
                    case "smsNum":
                        reNum = smsNum;
                        break;
                    case "orderMoney":
                        reNum = orderMoney;
                        break;
                    case "outlayNum":
                        reNum = outlayNum;
                        break;
                    case "clientLogNum":
                        reNum = clientLogNum;
                        break;
                    case "moodNum":
                        reNum = moodNum;
                        break;
                    case "acc_Rep":
                        reNum = acc_Rep;
                        break;
                    case "saleMoney":
                        reNum = saleMoney;
                        break;
                    case "loginNum":
                        reNum = loginNum;
                        break;
                    case "addGoodsNum":
                        reNum = addGoodsNum;
                        break;
                    case "faithfulNum":
                        reNum = faithfulNum;
                        break;
                    case "dormancyNum":
                        reNum = dormancyNum;
                        break;
                    case "outflowNum":
                        reNum = outflowNum;
                        break;
                    case "registration":
                        reNum = registration;
                        break;
                    case "S_Date":
                        return S_Date;
                    case "activeNum":
                        return activeNum;
                    case "newAdd":
                        return newAdd;
                }

                return reNum;
            }
        }
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// S_Date
        /// </summary>		
        public DateTime S_Date{get;set;}        
		/// <summary>
		/// accountNum
        /// </summary>		
        public decimal accountNum{get;set;}        
		/// <summary>
		/// NewAccNum
        /// </summary>		
        public decimal NewAccNum{get;set;}        
		/// <summary>
		/// userNum
        /// </summary>		
        public decimal userNum{get;set;}        
		/// <summary>
		/// smsNum
        /// </summary>		
        public decimal smsNum{get;set;}        
		/// <summary>
		/// freeSmsNum
        /// </summary>		
        public decimal freeSmsNum{get;set;}        
		/// <summary>
		/// sysSmsNum
        /// </summary>		
        public decimal sysSmsNum{get;set;}        
		/// <summary>
		/// saleNum
        /// </summary>		
        public decimal saleNum{get;set;}        
		/// <summary>
		/// saleMoney
        /// </summary>		
        public decimal saleMoney{get;set;}        
		/// <summary>
		/// memSaleNum
        /// </summary>		
        public int memSaleNum{get;set;}        
		/// <summary>
		/// memSaleMoney
        /// </summary>		
        public decimal memSaleMoney{get;set;}        
		/// <summary>
		/// retailSaleNum
        /// </summary>		
        public decimal retailSaleNum{get;set;}        
		/// <summary>
		/// retailSaleMoney
        /// </summary>		
        public decimal retailSaleMoney{get;set;}        
		/// <summary>
		/// orderNum
        /// </summary>		
        public int orderNum{get;set;}        
		/// <summary>
		/// orderMoney
        /// </summary>		
        public decimal orderMoney{get;set;}        
		/// <summary>
		/// orderAccount
        /// </summary>		
        public int orderAccount{get;set;}        
		/// <summary>
		/// loginNum
        /// </summary>		
        public int loginNum{get;set;}        
		/// <summary>
		/// loginPaidNum
        /// </summary>		
        public int loginPaidNum{get;set;}        
		/// <summary>
		/// reg_Attention
        /// </summary>		
        public decimal reg_Attention{get;set;}        
		/// <summary>
		/// registration
        /// </summary>		
        public int registration{get;set;}        
		/// <summary>
		/// moodNum
        /// </summary>		
        public int moodNum{get;set;}        
		/// <summary>
		/// unknownNum
        /// </summary>		
        public int unknownNum{get;set;}        
		/// <summary>
		/// activeNum
        /// </summary>		
        public int activeNum{get;set;}        
		/// <summary>
		/// faithfulNum
        /// </summary>		
        public int faithfulNum{get;set;}        
		/// <summary>
		/// dormancyNum
        /// </summary>		
        public int dormancyNum{get;set;}        
		/// <summary>
		/// outflowNum
        /// </summary>		
        public int outflowNum{get;set;}        
		/// <summary>
		/// outlayNum
        /// </summary>		
        public int outlayNum{get;set;}        
		/// <summary>
		/// outlayMoney
        /// </summary>		
        public decimal outlayMoney{get;set;}        
		/// <summary>
		/// addGoodsNum
        /// </summary>		
        public decimal addGoodsNum{get;set;}        
		/// <summary>
		/// saleGoodsNum
        /// </summary>		
        public decimal saleGoodsNum{get;set;}        
		/// <summary>
		/// feedBackCnt
        /// </summary>		
        public int feedBackCnt{get;set;}        
		/// <summary>
		/// addTime
        /// </summary>		
        public DateTime addTime{get;set;}        
		/// <summary>
		/// clientLogNum
        /// </summary>		
        public int clientLogNum{get;set;}        
		/// <summary>
		/// acc_Rep
        /// </summary>		
        public int acc_Rep{get;set;}        
		/// <summary>
		/// acc_RepCount
        /// </summary>		
        public int acc_RepCount{get;set;}        
		/// <summary>
		/// Attention
        /// </summary>		
        public int Attention { get; set; }

        public int newAdd { get; set; }
	}
}

