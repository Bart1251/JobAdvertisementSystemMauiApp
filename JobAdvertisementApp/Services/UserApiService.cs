using JobAdvertisementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobAdvertisementApp.Services
{
    public class UserApiService : ApiServiceBase<User>
    {
        public UserApiService(HttpClient httpClient) : base(httpClient, "api/User")
        {

        }
    }
}
