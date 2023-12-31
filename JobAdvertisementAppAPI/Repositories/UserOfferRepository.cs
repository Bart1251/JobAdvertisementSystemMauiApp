using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class UserOfferRepository : IUserOfferRepository
    {
        private readonly DataContext dataContext;

        public UserOfferRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateUserOffer(UserOffer userOffer)
        {
            dataContext.UserOffer.Add(userOffer);
            return Save();
        }

        public bool DeleteUserOffer(UserOffer userOffer)
        {
            dataContext.UserOffer.Remove(userOffer);
            return Save();
        }

        public IEnumerable<UserOffer> GetUserOffers()
        {
            return dataContext.UserOffer.ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }
    }
}
