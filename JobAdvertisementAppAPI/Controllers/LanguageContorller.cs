using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : Controller
    {
        private readonly ILanguageRepository languageRepository;
        private readonly IMapper mapper;

        public LanguageController(ILanguageRepository languageRepository, IMapper mapper)
        {
            this.languageRepository = languageRepository;
            this.mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LanguageDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserLanguages(int userId)
        {
            var languages = mapper.Map<IEnumerable<LanguageDto>>(languageRepository.GetUserLanguages(userId));

            if (languages.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(languages);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLanguage([FromBody] LanguageDto language)
        {
            Language languageMap = mapper.Map<Language>(language);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!languageRepository.CreateLanguage(languageMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{languageId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLanguage(int languageId)
        {
            var language = languageRepository.GetLanguages().Where(e => e.Id == languageId).First();

            if (language == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!languageRepository.DeleteLanguage(language))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
