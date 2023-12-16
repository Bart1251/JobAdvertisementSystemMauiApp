using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class JobLevelRepository : IJobLevelRepository
    {
        private readonly DataContext dataContext;

        public JobLevelRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateJobLevel(JobLevel jobLevel)
        {
            dataContext.JobLevel.Add(jobLevel);
            return Save();
        }

        public JobLevel? GetJobLevel(int id)
        {
            return dataContext.JobLevel.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool DeleteJobLevel(JobLevel jobLevel)
        {
            dataContext.JobLevel.Remove(jobLevel);
            return Save();
        }

        public IEnumerable<JobLevel> GetJobLevels()
        {
            return dataContext.JobLevel.ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateJobLevel(JobLevel jobLevel)
        {
            dataContext.JobLevel.Update(jobLevel);
            return Save();
        }
    }
}
