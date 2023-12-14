using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IWorkingShiftRepository
    {
        IEnumerable<WorkingShift> GetWorkingShifts();
        bool CreateWorkingShift(WorkingShift workingShift);
        bool UpdateWorkingShift(WorkingShift workingShift);
        bool DeleteWorkingShift(WorkingShift workingShift);
        bool Save();
    }
}
