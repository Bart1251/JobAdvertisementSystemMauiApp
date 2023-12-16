using AutoMapper;
using JobAdvertisementAppAPI.Dto;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = mapper.Map<IEnumerable<UserDto>>(userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpPost("images/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SaveUserProfileImage(string userId)
        {
            var user = userRepository.GetUser(int.Parse(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user == null)
                return NotFound();

            try
            {
                byte[] imageBytes = null;

                using (MemoryStream ms = new MemoryStream())
                {
                    Request.Body.CopyToAsync(ms);
                    imageBytes = ms.ToArray();
                }

                string filePath = Path.Combine("UploadedImages/Users", userId + ".png");
                System.IO.File.WriteAllBytes(filePath, imageBytes);

                return Ok();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet("images/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetUserProfileImage(string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string filePath = Path.Combine("UploadedImages/Users", userId + ".png");

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }

                byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

                return File(imageBytes, "image/png");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet("{email}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string email)
        {
            var user = mapper.Map<UserDto>(userRepository.GetUser(email));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            if (user == null)
                return BadRequest(ModelState);

            List<User> users = userRepository.GetUsers().Where(e => e.Email == user.Email).ToList();

            if(users.Count > 0)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = mapper.Map<User>(user);

            if(!userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody]UserDto user)
        {
            if (user == null)
                return BadRequest(ModelState);

            if (user.Id != userId)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = mapper.Map<User>(user);

            if (!userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
