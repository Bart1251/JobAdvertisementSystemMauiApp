using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IUserOfferRepository
    {
        IEnumerable<UserOffer> GetUserOffers();
        bool CreateUserOffer(UserOffer userOffer);
        bool DeleteUserOffer(UserOffer userOffer);
        bool Save();
    }
}
