using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IImagesRepository
    {
        Task SetAvatarAsync(Avatar avatar);
        Task<Avatar?> TryGetAvatarByUserIdAsync(string userId);
    }
}
