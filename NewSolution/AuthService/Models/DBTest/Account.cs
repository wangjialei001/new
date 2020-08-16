using System;
using System.Collections.Generic;

namespace AuthService.Models.DBTest
{
    public partial class Account
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public decimal? Money { get; set; }

        public virtual User U { get; set; }
    }
}
