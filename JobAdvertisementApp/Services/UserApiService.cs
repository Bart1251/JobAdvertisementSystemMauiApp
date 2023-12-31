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

        public async Task<bool> UserAddOfferAsync(string userId, string offerId)
        {
            HttpResponseMessage response = await httpClient.PostAsync(baseAddress + "/api/UserOffer/" + userId + "/" + offerId, new StringContent(""));

            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<bool> UserDeleteOfferAsync(string userId, string offerId)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(baseAddress + "/api/UserOffer/" + userId + "/" + offerId);

            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
