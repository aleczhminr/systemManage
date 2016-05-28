using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_TaskType
			/// <summary>
		/// Sys_TaskType
        /// </summary>	
    [Serializable]
	public partial class Sys_TaskType
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// tt_Marked
        /// </summary>		
        public string tt_Marked{get;set;}        
		/// <summary>
		/// tt_Name
        /// </summary>		
        public string tt_Name{get;set;}        
		/// <summary>
		/// tt_Explanation
        /// </summary>		
        public string tt_Explanation{get;set;}        
		/// <summary>
		/// dt_Level
        /// </summary>		
        public int dt_Level{get;set;}        
		/// <summary>
		/// vr_MaxId
        /// </summary>		
        public int vr_MaxId{get;set;}        
		/// <summary>
		/// vr_MinId
        /// </summary>		
        public int vr_MinId{get;set;}        
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

