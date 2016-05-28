using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_VisitSmsSnap
			/// <summary>
		/// Sys_VisitSmsSnap
        /// </summary>	
    [Serializable]
	public partial class Sys_VisitSmsSnap
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
		/// phoneNum
        /// </summary>		
        public string phoneNum{get;set;}        
		/// <summary>
		/// systype
        /// </summary>		
        public int systype{get;set;}        
		/// <summary>
		/// smstype
        /// </summary>		
        public int smstype{get;set;}        
		/// <summary>
		/// smsContent
        /// </summary>		
        public string smsContent{get;set;}        
		/// <summary>
		/// sendtime
        /// </summary>		
        public DateTime sendtime{get;set;}        
		/// <summary>
		/// sendStatus
        /// </summary>		
        public int sendStatus{get;set;}        
		/// <summary>
		/// smsChannel
        /// </summary>		
        public int smsChannel{get;set;}        
		/// <summary>
		/// priority
        /// </summary>		
        public int priority{get;set;}        
		/// <summary>
		/// realCnt
        /// </summary>		
        public int realCnt{get;set;}        
		/// <summary>
		/// recordVisit
        /// </summary>		
        public int recordVisit{get;set;}        
		/// <summary>
		/// Remark
        /// </summary>		
        public string Remark{get;set;}        
		/// <summary>
		/// insertime
        /// </summary>		
        public DateTime insertime{get;set;}        
		   
	}
}

