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

        public async Task<User> GetUserByEmailAsync(string email)
        {
            User result = null;

            HttpResponseMessage response = await httpClient.GetAsync(url + "/" + email);

            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<User>(content, jsonSerialzierOptions);
            }

            return result;
        }
    }
}
