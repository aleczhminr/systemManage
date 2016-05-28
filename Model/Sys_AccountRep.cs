using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_AccountRep
			/// <summary>
		/// Sys_AccountRep
        /// </summary>	
    [Serializable]
	public partial class Sys_AccountRep
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
		/// statTime
        /// </summary>		
        public DateTime statTime{get;set;}        
		/// <summary>
		/// endtime
        /// </summary>		
        public DateTime endtime{get;set;}        
		/// <summary>
		/// insertTime
        /// </summary>		
        public DateTime insertTime{get;set;}        
		/// <summary>
		/// Browser
        /// </summary>		
        public string Browser{get;set;}        
		   
	}
}

