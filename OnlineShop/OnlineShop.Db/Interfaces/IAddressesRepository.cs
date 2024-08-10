using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IAddressesRepository
    {
        Task AddAsync(Address address);
        Task<List<Address>> GetByUserIdAsync(string userId);
        Task<Address?> TryGetByIdAsync(Guid addressId);
        Task ResetLastAddressAsync(string userId, Guid addressId);
    }
}
