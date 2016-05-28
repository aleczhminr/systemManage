using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_Visit
			/// <summary>
		/// Sys_Visit
        /// </summary>	
    [Serializable]
	public partial class Sys_Visit
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// accid
        /// </summary>		
        public int accid{get;set;}        
		/// <summary>
		/// sendType
        /// </summary>		
        public int sendType{get;set;}        
		/// <summary>
		/// sendtime
        /// </summary>		
        public DateTime sendtime{get;set;}        
		/// <summary>
		/// sysType
        /// </summary>		
        public int sysType{get;set;}        
		/// <summary>
		/// sendContent
        /// </summary>		
        public string sendContent{get;set;}        
		/// <summary>
		/// Remark
        /// </summary>		
        public string Remark{get;set;}        
		/// <summary>
		/// loginTime
        /// </summary>		
        public DateTime loginTime{get;set;}        
		/// <summary>
		/// delayDay
        /// </summary>		
        public int delayDay{get;set;}        
		   
	}
}

