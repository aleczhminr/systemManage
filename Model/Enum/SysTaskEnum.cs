using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 系统任务
    /// </summary>
   public static class SysTaskEnum
    {
       public enum TaskStatus : int
       {
           普通=0,
           占用=1,
           处理延后=2,
           处理成功=3
       }

    }
}
