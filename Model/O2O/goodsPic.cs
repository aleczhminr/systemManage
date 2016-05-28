using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.O2O
{
    //goodsPic
    /// <summary>
    /// goodsPic
    /// </summary>	
    [Serializable]
    public partial class goodsPic
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// gid
        /// </summary>		
        public int gid { get; set; }
        /// <summary>
        /// ygid
        /// </summary>		
        public int ygid { get; set; }
        /// <summary>
        /// picUrl
        /// </summary>		
        public string picUrl { get; set; }
        /// <summary>
        /// picOrder
        /// </summary>		
        public int picOrder { get; set; }

    }
}
