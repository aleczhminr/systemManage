using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserPortraitModel
    {

    }


    #region 用户行为轨迹图表
    public class UserBehaviorChartModel
    {
        public UserBehaviorChartModel()
        {
            DataList = new List<SysRpt_ShopDayInfo>();
        }
        /// <summary>
        /// 店铺所有数据列表
        /// </summary>
        public List<SysRpt_ShopDayInfo> DataList { get; set; }
    }
    #endregion
}
