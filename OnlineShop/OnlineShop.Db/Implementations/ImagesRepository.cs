using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Db.Implementations
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly DatabaseContext databaseContext;

        public ImagesRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public Avatar? TryGetAvatarByUserId(string userId)
        {
            return databaseContext.Avatars.FirstOrDefault(avatar => avatar.UserId == userId);
        }

        public void SetAvatar(Avatar newAvatar)
        {
            var avatar = databaseContext.Avatars.FirstOrDefault(a => a.UserId == newAvatar.UserId);

            if(avatar == null)
                databaseContext.Avatars.Add(newAvatar);
            else
            {
                avatar.Url = newAvatar.Url;
            }

            databaseContext.SaveChanges();
        }
    }
}
