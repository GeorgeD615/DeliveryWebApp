using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IImagesRepository
    {
        public void SetAvatar(Avatar avatar);
        public Avatar? TryGetAvatarByUserId(string userId);
    }
}
