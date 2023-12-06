
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> GetUserProfiles(int userId);
        IEnumerable<Profile> GetProfiles();
        bool CreateProfile(Profile profile);
        bool DeleteProfile(Profile profile);
        bool Save();
    }
}
