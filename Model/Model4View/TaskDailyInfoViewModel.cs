using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Model.Model4View
{
    public class TaskDailyInfoViewModel
    {
    }

    public class TaskDailyInfoContext : DbContext
    {
        public DbSet<Sys_TaskDailyInfo> TaskDailyInfoList { get; set; }
    }
}
