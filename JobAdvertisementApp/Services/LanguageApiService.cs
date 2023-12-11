using JobAdvertisementApp.Models;
using System.Text;
using System.Text.Json;

namespace JobAdvertisementApp.Services
{
    public class LanguageApiService : ApiServiceBase<Language>
    {
        public LanguageApiService(HttpClient httpClient) : base(httpClient, "api/Language")
        {

        }

        public async Task<bool> UserAddLanguageAsync(string userId, string languageId)
        {
            HttpResponseMessage response = await httpClient.PostAsync(baseAddress + "/api/UserLanguage/" + userId + "/" + languageId, new StringContent(""));

            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<bool> UserDeleteLanguageAsync(string userId, string languageId)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(baseAddress + "/api/UserLanguage/" + userId + "/" + languageId);

            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
