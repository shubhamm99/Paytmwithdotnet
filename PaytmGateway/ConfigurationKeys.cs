using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaytmGateway
{
    public static class ConfigurationKeys
    {
        public static string MerchantId = System.Configuration.ConfigurationManager.AppSettings["MerchantId"];

        public static string MerchantKey = System.Configuration.ConfigurationManager.AppSettings["MerchantKey"];

        public static string Website = System.Configuration.ConfigurationManager.AppSettings["Website"];

        public static string IndustryType = System.Configuration.ConfigurationManager.AppSettings["IndustryType"];

        public static string ChannelId = System.Configuration.ConfigurationManager.AppSettings["ChannelId"];

    }
}