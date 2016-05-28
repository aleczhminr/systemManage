using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sms_Passage
			/// <summary>
		/// Sms_Passage
        /// </summary>	
    [Serializable]
	public partial class Sms_Passage
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// p_account
        /// </summary>		
        public string p_account{get;set;}        
		/// <summary>
		/// p_Service
        /// </summary>		
        public string p_Service{get;set;}        
		/// <summary>
		/// passageNum
        /// </summary>		
        public int passageNum{get;set;}        
		/// <summary>
		/// p_Providers
        /// </summary>		
        public string p_Providers{get;set;}        
		/// <summary>
		/// p_exploin
        /// </summary>		
        public string p_exploin{get;set;}        
		/// <summary>
		/// p_price
        /// </summary>		
        public decimal p_price{get;set;}        
		/// <summary>
		/// p_smsNum
        /// </summary>		
        public int p_smsNum{get;set;}        
		/// <summary>
		/// p_money
        /// </summary>		
        public decimal p_money{get;set;}        
		/// <summary>
		/// sys_money
        /// </summary>		
        public decimal sys_money{get;set;}        
		/// <summary>
		/// insertTime
        /// </summary>		
        public DateTime insertTime{get;set;}        
		/// <summary>
		/// p_remark
        /// </summary>		
        public string p_remark{get;set;}        
		   
	}
}

