using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_VisitEmail
			/// <summary>
		/// Sys_VisitEmail
        /// </summary>	
    [Serializable]
	public partial class Sys_VisitEmail
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
		/// email
        /// </summary>		
        public string email{get;set;}        
		/// <summary>
		/// emailTitle
        /// </summary>		
        public string emailTitle{get;set;}        
		/// <summary>
		/// systype
        /// </summary>		
        public int systype{get;set;}        
		/// <summary>
		/// emailContent
        /// </summary>		
        public string emailContent{get;set;}        
		/// <summary>
		/// sendtime
        /// </summary>		
        public DateTime sendtime{get;set;}        
		/// <summary>
		/// sendStatus
        /// </summary>		
        public int sendStatus{get;set;}        
		/// <summary>
		/// emailChannel
        /// </summary>		
        public string emailChannel{get;set;}        
		/// <summary>
		/// priority
        /// </summary>		
        public int priority{get;set;}        
		/// <summary>
		/// recordVisit
        /// </summary>		
        public int recordVisit{get;set;}        
		/// <summary>
		/// remark
        /// </summary>		
        public string remark{get;set;}        
		/// <summary>
		/// insertime
        /// </summary>		
        public DateTime insertime{get;set;}        
		/// <summary>
		/// EmailAisle
        /// </summary>		
        public int EmailAisle{get;set;}        
		/// <summary>
		/// ContentTemplate
        /// </summary>		
        public int ContentTemplate{get;set;}        
		/// <summary>
		/// EmailBatch
        /// </summary>		
        public int EmailBatch{get;set;}        
		   
	}
}

