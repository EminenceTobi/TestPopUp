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
    public class PaymentLinkController : Controller
    {
        private readonly ILogger<PaymentLinkController> _logger;
        private readonly IConfiguration _config;

        public PaymentLinkController(ILogger<PaymentLinkController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        public IActionResult PaymentLink()
        {
            return View();
        }
        public IActionResult Testing()
        {
            return View();
        }
    }
}