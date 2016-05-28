using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_VisitReason
			/// <summary>
    /// 回访类别
        /// </summary>	
    [Serializable]
	public partial class Sys_VisitReason
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// vr_name
        /// </summary>		
        public string vr_name{get;set;}        
		/// <summary>
		/// vr_id
        /// </summary>		
        public int vr_id{get;set;}        
		/// <summary>
		/// insertName
        /// </summary>		
        public string insertName{get;set;}        
		/// <summary>
		/// insertTime
        /// </summary>		
        public DateTime insertTime{get;set;}        
		/// <summary>
		/// vr_state
        /// </summary>		
        public int vr_state{get;set;}        
		   
	}
}

