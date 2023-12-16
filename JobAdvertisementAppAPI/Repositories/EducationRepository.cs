using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly DataContext dataContext;

        public EducationRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateEducation(Education education)
        {
            dataContext.Education.Add(education);
            return Save();
        }

        public bool DeleteEducation(Education education)
        {
            dataContext.Education.Remove(education);
            return Save();
        }

        public IEnumerable<Education> GetEducations()
        {
            return dataContext.Education.ToList();
        }

        public IEnumerable<Education> GetUserEducations(int userId)
        {
            return dataContext.Education.Where(e => e.User.Id == userId).OrderByDescending(e => e.PeriodOfEducationEnd).ThenByDescending(e => e.PeriodOfEducationStart).ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateEducation(Education education)
        {
            dataContext.Education.Update(education);
            return Save();
        }
    }
}
