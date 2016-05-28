using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Menu
{
    /// <summary>
    /// 菜单列表
    /// </summary>
    public class MenuList
    {
        /// <summary>
        /// 上级菜单
        /// </summary>
        public string superiorMenu { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int menuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menuName { get; set; }
        /// <summary>
        /// 菜单类别  1 左侧菜单  2 快速导航菜单
        /// </summary>
        public int menuType { get; set; }
        /// <summary>
        /// 连接页面
        /// </summary>
        public string menuUrl { get; set; }
    }

    public class MenuDataList
    {
        public MenuDataList()
        {
        }
        /// <summary>
        /// 得到所有菜单配置
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, MenuList> GetAllList()
        {
            Dictionary<int, MenuList> _MenuList = new Dictionary<int, MenuList>();
            //左侧边菜单

            #region 店铺管理
            _MenuList.Add(1, new MenuList()
            {
                superiorMenu = "店铺管理",
                menuId = 1,
                menuName = "店铺列表",
                menuType = 1,
                menuUrl = "/ShopList/"
            });
            _MenuList.Add(2, new MenuList()
            {
                superiorMenu = "店铺管理",
                menuId = 2,
                menuName = "店铺审核",
                menuType = 1,
                menuUrl = "/HomePage/ShopCheck"
            });
            #endregion

            #region 运营分析
            _MenuList.Add(3, new MenuList()
            {
                superiorMenu = "运营分析",
                menuId = 3,
                menuName = "平台趋势",
                menuType = 1,
                menuUrl = "/Operation/Tendency/"
            });
            _MenuList.Add(4, new MenuList()
            {
                superiorMenu = "运营分析",
                menuId = 4,
                menuName = "数据对比",
                menuType = 1,
                menuUrl = "/Operation/MonthComparison"
            });
            _MenuList.Add(5, new MenuList()
            {
                superiorMenu = "运营分析",
                menuId = 5,
                menuName = "用户来源",
                menuType = 1,
                menuUrl = "/Operation/RegisterSource"
            });
            _MenuList.Add(6, new MenuList()
            {
                superiorMenu = "运营分析",
                menuId = 6,
                menuName = "活跃率分析",
                menuType = 1,
                menuUrl = "/Operation/ActiveAnalysis"
            });
            _MenuList.Add(7, new MenuList()
            {
                superiorMenu = "运营分析",
                menuId = 7,
                menuName = "留存率分析",
                menuType = 1,
                menuUrl = "/UserRetention/Index"
            });
            #endregion

            #region 数据筛选
            //_MenuList.Add(8, new MenuList()
            //{
            //    superiorMenu = "核心运营",
            //    menuId = 8,
            //    menuName = "筛选器",
            //    menuType = 2,
            //    menuUrl = "/FiltrateData/AccountFiltrate"
            //});
            #endregion

            #region 订单管理
            _MenuList.Add(9, new MenuList()
            {
                superiorMenu = "订单管理",
                menuId = 9,
                menuName = "订单信息",
                menuType = 1,
                menuUrl = "/Order/Index"
            });
            _MenuList.Add(10, new MenuList()
            {
                superiorMenu = "订单管理",
                menuId = 10,
                menuName = "发票信息",
                menuType = 1,
                menuUrl = "/Order/InvoiceList"
            });
            _MenuList.Add(11, new MenuList()
            {
                superiorMenu = "订单管理",
                menuId = 11,
                menuName = "订单类型分析",
                menuType = 1,
                menuUrl = "/OrderAnalyze/Index"
            });
            _MenuList.Add(12, new MenuList()
            {
                superiorMenu = "订单管理",
                menuId = 12,
                menuName = "新用户订单类型",
                menuType = 1,
                menuUrl = "/OrderAnalyze/NewUsrOrderAnalyze"
            });
            #endregion

            #region 短信
            _MenuList.Add(13, new MenuList()
            {
                superiorMenu = "短信",
                menuId = 13,
                menuName = "短信列表",
                menuType = 1,
                menuUrl = "/sms"
            });
            _MenuList.Add(14, new MenuList()
            {
                superiorMenu = "短信",
                menuId = 14,
                menuName = "短信审核",
                menuType = 1,
                menuUrl = "/sms/smsreview"
            });
            _MenuList.Add(15, new MenuList()
            {
                superiorMenu = "短信",
                menuId = 15,
                menuName = "通道设置",
                menuType = 1,
                menuUrl = "/sms/channelOption"
            });
            _MenuList.Add(16, new MenuList()
            {
                superiorMenu = "短信",
                menuId = 16,
                menuName = "通道测试",
                menuType = 1,
                menuUrl = "/sms/channeltest"
            });
            _MenuList.Add(17, new MenuList()
            {
                superiorMenu = "短信",
                menuId = 17,
                menuName = "短信预告",
                menuType = 1,
                menuUrl = "/sms/NoticeText"
            });
            _MenuList.Add(18, new MenuList()
            {
                superiorMenu = "短信",
                menuId = 18,
                menuName = "短信内容管理",
                menuType = 1,
                menuUrl = "/sms/SmsContentMaster"
            });
            #endregion

            #region 回访和反馈
            _MenuList.Add(19, new MenuList()
            {
                superiorMenu = "回访和反馈",
                menuId = 19,
                menuName = "回访信息",
                menuType = 1,
                menuUrl = "/PlatformVisit"
            });
            _MenuList.Add(20, new MenuList()
            {
                superiorMenu = "回访和反馈",
                menuId = 20,
                menuName = "用户反馈",
                menuType = 1,
                menuUrl = "/PlatformVisit/UsrFeedback"
            });
            _MenuList.Add(21, new MenuList()
            {
                superiorMenu = "回访和反馈",
                menuId = 21,
                menuName = "历史回访",
                menuType = 1,
                menuUrl = "/PlatformVisit/VisitList"
            });
            _MenuList.Add(139, new MenuList()
            {
                superiorMenu = "回访和反馈",
                menuId = 139,
                menuName = "需求列表",
                menuType = 1,
                menuUrl = "/RequirementManage/Index"
            });
            #endregion

            #region 消息中心
            //_MenuList.Add(22, new MenuList()
            //{
            //    superiorMenu = "消息中心",
            //    menuId = 22,
            //    menuName = "PC消息管理",
            //    menuType = 1,
            //    menuUrl = "/MsgCenter/PCMessageList"
            //});
            //_MenuList.Add(23, new MenuList()
            //{
            //    superiorMenu = "消息中心",
            //    menuId = 23,
            //    menuName = "移动端消息管理",
            //    menuType = 1,
            //    menuUrl = "/MsgCenter/MobileMessageList"
            //});
            //_MenuList.Add(24, new MenuList()
            //{
            //    superiorMenu = "消息中心",
            //    menuId = 24,
            //    menuName = "推送消息",
            //    menuType = 1,
            //    menuUrl = "/MsgCenter/SendMessage"
            //});
            _MenuList.Add(135, new MenuList()
            {
                superiorMenu = "消息中心",
                menuId = 135,
                menuName = "发送消息",
                menuType = 1,
                menuUrl = "/MessageSending/Index"
            });
            _MenuList.Add(137, new MenuList()
            {
                superiorMenu = "消息中心",
                menuId = 137,
                menuName = "消息批次",
                menuType = 1,
                menuUrl = "/MessageSending/BatchSummary"
            });
            _MenuList.Add(136, new MenuList()
            {
                superiorMenu = "消息中心",
                menuId = 136,
                menuName = "消息详情",
                menuType = 1,
                menuUrl = "/MessageSending/BatchList"
            });
            #endregion

            #region 优惠券
            _MenuList.Add(25, new MenuList()
            {
                superiorMenu = "优惠券",
                menuId = 25,
                menuName = "优惠券信息",
                menuType = 1,
                menuUrl = "/OrderCoupon"
            });
            _MenuList.Add(26, new MenuList()
            {
                superiorMenu = "优惠券",
                menuId = 26,
                menuName = "新增优惠券",
                menuType = 1,
                menuUrl = "/OrderCoupon/CouponAdd"
            });
            #endregion

            #region 积分商城管理
            _MenuList.Add(27, new MenuList()
            {
                superiorMenu = "积分商城管理",
                menuId = 27,
                menuName = "兑换管理",
                menuType = 1,
                menuUrl = "/IntegralManage/IntegralStore"
            });
            _MenuList.Add(28, new MenuList()
            {
                superiorMenu = "积分商城管理",
                menuId = 28,
                menuName = "任务审核",
                menuType = 1,
                menuUrl = "/IntegralManage/TaskControls"
            });
            _MenuList.Add(123, new MenuList()
            {
                superiorMenu = "积分商城管理",
                menuId = 123,
                menuName = "积分分析",
                menuType = 1,
                menuUrl = "/IntegralManage/IntegralStat"
            });
            _MenuList.Add(124, new MenuList()
            {
                superiorMenu = "积分商城管理",
                menuId = 124,
                menuName = "积分统计",
                menuType = 1,
                menuUrl = "/IntegralManage/IntegralInput"
            });
            #endregion

            #region 知识管理系统
            _MenuList.Add(29, new MenuList()
            {
                superiorMenu = "知识管理系统",
                menuId = 29,
                menuName = "知识库",
                menuType = 1,
                menuUrl = "/QuestionLibrary/QuestionList"
            });
            #endregion

            #region 日检
            _MenuList.Add(30, new MenuList()
            {
                superiorMenu = "日检",
                menuId = 30,
                menuName = "当日任务",
                menuType = 1,
                menuUrl = "/DailyCheck"
            });
            _MenuList.Add(31, new MenuList()
            {
                superiorMenu = "日检",
                menuId = 31,
                menuName = "历史任务列表",
                menuType = 1,
                menuUrl = "/DailyCheck/CheckTaskList"
            });
            _MenuList.Add(32, new MenuList()
            {
                superiorMenu = "日检",
                menuId = 32,
                menuName = "新增日检任务",
                menuType = 1,
                menuUrl = "/DailyCheck/AddCheckItem"
            });
            #endregion

            #region 代理管理
            _MenuList.Add(33, new MenuList()
            {
                superiorMenu = "代理管理",
                menuId = 33,
                menuName = "代理商列表",
                menuType = 1,
                menuUrl = "/Agent/Index"
            });
            _MenuList.Add(34, new MenuList()
            {
                superiorMenu = "代理管理",
                menuId = 34,
                menuName = "新增代理商",
                menuType = 1,
                menuUrl = "/Agent/AddNewAgent"
            });
            #endregion

            #region 今日待完成事项
            _MenuList.Add(35, new MenuList()
            {
                superiorMenu = "今日待完成事项",
                menuId = 35,
                menuName = "回访信息",
                menuType = 1,
                menuUrl = "/PlatformVisit"
            });
            _MenuList.Add(36, new MenuList()
            {
                superiorMenu = "今日待完成事项",
                menuId = 36,
                menuName = "1天内未关闭",
                menuType = 1,
                menuUrl = "/PlatformVisit/VisitFigureOut"
            });
            _MenuList.Add(37, new MenuList()
            {
                superiorMenu = "今日待完成事项",
                menuId = 37,
                menuName = "超1天内未关闭",
                menuType = 1,
                menuUrl = "/PlatformVisit/VisitFigureOut?type=2"
            });
            _MenuList.Add(38, new MenuList()
            {
                superiorMenu = "今日待完成事项",
                menuId = 38,
                menuName = "已关闭事件",
                menuType = 1,
                menuUrl = "/PlatformVisit/VisitFigureOut?type=3"
            });
            #endregion

            #region 回访事件分析
            _MenuList.Add(39, new MenuList()
            {
                superiorMenu = "回访事件分析",
                menuId = 39,
                menuName = "回访事件分析",
                menuType = 1,
                menuUrl = "/PlatformVisit/CaseAnalyze"
            });
            #endregion

            #region 快速导航
            //_MenuList.Add(40, new MenuList()
            //{
            //    superiorMenu = "快速导航",
            //    menuId = 40,
            //    menuName = "快速导航",
            //    menuType = 1,
            //    menuUrl = "/FunctionMap/Index"
            //});
            #endregion


            #region 系统管理
            _MenuList.Add(501, new MenuList()
            {
                superiorMenu = "系统管理",
                menuId = 501,
                menuName = "账号管理",
                menuType = 1,
                menuUrl = "/SysManage/UserList/"
            });
            #endregion

            //快速导航

            #region 导航 基本数据
            _MenuList.Add(101, new MenuList()
            {
                superiorMenu = "基本数据",
                menuId = 101,
                menuName = "平台趋势",
                menuType = 2,
                menuUrl = "/Operation/Tendency/"
            });
            _MenuList.Add(102, new MenuList()
            {
                superiorMenu = "基本数据",
                menuId = 102,
                menuName = "用户来源",
                menuType = 2,
                menuUrl = "/Operation/RegisterSource"
            });
            _MenuList.Add(103, new MenuList()
            {
                superiorMenu = "基本数据",
                menuId = 103,
                menuName = "移动端数据",
                menuType = 2,
                menuUrl = "/Operation/SourceData"
            });
            _MenuList.Add(104, new MenuList()
            {
                superiorMenu = "基本数据",
                menuId = 104,
                menuName = "活跃率分析",
                menuType = 2,
                menuUrl = "/Operation/ActiveAnalysis"
            });
            _MenuList.Add(105, new MenuList()
            {
                superiorMenu = "基本数据",
                menuId = 105,
                menuName = "订单类型分析",
                menuType = 2,
                menuUrl = "/OrderAnalyze/Index"
            });
            _MenuList.Add(106, new MenuList()
            {
                superiorMenu = "基本数据",
                menuId = 106,
                menuName = "活跃店铺列表",
                menuType = 2,
                menuUrl = "/Operation/AccountActiveList"
            });
            _MenuList.Add(107, new MenuList()
            {
                superiorMenu = "基本数据",
                menuId = 107,
                menuName = "每日登录用户分析",
                menuType = 2,
                menuUrl = "/DailyAnalyze/Index"
            });
            _MenuList.Add(108, new MenuList()
            {
                superiorMenu = "基本数据",
                menuId = 108,
                menuName = "活跃用户变化分析",
                menuType = 2,
                menuUrl = "/UserRetention/ActiveStatus"
            });
            _MenuList.Add(109, new MenuList()
            {
                superiorMenu = "基本数据",
                menuId = 109,
                menuName = "推荐注册（转介绍）",
                menuType = 2,
                menuUrl = "/Recommend/Index"
            });
            #endregion

            #region 导航 核心运营
            _MenuList.Add(110, new MenuList()
            {
                superiorMenu = "核心运营",
                menuId = 110,
                menuName = "数据对比",
                menuType = 2,
                menuUrl = "/Operation/MonthComparison"
            });
            _MenuList.Add(111, new MenuList()
            {
                superiorMenu = "核心运营",
                menuId = 111,
                menuName = "转化率分析",
                menuType = 2,
                menuUrl = "/FunnelAnalyze/Index"
            });

            _MenuList.Add(140, new MenuList()
            {
                superiorMenu = "核心运营",
                menuId = 140,
                menuName = "筛选器（新）",
                menuType = 2,
                menuUrl = "/FiltrateData/FilterEx"
            });
            _MenuList.Add(145, new MenuList()
            {
                superiorMenu = "核心运营",
                menuId = 145,
                menuName = "运营分析报表",
                menuType = 2,
                menuUrl = "/OperationReport/Index"
            });
            #endregion

            #region 导航 用户留存
            _MenuList.Add(112, new MenuList()
            {
                superiorMenu = "用户留存",
                menuId = 112,
                menuName = "用户留存率分析",
                menuType = 2,
                menuUrl = "/UserRetention/Index"
            });
            _MenuList.Add(113, new MenuList()
            {
                superiorMenu = "用户留存",
                menuId = 113,
                menuName = "客服后登录留存分析",
                menuType = 2,
                menuUrl = "/CustomerCare/CareRetention/"
            });
            _MenuList.Add(130, new MenuList()
            {
                superiorMenu = "用户留存",
                menuId = 130,
                menuName = "用户留存优化测试",
                menuType = 2,
                menuUrl = "/UserRetention/MethodTest/"
            });
            
            #endregion

            #region 导航 订单
            _MenuList.Add(114, new MenuList()
            {
                superiorMenu = "订单",
                menuId = 114,
                menuName = "订单续费分析",
                menuType = 2,
                menuUrl = "/CustomerCare/OrderRenewal/"
            });
            _MenuList.Add(133, new MenuList()
            {
                superiorMenu = "订单",
                menuId = 133,
                menuName = "手机充值订单状态列表",
                menuType = 2,
                menuUrl = "/Order/MobileRecharge/"
            });
            #endregion

            #region 导航 客服/用户反馈
            _MenuList.Add(115, new MenuList()
            {
                superiorMenu = "客服/用户反馈",
                menuId = 115,
                menuName = "客服数据分析",
                menuType = 2,
                menuUrl = "/CustomerCare/Index/"
            });
            _MenuList.Add(116, new MenuList()
            {
                superiorMenu = "客服/用户反馈",
                menuId = 116,
                menuName = "客服用户比例分析",
                menuType = 2,
                menuUrl = "/CustomerCare/CarePartition/"
            });
            _MenuList.Add(117, new MenuList()
            {
                superiorMenu = "客服/用户反馈",
                menuId = 117,
                menuName = "客服短信审核分析",
                menuType = 2,
                menuUrl = "/CustomerCare/SmsCheckPerform/"
            });
            #endregion

            #region 导航 其他
            _MenuList.Add(118, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 118,
                menuName = "目标和汇总",
                menuType = 2,
                menuUrl = "/Home/MonthlyReview"
            });
            _MenuList.Add(119, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 119,
                menuName = "商品商场",
                menuType = 2,
                menuUrl = "/GoodsStore/index"
            });
            _MenuList.Add(120, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 120,
                menuName = "归属地分析",
                menuType = 2,
                menuUrl = "/AreaMap/DataDistributed"
            });
            _MenuList.Add(121, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 121,
                menuName = "交易地图",
                menuType = 2,
                menuUrl = "http://yunying.yuanbei.biz/AreaMap/SelectMap?type=sale"
            });
            _MenuList.Add(122, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 122,
                menuName = "推广管理",
                menuType = 2,
                menuUrl = "/Promote/Index"
            });
            _MenuList.Add(125, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 125,
                menuName = "用户扩展信息字典",
                menuType = 2,
                menuUrl = "/UserPortrait/Index"
            });
            _MenuList.Add(126, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 126,
                menuName = "标签管理",
                menuType = 2,
                menuUrl = "/TagManage/Index"
            });
            _MenuList.Add(127, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 127,
                menuName = "24小时信息分析",
                menuType = 2,
                menuUrl = "/RegTimeReport/Index"
            });
            _MenuList.Add(134, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 134,
                menuName = "Venn集合分析",
                menuType = 2,
                menuUrl = "/DataAnalyze/Index"
            });
            _MenuList.Add(141, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 141,
                menuName = "循环消息后台",
                menuType = 2,
                menuUrl = "/HangFire"
            });
            #endregion
            _MenuList.Add(128, new MenuList()
            {
                superiorMenu = "客服/用户反馈",
                menuId = 128,
                menuName = "提现相关",
                menuType = 2,
                menuUrl = "/CustomerCare/WithdrawalInfo"
            });
            _MenuList.Add(129, new MenuList()
            {
                superiorMenu = "订单",
                menuId = 129,
                menuName = "手机橱窗订单统计",
                menuType = 2,
                menuUrl = "/MobileShow/Index"
            });
            _MenuList.Add(132, new MenuList()
            {
                superiorMenu = "客服/用户反馈",
                menuId = 132,
                menuName = "内测用户管理",
                menuType = 2,
                menuUrl = "/AlphaApply/Index"
            });
            _MenuList.Add(131, new MenuList()
            {
                superiorMenu = "客服/用户反馈",
                menuId = 131,
                menuName = "用户商品评论管理",
                menuType = 2,
                menuUrl = "/Evaluation/Index"
            });

            _MenuList.Add(138, new MenuList()
            {
                superiorMenu = "客服/用户反馈",
                menuId = 138,
                menuName = "支付宝申请管理",
                menuType = 2,
                menuUrl = "/AlipayInfo/Index"
            });
            _MenuList.Add(142, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 142,
                menuName = "修改密码",
                menuType = 2,
                menuUrl = "/HomePage/NewPwd"
            });
            _MenuList.Add(143, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 143,
                menuName = "第三方收单统计",
                menuType = 2,
                menuUrl = "/ShopAlipay/Index"
            });
            _MenuList.Add(144, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 144,
                menuName = "动作触发消息模板设置",
                menuType = 2,
                menuUrl = "/MessageSending/TriggerTemplate"
            });
            _MenuList.Add(146, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 146,
                menuName = "细分注册渠道",
                menuType = 2,
                menuUrl = "/Operation/DetailSource"
            });
            _MenuList.Add(147, new MenuList()
            {
                superiorMenu = "其他",
                menuId = 147,
                menuName = "消息推送规则列表",
                menuType = 2,
                menuUrl = "/MessageSending/MessageRuleList"
            });
            
            return _MenuList;
        }
        /// <summary>
        /// 得到总菜单的ICON
        /// </summary>
        /// <param name="menuName"></param>
        /// <returns></returns>
        public string GetIco(string menuName)
        {
            switch (menuName)
            {
                case "店铺管理":
                    return "pg-home";
                case "运营分析":
                    return "fa-line-chart";
                case "数据筛选":
                    return "fa-filter";
                case "订单管理":
                    return "fa-file-text";
                case "短信":
                    return "pg-social";
                case "回访和反馈":
                    return "fa-phone";
                case "消息中心":
                    return "fa-info";
                case "优惠券":
                    return "fa-ticket";
                case "积分商城管理":
                    return "fa-ticket";
                case "知识管理系统":
                    return "fa-phone";
                case "日检":
                    return "pg-calender";
                case "代理管理":
                    return "fa-map-marker";
                case "今日待完成事项":
                    return "fa-file-text";
                case "回访事件分析":
                    return "fa-file-text";
                case "快速导航":
                    return "fa-location-arrow";
                default:
                    return "fa-file-text";
            }
        }
    }


    public class UserMenu
    {
        /// <summary>
        /// 左边菜单
        /// </summary>
        public List<UserMenuItem> LeftMenu { get; set; }
        /// <summary>
        /// 菜单地图
        /// </summary>
        public List<UserMenuItem> FunctionMap { get; set; }
    }
    public class UserMenuItem
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string MenuTitle { get; set; }
        /// <summary>
        /// 菜单连接
        /// </summary>
        public string MenuUrl { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<UserMenuItem> ItemList { get; set; }
    } 
}
