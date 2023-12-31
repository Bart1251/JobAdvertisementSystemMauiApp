using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertisementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOfferController : Controller
    {
        private readonly IUserOfferRepository userOfferRepository;
        private readonly IUserRepository userRepository;
        private readonly IOfferRepository offerRepository;

        public UserOfferController(IUserOfferRepository userOfferRepository, IUserRepository userRepository, IOfferRepository offerRepository)
        {
            this.userOfferRepository = userOfferRepository;
            this.userRepository = userRepository;
            this.offerRepository = offerRepository;
        }

        [HttpPost("{userId}/{offerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserOffer(int userId, int offerId)
        {
            var user = userRepository.GetUsers().Where(e => e.Id == userId).FirstOrDefault();
            var offer = offerRepository.GetOffers().Where(e => e.Id == offerId).FirstOrDefault();

            if (user == null || offer == null)
                return NotFound();

            UserOffer userOffer = new UserOffer()
            {
                UserId = userId,
                OfferId = offerId,
                User = user,
                Offer = offer
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!userOfferRepository.CreateUserOffer(userOffer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{userId}/{offerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserOffer(int userId, int offerId)
        {
            var userOffer = userOfferRepository.GetUserOffers().Where(e => e.UserId == userId && e.OfferId == offerId).FirstOrDefault();

            if (userOffer == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!userOfferRepository.DeleteUserOffer(userOffer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
