using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Models;
using JobAdvertisementAppAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Profile = JobAdvertisementAppAPI.Models.Profile;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository profileRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public ProfileController(IProfileRepository profileRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.profileRepository = profileRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type=typeof(IEnumerable<ProfileDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserProfiles(int userId)
        {
            var profiles = mapper.Map<IEnumerable<ProfileDto>>(profileRepository.GetUserProfiles(userId).ToList());

            if(profiles.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profiles);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProfile(int userId, [FromBody] ProfileDto profile)
        {
            var user = userRepository.GetUsers().Where(e => e.Id == userId).First();

            if (user == null)
                return NotFound();

            Profile profileMap = mapper.Map<Profile>(profile);
            profileMap.User = user;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!profileRepository.CreateProfile(profileMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{profileId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProfile(int profileId)
        {
            var profile = profileRepository.GetProfiles().Where(e => e.Id == profileId).First();

            if (profile == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!profileRepository.DeleteProfile(profile))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
