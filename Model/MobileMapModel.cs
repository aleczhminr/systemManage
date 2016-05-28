using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MobileMapModel
    {
        public int id { get; set; }
        public string gName { get; set; }
        public int gid { get; set; }
        public int accid { get; set; }
        public string accountName { get; set; }
        public string maxClass { get; set; }
        public string minClass { get; set; }
        public int goodsNum { get; set; }
        public decimal price { get; set; }
        public int accountid { get; set; }
        public string TrueConty { get; set; }
        public string picUrl { get; set; }
        public string CompanyAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ShortUrl { get; set; }
        
    }

    public class GpsModel
    {
        public int AccId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }

    public class ShopLocationModel
    {
        public int AccId { get; set; }
        public string CompanyName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string PhoneNumber { get; set; }
        public int ActiveStatus { get; set; }
        public string UserRealName { get; set; }

        public string CompanyAddress { get; set; }
    }
}
