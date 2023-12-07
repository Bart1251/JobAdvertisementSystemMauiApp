using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface ILanguageRepository
    {
        IEnumerable<Language> GetUserLanguages(int userId);
        IEnumerable<Language> GetLanguages();
        bool CreateLanguage(Language language);
        bool UpdateLanguage(Language language);
        bool DeleteLanguage(Language language);
        bool Save();
    }
}
