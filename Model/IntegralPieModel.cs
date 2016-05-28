using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class IntegralPieModel
    {
        public IntegralPieModel()
        {
            GoodsType = new List<IntPie>();
            ExRatio = new List<IntPie>();
            VisitRatio = new List<DecimalPie>();
            VisitRatioSec = new List<DecimalPie>();
        }

        public List<IntPie> GoodsType { get; set; }
        public List<IntPie> ExRatio { get; set; }
        public List<DecimalPie> VisitRatio { get; set; }

        public List<DecimalPie> VisitRatioSec { get; set; }
    }

    public class IntPie
    {
        public IntPie()
        {
            
        }

        public IntPie(string name,int num)
        {
            Name = name;
            Value = num;
        }
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class DecimalPie
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
