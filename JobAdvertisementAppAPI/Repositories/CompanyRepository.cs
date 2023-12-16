using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext dataContext;

        public CompanyRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateCompany(Company company)
        {
            dataContext.Company.Add(company);
            return Save();
        }

        public bool DeleteCompany(Company company)
        {
            dataContext.Company.Remove(company);
            return Save();
        }

        public IEnumerable<Company> GetCompanies()
        {
            return dataContext.Company.ToList();
        }

        public Company? GetCompany(int id)
        {
            return dataContext.Company.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateCompany(Company company)
        {
            dataContext.Company.Update(company);
            return Save();
        }
    }
}
