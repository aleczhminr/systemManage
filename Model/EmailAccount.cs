using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//EmailAccount
			/// <summary>
		/// EmailAccount
        /// </summary>	
    [Serializable]
	public partial class EmailAccount
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// sendType
        /// </summary>		
        public int sendType{get;set;}        
		/// <summary>
		/// accountContent
        /// </summary>		
        public string accountContent{get;set;}        
		/// <summary>
		/// sendNumber
        /// </summary>		
        public int sendNumber{get;set;}        
		/// <summary>
		/// updatetime
        /// </summary>		
        public DateTime updatetime{get;set;}        
		   
	}
}

