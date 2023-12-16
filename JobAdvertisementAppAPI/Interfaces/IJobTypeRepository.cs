using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IJobTypeRepository
    {
        IEnumerable<JobType> GetJobTypes();
        public JobType? GetJobType(int id);
        bool CreateJobType(JobType jobType);
        bool UpdateJobType(JobType jobType);
        bool DeleteJobType(JobType jobType);
        bool Save();
    }
}
