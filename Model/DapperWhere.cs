using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    ///条件
    ///</summary>
    [Serializable]
    public partial class DapperWhere
    {
        private string _ColumnName;
        private object _Value;
        private string _Where = "";
        public DapperWhere() { }
        /// <summary>
        /// 条件
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="dbtype">类别</param>
        /// <param name="size">长度</param>
        public DapperWhere(string columnName)
        {
            _ColumnName = columnName;
        }
        /// <summary>
        /// 条件
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="dbtype">类别</param>
        /// <param name="size">长度</param>
        /// <param name="value">值</param>
        public DapperWhere(string columnName, object value)
        {
            _ColumnName = columnName;
            _Value = value;
        }
        /// <summary>
        /// 条件
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="dbtype">类别</param>
        /// <param name="size">长度</param>
        /// <param name="value">值</param>
        public DapperWhere(string columnName, object value,string where)
        {
            _ColumnName = columnName;
            _Value = value;
            _Where = where;
        }
        /// <summary>
        /// 属性值
        /// </summary>
        public object Value
        {
            set { _Value = value; }
            get { return _Value; }
        }
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            set { _ColumnName = value; }
            get { return _ColumnName; }
        }
        /// <summary>
        /// 条件语句，默认不用设置
        /// <para>1、默认为 列名=@列名</para>
        /// <para>2、如果设置此值，将覆盖 1</para>
        /// </summary>
        public string Where
        {
            set { _Where = value; }
            get {
                if (_Where.Length > 0)
                {
                    return " " + _Where + " ";
                }
                else
                {
                    return " " + _ColumnName + "=@" + _ColumnName + " ";
                }
            }
        }
    }
}
