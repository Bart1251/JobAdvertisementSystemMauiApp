using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext dataContext;

        public ProfileRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateProfile(Profile profile)
        {
            dataContext.Profile.Add(profile);
            return Save();
        }

        public bool DeleteProfile(Profile profile)
        {
            dataContext.Profile.Remove(profile);
            return Save();
        }

        public IEnumerable<Profile> GetUserProfiles(int userId)
        {
            return dataContext.Profile.Where(e => e.User.Id == userId);
        }

        public IEnumerable<Profile> GetProfiles()
        {
            return dataContext.Profile.ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }
    }
}
