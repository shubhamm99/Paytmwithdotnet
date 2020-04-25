using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaytmGateway
{
    public static class PaytmParamKeys
    {
        public static string MId = "MID";

        public static string WEBSITE = "WEBSITE";

        public static string INDUSTRY_TYPE_ID = "INDUSTRY_TYPE_ID";

        public static string CHANNEL_ID = "CHANNEL_ID";
        public static string ORDER_ID = "ORDER_ID";
        public static string CUST_ID = "CUST_ID";
        public static string MOBILE_NO = "MOBILE_NO";
        public static string EMAIL = "EMAIL";
        public static string TXN_AMOUNT = "TXN_AMOUNT";
        public static string CALLBACK_URL = "CALLBACK_URL";
    }

    public static class PaytmUrl
    {
        public static string StagingUrl = "https://securegw-stage.paytm.in/order/process";
        public static string ProductionUrl = "https://securegw.paytm.in/order/process";
    }

    public static class PathLocation
    {
        public static string PaytmTemplate = "/Content/Templates/PaytmCheckOutPage.html";
    }

    public static class ResponseCodes
    {

    }
}