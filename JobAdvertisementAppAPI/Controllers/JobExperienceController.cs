using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobExperienceController : Controller
    {
        private readonly IJobExperienceRepository jobExperienceRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public JobExperienceController(IJobExperienceRepository jobExperienceRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.jobExperienceRepository = jobExperienceRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<JobExpirienceDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserJobExperiences(int userId)
        {
            var jobExperiences = mapper.Map<IEnumerable<JobExpirienceDto>>(jobExperienceRepository.GetUserJobExperiences(userId));

            if (jobExperiences.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(jobExperiences);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateJobExperiences(int userId, [FromBody] JobExpirienceDto jobExperience)
        {
            var user = userRepository.GetUsers().Where(e => e.Id == userId).FirstOrDefault();

            if (user == null)
                return NotFound();

            JobExperience jobExperienceMap = mapper.Map<JobExperience>(jobExperience);
            jobExperienceMap.User = user;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!jobExperienceRepository.CreateJobExperience(jobExperienceMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{jobExperienceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProfile(int jobExperienceId)
        {
            var jobExperience = jobExperienceRepository.GetJobExperiences().Where(e => e.Id == jobExperienceId).FirstOrDefault();

            if (jobExperience == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!jobExperienceRepository.DeleteJobExperience(jobExperience))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
