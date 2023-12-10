using JobAdvertisementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertisementApp.Services
{
    public class JobExperienceApiService : ApiServiceBase<JobExperience>
    {
        public JobExperienceApiService(HttpClient httpClient) : base(httpClient, "api/JobExperience")
        {

        }
    }
}
