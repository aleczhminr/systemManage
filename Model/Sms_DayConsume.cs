using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sms_DayConsume
			/// <summary>
		/// Sms_DayConsume
        /// </summary>	
    [Serializable]
	public partial class Sms_DayConsume
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// passageNum
        /// </summary>		
        public int passageNum{get;set;}        
		/// <summary>
		/// sms_Num
        /// </summary>		
        public int sms_Num{get;set;}        
		/// <summary>
		/// sms_Sendtime
        /// </summary>		
        public DateTime sms_Sendtime{get;set;}        
		/// <summary>
		/// sms_Source
        /// </summary>		
        public string sms_Source{get;set;}        
		/// <summary>
		/// sms_Name
        /// </summary>		
        public string sms_Name{get;set;}        
		/// <summary>
		/// sms_remark
        /// </summary>		
        public string sms_remark{get;set;}        
		/// <summary>
		/// sms_status
        /// </summary>		
        public int sms_status{get;set;}        
		   
	}
}

