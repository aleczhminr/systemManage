using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace OperationPlatform.HelperEx
{
    public class ValidateCode
    {
        //生成图象验证码函数
        public static byte[] validatecode(string vnum)
        {
            System.Drawing.Bitmap img = null;
            System.Drawing.Graphics g = null;

            try
            {
                Random r = new Random();
                int gheight = Convert.ToInt32(Math.Floor((decimal) vnum.Length*13));
                //'gheight为图片宽度,根据字符长度自动更改图片宽度
                img = new System.Drawing.Bitmap(gheight, 20);
                g = System.Drawing.Graphics.FromImage(img);
                //g.DrawString(vnum, New System.Drawing.Font("Arial", 10), New System.Drawing.SolidBrush(Color.Blue), 3, 3)
                //新增，修改
                //画图片的背景噪音线
                //For i As Integer = 0 To 25
                for (int i = 0; i <= 10; i++)
                {
                    int x1 = 0;
                    x1 = r.Next(img.Width);
                    int x2 = r.Next(img.Width);
                    int y1 = r.Next(img.Height);
                    int y2 = r.Next(img.Height);
                    g.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Silver), x1, y1, x2, y2);
                }
                System.Drawing.Font font = null;
                font = new System.Drawing.Font("Arial", 12);
                System.Drawing.Drawing2D.LinearGradientBrush brush = null;
                brush =
                    new System.Drawing.Drawing2D.LinearGradientBrush(
                        new System.Drawing.Rectangle(0, 0, img.Width, img.Height), System.Drawing.Color.Blue,
                        System.Drawing.Color.Blue, 1.2F, true);
                g.DrawString(vnum, font, brush, 2, 2);

                //'画图片的前景噪音点 
                //For ii As Integer = 0 To 100
                //    Dim x As Integer = r.Next(img.Width)
                //    Dim y As Integer = r.Next(img.Height)
                //    img.SetPixel(x, y, Color.FromArgb(r.Next()))
                //Next

                //画图片的边框线 
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Silver), 0, 0, img.Width - 1, img.Height - 1);

                //在矩形内绘制字串（字串，字体，画笔颜色，左上x.左上y） 
                System.IO.MemoryStream ms1 = null;
                ms1 = new System.IO.MemoryStream();
                img.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);

                return ms1.ToArray();
            }
            finally
            {
                g.Dispose();
                img.Dispose();
            }
        }
        //--------------------------------------------
        //函数名称:rndnum
        //函数参数:vcodenum--设定返回随机字符串的位数
        //函数功能:产生数字和字符混合的随机字符串
        //--------------------------------------------
        public static string rndnum(int vcodenum)
        {
            //Dim vchar As String = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z"
            string vchar = "2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            //Dim vchar As String = "0,1,2,3,4,5,6,7,8,9"
            string[] VcArray = vchar.Split(",".ToCharArray()); //将字符串生成数组  
            string vnum = "";
            byte i = 0;
            System.Random ro = new Random();
            for (i = 1; i <= vcodenum; i++)
            {
                //Random ss = new Random();
                ////vnum = vnum & vcarray(Int(35 * Rnd())) '数组一般从0开始读取，所以这里为35*rnd

                //vnum = vnum + vcarray[(int)Math.Floor(28 * (float)(ss.Next() + ss.Next()) / 2)]; //数组一般从0开始读取，所以这里为35*rnd

                double decA = ro.NextDouble();
                vnum = vnum + VcArray[Convert.ToInt32(32 * decA)];
            }
            return vnum;
        }
    }
}