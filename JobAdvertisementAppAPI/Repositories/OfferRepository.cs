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
