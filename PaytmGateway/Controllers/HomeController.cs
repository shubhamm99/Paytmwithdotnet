using paytm;
using PaytmGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PaytmGateway.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PaymentRequestViewModel model = new PaymentRequestViewModel();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> MakePaymentRequest(PaymentRequestViewModel model)
        {
            try
            {
                var callbackUrl = "http://localhost:63531/Home/GetPaytmResponse";
                /* initialize a TreeMap object */
                Dictionary<string, string> paytmParams = new Dictionary<string, string>
                {
                    // Merchant Id
                    { PaytmParamKeys.MId, ConfigurationKeys.MerchantId},

                    // Website
                    { PaytmParamKeys.WEBSITE, ConfigurationKeys.Website },

                    // Industry Type Id
                    { PaytmParamKeys.INDUSTRY_TYPE_ID, ConfigurationKeys.IndustryType },

                    // Chanel Id
                    { PaytmParamKeys.CHANNEL_ID, ConfigurationKeys.ChannelId },

                    // Order id generated from website
                    { PaytmParamKeys.ORDER_ID, Guid.NewGuid().ToString() },

                    // Customer Id generated from Website
                    { PaytmParamKeys.CUST_ID, Guid.NewGuid().ToString() },

                    // From model
                    { PaytmParamKeys.MOBILE_NO, model.PhoneNumber.Trim() },

                    // From model
                    { PaytmParamKeys.EMAIL, model.Email.Trim() },

                    // From model
                    { PaytmParamKeys.TXN_AMOUNT, model.Amount.ToString() },

                    // Response will be sent to this url
                    { PaytmParamKeys.CALLBACK_URL, callbackUrl}
                };

                string checksum = CheckSum.generateCheckSum(ConfigurationKeys.MerchantKey, paytmParams);//merchant Key
                /* Prepare HTML Form and Submit to Paytm */
                string outputHtml = "";
                outputHtml += "<html>";
                outputHtml += "<head>";
                outputHtml += "<title>Merchant Checkout Page</title>";
                outputHtml += "</head>";
                outputHtml += "<body>";
                outputHtml += "<center><h1>Please do not refresh this page...</h1></center>";
                outputHtml += "<form method='post' action='" + PaytmUrl.StagingUrl + "' name='paytm_form'>";
                foreach (string key in paytmParams.Keys)
                {
                    outputHtml += "<input type='hidden' name='" + key + "' value='" + paytmParams[key] + "'>";
                }
                outputHtml += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
                outputHtml += "</form>";
                outputHtml += "<script type='text/javascript'>";
                outputHtml += "document.paytm_form.submit();";
                outputHtml += "</script>";
                outputHtml += "</body>";
                outputHtml += "</html>";

                ViewBag.HtmlData = outputHtml;
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public async Task<ActionResult> GetPaytmResponse(PaymentResponseViewModel model)
        {
            try
            {
                string paytmChecksum = "";
                Dictionary<string, string> paytmParams = new Dictionary<string, string>();
                foreach (string key in Request.Form.Keys)
                {
                    if (key.Equals("CHECKSUMHASH"))
                    {
                        paytmChecksum = Request.Form[key];
                    }
                    else
                    {
                        paytmParams.Add(key.Trim(), Request.Form[key].Trim());
                    }
                }

                bool isValidChecksum = CheckSum.verifyCheckSum(ConfigurationKeys.MerchantKey, paytmParams, paytmChecksum);
                if (isValidChecksum)
                {
                    if (model.RESPCODE == "01")
                    {
                        ViewBag.Message = "Payment Successfull";
                    }
                    else
                    {
                        ViewBag.Message = "Payment Failed";
                        ViewBag.ErrorDetail = model.RESPMSG;
                    }
                }
                else
                {
                    ViewBag.Message = "Checksum Mismatched";
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }
    }
}