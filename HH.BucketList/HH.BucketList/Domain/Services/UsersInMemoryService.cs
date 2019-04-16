using HH.BucketList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.BucketList.Domain.Services
{
    public class UsersInMemoryService
    {
        private static List<User> users = new List<User>
        {
            new User {
                Id = Guid.Empty,
                UserName = "Siegfried",
                Email = "siegfried@bucketlists.test"
            }
        };

        public async Task<User> GetUserById(Guid id)
        {
            await Task.Delay(1000);
            return users.FirstOrDefault(u => u.Id == id);
        }

        public async Task SaveUser(User user)
        {
            var oldUser = await GetUserById(user.Id);
            oldUser = user;
        }
    }
}
