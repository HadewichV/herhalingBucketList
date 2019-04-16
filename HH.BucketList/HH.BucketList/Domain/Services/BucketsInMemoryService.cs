using HH.BucketList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.BucketList.Domain.Services
{
    public class BucketsInMemoryService
    {
        private static List<BucketL> bucketLists = new List<BucketL>
        {
            new BucketL {
                Id = Guid.NewGuid(),
                OwnerId = Guid.Empty, //the first user 
                Title = "Siegfried's first bucket list",
                Description = "A simple bucket list",
                ImageUrl = null,
                IsFavorite = true,
                Items = new List<BucketItem>
                {
                    new BucketItem
                    {
                        Id = Guid.NewGuid(),
                        ItemDescription ="Make a world trip",
                        Order = 1
                    },
                    new BucketItem
                    {
                        Id = Guid.NewGuid(),
                        ItemDescription ="Learn Xamarin",
                        CompletionDate = DateTime.Now,
                        Order = 2
                    },
                    new BucketItem
                    {
                        Id = Guid.NewGuid(),
                        ItemDescription ="Publish my first mobile app",
                        Order = 3
                    }
                }
            } 
        };

        public async Task<IEnumerable<BucketL>> GetBucketListsForUser(Guid userid)
        {
            await Task.Delay(1000);
            return bucketLists.Where(b => b.OwnerId == userid);
        }

        public async Task<BucketL> GetBucketList(Guid bucketLId)
        {
            await Task.Delay(1000);
            return bucketLists.FirstOrDefault(b => b.Id == bucketLId);
        }

        public async Task SaveBucketList(BucketL bucketL)
        {
            await Task.Delay(1000);
            var savedbucketL = bucketLists.FirstOrDefault(b => b.Id == bucketL.Id);

            if (savedbucketL == null) //this is a new bucket 
            {
                savedbucketL = bucketL;
                savedbucketL.Id = Guid.NewGuid();
                bucketLists.Add(savedbucketL);
            }

            savedbucketL.Title = bucketL.Title;
            savedbucketL.Description = bucketL.Description;
            savedbucketL.ImageUrl = bucketL.ImageUrl;
            savedbucketL.IsFavorite = bucketL.IsFavorite;
            savedbucketL.OwnerId = bucketL.OwnerId;
            savedbucketL.Items = bucketL.Items;
        }

        public async Task DeleteBucketList(Guid bucketLId)
        {
            await Task.Delay(1000);
            var bucketL = bucketLists.FirstOrDefault(b => b.Id == bucketLId);
            bucketLists.Remove(bucketL);
        }
    }
}
