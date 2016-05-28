using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sms_Consume
			/// <summary>
		/// Sms_Consume
        /// </summary>	
    [Serializable]
	public partial class Sms_Consume
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
		/// c_Num
        /// </summary>		
        public int c_Num{get;set;}        
		/// <summary>
		/// sms_Surplus
        /// </summary>		
        public int sms_Surplus{get;set;}        
		/// <summary>
		/// c_name
        /// </summary>		
        public string c_name{get;set;}        
		/// <summary>
		/// c_Time
        /// </summary>		
        public DateTime c_Time{get;set;}        
		/// <summary>
		/// c_remark
        /// </summary>		
        public string c_remark{get;set;}        
		   
	}
}

