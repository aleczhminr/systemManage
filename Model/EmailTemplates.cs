using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//EmailTemplates
			/// <summary>
		/// EmailTemplates
        /// </summary>	
    [Serializable]
	public partial class EmailTemplates
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// temp_Content
        /// </summary>		
        public string temp_Content{get;set;}        
		/// <summary>
		/// inertTime
        /// </summary>		
        public DateTime inertTime{get;set;}        
		/// <summary>
		/// inertId
        /// </summary>		
        public int inertId{get;set;}        
		/// <summary>
		/// showImg
        /// </summary>		
        public string showImg{get;set;}        
		   
	}
}

