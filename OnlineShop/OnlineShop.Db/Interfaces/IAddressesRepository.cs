using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IAddressesRepository
    {
        void Add(Address address);
        List<Address> GetByUserId(string userId);
        Address? TryGetById(Guid addressId);
        void ResetLastAddress(string userId, Guid addressId);
    }
}
