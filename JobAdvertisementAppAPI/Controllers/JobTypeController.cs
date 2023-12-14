using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : Controller
    {
        private readonly IJobTypeRepository jobTypeRepository;
        private readonly IMapper mapper;

        public JobTypeController(IJobTypeRepository jobTypeRepository, IMapper mapper)
        {
            this.jobTypeRepository = jobTypeRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<JobTypeDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetJobTypes()
        {
            var jobTypes = mapper.Map<IEnumerable<JobTypeDto>>(jobTypeRepository.GetJobTypes());

            if (jobTypes.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(jobTypes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateJobType([FromBody] JobTypeDto jobType)
        {
            JobType jobTypeMap = mapper.Map<JobType>(jobType);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!jobTypeRepository.CreateJobType(jobTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{jobTypeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteJobType(int jobTypeId)
        {
            var jobType = jobTypeRepository.GetJobTypes().Where(e => e.Id == jobTypeId).First();

            if (jobType == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!jobTypeRepository.DeleteJobType(jobType))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
