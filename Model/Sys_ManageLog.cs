using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_ManageLog
			/// <summary>
		/// Sys_ManageLog
        /// </summary>	
    [Serializable]
	public partial class Sys_ManageLog
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
        public int ID{get;set;}        
		/// <summary>
		/// LogType
        /// </summary>		
        public int LogType{get;set;}        
		/// <summary>
		/// OperDate
        /// </summary>		
        public DateTime OperDate{get;set;}        
		/// <summary>
		/// Managerid
        /// </summary>		
        public int Managerid{get;set;}        
		/// <summary>
		/// LogInfo
        /// </summary>		
        public string LogInfo{get;set;}        
		/// <summary>
		/// LogMode
        /// </summary>		
        public int LogMode{get;set;}        
		/// <summary>
		/// TypeID
        /// </summary>		
        public string TypeID{get;set;}        
		/// <summary>
		/// Loginbrslast
        /// </summary>		
        public string Loginbrslast{get;set;}        
		/// <summary>
		/// IP
        /// </summary>		
        public string IP{get;set;}        
		   
	}
}

