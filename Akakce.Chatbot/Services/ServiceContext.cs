using Akakce.Chatbot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Akakce.Chatbot.Services
{
    public class ServiceContext
    {
        #region Signleton Instance

        private static ServiceContext _instance;
        public static ServiceContext Instance
        {
            get => (_instance == null) ? _instance = new ServiceContext() : _instance;
        }

        #endregion

        private HttpClient _client;
        public HttpClient Client
        {
            get => (_client == null) ? _client = new HttpClient() : _client;
            set => _client = value;
        }

        public ServiceContext()
        {
            Client.BaseAddress = new Uri(SettingsHelper.APIEndpoint);
        }

        public async Task<List<ProductModel>> Search(string query)
        {
            var response = await Client.GetAsync($"/api/product/search/{query}");
            if (response.IsSuccessStatusCode)
                return await response.Content?.ReadAsAsync<List<ProductModel>>();
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new List<ProductModel>();
            else
                return null;
        }

        public async Task<List<ProductDetailModel>> Get(string url)
        {
            var response = await Client.PostAsync<String>($"/api/product/get", url, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            if (response.IsSuccessStatusCode)
                return await response.Content?.ReadAsAsync<List<ProductDetailModel>>();
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new List<ProductDetailModel>();
            else
                return null;
        }
    }
}