using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using JobAdvertisementAppAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;

        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            this.companyRepository = companyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CompanyDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanies()
        {
            var companies = mapper.Map<IEnumerable<CompanyDto>>(companyRepository.GetCompanies());

            if (companies.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        [ProducesResponseType(200, Type = typeof(CompanyDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCompany(int companyId)
        {
            var company = mapper.Map<CompanyDto>(companyRepository.GetCompany(companyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompany([FromBody] CompanyDto company)
        {
            Company companyMap = mapper.Map<Company>(company);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!companyRepository.CreateCompany(companyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCompany(int companyId)
        {
            var company = companyRepository.GetCompanies().Where(e => e.Id == companyId).First();

            if (company == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!companyRepository.DeleteCompany(company))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost("images/{companyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SaveUserProfileImage(string companyId)
        {
            var company = companyRepository.GetCompany(int.Parse(companyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (company == null)
                return NotFound();

            try
            {
                byte[] imageBytes = null;

                using (MemoryStream ms = new MemoryStream())
                {
                    Request.Body.CopyToAsync(ms);
                    imageBytes = ms.ToArray();
                }

                string filePath = Path.Combine("UploadedImages/Companies", companyId + ".png");
                System.IO.File.WriteAllBytes(filePath, imageBytes);

                return Ok();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet("images/{companyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyProfileImage(string companyId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string filePath = Path.Combine("UploadedImages/Companies", companyId + ".png");

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }

                byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

                return File(imageBytes, "image/png");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
        }
    }
}
