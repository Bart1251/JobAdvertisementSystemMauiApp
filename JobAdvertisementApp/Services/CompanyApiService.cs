using JobAdvertisementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertisementApp.Services
{
    public class CompanyApiService : ApiServiceBase<Company>
    {
        public CompanyApiService(HttpClient httpClient) : base(httpClient, "api/Company")
        {

        }
    }
}
