using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IUserLanguageRepository
    {
        IEnumerable<UserLanguage> GetLanguages();
        bool CreateUserLanguage(UserLanguage userLanguage);
        bool DeleteUserLanguage(UserLanguage userLanguage);
        bool Save();
    }
}
