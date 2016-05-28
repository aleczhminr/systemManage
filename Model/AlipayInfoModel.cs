using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    #region AlipayInfo 申请支付宝信息
    public class AlipayInfoModel
    {

        /// <summary>
        /// alipay信息ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public int accId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string accountName { get; set; }

        /// <summary>
        /// 状态  
        ///0或不存在	初始化界面，文字编辑界面
        ///1	       	图片信息编辑界面
        ///2			等待客服审核文字和图片信息界面
        ///3			确认条码收单订单界面
        ///4			等待客服审核是否点击确认条码收单
        ///5			客服审核成功， 耐心等待4到5天
        ///6			Key和Pid码输入
        ///7			等待界面
        ///8			成功界面 
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string statusDes { get; set; }

        /// <summary>
        /// 商家客户类别
        /// 公司是1
        /// 个人是2
        /// </summary>
        public int companyType { get; set; }

        /// <summary>
        ///  申请时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        ///  修改时间
        /// </summary>
        public DateTime modifiedTime { get; set; }

        /// <summary>
        ///  完成时间
        /// </summary>
        public DateTime completeTime { get; set; }

        /// <summary>
        /// 法人姓名
        /// </summary>
        public string artificialPerson { get; set; }

        /// <summary>
        ///  联系电话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        ///  支付宝账号
        /// </summary>
        public string alipayAccount { get; set; }

        /// <summary>
        ///  商户名称
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        ///   行业类别
        /// </summary>
        public string industryClassification { get; set; }

        /// <summary>
        ///  营业面积
        /// </summary>
        public string businessArea { get; set; }

        /// <summary>
        ///  店外全景图片地址
        /// </summary>
        public string shopOutPhotoURL { get; set; }

        /// <summary>
        ///  店内照片图片地址
        /// </summary>
        public string shopInPhotoURL { get; set; }

        /// <summary>
        ///  营业执照图片地址
        /// </summary>
        public string businessLicensePhotoURL { get; set; }

        /// <summary>
        ///  门牌号图片地址
        /// </summary>
        public string doorPhotoURL { get; set; }

        /// <summary>
        ///  工作照片1
        /// </summary>
        public string firstShopWorkPhotoURL { get; set; }

        /// <summary>
        /// 工作照片2
        /// </summary>
        public string secondShopWorkPhotoURL { get; set; }

        /// <summary>
        /// 工作照片3
        /// </summary>
        public string thirdShopWorkPhotoURL { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string companyAddress { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string LgUserIp { get; set; }

        /// <summary>
        /// 登录ID
        /// </summary>
        public string LgUserId { get; set; }

        /// <summary>
        /// pid码
        /// </summary>
        public string PID { get; set; }

        /// <summary>
        /// key码
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 克服反馈提示
        /// </summary>
        public string remark { get; set; }
    }

    #endregion

    #region AlipayInfo 申请支付宝信息
    public class T_AlipayInfoLogModel
    {

        /// <summary>
        /// id
        /// </summary>		
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// accId
        /// </summary>		
        public int accId
        {
            get;
            set;
        }
        /// <summary>
        /// createTime
        /// </summary>		
        public DateTime createTime
        {
            get;
            set;
        }
        /// <summary>
        /// columnName
        /// </summary>		
        public string columnName
        {
            get;
            set;
        }
        /// <summary>
        /// oldValue
        /// </summary>		
        public string oldValue
        {
            get;
            set;
        }
        /// <summary>
        /// nowValue
        /// </summary>		
        public string nowValue
        {
            get;
            set;
        }
        /// <summary>
        /// lgUserId
        /// </summary>		
        public string lgUserId
        {
            get;
            set;
        }
        /// <summary>
        /// lgUserIp
        /// </summary>		
        public string lgUserIp
        {
            get;
            set;
        }

    }

    #endregion
}
