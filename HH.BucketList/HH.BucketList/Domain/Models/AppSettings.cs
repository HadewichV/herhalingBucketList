using System;
using System.Collections.Generic;
using System.Text;

namespace HH.BucketList.Domain.Models
{
    public class AppSettings
    {
        public Guid CurrentUserId { get; set; }
        public bool EnableListSharing { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
