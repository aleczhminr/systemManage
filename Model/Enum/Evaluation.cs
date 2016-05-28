using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public static class EvaluationEnum
    {
        public enum ProductType : int
        {
            虚拟商品 = 0,
            实物 = 1
        }
        public enum isDisplay : int
        {
            显示 = 0,
            未处理 = 1,
            不显示 = 2
        }
    }
}
