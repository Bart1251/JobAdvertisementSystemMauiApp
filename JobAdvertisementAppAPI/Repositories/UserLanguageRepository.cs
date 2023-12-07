using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class UserLanguageRepository : IUserLanguageRepository
    {
        private readonly DataContext dataContext;

        public UserLanguageRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateUserLanguage(UserLanguage userLanguage)
        {
            dataContext.UserLanguage.Add(userLanguage);
            return Save();
        }

        public bool DeleteUserLanguage(UserLanguage userLanguage)
        {
            dataContext.UserLanguage.Remove(userLanguage);
            return Save();
        }

        public IEnumerable<UserLanguage> GetLanguages()
        {
            return dataContext.UserLanguage.ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }
    }
}
