using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Implementations
{
    public class AddressesDbRepository : IAddressesRepository
    {
        private readonly DatabaseContext databaseContext;

        public AddressesDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task AddAsync(Address address)
        {
            var userAddresses = await databaseContext.Addresses.
                Where(addressFromDB => addressFromDB.UserId == address.UserId).
                ToListAsync();

            address.IsLast = true;

            if (userAddresses.IsNullOrEmpty())
            {
                databaseContext.Addresses.Add(address);
                await databaseContext.SaveChangesAsync();
                return;
            }

            if (userAddresses.Any(oldAddress => oldAddress.City == address.City &&
                                                                oldAddress.Street == address.Street &&
                                                                oldAddress.House == address.House &&
                                                                oldAddress.Flat == address.Flat))
                return;

            foreach (var a in userAddresses)
                a.IsLast = false;

            databaseContext.Addresses.Add(address);

            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<Address>> GetByUserIdAsync(string userId)
        {
            return await databaseContext.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<Address?> TryGetByIdAsync(Guid addressId)
        {
            return await databaseContext.Addresses.FirstOrDefaultAsync(address => address.Id == addressId);
        }

        public async Task ResetLastAddressAsync(string userId, Guid addressId)
        {
            var userAddresses = await GetByUserIdAsync(userId);

            foreach (var a in userAddresses)
                a.IsLast = a.Id == addressId;

            await databaseContext.SaveChangesAsync();
        }
    }
}
