using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Acme.News_Website.UI.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        public ApiService(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }
        public async Task<string> GetApi(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _config.GetSection("RedirectUrl:ApiUrl").Value +url);
            //request.Headers.Add("Accept", "application/json");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return responseString;
            }else
            {
                throw new Exception("Invalid request url !");
            }
        }
        public async Task<string> PostApi(object body, string url)
        {
            string json = JsonConvert.SerializeObject(body);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsync(_config.GetSection("RedirectUrl:ApiUrl").Value + url,requestBody);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return responseString;
            }
            else
            {
                throw new Exception("Invalid request url !");
            }
        }
        public async Task<string> UrlFriendly(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(" ","-");
        }
    }
}
