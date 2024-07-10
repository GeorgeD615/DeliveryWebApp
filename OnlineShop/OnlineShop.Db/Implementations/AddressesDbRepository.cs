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
        public void Add(Address address)
        {
            var userAddresses = databaseContext.Addresses.Where(addressFromDB => addressFromDB.UserId == address.UserId).ToList();

            address.IsLast = true;

            if (userAddresses.IsNullOrEmpty())
            {
                databaseContext.Addresses.Add(address);
                databaseContext.SaveChanges();
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

            databaseContext.SaveChanges();
        }

        public List<Address> GetByUserId(string userId)
        {
            return databaseContext.Addresses.Where(a => a.UserId == userId).ToList();
        }

        public Address? TryGetById(Guid addressId)
        {
            return databaseContext.Addresses.FirstOrDefault(address => address.Id == addressId);
        }

        public void ResetLastAddress(string userId, Guid addressId)
        {
            var userAddresses = GetByUserId(userId);

            foreach (var a in userAddresses)
                a.IsLast = a.Id == addressId;

            databaseContext.SaveChanges();
        }
    }
}
