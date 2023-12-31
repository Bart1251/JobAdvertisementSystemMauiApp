using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using JobAdvertisementAppAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace JobAdvertisementAppAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : Controller
    {
        private readonly IOfferRepository offerRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IJobLevelRepository jobLevelRepository;
        private readonly ITypeOfContractRepository typeOfContractRepository;
        private readonly IJobTypeRepository jobTypeRepository;
        private readonly IWorkingShiftRepository workingShiftRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public OfferController(IOfferRepository offerRepository, ICompanyRepository companyRepository, IJobLevelRepository jobLevelRepository, ITypeOfContractRepository typeOfContractRepository, IJobTypeRepository jobTypeRepository, IWorkingShiftRepository workingShiftRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.offerRepository = offerRepository;
            this.companyRepository = companyRepository;
            this.jobLevelRepository = jobLevelRepository;
            this.typeOfContractRepository = typeOfContractRepository;
            this.jobTypeRepository = jobTypeRepository;
            this.workingShiftRepository = workingShiftRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Offer>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOffers(int? categoryId = null, int? jobLevelId = null, int? typeOfContractId = null, int? jobTypeId = null, int? workingShiftId = null, string? position = null, int? maxDistance = null, string? location = null)
        {
            List<Offer> offers = (List<Offer>)offerRepository.GetNotExpierdOffers();
            if (position != null)
                offers = offers.Where(e => e.Position.ToLower().Contains(position.ToLower())).ToList();
            if (categoryId != null)
                offers = offers.Where(e => e.Category.Id == categoryId).ToList();
            if (jobLevelId != null)
                offers = offers.Where(e => e.JobLevel.Id == jobLevelId).ToList();
            if (typeOfContractId != null)
                offers = offers.Where(e => e.TypeOfContract.Id == typeOfContractId).ToList();
            if (jobTypeId != null)
                offers = offers.Where(e => e.JobType.Id == jobTypeId).ToList();
            if (workingShiftId != null)
                offers = offers.Where(e => e.WorkingShift.Id == workingShiftId).ToList();

            if(maxDistance != null && location != null)
            {
                for (int i = 0; i < offers.Count();)
                {
                    if (string.IsNullOrEmpty(offers[i].Company.Location))
                    {
                        offers.RemoveAt(i);
                        continue;
                    }
                    try
                    {
                        HttpClient client = new HttpClient();

                        string url = $"http://dev.virtualearth.net/REST/v1/Locations?q={Uri.EscapeDataString(location)}&key=AsUeck_Ez--mXKxU6JpA3KZmTmvAuMDmEfYZuQ6gpeE0wmcFOynbPUnxHkk2Waqn";
                        var response = await client.GetStringAsync(url);
                        var json = JObject.Parse(response);
                        var coordinates = json["resourceSets"][0]["resources"][0]["point"]["coordinates"];
                        double latitude = (double)coordinates.First;
                        double longitude = (double)coordinates.Last;

                        double otherLatitude = double.Parse(offers[i].Company.Location.Split(";").FirstOrDefault().Replace(".", ","));
                        double otherLongitude = double.Parse(offers[i].Company.Location.Split(";").Last().Replace(".", ","));

                        double R = 6371; // Promień Ziemi w km
                        double dLat = (otherLatitude - latitude) * (Math.PI / 180);
                        double dLon = (otherLongitude - longitude) * (Math.PI / 180);
                        latitude *= (Math.PI / 180);
                        otherLatitude *= (Math.PI / 180);

                        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(latitude) * Math.Cos(otherLatitude);
                        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                        double distance = R * c;

                        if (distance > maxDistance)
                            offers.RemoveAt(i);
                        else
                            i++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\nException Caught!");
                        Console.WriteLine($"Message :{e.Message}");
                    }
                }
            }
            
            if (offers.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(offers);
        }

        [HttpGet("{offerId}")]
        [ProducesResponseType(200, Type = typeof(Offer))]
        [ProducesResponseType(400)]
        public IActionResult GetOffer(int offerId)
        {
            var offer = offerRepository.GetOffer(offerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (offer == null)
                return NotFound();

            return Ok(offer);
        }

        [HttpGet("User/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Offer>))]
        [ProducesResponseType(400)]
        public IActionResult GetOffersUserApplied(int userId)
        {
            var offers = offerRepository.GetOffersUserApplied(userId);

            if (offers.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(offers);
        }

        [HttpGet("Company/{companyId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Offer>))]
        [ProducesResponseType(400)]
        public IActionResult GetOffersFromCompany(int companyId)
        {
            var offers = offerRepository.GetOffersFromCompany(companyId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (offers == null)
                return NotFound();

            return Ok(offers);
        }

        [HttpGet("Count")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetOffersCount()
        {
            int offersCount = offerRepository.GetNotExpiredOffersCount();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(offersCount);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOffer([FromBody] OfferDto offer, int companyId, int jobLevelId, int typeOfContractId, int jobTypeId, int workingShiftId, int categoryId)
        {
            Offer offerMap = mapper.Map<Offer>(offer);
            offerMap.Company = companyRepository.GetCompany(companyId);
            offerMap.JobLevel = jobLevelRepository.GetJobLevel(jobLevelId);
            offerMap.TypeOfContract = typeOfContractRepository.GetTypeOfContract(typeOfContractId);
            offerMap.JobType = jobTypeRepository.GetJobType(jobTypeId);
            offerMap.WorkingShift = workingShiftRepository.GetWorkingShift(workingShiftId);
            offerMap.Category = categoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!offerRepository.CreateOffer(offerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{offerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOffer(int offerId)
        {
            var offer = offerRepository.GetOffers().Where(e => e.Id == offerId).FirstOrDefault();

            if (offer == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!offerRepository.DeleteOffer(offer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
