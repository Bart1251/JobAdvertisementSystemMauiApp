using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfContractController : Controller
    {
        private readonly ITypeOfContractRepository typeOfContractRepository;
        private readonly IMapper mapper;

        public TypeOfContractController(ITypeOfContractRepository typeOfContractRepository, IMapper mapper)
        {
            this.typeOfContractRepository = typeOfContractRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TypeOfContractDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTypeOfContracts()
        {
            var typeOfContracts = mapper.Map<IEnumerable<TypeOfContractDto>>(typeOfContractRepository.GetTypeOfContracts());

            if (typeOfContracts.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(typeOfContracts);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTypeOfContract([FromBody] TypeOfContractDto typeOfContract)
        {
            TypeOfContract typeOfContractMap = mapper.Map<TypeOfContract>(typeOfContract);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!typeOfContractRepository.CreateTypeOfContract(typeOfContractMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{typeOfContractId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTypeOfContract(int typeOfContractId)
        {
            var typeOfContract = typeOfContractRepository.GetTypeOfContracts().Where(e => e.Id == typeOfContractId).First();

            if (typeOfContract == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!typeOfContractRepository.DeleteTypeOfContract(typeOfContract))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
