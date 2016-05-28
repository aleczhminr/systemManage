using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Model;

namespace Controls.IndustryFilter
{
    public static class IndustryFilter
    {
        public static int FilterIndustry()
        {
            //更新基表店铺Id
            //IndustryFilterBLL.UpdateIndustryAccount();
            //用现有行业表更新用户
            //IndustryFilterBLL.UpdateFormerShopType();
            //用扩展信息更新表
            //IndustryFilterBLL.UpdateIndustryByExtendinfo();

            #region 使用关键词进行清洗操作
            //获取清洗字典
            List<IndustryFilterDic> dicList = IndustryFilterBLL.GetFilterDic();
            //获取店铺Id和店铺名
            List<ShopNamePair> shopList = IndustryFilterBLL.GetAccIdPair();

            foreach (var dicItem in dicList)
            {
                if (!string.IsNullOrEmpty(dicItem.FilterWord))
                {
                    List<string> dicFilter = dicItem.FilterWord.Split(',').ToList();

                    //区分是否有特殊关键词的例外处理
                    if (string.IsNullOrEmpty(dicItem.SpecFilter))
                    {
                        foreach (var shopItem in shopList)
                        {
                            
                            foreach (var strItem in dicFilter)
                            {
                                if (shopItem.CompanyName.Contains(strItem))
                                {
                                    ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var shopItem in shopList)
                        {
                            foreach (var strItem in dicFilter)
                            {
                                //例外处理
                                switch (dicItem.Industry_2)
                                {
                                    case "服装鞋帽/箱包皮具":
                                        if (shopItem.CompanyName.Contains(strItem) ||
                                            shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 1, 1) == "包")
                                        {
                                            if (shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 1, 1) == "包")
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, "包");
                                            }
                                            else
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                            }
                                        }
                                        break;
                                    case "珠宝/饰品/文玩":
                                        if (shopItem.CompanyName.Contains(strItem) ||
                                            shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "黄金")
                                        {
                                            if (shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "黄金")
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, "黄金");
                                            }
                                            else
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                            }
                                        }
                                        break;
                                    case "运动户外":
                                        if (shopItem.CompanyName.Contains(strItem) && !shopItem.CompanyName.Contains("体育彩票"))
                                            ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                        break;
                                    case "烟酒茶行":
                                        if (shopItem.CompanyName.Contains(strItem) && !shopItem.CompanyName.Contains("烟花"))
                                            ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                        break;
                                    case "眼镜店":
                                        if (shopItem.CompanyName.Contains(strItem) ||
                                            shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "眼睛")
                                        {
                                            if (shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "眼睛")
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, "眼睛");
                                            }
                                            else
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                            }
                                        }
                                        break;
                                    case "图书/音像":
                                        if (shopItem.CompanyName.Contains(strItem) ||
                                            shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "书城")
                                        {
                                            if (shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "书城")
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, "书城");
                                            }
                                            else
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                            }
                                        }
                                        break;
                                    case "美容":
                                        if (shopItem.CompanyName.Contains(strItem) && !shopItem.CompanyName.Contains("美容品"))
                                            ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                        break;
                                    case "宾馆酒店":
                                        if (shopItem.CompanyName.Contains(strItem) && !shopItem.CompanyName.Contains("酒店用品"))
                                            ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                        break;
                                    case "网吧":
                                        if (shopItem.CompanyName.Contains(strItem) ||
                                            shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "网络")
                                        {
                                            if (shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "网络")
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, "网络");
                                            }
                                            else
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                            }
                                        }
                                        break;
                                    case "桌游棋牌":
                                        if (shopItem.CompanyName.Contains(strItem) && !shopItem.CompanyName.Contains("麻将机"))
                                            ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                        break;
                                    case "公园景点":
                                        if (shopItem.CompanyName.Contains(strItem) ||
                                            shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "景区")
                                        {
                                            if (shopItem.CompanyName.Substring(shopItem.CompanyName.Length - 2, 2) == "景区")
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, "景区");
                                            }
                                            else
                                            {
                                                ProcessFilter(dicItem.Industry_1, dicItem.Industry_2, shopItem.AccId, strItem);
                                            }
                                        }
                                        break;
                                }
                            }

                        }

                    }
                }
                                
            }

            #endregion

            return 1;

        }

        /// <summary>
        /// 更新方法
        /// </summary>
        /// <param name="Industry_1"></param>
        /// <param name="Industry_2"></param>
        /// <param name="AccId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static int ProcessFilter(string Industry_1, string Industry_2, int AccId, string keyword)
        {
            ShopExtIndustry tempModel = new ShopExtIndustry();
            tempModel.AccId = AccId;
            tempModel.Industry_1 = Industry_1;
            tempModel.Industry_2 = Industry_2;
            //更新行业清洗基表
            IndustryFilterBLL.FilterBaseIndustry(tempModel);

            //添加更新日志
            IndustryFilterLog logModel = new IndustryFilterLog();
            logModel.AccId = AccId;
            logModel.Keyword = keyword;
            logModel.NowIndustry = Industry_1 + "-" + Industry_2;
            logModel.UpdateTime = DateTime.Now;

            IndustryFilterBLL.UpdateIndustryFilterLog(logModel);
            IndustryFilterBLL.UpdateFilterStatus(AccId);

            return 1;
        }
    }


}
