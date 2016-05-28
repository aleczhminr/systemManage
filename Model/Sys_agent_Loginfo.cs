using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_agent_Loginfo
			/// <summary>
		/// Sys_agent_Loginfo
        /// </summary>	
    [Serializable]
	public partial class Sys_agent_Loginfo
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// am_id
        /// </summary>		
        public int am_id{get;set;}        
		/// <summary>
		/// logTime
        /// </summary>		
        public DateTime logTime{get;set;}        
		/// <summary>
		/// logIp
        /// </summary>		
        public string logIp{get;set;}        
		/// <summary>
		/// logBrowse
        /// </summary>		
        public string logBrowse{get;set;}        
		/// <summary>
		/// logType
        /// </summary>		
        public int logType{get;set;}        
		   
	}
}

