using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IOfferRepository
    {
        IEnumerable<Offer> GetOffers();
        public Offer? GetOffer(int id);
        bool CreateOffer(Offer offer);
        bool UpdateOffer(Offer offer);
        bool DeleteOffer(Offer offer);
        bool Save();
    }
}
