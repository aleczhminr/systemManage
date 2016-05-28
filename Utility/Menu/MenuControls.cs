using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Menu
{
    /// <summary>
    /// 菜单处理程序
    /// </summary>
    public class MenuControls
    {
        /// <summary>
        /// 得到菜单
        /// </summary>
        /// <param name="menuIdS"></param>
        public UserMenu GetMenu(int[] menuIdS)
        {
            MenuDataList mDL = new MenuDataList();
            Dictionary<int, MenuList> AllMenu = mDL.GetAllList();

            Dictionary<string, UserMenuItem> LeftMenu = new Dictionary<string, UserMenuItem>();
            Dictionary<string, UserMenuItem> FunctionMap = new Dictionary<string, UserMenuItem>();

            foreach (int menuId in menuIdS)
            {
                try
                {
                    MenuList item = AllMenu[menuId];

                    UserMenuItem umI = new UserMenuItem();
                    umI.MenuTitle = item.menuName;
                    umI.MenuUrl = item.menuUrl;

                    if (item.menuType == 1)
                    {
                        if (!LeftMenu.ContainsKey(item.superiorMenu))
                        {
                            UserMenuItem superiorMenu = new UserMenuItem();
                            superiorMenu.ItemList = new List<UserMenuItem>();
                            superiorMenu.MenuTitle = item.superiorMenu;
                            superiorMenu.MenuUrl = "javascript:void(0)";
                            superiorMenu.MenuIcon = mDL.GetIco(item.superiorMenu);
                            LeftMenu[item.superiorMenu] = superiorMenu;
                        }
                        LeftMenu[item.superiorMenu].ItemList.Add(umI);
                    }
                    else
                    {
                        if (!FunctionMap.ContainsKey(item.superiorMenu))
                        {
                            UserMenuItem superiorMenu = new UserMenuItem();
                            superiorMenu.ItemList = new List<UserMenuItem>();
                            superiorMenu.MenuTitle = item.superiorMenu;
                            superiorMenu.MenuUrl = "javascript:void(0)";
                            superiorMenu.MenuIcon = mDL.GetIco(item.superiorMenu);
                            FunctionMap[item.superiorMenu] = superiorMenu;
                        }
                        FunctionMap[item.superiorMenu].ItemList.Add(umI);
                    }
                }
                catch (Exception ex)
                {

                    continue;
                }
                

            }

            UserMenu um = new UserMenu();
            um.LeftMenu = LeftMenu.Values.ToList();
            um.FunctionMap = FunctionMap.Values.ToList();
            return um;
        }

        /// <summary>
        /// 得到模板信息
        /// </summary>
        /// <param name="deparId"></param>
        /// <returns></returns>
        public List<int> GetDepartmentMenuId(int deparId)
        {
            List<int> menuIdS = new List<int>();
            switch (deparId)
            {
                case 0://管理员
                    menuIdS.AddRange(new int[] { 
                        1, 2, //店铺管理
                        3, 4, 5, 6, 7,//运营分析
                        8,//数据筛选
                        9,10,11,12,//订单管理
                        13,14,15,16,17,18,//短信
                        19,20,21,//回访和反馈
                        22,23,24,//消息中心
                        25,26,//优惠券
                        27,28,//积分商城管理
                        29,//知识管理系统
                        30,31,32,//日检
                        33,34,//代理管理
                        35,36,37,38,//今日待完成事项
                        39, //回访事件分析

                        101,102,103,104,105,106,107,108,109,//导航 基本数据
                        110,111,//导航 核心运营
                        112,113,//导航 用户留存
                        114,//导航 订单
                        115,116,117,// 导航 客服/用户反馈
                        118,119,120,121,122//导航 其他
                    });
                    break;
                case 2://客服
                    menuIdS.AddRange(new int[] {
                        1, 2, //店铺管理
                        35,36,37,38,//今日待完成事项
                        9,10,11,12,//订单管理
                        13,14,15,16,17,18,//短信
                        22,23,24,//消息中心
                        25,26,//优惠券
                        8,//数据筛选
                        30,31,32,//日检
                        27,28,//积分商城管理
                        29,//知识管理系统
                        39 //回访事件分析
                    });
                    break;
                case 4://运营
                    menuIdS.AddRange(new int[] { 
                        1, 2, //店铺管理
                        3, 4, 5, 6, 7,//运营分析
                        8,//数据筛选
                        9,10,11,12,//订单管理
                        13,14,15,16,17,18,//短信
                        19,20,21,//回访和反馈
                        22,23,24,//消息中心
                        25,26,//优惠券
                        30,31,32,//日检
                        33,34//代理管理
                    });
                    break;
                case 5://普通客服（新人）
                    menuIdS.AddRange(new int[] { 
                        1, 2, //店铺管理
                        3, 4, 5, 6, 7//运营分析
                    });
                    break;
                case 6://客服管理人员
                    menuIdS.AddRange(new int[] {
                        1, 2, //店铺管理
                        35,36,37,38,//今日待完成事项
                        9,10,11,12,//订单管理
                        3, 4, 5, 6, 7,//运营分析
                        8,//数据筛选
                        13,14,15,16,17,18,//短信
                        19,20,21,//回访和反馈
                        22,23,24,//消息中心
                        25,26,//优惠券
                        30,31,32,//日检
                        27,28,//积分商城管理
                        29,//知识管理系统
                        39 //回访事件分析
                    });
                    break;
                case 7://普通运营（新人）
                    menuIdS.AddRange(new int[] { 
                        1, 2, //店铺管理
                        3, 4, 5, 6, 7,//运营分析
                        8//数据筛选
                    });
                    break;
                case 8://客服运营
                    menuIdS.AddRange(new int[] { 
                        1, 2, //店铺管理
                        35,36,37,38,//今日待完成事项
                        9,10,//订单管理
                        13,14,15,16,17,18,//短信
                        19,20,21,//回访和反馈
                        22,23,24,//消息中心
                        25,26,//优惠券
                        8,//数据筛选
                        30,31,32,//日检
                        27,28,//积分商城管理
                        29,//知识管理系统
                        39 //回访事件分析
                    });
                    break;
                case 9://临时权限
                    menuIdS.AddRange(new int[] { 
                        1//店铺管理
                    });
                    break;
                case 50://特殊权限
                    menuIdS.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 501, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122 });
                    break;
                default://默认权限
                    menuIdS.AddRange(new int[] {
                        1, //店铺管理
                        19,20,21,//回访和反馈
                        13,14,15,16,17,18,//短信
                    });
                    break;
            }
            return menuIdS;
        }
    }
}
