using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sms_Recharge
			/// <summary>
		/// Sms_Recharge
        /// </summary>	
    [Serializable]
	public partial class Sms_Recharge
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// p_id
        /// </summary>		
        public int p_id{get;set;}        
		/// <summary>
		/// r_money
        /// </summary>		
        public decimal r_money{get;set;}        
		/// <summary>
		/// m_original
        /// </summary>		
        public decimal m_original{get;set;}        
		/// <summary>
		/// m_all
        /// </summary>		
        public decimal m_all{get;set;}        
		/// <summary>
		/// r_sms
        /// </summary>		
        public int r_sms{get;set;}        
		/// <summary>
		/// sms_original
        /// </summary>		
        public int sms_original{get;set;}        
		/// <summary>
		/// sms_all
        /// </summary>		
        public int sms_all{get;set;}        
		/// <summary>
		/// r_exploin
        /// </summary>		
        public string r_exploin{get;set;}        
		/// <summary>
		/// insertTime
        /// </summary>		
        public DateTime insertTime{get;set;}        
		/// <summary>
		/// insertName
        /// </summary>		
        public string insertName{get;set;}        
		   
	}
}

