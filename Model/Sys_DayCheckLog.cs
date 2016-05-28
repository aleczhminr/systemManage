using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_DayCheckLog
			/// <summary>
		/// Sys_DayCheckLog
        /// </summary>	
    [Serializable]
	public partial class Sys_DayCheckLog
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// Check_Type
        /// </summary>		
        public int Check_Type{get;set;}        
		/// <summary>
		/// Check_Status
        /// </summary>		
        public int Check_Status{get;set;}        
		/// <summary>
		/// Check_Contnet
        /// </summary>		
        public string Check_Contnet{get;set;}        
		/// <summary>
		/// Check_User
        /// </summary>		
        public string Check_User{get;set;}        
		/// <summary>
		/// Check_DateTime
        /// </summary>		
        public DateTime Check_DateTime{get;set;}        
		/// <summary>
		/// Check_IP
        /// </summary>		
        public string Check_IP{get;set;}        
		/// <summary>
		/// Check_Client
        /// </summary>		
        public string Check_Client{get;set;}        
		   
	}
}

