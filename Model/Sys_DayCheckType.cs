using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_DayCheckType
			/// <summary>
		/// Sys_DayCheckType
        /// </summary>	
    [Serializable]
	public partial class Sys_DayCheckType
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// Check_Title
        /// </summary>		
        public string Check_Title{get;set;}        
		/// <summary>
		/// Check_Describe
        /// </summary>		
        public string Check_Describe{get;set;}        
		/// <summary>
		/// Check_Type
        /// </summary>		
        public string Check_Type{get;set;}        
		/// <summary>
		/// Check_Priority
        /// </summary>		
        public int Check_Priority{get;set;}        
		/// <summary>
		/// Check_ConditionStatus
        /// </summary>		
        public int Check_ConditionStatus{get;set;}        
		/// <summary>
		/// Check_Condition
        /// </summary>		
        public string Check_Condition{get;set;}        
		   
	}
}

