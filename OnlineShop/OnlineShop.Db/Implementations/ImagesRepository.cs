using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Implementations
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly DatabaseContext databaseContext;

        public ImagesRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<Avatar?> TryGetAvatarByUserIdAsync(string userId)
        {
            return await databaseContext.Avatars.FirstOrDefaultAsync(avatar => avatar.UserId == userId);
        }

        public async Task SetAvatarAsync(Avatar newAvatar)
        {
            var avatar = await databaseContext.Avatars.FirstOrDefaultAsync(a => a.UserId == newAvatar.UserId);

            if(avatar == null)
                databaseContext.Avatars.Add(newAvatar);
            else
                avatar.Url = newAvatar.Url;

            await databaseContext.SaveChangesAsync();
        }
    }
}
