using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class T_Common_SmsBLL
    {
        public static Dictionary<string, string> GetSmsContent(int page, string type, string subType)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.GetSmsContent(page, type,subType);
        }

        public static List<string> GetMinCate(string maxCate)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.GetminCate(maxCate);
        }

        public static string AddCate(string cate)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.AddCate(cate);
        }

        public static string UpdateCommonSmsContent(int smsid, string maxCate, string minCate, string smscontent)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.UpdateCommonSmsContent(smsid, maxCate, minCate, smscontent);
        }

        public static string AddCommonSms(string maxCate, string minCate, string smscontent)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.AddCommonSms(maxCate, minCate, smscontent);
        }

        public static string DeleteSms(int id)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.DeleteSms(id);
        }

        public static string GetCateList(string type)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.GetCateList(type);
        }

        public static string UpdateCateName(string maxName, string minName, string updateName)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.UpdateCateName(maxName, minName, updateName);
        }

        public static string DeleteCate(string maxName, string minName)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.DeleteCate(maxName, minName);
        }

        public static string ChangeStatus(string maxName, string minName)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.ChangeStatus(maxName, minName);
        }

        public static string ChangeRanking(string maxName, string minName, int rank)
        {
            T_Common_SmsDAL dal = new T_Common_SmsDAL();
            return dal.ChangeRanking(maxName, minName, rank);
        }
    }
}
