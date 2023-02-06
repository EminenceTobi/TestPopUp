using CommsPayDeveloperWeb.BLL;
using CommsPayDeveloperWeb.BLL.Redis;
using CommsPayDeveloperWeb.Presentation.Payload;
using CommsPayDeveloperWeb.Presentation.Response;
using CommsPayDeveloperWeb.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPush;

namespace CommsPayDeveloperWeb.Controllers
{
    [SessionTimeout]
    [UpdateTimeout]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IConfiguration _config;
        private readonly IRedisCache _redis;

        public DashboardController(ILogger<DashboardController> logger, IConfiguration configuration, IRedisCache redis)
        {
            _logger = logger;
            _config = configuration;
            _redis = redis;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Subscribe([FromBody] SubscribePayload payload)
        {
            var client = HttpContext.Session.GetString("email");

            var sub = _redis.GetCache($"{client}-sub").Result;
            var subscription = new PushSubscription(payload.Endpoint, payload.P256dh, payload.Auth);
            var newSub = JsonConvert.SerializeObject(subscription);
            if (sub == newSub) return Json(new { status = true, messagce = "client already added" });

            var response = _redis.SetCache($"{client}-sub", newSub).Result;
            return Json(new { status = true, message = "client added" });
        }

        public IActionResult _AdminDashboard()
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).TranAPICall("transaction/get-dashboard", "Get", null, "", "Bearer", token).Result;

            var objresp = resp == null ? null : JsonConvert.DeserializeObject<TransactionAppResponse<AdminDashboardResponse>>(resp);

            return PartialView(objresp);
        }

        public IActionResult _ManagerDashboard()
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).TranAPICall("transaction/get-dashboard", "Get", null, "", "Bearer", token).Result;

            var objresp = resp == null ? null : JsonConvert.DeserializeObject<TransactionAppResponse<ManagerDashboardResponse>>(resp);

            return PartialView(objresp);
        }

        public IActionResult _PaypointDashboard()
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).TranAPICall("transaction/get-dashboard", "Get", null, "", "Bearer", token).Result;

            var objresp = resp == null ? null : JsonConvert.DeserializeObject<TransactionAppResponse<PaypointDashboardResponse>>(resp);

            return PartialView(objresp);
        }
    }
}