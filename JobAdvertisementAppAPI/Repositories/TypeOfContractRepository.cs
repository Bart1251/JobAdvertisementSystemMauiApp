using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class TypeOfContractRepository : ITypeOfContractRepository
    {
        private readonly DataContext dataContext;

        public TypeOfContractRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateTypeOfContract(TypeOfContract typeOfContract)
        {
            dataContext.TypeOfContract.Add(typeOfContract);
            return Save();
        }

        public TypeOfContract? GetTypeOfContract(int id)
        {
            return dataContext.TypeOfContract.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool DeleteTypeOfContract(TypeOfContract typeOfContract)
        {
            dataContext.TypeOfContract.Remove(typeOfContract);
            return Save();
        }

        public IEnumerable<TypeOfContract> GetTypeOfContracts()
        {
            return dataContext.TypeOfContract.ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateTypeOfContract(TypeOfContract typeOfContract)
        {
            dataContext.TypeOfContract.Update(typeOfContract);
            return Save();
        }
    }
}
