using CommsPayDeveloperWeb.BLL;
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

namespace CommsPayDeveloperWeb.Controllers
{
    [SessionTimeout]
    public class BusinessController : Controller
    {
        private readonly ILogger<BusinessController> _logger;
        private readonly IConfiguration _config;

        public BusinessController(ILogger<BusinessController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        public IActionResult Profile()
        {
            var token = HttpContext.Session.GetString("token");
            ViewBag.IsUpdated = HttpContext.Session.GetString("disbursementid");
            var resp = new ConsumeService(_config).APICall("business/get-business-info", "Get", null, "", "Bearer", token).Result;

            var objresp = resp == null ? null : JsonConvert.DeserializeObject<AppResponse<BusinessProfileResponse>>(resp);

            return View(objresp);
        }

        public IActionResult _UpdateBusinessPartial()
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).APICall("business/get-business-info-edit", "Get", null, "", "Bearer", token).Result;

            var objresp = resp == null ? null : JsonConvert.DeserializeObject<AppResponse<BusinessProfileEditResponse>>(resp);
            return PartialView(objresp);
        }

        public IActionResult _StateSelectPartial(int id)
        {
            ViewBag.Id = id.ToString();
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).APICall($"state/get-state-by-country-id/{id}", "Get", null, "", "Bearer", token).Result;

            var objresp = resp == null ? null : JsonConvert.DeserializeObject<MerchantResponseWithList<StateResponse>>(resp);
            return PartialView(objresp);
        }

        public IActionResult _CategorySelectPartial(int id)
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).APICall($"businesscategory/get-category/{id}", "Get", null, "", "Bearer", token).Result;

            var objresp = resp == null ? null : JsonConvert.DeserializeObject<MerchantResponseWithList<BusinessCategoryResp>>(resp);
            return PartialView(objresp);
        }

        public IActionResult _CountrySelectPartial()
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).APICall("country/get-all-countries", "Get", null, "", "Bearer", token).Result;

            var objresp = resp == null ? null : JsonConvert.DeserializeObject<MerchantResponseWithList<CountryResponse>>(resp);
            return PartialView(objresp);
        }

        public IActionResult _DocumentTypeSelectPartial()
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).APICall("businessdocument/get-document-types", "Get", null, "", "Bearer", token).Result;

            var objresp = resp == null ? null : JsonConvert.DeserializeObject<MerchantResponseWithList<DocumentTypeResponse>>(resp);
            return PartialView(objresp);
        }

        [HttpPost]
        public JsonResult UpdateBusinessProfile([FromBody] EditBusinessPayload payload)
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).APICall("business/update-business-profile", "Post", payload, "", "Bearer", token).Result;
            var objresp = resp == null ? new AppResponse<string> { Status = false, Message = "Something went wrong, try again later" } : JsonConvert.DeserializeObject<AppResponse<string>>(resp);
            HttpContext.Session.SetString("disbursementid", objresp.Data);
            return Json(objresp);
        }

        [HttpPost]
        public JsonResult UpdateBusinessContact([FromBody] EditBusinessPayload payload)
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).APICall("business/update-business-contact", "Post", payload, "", "Bearer", token).Result;
            var objresp = resp == null ? new AppResponse<string> { Status = false, Message = "Something went wrong, try again later" } : JsonConvert.DeserializeObject<AppResponse<string>>(resp);
            HttpContext.Session.SetString("disbursementid", objresp.Data);
            return Json(objresp);
        }

        [HttpPost]
        public JsonResult UploadNewDocument([FromBody] CreateBusinessDocumentPayload payload)
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).APICall("businessdocument/updload-business-document", "Post", payload, "", "Bearer", token).Result;
            var objresp = resp == null ? new AppResponse<string> { Status = false, Message = "Something went wrong, try again later" } : JsonConvert.DeserializeObject<AppResponse<string>>(resp);
            return Json(objresp);
        }

        [HttpPost]
        public JsonResult ReuploadDocument([FromBody] EditBusinessDocumentPayload payload)
        {
            var token = HttpContext.Session.GetString("token");
            var resp = new ConsumeService(_config).APICall("businessdocument/reupload-business-document", "Post", payload, "", "Bearer", token).Result;
            var objresp = resp == null ? new AppResponse<string> { Status = false, Message = "Something went wrong, try again later" } : JsonConvert.DeserializeObject<AppResponse<string>>(resp);
            return Json(objresp);
        }
    }
}