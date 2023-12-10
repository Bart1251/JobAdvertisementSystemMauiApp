using JobAdvertisementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertisementApp.Services
{
    public class EducationApiService : ApiServiceBase<Education>
    {
        public EducationApiService(HttpClient httpClient) : base(httpClient, "api/Education")
        {

        }
    }
}
