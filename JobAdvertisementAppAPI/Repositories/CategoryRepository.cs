using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool CreateCategory(Category category)
        {
            dataContext.Category.Add(category);
            return Save();
        }

        public Category? GetCategory(int id)
        {
            return dataContext.Category.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool DeleteCategory(Category category)
        {
            dataContext.Category.Remove(category);
            return Save();
        }

        public IEnumerable<Category> GetCategories()
        {
            return dataContext.Category.ToList();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateCategory(Category category)
        {
            dataContext.Category.Update(category);
            return Save();
        }
    }
}
