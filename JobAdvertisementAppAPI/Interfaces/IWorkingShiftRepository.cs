using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IWorkingShiftRepository
    {
        IEnumerable<WorkingShift> GetWorkingShifts();
        public WorkingShift? GetWorkingShift(int id);
        bool CreateWorkingShift(WorkingShift workingShift);
        bool UpdateWorkingShift(WorkingShift workingShift);
        bool DeleteWorkingShift(WorkingShift workingShift);
        bool Save();
    }
}
