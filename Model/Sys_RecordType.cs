using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_RecordType
			/// <summary>
		/// Sys_RecordType
        /// </summary>	
    [Serializable]
	public partial class Sys_RecordType
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// rt_Name
        /// </summary>		
        public string rt_Name{get;set;}        
		/// <summary>
		/// rt_id
        /// </summary>		
        public int rt_id{get;set;}        
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

