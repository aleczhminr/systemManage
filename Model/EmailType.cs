using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//EmailType
			/// <summary>
		/// EmailType
        /// </summary>	
    [Serializable]
	public partial class EmailType
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// typeName
        /// </summary>		
        public string typeName{get;set;}        
		/// <summary>
		/// sendType
        /// </summary>		
        public string sendType{get;set;}        
		   
	}
}

