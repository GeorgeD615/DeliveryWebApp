using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IAddressRepository
    {
        void Add(Address address);
        void Remove(Address address);
        List<Address> GetByUserId(string userId);
        Address? TryGetById(Guid addressId);
        void ResetLastAddress(string userId, Guid addressId);
    }
}
