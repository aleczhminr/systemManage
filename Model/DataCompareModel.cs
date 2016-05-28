using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DataCompareModel
    {
        public DataCompareModel(string name)
        {
            Name = name;
            FormerData = 0;
            AfterData = 0;
        }

        public string Name { get; set; }
        public int FormerData { get; set; }
        public int AfterData { get; set; }
    }

}
