using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Akakce.API.Interfaces;

namespace Akakce.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IAkakceCrawler akakceCrawler;
        public ProductController(IAkakceCrawler akakceCrawler)
        {
            this.akakceCrawler = akakceCrawler;
        }

        [HttpGet]
        [Route("search/{query}")]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query)) return BadRequest();

            var products = await this.akakceCrawler.Search(query);

            if (products.Count == 0) return NotFound();

            return Ok(products);
        }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> Get([FromBody]string url)
        {
            if (string.IsNullOrEmpty(url)) return BadRequest();

            var productDetail = await this.akakceCrawler.GetDetail(url);

            if (productDetail.Count == 0) return NotFound();

            return Ok(productDetail);
        }
    }
}
