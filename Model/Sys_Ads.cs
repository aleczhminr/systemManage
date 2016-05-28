using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_Ads
			/// <summary>
		/// Sys_Ads
        /// </summary>	
    [Serializable]
	public partial class Sys_Ads
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// title
        /// </summary>		
        public string title{get;set;}        
		/// <summary>
		/// picString
        /// </summary>		
        public string picString{get;set;}        
		/// <summary>
		/// adString
        /// </summary>		
        public string adString{get;set;}        
		/// <summary>
		/// createTime
        /// </summary>		
        public DateTime createTime{get;set;}        
		/// <summary>
		/// startTime
        /// </summary>		
        public DateTime startTime{get;set;}        
		/// <summary>
		/// endTime
        /// </summary>		
        public DateTime endTime{get;set;}        
		/// <summary>
		/// status
        /// </summary>		
        public int status{get;set;}        
		/// <summary>
		/// viewNums
        /// </summary>		
        public int viewNums{get;set;}        
		/// <summary>
		/// operatorId
        /// </summary>		
        public int operatorId{get;set;}        
		   
	}
}

