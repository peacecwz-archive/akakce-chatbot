using Akakce.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akakce.API.Models;
using System.Net.Http;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.Text;

namespace Akakce.API.Crawlers
{
    public class AkakceCrawler : IAkakceCrawler
    {
        public AkakceCrawler()
        {
            handler = new HttpClientHandler()
            {
                CookieContainer = (cookies != null) ? cookies : cookies = new CookieContainer(),
                UseCookies = true
            };
            Client = new HttpClient(handler);
            Client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36");
            Doc = new HtmlDocument();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private HttpClientHandler handler;
        private CookieContainer cookies;

        public HttpClient Client { get; set; }
        public HtmlDocument Doc { get; set; }

        public async Task<List<ProductDetailModel>> GetDetail(string url)
        {
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string html = "";
                var stream = await response.Content?.ReadAsStreamAsync();
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("windows-1254")))
                    html = reader.ReadToEnd();
                Doc.LoadHtml(html);
                List<ProductDetailModel> productDetails = new List<ProductDetailModel>();
                var sellers = Doc.GetElementbyId("PL")?.SelectNodes(".//li");
                foreach (var seller in sellers)
                {
                    ProductDetailModel productDetail = new ProductDetailModel();
                    productDetail.Name = WebUtility.HtmlDecode(seller.SelectSingleNode(".//b")?.InnerText);
                    productDetail.Price = WebUtility.HtmlDecode(seller.SelectSingleNode(".//span[@class='pt_v8']")?.InnerText);
                    productDetail.Seller = WebUtility.HtmlDecode(seller.SelectSingleNode(".//span[@class='v_v8']")?.InnerText?.Replace("Satıcı:", ""));
                    productDetail.Seller = (productDetail.Seller.Split('/').Count() > 0) ? productDetail.Seller.Split('/')[0] : productDetail.Seller;
                    productDetail.Url = WebUtility.HtmlDecode(seller.SelectSingleNode(".//a[@class='iC']").GetAttributeValue("href", ""));
                    productDetail.Url = WebUtility.HtmlDecode("http://www.akakce.com/c" + productDetail.Url.Substring(productDetail.Url.IndexOf("/?c")));
                    productDetail.ImageUrl = WebUtility.HtmlDecode("http:" + seller.SelectSingleNode(".//img").GetAttributeValue("style", "").Replace("background-image:url(", "").Replace(")", ""));

                    productDetails.Add(productDetail);
                }
                return productDetails;
            }
            return new List<ProductDetailModel>();
        }

        public async Task<List<ProductModel>> Search(string query)
        {
            var response = await Client.GetAsync($"http://www.akakce.com/arama/?q={WebUtility.HtmlEncode(query)}");
            if (response.IsSuccessStatusCode)
            {
                string html = "";
                var stream = await response.Content?.ReadAsStreamAsync();
                using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("windows-1254")))
                    html = reader.ReadToEnd();
                Doc.LoadHtml(html);
                List<ProductModel> crawledProducts = new List<ProductModel>();

                var products = Doc.GetElementbyId("APL").SelectNodes(".//li").Where(x => !string.IsNullOrEmpty(x.InnerText) && (x.InnerText.IndexOf("Fiyatları Gör") > -1 | x.InnerText.IndexOf("Satıcıya Git") > -1)).ToList();
                foreach (var product in products)
                {
                    try
                    {
                        ProductModel crawledProduct = new ProductModel();
                        var link = product.SelectNodes(".//a")[0];

                        crawledProduct.Name = WebUtility.HtmlDecode(product.SelectSingleNode(".//b[@class='pn_v8']").InnerText);
                        crawledProduct.ProductUrl = WebUtility.HtmlDecode("http://www.akakce.com" + link.GetAttributeValue("href", ""));
                        var img = product.SelectNodes(".//img")[0];
                        string imageUrl = img.GetAttributeValue("style", "").Replace("background-image:url(", "").Replace(")", "");
                        if (!string.IsNullOrEmpty(imageUrl))
                            crawledProduct.ImageUrl = WebUtility.HtmlDecode("http:" + imageUrl);
                        else
                        {
                            imageUrl = img.GetAttributeValue("data-src", "");
                            crawledProduct.ImageUrl = WebUtility.HtmlDecode("http:" + imageUrl);
                        }
                        crawledProducts.Add(crawledProduct);
                    }
                    catch
                    {

                    }
                }
                return crawledProducts;
            }
            return new List<ProductModel>();
        }
    }
}
