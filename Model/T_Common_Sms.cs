using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class T_Common_Sms
    {
        public int id { get; set; }
        public string sms_maxclass { get; set; }
        public string sms_class { get; set; }
        public string sms_content { get; set; }
        public DateTime sms_time { get; set; }
        public int sms_ranking { get; set; }
        public int HiddenType { get; set; }
    }

    public class SimpleSms
    {
        public string sms_class { get; set; }
        public int sms_ranking { get; set; }
        public int HiddenType { get; set; }
    }
}
