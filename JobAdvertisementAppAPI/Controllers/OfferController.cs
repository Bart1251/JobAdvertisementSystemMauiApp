using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetOffers()
        {
            var offers = offerRepository.GetOffers();

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
