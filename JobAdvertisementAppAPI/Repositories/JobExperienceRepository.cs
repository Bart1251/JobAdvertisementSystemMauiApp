using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class JobExperienceRepository : IJobExperienceRepository
    {
        private readonly DataContext dataContext;

        public JobExperienceRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateJobExperience(JobExperience jobExperience)
        {
            dataContext.JobExperience.Add(jobExperience);
            return Save();
        }

        public bool DeleteJobExperience(JobExperience jobExperience)
        {
            dataContext.JobExperience.Remove(jobExperience);
            return Save();
        }

        public IEnumerable<JobExperience> GetJobExperiences()
        {
            return dataContext.JobExperience.ToList();
        }

        public IEnumerable<JobExperience> GetUserJobExperiences(int userId)
        {
            return dataContext.JobExperience.Where(e => e.User.Id == userId).ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateJobExperience(JobExperience jobExperience)
        {
            dataContext.JobExperience.Update(jobExperience);
            return Save();
        }
    }
}
