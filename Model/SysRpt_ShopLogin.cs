using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//SysRpt_ShopLogin
			/// <summary>
		/// SysRpt_ShopLogin
        /// </summary>	
    [Serializable]
	public partial class SysRpt_ShopLogin
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// accountid
        /// </summary>		
        public int accountid{get;set;}        
		/// <summary>
		/// AgentId
        /// </summary>		
        public int AgentId{get;set;}        
		/// <summary>
		/// regTime
        /// </summary>		
        public DateTime regTime{get;set;}        
		/// <summary>
		/// num
        /// </summary>		
        public int num{get;set;}        
		/// <summary>
		/// loginType
        /// </summary>		
        public int loginType{get;set;}        
		/// <summary>
		/// startTime
        /// </summary>		
        public DateTime startTime{get;set;}        
		/// <summary>
		/// endTime
        /// </summary>		
        public DateTime endTime{get;set;}        
		/// <summary>
		/// firstId
        /// </summary>		
        public int firstId{get;set;}        
		/// <summary>
		/// stateVal
        /// </summary>		
        public int stateVal{get;set;}        
		   
	}
}

