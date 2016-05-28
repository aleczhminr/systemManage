using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
    /// <summary>
    ///店铺 活跃状态 变更 日志记录	
    ///</summary>	
    [Serializable]
    public partial class SysRpt_ShopActive
    {
        private int _id;
        private int _accid;
        private DateTime _regtime;
        private int _active;
        private int _lastnum;
        private DateTime _starttime = DateTime.Now;
        private DateTime _updatetime;
        private string _remark;
        private int _firstid;
        private int _stateval = 0;

        /// <summary>
        /// id
        /// </summary>		
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 店铺ID
        /// </summary>		
        public int accid
        {
            get { return _accid; }
            set { _accid = value; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>		
        public DateTime regTime
        {
            get { return _regtime; }
            set { _regtime = value; }
        }
        /// <summary>
        /// 当前状态
        /// </summary>		
        public int active
        {
            get { return _active; }
            set { _active = value; }
        }
        /// <summary>
        /// 持续时间
        /// </summary>		
        public int lastNum
        {
            get { return _lastnum; }
            set { _lastnum = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>		
        public DateTime startTime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>		
        public DateTime updatetime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 上一个ID
        /// </summary>		
        public int firstID
        {
            get { return _firstid; }
            set { _firstid = value; }
        }
        /// <summary>
        /// 状态  0 为当前，1以历史
        /// </summary>		
        public int stateVal
        {
            get { return _stateval; }
            set { _stateval = value; }
        }

    }

    /// <summary>
    /// 后台店铺状态列表
    /// </summary>
    public partial class SysShopActiveList
    {
        public List<SysShopSummarizeInfo> shopList { get; set; }
        public int rowCount { get; set; }
    }
}

