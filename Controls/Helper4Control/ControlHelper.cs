using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Helper4Control
{
    public static class ControlHelper
    {
        //小数点显示 显示2位数字
        public static double formats(double shu, int num)
        {
            string str = shu.ToString();
            int wei = (str.IndexOf(".", 0) + 1); //找到小数点出现的位置，就是整数的位数
            double q = 0;
            double h = 0;
            if (wei > 0 & str.Length - wei > num & num > 0)
            {
                q = System.Convert.ToDouble(str.Substring(0, wei + num));
                h = str.Substring((wei + num + 1) - 1, 1).Length;//加了length
            }
            else
            {
                q = shu;
            }
            if (h > 4)
            {
                q = q + 1 / ((System.Math.Pow(10, num)));
            }
            return q;
        }
        public static decimal formats(decimal shu, int num)
        {
            string str = shu.ToString();
            int wei = (str.IndexOf(".", 0) + 1); //找到小数点出现的位置，就是整数的位数
            decimal q = 0;
            decimal h = 0;
            if (wei > 0 & str.Length - wei > num & num > 0)
            {
                q = System.Convert.ToDecimal(str.Substring(0, wei + num));
                h = str.Substring((wei + num + 1) - 1, 1).Length;//加了length
            }
            else
            {
                q = shu;
            }
            if (h > 4)
            {
                q = q + Convert.ToDecimal(1 / ((System.Math.Pow(10, num))));
            }
            return q;
        }
        /// <summary>
        /// 字符转Double
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num">小数位数</param>
        /// <returns></returns>
        public static double StringToDouble(string str, int num)
        {
            double shu = 0;
            if (!double.TryParse(str, out shu))
            {
                shu = 0;
            }
            int wei = (str.IndexOf(".", 0) + 1); //找到小数点出现的位置，就是整数的位数
            double q = 0;
            double h = 0;
            if (wei > 0 & str.Length - wei > num & num > 0)
            {
                q = System.Convert.ToDouble(str.Substring(0, wei + num));
                h = str.Substring((wei + num + 1) - 1, 1).Length;//加了length
            }
            else
            {
                q = shu;
            }
            if (h > 4)
            {
                q = q + 1 / ((System.Math.Pow(10, num)));
            }
            return q;
        }
        public static int date_difference(System.DateTime d1, System.DateTime d2)
        {
            System.DateTime date1 = d1;
            System.DateTime date2 = d2;
            TimeSpan date_p = date2 - date1;
            int day = date_p.Days + 1;
            return day;
        }
        public static int months_difference(System.DateTime d1, System.DateTime d2)
        {
            System.DateTime date1 = d1;
            System.DateTime date2 = d2;
            int d1_y = date1.Year;
            int d1_m = date1.Month;
            int d2_y = date2.Year;
            int d2_m = date2.Month;
            int y = 0;
            int m = 0;
            m = d2_m - d1_m;
            if (m < 0)
            {
                m = m + 12;
                d2_y = d2_y - 1;
            }
            y = d2_y - d1_y;
            m = m + y * 12 + 1;
            return m;
        }
        public static int year_difference(System.DateTime d1, System.DateTime d2)
        {
            System.DateTime date1 = d1;
            System.DateTime date2 = d2;
            int d1_y = date1.Year;
            int d2_y = date2.Year;
            int y = 0;
            y = d2_y - d1_y + 1;
            return y;
        }
        //格式 化时间 把 20101009 转化成 2010-10-09
        public static string yyyyMMdd(string date_str)
        {
            string str = "";
            string yuan = date_str; //从yuan里面取，从第5个取2个字符串
            str = yuan.Substring(0, 4) + "-" + yuan.Substring(4, 2) + "-" + yuan.Substring(6, 2);
            return str;
        }
        public static string yyyyMM(string date_str)
        {
            string str = "";
            string yuan = date_str;
            str = yuan.Substring(0, 4) + "-" + yuan.Substring(4, 2);
            return str;
        }
        public static string MMdd(string date_str)
        {
            string str = "";
            string yuan = date_str; //从yuan里面取，从第5个取2个字符串
            str = yuan.Substring(4, 2) + "-" + yuan.Substring(6, 2);
            return str;
        }
        public static string color_rgb()
        {
            //Microsoft.VisualBasic.VBMath.Randomize();
            System.Random SS = new Random();

            int r = System.Convert.ToInt32((int)Math.Floor((decimal)(255 * SS.Next()) + 1));//Microsoft.VisualBasic.VBMath.Rnd()
            int g = System.Convert.ToInt32((int)Math.Floor((decimal)(255 * SS.Next()) + 1));//Microsoft.VisualBasic.VBMath.Rnd()
            int b = System.Convert.ToInt32((int)Math.Floor((decimal)(255 * SS.Next()) + 1));//Microsoft.VisualBasic.VBMath.Rnd()
            string str = "#" + System.Convert.ToString(r, 16).ToUpper() + System.Convert.ToString(g, 16).ToUpper() + System.Convert.ToString(b, 16).ToUpper();
            return str;
        }
        //Public Function obj_null_return(ByVal name As Object, ByVal valu As Object) As Object
        //    If (name Is DBNull.Value) Then
        //        Return valu
        //    Else
        //        If (Trim(name).Length > 0) Then
        //            Return name
        //        Else
        //            Return valu
        //        End If
        //    End If
        //End Function
        public static object obj_null_return(object name, object valu)
        {
            if (name == DBNull.Value)
            {
                return valu;
            }
            else
            {
                if (System.Convert.ToString(name).ToString().Trim().Length > 0)
                {
                    return name;
                }
                else
                {
                    return valu;
                }
            }
        }
        public static object string_null_return(object valur, object retu)
        {
            if (string.IsNullOrEmpty((string)valur))//加了string装换 
            {
                return retu;
            }
            else if (System.Convert.ToString(valur).ToString().Trim().Length < 1)
            {
                return retu;
            }
            else
            {
                return valur;
            }
        }
        public static string time_string(object time, string yyMMdd)
        {
            System.DateTime date_time = System.DateTime.Now;
            string return1 = "";
            try
            {
                date_time = (DateTime)time;
                return1 = date_time.ToString(yyMMdd);
            }
            catch (Exception ex)
            {
                return1 = ex.ToString();
            }
            return return1;
        }

        public static string Sabsp(string str)
        {
            str = str.ToString().Trim();
            while ((str.IndexOf("  ", 0) + 1) > 0)
            {
                str = str.Replace("  ", " ");
            }
            return str;
        }
        /// <summary>
        /// 店铺ID 加密
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string accountIdEncryption(object id)
        {
            StringBuilder str = new StringBuilder();
            char[] v = id.ToString().ToCharArray();
            foreach (char c in v)
            {
                str.Append(Convert.ToInt32(c.ToString()) + 44);
            }
            return str.ToString();
        }
        /// <summary>
        /// 店铺ID解密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int accountIdDecryption(object val)
        {
            char[] v = val.ToString().ToCharArray();
            int index = 0;
            StringBuilder z = new StringBuilder();
            StringBuilder d = new StringBuilder();
            foreach (char c in v)
            {
                d.Append(c);
                index++;
                if (index == 2)
                {
                    z.Append((Convert.ToInt32(d.ToString()) - 44).ToString());
                    d = new StringBuilder();
                    index = 0;
                }
            }
            return Convert.ToInt32(z.ToString());
        }


        public static int NumberAlignment(int Old, float num)
        {
            return Convert.ToInt32(Old * num);
        }

        public static decimal NumberAlignment(decimal old, decimal num)
        {
            return Convert.ToDecimal(old * num);
        }

        /// <summary>
        /// 获取终止时间通用方法
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetEndDate(DateTime date)
        {
            return date.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static string GetFixDecimalStr(decimal numerator, decimal denominator)
        {
            return denominator == 0
                                        ? "-"
                                        : (Convert.ToDecimal(numerator - denominator) * 100 / denominator).ToString("F2") +
                                          "%";
        }
    }
}
