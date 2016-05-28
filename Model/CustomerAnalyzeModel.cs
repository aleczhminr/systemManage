using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CustomerAnalyzeModel
    {
        public CustomerAnalyzeModel()
        {
            csList = new List<CustomerSource>();
            osList = new List<OtherSoftware>();
            unrecordNum = 0;
        }

        public List<CustomerSource> csList { get; set; }

        public List<OtherSoftware> osList { get; set; }

        public int unrecordNum { get; set; }
    }

    public class CustomerSource
    {
        public string sourceType { get; set; }
        public int? sourceCount { get; set; }
    }

    public class OtherSoftware
    {
        public string softwareType { get; set; }
        public int? softwareCount { get; set; }
    }
}
