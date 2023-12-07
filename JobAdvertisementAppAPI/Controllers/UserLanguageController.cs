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
    public class UserLanguageController : Controller
    {
        private readonly IUserLanguageRepository userLanguageRepository;
        private readonly IUserRepository userRepository;
        private readonly ILanguageRepository languageRepository;

        public UserLanguageController(IUserLanguageRepository userLanguageRepository, IUserRepository userRepository, ILanguageRepository languageRepository)
        {
            this.userLanguageRepository = userLanguageRepository;
            this.userRepository = userRepository;
            this.languageRepository = languageRepository;
        }

        [HttpPost("{userId}/{languageId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLanguage(int userId, int languageId)
        {
            var user = userRepository.GetUsers().Where(e => e.Id == userId).First();
            var language = languageRepository.GetLanguages().Where(e => e.Id == languageId).First();

            if (user == null || language == null)
                return NotFound();

            UserLanguage userLanguage = new UserLanguage()
            {
                UserId = userId,
                LanguageId = languageId,
                User = user,
                Language = language
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!userLanguageRepository.CreateUserLanguage(userLanguage))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{userId}/{language}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLanguage(int userId, int languageId)
        {
            var userLanguage = userLanguageRepository.GetLanguages().Where(e => e.UserId == userId && e.LanguageId == languageId).First();

            if (userLanguage == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!userLanguageRepository.DeleteUserLanguage(userLanguage))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
