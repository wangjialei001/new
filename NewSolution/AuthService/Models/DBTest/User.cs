using System;
using System.Collections.Generic;

namespace AuthService.Models.DBTest
{
    public partial class User
    {
        public User()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? Sex { get; set; }
        public float? Money { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
