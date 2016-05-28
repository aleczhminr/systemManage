using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	 	//Sys_ProvinceCityList
			/// <summary>
		/// Sys_ProvinceCityList
        /// </summary>	
    [Serializable]
	public partial class Sys_ProvinceCityList
	{
   		     
      	/// <summary>
		/// id
        /// </summary>		
        public int id{get;set;}        
		/// <summary>
		/// PC_Name
        /// </summary>		
        public string PC_Name{get;set;}        
		/// <summary>
		/// PC_Id
        /// </summary>		
        public int PC_Id{get;set;}        
		/// <summary>
		/// PC_Order
        /// </summary>		
        public int PC_Order{get;set;}        
		/// <summary>
		/// Json_Name
        /// </summary>		
        public string Json_Name{get;set;}        
		/// <summary>
		/// Prov_Name
        /// </summary>		
        public string Prov_Name{get;set;}        
		   
	}
}

