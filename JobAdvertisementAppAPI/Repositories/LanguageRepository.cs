using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAdvertisementAppAPI.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly DataContext dataContext;

        public LanguageRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateLanguage(Language language)
        {
            dataContext.Language.Add(language);
            return Save();
        }

        public bool DeleteLanguage(Language language)
        {
            dataContext.Language.Remove(language);
            return Save();
        }

        public IEnumerable<Language> GetLanguages()
        {
            return dataContext.Language.ToList();
        }

        public IEnumerable<Language> GetUserLanguages(int userId)
        {
            return dataContext.User.Where(u => u.Id == userId).SelectMany(u => u.UserLanguages).Select(ul => ul.Language).ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateLanguage(Language language)
        {
            dataContext.Language.Update(language);
            return Save();
        }
    }
}
