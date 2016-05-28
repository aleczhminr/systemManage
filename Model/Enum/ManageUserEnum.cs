using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public static class ManageUserEnum
    {

        public enum Session : int
        {
            管理员 = 0,
            客服 = 2,
            运营 = 4,
            普通客服 = 5,
            客服管理 = 6,
            普通运营 = 7,
            客服运营 = 8,
            临时权限 = 9,
            特殊权限 = 50
        }

    }
}
