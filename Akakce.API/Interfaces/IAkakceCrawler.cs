using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akakce.API.Interfaces
{
    public interface IAkakceCrawler
    {
        Task<List<Models.ProductModel>> Search(string query);
        Task<List<Models.ProductDetailModel>> GetDetail(string url);
    }
}
