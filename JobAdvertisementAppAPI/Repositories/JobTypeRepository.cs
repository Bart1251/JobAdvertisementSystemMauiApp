using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class JobTypeRepository : IJobTypeRepository
    {
        private readonly DataContext dataContext;

        public JobTypeRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateJobType(JobType jobType)
        {
            dataContext.JobType.Add(jobType);
            return Save();
        }

        public bool DeleteJobType(JobType jobType)
        {
            dataContext.JobType.Remove(jobType);
            return Save();
        }

        public IEnumerable<JobType> GetJobTypes()
        {
            return dataContext.JobType.ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateJobType(JobType jobType)
        {
            dataContext.JobType.Update(jobType);
            return Save();
        }
    }
}
