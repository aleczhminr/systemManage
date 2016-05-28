using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_VisitManner
			/// <summary>
		/// Sys_VisitManner
        /// </summary>	
    [Serializable]
	public partial class Sys_VisitManner
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// vm_name
        /// </summary>		
        public string vm_name{get;set;}        
		/// <summary>
		/// vm_id
        /// </summary>		
        public int vm_id{get;set;}        
		/// <summary>
		/// insertName
        /// </summary>		
        public string insertName{get;set;}        
		/// <summary>
		/// insertTime
        /// </summary>		
        public DateTime insertTime{get;set;}        
		   
	}
}

