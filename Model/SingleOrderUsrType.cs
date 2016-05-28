using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SingleOrderUsrType
    {
        public SingleOrderUsrType()
        {
            usrType = new List<UsrType>();
        }

        public List<UsrType> usrType { get; set; }
    }

    public class UsrType
    {
        public UsrType()
        {
            date = DateTime.Now;
            oldUsr = 0;
            newUsr = 0;
        }

        public DateTime date { get; set; }
        public int oldUsr { get; set; }
        public int newUsr { get; set; }
        public string XLable { get; set; }
    }
}
