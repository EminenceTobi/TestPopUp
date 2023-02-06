using CrediPayPublic.Views.Shared.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CrediPayPublic.Models.BLL
{
    public class ConsumeService
    {
        private readonly IConfiguration _config;

        public ConsumeService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<string> APICall(string method, string type, object data, string qsparam, string header, string baseurl = "")
        {
            try

            {
#if DEBUG
                baseurl = "http://localhost:8889/api/v1/";
                //baseurl = _config.GetSection("baseurl").Value;

#else
baseurl=_config.GetSection("baseurl").Value;
#endif

                using (var bankservice = new HttpClient())
                {
                    bankservice.BaseAddress = new Uri(baseurl);
                    bankservice.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    bankservice.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(header);
                    bankservice.Timeout = new TimeSpan(0, 0, 1, 0);
                    bankservice.DefaultRequestHeaders.Add("APIKey", header);
                    var json = JsonConvert.SerializeObject(data);
                    var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var resp = type == "Post" ? bankservice.PostAsync(baseurl + method + qsparam, stringContent).Result : bankservice.GetAsync(baseurl + method + qsparam).Result;
                    var resp2 = await resp.Content.ReadAsStringAsync();
                    if (resp.IsSuccessStatusCode)
                    {
                        //  response = JsonConvert.DeserializeObject<List<HomeModel>>(resp2);
                        return resp2;
                    }
                    else if (resp2.Contains("Invalid authorization header"))
                    {
                        return JsonConvert.SerializeObject(new AppResponse<string> { IsSuccess = false, Message = "Invalid Username or password" });
                    }
                    else if (resp2.Contains("Account is Inactive"))
                    {
                        return JsonConvert.SerializeObject(new AppResponse<string> { IsSuccess = false, Message = "Your account is yet to be activated. Kindly check your email for the activation link." });
                    }
                    return null;
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)e.Response;
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return null;
                    }

                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}