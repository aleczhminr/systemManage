using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SmsManageBLL
    {
        /// <summary>
        /// 获取短信分类
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<string> GetSmsClass(string classId)
        {
            var dal = new SmsManageDal();
            return dal.GetSmsClass(classId);
        }

        /// <summary>
        /// 保存短信
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int SaveSms(string classMax, string classMin, string content)
        {
            var dal = new SmsManageDal();
            return dal.SaveSms(classMax, classMin, content);
        }

        /// <summary>
        /// 删除短信
        /// </summary>
        /// <param name="smsId"></param>
        /// <returns></returns>
        public int DeleteSms(string smsId)
        {
            var dal = new SmsManageDal();
            return dal.DeleteSms(smsId);
        }

        /// <summary>
        /// 更新短信
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="content"></param>
        /// <param name="smsId"></param>
        /// <returns></returns>
        public int UpdateSms(string classMax, string classMin, string content, string smsId)
        {
            var dal = new SmsManageDal();
            return dal.UpdateSms(classMax, classMin, content, smsId);
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="cName"></param>
        /// <returns></returns>
        public int EditClass(string classMax, string classMin, string cName)
        {
            var dal = new SmsManageDal();
            return dal.EditClass(classMax, classMin, cName);
        }

        /// <summary>
        /// 编辑顺序
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int EditOrder(string classMax, string classMin, string order)
        {
            var dal = new SmsManageDal();
            return dal.EditOrder(classMax, classMin, order);
        }

        /// <summary>
        /// 隐藏分类
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <param name="hidden"></param>
        /// <returns></returns>
        public int HideSms(string classMax, string classMin, string hidden)
        {
            var dal = new SmsManageDal();
            return dal.HideSms(classMax, classMin, hidden);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="classMax"></param>
        /// <param name="classMin"></param>
        /// <returns></returns>
        public int ClassDelete(string classMax, string classMin)
        {
            var dal = new SmsManageDal();
            return dal.ClassDelete(classMax, classMin);
        }
    }
}
