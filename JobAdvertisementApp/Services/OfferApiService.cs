using JobAdvertisementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertisementApp.Services
{
    public class OfferApiService : ApiServiceBase<Offer>
    {
        public OfferApiService(HttpClient httpClient) : base(httpClient, "api/Offer")
        {

        }
    }
}
