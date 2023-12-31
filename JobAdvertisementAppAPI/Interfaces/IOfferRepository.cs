using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Interfaces
{
    public interface IOfferRepository
    {
        IEnumerable<Offer> GetOffers();
        IEnumerable<Offer> GetOffersFromCompany(int id);
        IEnumerable<Offer> GetNotExpierdOffers();
        int GetNotExpiredOffersCount();
        Offer? GetOffer(int id);
        bool CreateOffer(Offer offer);
        bool UpdateOffer(Offer offer);
        bool DeleteOffer(Offer offer);
        IEnumerable<Offer> GetOffersUserApplied(int id);
        bool Save();
    }
}
