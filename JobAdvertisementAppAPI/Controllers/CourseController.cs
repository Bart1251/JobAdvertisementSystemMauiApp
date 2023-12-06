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
    public class CourseController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CourseController(ICourseRepository courseRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CourseDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserCourses(int userId)
        {
            var courses = mapper.Map<IEnumerable<CourseDto>>(courseRepository.GetUserCourses(userId));

            if (courses.Count() == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(courses);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCourse(int userId, [FromBody] CourseDto course)
        {
            var user = userRepository.GetUsers().Where(e => e.Id == userId).First();

            if (user == null)
                return NotFound();

            Course courseMap = mapper.Map<Course>(course);
            courseMap.User = user;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!courseRepository.CreateCourse(courseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{courseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProfile(int courseId)
        {
            var course = courseRepository.GetCourses().Where(e => e.Id == courseId).First();

            if (course == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!courseRepository.DeleteCourse(course))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
