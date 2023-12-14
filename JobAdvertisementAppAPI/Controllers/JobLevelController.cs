using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobLevelController : Controller
    {
        private readonly IJobLevelRepository jobLevelRepository;
        private readonly IMapper mapper;

        public JobLevelController(IJobLevelRepository jobLevelRepository, IMapper mapper)
        {
            this.jobLevelRepository = jobLevelRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<JobLevelDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetJobLevels()
        {
            var jobLevels = mapper.Map<IEnumerable<JobLevelDto>>(jobLevelRepository.GetJobLevels());

            if (jobLevels.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(jobLevels);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateJobLevel([FromBody] JobLevelDto jobLevel)
        {
            JobLevel jobLevelMap = mapper.Map<JobLevel>(jobLevel);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!jobLevelRepository.CreateJobLevel(jobLevelMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{jobLevelId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteJobLevel(int jobLevelId)
        {
            var jobLevel = jobLevelRepository.GetJobLevels().Where(e => e.Id == jobLevelId).First();

            if (jobLevel == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!jobLevelRepository.DeleteJobLevel(jobLevel))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
