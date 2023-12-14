using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingShiftController : Controller
    {
        private readonly IWorkingShiftRepository workingShiftRepository;
        private readonly IMapper mapper;

        public WorkingShiftController(IWorkingShiftRepository workingShiftRepository, IMapper mapper)
        {
            this.workingShiftRepository = workingShiftRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WorkingShiftDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetWorkingShifts()
        {
            var workingShifts = mapper.Map<IEnumerable<WorkingShiftDto>>(workingShiftRepository.GetWorkingShifts());

            if (workingShifts.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(workingShifts);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWorkingShift([FromBody] WorkingShiftDto workingShift)
        {
            WorkingShift workingShiftMap = mapper.Map<WorkingShift>(workingShift);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!workingShiftRepository.CreateWorkingShift(workingShiftMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{workingShiftId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWorkingShift(int workingShiftId)
        {
            var workingShift = workingShiftRepository.GetWorkingShifts().Where(e => e.Id == workingShiftId).First();

            if (workingShift == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!workingShiftRepository.DeleteWorkingShift(workingShift))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
