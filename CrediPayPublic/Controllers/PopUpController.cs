using CrediPayPublic.Models.BLL;
using CrediPayPublic.Models.BLL.Redis;
using CrediPayPublic.Views.PaymentLink.Responses;
using CrediPayPublic.Views.PopUp.Request;
using CrediPayPublic.Views.PopUp.Response;
using CrediPayPublic.Views.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CrediPayPublic.Controllers
{
    public class PopUpController : Controller
    {
        private readonly ILogger<PopUpController> _logger;
        private readonly IConfiguration _config;

        public PopUpController(ILogger<PopUpController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        public IActionResult PopUp()
        {
            return View();
        }
        [HttpPost]
        public JsonResult InitiatePayment([FromBody] InitiatePaymentRequest request)
        {
            var key = HttpContext.Session.GetString("publickey");
            var resp = new ConsumeService(_config).APICall("public/initiate-payment", "Post", request, "", request.Key, "").Result;
            var objresp = resp == null ? new AppResponse<InitiatePaymentResponse> { IsSuccess = false, Message = "Something went wrong, try again later" } : JsonConvert.DeserializeObject<AppResponse<InitiatePaymentResponse>>(resp);
            return Json(objresp);
        }
        [HttpPost]
        public JsonResult CompletePayment([FromBody] CompletePaymentRequest request)
        {
             var key = HttpContext.Session.GetString("publickey");
            var resp = new ConsumeService(_config).APICall("public/complete-payment", "Post", request, "", key, "").Result;
            var objresp = resp == null ? new AppResponse<string> { IsSuccess = false, Message = "Something went wrong, try again later" } : JsonConvert.DeserializeObject<AppResponse<string>>(resp);
            return Json(objresp);
        }
        [HttpPost]
        public JsonResult ValidatePayment([FromBody] ValidatePaymentRequest request)
        {
            var azurebloburl = _config.GetSection("azureblobeurl").Value;
            AppResponse<string> response = new AppResponse<string>();
            var resp = new ConsumeService(_config).APICall("public/validate-payment-popup", "Post", request, "", request.Key, "").Result;
            var objresp = resp == null ? new AppResponse<long> { IsSuccess = false, Message = "Something went wrong, try again later" } : JsonConvert.DeserializeObject<AppResponse<long>>(resp);
            if (objresp.IsSuccess)
            {
                HttpContext.Session.SetString("publickey", "pk_test_fb24197ac74d4006aa23f326d090b6eb");
                var logo = $"{azurebloburl}/business_{objresp.Value}_logo.png";
                response = new AppResponse<string> { IsSuccess = true, Message = objresp.Message, Value = logo };
            }
            else
            {
                HttpContext.Session.SetString("publickey", "pk_test_fb24197ac74d4006aa23f326d090b6eb");
                response = new AppResponse<string> { IsSuccess = false, Message = objresp.Message, Value = "/app-assets/images/credipay.png" };
            }

            return Json(response);
        }
    }
}