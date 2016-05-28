using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//CRM_Aut_Record
			/// <summary>
		/// CRM_Aut_Record
        /// </summary>	
    [Serializable]
	public partial class CRM_Aut_Record
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// r_accid
        /// </summary>		
        public int r_accid{get;set;}        
		/// <summary>
		/// r_max
        /// </summary>		
        public int r_max{get;set;}        
		/// <summary>
		/// r_min
        /// </summary>		
        public int r_min{get;set;}        
		/// <summary>
		/// t_tex
        /// </summary>		
        public string t_tex{get;set;}        
		/// <summary>
		/// r_time
        /// </summary>		
        public DateTime r_time{get;set;}        
		/// <summary>
		/// r_status
        /// </summary>		
        public int r_status{get;set;}        
		/// <summary>
		/// r_remark
        /// </summary>		
        public string r_remark{get;set;}        
		/// <summary>
		/// r_result
        /// </summary>		
        public string r_result{get;set;}        
		   
	}
}

