using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public static class IndustryAnalyzeBLL
    {
        /// <summary>
        /// 根据字典更新获取
        /// </summary>
        /// <returns></returns>
        public static string UpdateUserIndustry()
        {
            IndustryAnalyzeDAL dal = new IndustryAnalyzeDAL();
            List<IndustryAnalyzeModel> list = dal.GetUserIndustry();

            #region 建立后台筛选字典
            Dictionary<string, List<string>> filterDic = new Dictionary<string, List<string>>()
            {
                {"服装鞋帽/箱包皮具",new List<string>()},
                {"美妆日化",new List<string>()},
                {"家用电器/数码电子",new List<string>()},
                {"便利店/超市",new List<string>()},
                {"百货精品",new List<string>()},
                {"烟酒茶行",new List<string>()},
                {"珠宝/饰品/文玩",new List<string>()},
                {"家纺家居",new List<string>()},
                {"运动户外",new List<string>()},
                {"生鲜果蔬/粮油干货",new List<string>()},
                {"文体办公用品",new List<string>()},
                {"母婴零售",new List<string>()},
                {"建材五金",new List<string>()},
                {"零食副食",new List<string>()},
                {"医药/保健/成人",new List<string>()},
                {"医疗器械",new List<string>()},
                {"花鸟鱼虫",new List<string>()},
                {"眼镜店",new List<string>()},
                {"图书/音像",new List<string>()},
                {"玩具店",new List<string>()},
                {"代购",new List<string>()},
                {"农资",new List<string>()},
                {"车行",new List<string>()},
                {"乐器行",new List<string>()},
                {"零售其他",new List<string>()},

                {"美容",new List<string>()},
                {"美发",new List<string>()},
                {"美甲美睫",new List<string>()},
                {"纹身",new List<string>()},
                {"整形",new List<string>()},
                {"纤体瘦身",new List<string>()},
                {"康复护理",new List<string>()},
                {"丽人其它",new List<string>()},

                {"KTV",new List<string>()},
                {"网吧",new List<string>()},
                {"影院影吧",new List<string>()},
                {"桌游棋牌",new List<string>()},
                {"洗浴/足疗/按摩",new List<string>()},
                {"养生保健",new List<string>()},
                {"公园景点",new List<string>()},
                {"采摘/农家乐",new List<string>()},
                {"游乐游艺",new List<string>()},
                {"运动健身",new List<string>()},
                {"休闲娱乐其他",new List<string>()},

                {"汽修汽配",new List<string>()},
                {"摄影",new List<string>()},
                {"冲印打印",new List<string>()},
                {"广告传媒",new List<string>()},
                {"教育培训",new List<string>()},
                {"旅行社",new List<string>()},
                {"售票厅",new List<string>()},
                {"洗衣护理",new List<string>()},
                {"家政",new List<string>()},
                {"宠物店",new List<string>()},
                {"家电维修",new List<string>()},
                {"宾馆酒店",new List<string>()},
                {"加油站",new List<string>()},
                {"生活服务其他",new List<string>()},

                {"餐厅/饭馆",new List<string>()},
                {"特色小吃",new List<string>()},
                {"蛋糕/甜品/饮品",new List<string>()},
                {"咖啡/茶楼/酒吧",new List<string>()}

                //{"零食副食",new List<string>()},
                //{"零食副食",new List<string>()},
                //{"零食副食",new List<string>()},
 
            };

            
            #endregion

            return "";
        }
        
    }
}
