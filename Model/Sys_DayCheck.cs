using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_DayCheck
			/// <summary>
		/// Sys_DayCheck
        /// </summary>	
    [Serializable]
	public partial class Sys_DayCheck
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// 1选择,2填空
        /// </summary>		
        public int check_type{get;set;}        
		/// <summary>
		/// check_value
        /// </summary>		
        public string check_value{get;set;}        
		/// <summary>
		/// checkerid
        /// </summary>		
        public int checkerid{get;set;}        
		/// <summary>
		/// checktime
        /// </summary>		
        public DateTime checktime{get;set;}        
		/// <summary>
		/// check_Item
        /// </summary>		
        public int check_Item{get;set;}        
		/// <summary>
		/// log_IP
        /// </summary>		
        public string log_IP{get;set;}        
		/// <summary>
		/// log_Browser
        /// </summary>		
        public string log_Browser{get;set;}        
		   
	}
}

