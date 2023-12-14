using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class WorkingShiftRepository : IWorkingShiftRepository
    {
        private readonly DataContext dataContext;

        public WorkingShiftRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateWorkingShift(WorkingShift workingShift)
        {
            dataContext.WorkingShift.Add(workingShift);
            return Save();
        }

        public bool DeleteWorkingShift(WorkingShift workingShift)
        {
            dataContext.WorkingShift.Remove(workingShift);
            return Save();
        }

        public IEnumerable<WorkingShift> GetWorkingShifts()
        {
            return dataContext.WorkingShift.ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateWorkingShift(WorkingShift workingShift)
        {
            dataContext.WorkingShift.Update(workingShift);
            return Save();
        }
    }
}
