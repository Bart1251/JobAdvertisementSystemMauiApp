using JobAdvertisementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertisementApp.Services
{
    public class WorkingShiftApiService : ApiServiceBase<WorkingShift>
    {
        public WorkingShiftApiService(HttpClient httpClient) : base(httpClient, "api/WorkingShift")
        {

        }
    }
}
