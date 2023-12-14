using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface ITypeOfContractRepository
    {
        IEnumerable<TypeOfContract> GetTypeOfContracts();
        bool CreateTypeOfContract(TypeOfContract typeOfContract);
        bool UpdateTypeOfContract(TypeOfContract typeOfContract);
        bool DeleteTypeOfContract(TypeOfContract typeOfContract);
        bool Save();
    }
}
