using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RegUsrDiff
    {
        public int YesterIOS { get; set; }
        public int YesterAndroid { get; set; }
        public int YesterPC { get; set; }
        public int YesterWeb { get; set; }
        public int YesterMobileWeb { get; set; }
        public int YesterPad { get; set; }

        public int IOS { get; set; }
        public int Android { get; set; }
        public int PC { get; set; }
        public int Web { get; set; }
        public int MobileWeb { get; set; }
        public int Pad { get; set; }
    }

    public class LoginTypeDiff
    {
        public int YesterPCLog { get; set; }
        public int YesterWebLog { get; set; }
        public int YesterIOSLog { get; set; }
        public int YesterAndroidLog { get; set; }
        public int YesterPadLog { get; set; }

        public int PCLog { get; set; }
        public int WebLog { get; set; }
        public int IOSLog { get; set; }
        public int AndroidLog { get; set; }
        public int PadLog { get; set; }

        #region 登录端重合维恩图数据
        public int WebPC { get; set; }
        public int WebIOS { get; set; }
        public int WebAndroid { get; set; }
        public int PCAndroid { get; set; }
        public int PCIOS { get; set; }
        public int IOSAndroid { get; set; }

        public int WebPad { get; set; }
        public int IOSPad { get; set; }
        public int AndroidPad { get; set; }
        public int PCPad { get; set; }

        #endregion
    }

    public class LoginTrend {
        public DateTime dayDate { get; set; }
        public string terminal { get; set; }
        public int logCount { get; set; }
    }
}
