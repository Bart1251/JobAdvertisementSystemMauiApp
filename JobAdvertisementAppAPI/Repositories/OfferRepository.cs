using JobAdvertisementAppAPI.Data;
using JobAdvertisementAppAPI.Interfaces;
using JobAdvertisementAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAdvertisementAppAPI.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly DataContext dataContext;

        public OfferRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public bool CreateOffer(Offer offer)
        {
            dataContext.Offer.Add(offer);
            return Save();
        }

        public Offer? GetOffer(int id)
        {
            return dataContext.Offer.Where(e => e.Id == id)
                .Include(e => e.Company)
                .Include(e => e.JobLevel)
                .Include(e => e.TypeOfContract)
                .Include(e => e.JobType)
                .Include(e => e.WorkingShift)
                .Include(e => e.Category).FirstOrDefault();
        }

        public IEnumerable<Offer> GetOffersFromCompany(int id)
        {
            return dataContext.Offer.Where(e => e.Company.Id == id)
                .Include(e => e.Company)
                .Include(e => e.JobLevel)
                .Include(e => e.TypeOfContract)
                .Include(e => e.JobType)
                .Include(e => e.WorkingShift)
                .Include(e => e.Category);
        }

        public IEnumerable<Offer> GetOffersUserApplied(int id)
        {
            return dataContext.User
                .Where(u => u.Id == id)
                .SelectMany(u => u.UserOffers.Select(uo => uo.Offer))
                .Include(o => o.Company)
                .Include(o => o.JobLevel)
                .Include(o => o.TypeOfContract)
                .Include(o => o.JobType)
                .Include(o => o.WorkingShift)
                .Include(o => o.Category)
                .ToList();
        }

        public bool DeleteOffer(Offer offer)
        {
            dataContext.Offer.Remove(offer);
            return Save();
        }

        public IEnumerable<Offer> GetNotExpierdOffers()
        {
            return dataContext.Offer.Where(e => e.Expires > DateTime.Now)
                .Include(e => e.Company)
                .Include(e => e.JobLevel)
                .Include(e => e.TypeOfContract)
                .Include(e => e.JobType)
                .Include(e => e.WorkingShift)
                .Include(e => e.Category).ToList();
        }

        public IEnumerable<Offer> GetOffers()
        {
            return dataContext.Offer
                .Include(e => e.Company)
                .Include(e => e.JobLevel)
                .Include(e => e.TypeOfContract)
                .Include(e => e.JobType)
                .Include(e => e.WorkingShift)
                .Include(e => e.Category).ToList();
        }

        public int GetNotExpiredOffersCount()
        {
            return dataContext.Offer.Where(e => e.Expires > DateTime.Now).Count();
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 0;
        }

        public bool UpdateOffer(Offer offer)
        {
            dataContext.Offer.Update(offer);
            return Save();
        }
    }
}
