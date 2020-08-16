using System;
using System.Collections.Generic;

namespace AuthEFService.Models.DBTest
{
    public partial class UserRole
    {
        public int Uid { get; set; }
        public int Rid { get; set; }

        public virtual Role R { get; set; }
        public virtual User U { get; set; }
    }
}
