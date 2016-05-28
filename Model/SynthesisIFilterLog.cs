using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    ///筛选检索	
    ///</summary>
    [Serializable]
    public partial class SynthesisIFilterLog
    {
        private int _id;
        private string _strsql = "";
        private DateTime _insertime = DateTime.Now;
        private string _insername;
        private string _resultdata;
        private int _resultCount;
        private string _verification;
        private int _userid;

        /// <summary>
        /// id
        /// </summary>		
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 查询条件
        /// </summary>		
        public string strSql
        {
            get { return _strsql; }
            set { _strsql = value; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime inserTime
        {
            get { return _insertime; }
            set { _insertime = value; }
        }
        /// <summary>
        /// 录入人
        /// </summary>		
        public string inserName
        {
            get { return _insername; }
            set { _insername = value; }
        }
        /// <summary>
        /// 返回数据
        /// </summary>		
        public string resultData
        {
            get { return _resultdata; }
            set { _resultdata = value; }
        }
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int resultCount
        {
            get { return _resultCount; }
            set { _resultCount = value; }
        }

        /// <summary>
        /// 验证值
        /// </summary>		
        public string verification
        {
            get { return _verification; }
            set { _verification = value; }
        }
        /// <summary>
        /// 查询人标示
        /// </summary>		
        public int userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

    }
}
