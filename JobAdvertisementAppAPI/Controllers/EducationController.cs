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
    public class EducationController : Controller
    {
        private readonly IEducationRepository educationRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public EducationController(IEducationRepository educationRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.educationRepository = educationRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EducationDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserEducations(int userId)
        {
            var educations = mapper.Map<IEnumerable<EducationDto>>(educationRepository.GetUserEducations(userId));

            if (educations.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(educations);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEducation(int userId, [FromBody] EducationDto education)
        {
            var user = userRepository.GetUsers().Where(e => e.Id == userId).First();

            if (user == null)
                return NotFound();

            Education educationMap = mapper.Map<Education>(education);
            educationMap.User = user;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!educationRepository.CreateEducation(educationMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{educationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProfile(int educationId)
        {
            var education = educationRepository.GetEducations().Where(e => e.Id == educationId).First();

            if (education == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!educationRepository.DeleteEducation(education))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
