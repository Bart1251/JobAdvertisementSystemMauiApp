using AutoMapper;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.DistanceMatrix.Request;
using GoogleMapsApi.Entities.DistanceMatrix.Response;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Google.Maps;
using Newtonsoft.Json.Linq;
using System.Net;

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
        public IActionResult GetOffers(int? categoryId = null, int? jobLevelId = null, int? typeOfContractId = null, int? jobTypeId = null, int? workingShiftId = null, string? position = null)
        {
            var offers = offerRepository.GetNotExpierdOffers();
            if (position != null)
                offers = offers.Where(e => e.Position.ToLower().Contains(position.ToLower()));
            if (categoryId != null)
                offers = offers.Where(e => e.Category.Id == categoryId);
            if (jobLevelId != null)
                offers = offers.Where(e => e.JobLevel.Id == jobLevelId);
            if (typeOfContractId != null)
                offers = offers.Where(e => e.TypeOfContract.Id == typeOfContractId);
            if (jobTypeId != null)
                offers = offers.Where(e => e.JobType.Id == jobTypeId);
            if (workingShiftId != null)
                offers = offers.Where(e => e.WorkingShift.Id == workingShiftId);

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
