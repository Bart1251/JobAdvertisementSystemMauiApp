using JobAdvertisementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobAdvertisementApp.Services
{
    public class OfferApiService : ApiServiceBase<Offer>
    {
        public OfferApiService(HttpClient httpClient) : base(httpClient, "api/Offer")
        {

        }

        public virtual async Task<IEnumerable<Offer>> GetFiletredAsync(string categoryId, string jobLevelId, string typeOfContractId, string jobTypeId, string workingShiftId, string position = "", string maxDistance = "", string location = "")
        {
            IEnumerable<Offer> result = new List<Offer>();

            HttpResponseMessage response = await httpClient.GetAsync($"{url}?categoryId={categoryId}&jobLevelId={jobLevelId}&typeOfContractId={typeOfContractId}&jobTypeId={jobTypeId}&workingShiftId={workingShiftId}&position={position}&maxDistance={maxDistance}&location={location}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<List<Offer>>(content, jsonSerialzierOptions);
            }

            return result;
        }
    }
}
