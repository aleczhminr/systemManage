using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sms_Reconciliation
			/// <summary>
		/// Sms_Reconciliation
        /// </summary>	
    [Serializable]
	public partial class Sms_Reconciliation
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
		/// r_Money
        /// </summary>		
        public decimal r_Money{get;set;}        
		/// <summary>
		/// sys_Money
        /// </summary>		
        public decimal sys_Money{get;set;}        
		/// <summary>
		/// r_SmsNum
        /// </summary>		
        public int r_SmsNum{get;set;}        
		/// <summary>
		/// sys_SmsNum
        /// </summary>		
        public int sys_SmsNum{get;set;}        
		/// <summary>
		/// r_Type
        /// </summary>		
        public int r_Type{get;set;}        
		/// <summary>
		/// t_exploin
        /// </summary>		
        public string t_exploin{get;set;}        
		/// <summary>
		/// r_Nmae
        /// </summary>		
        public string r_Nmae{get;set;}        
		/// <summary>
		/// r_Time
        /// </summary>		
        public DateTime r_Time{get;set;}        
		/// <summary>
		/// r_remark
        /// </summary>		
        public string r_remark{get;set;}        
		   
	}
}

