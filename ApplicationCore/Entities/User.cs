using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class User
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public String HashedPassword { get; set; }
        public String Salt { get; set; }
        public String PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public DateTime LockoutEndDate { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public bool IsLocked { get; set; }
        public int AccessFailedCount { get; set; }
        public ICollection<Favorite> Favorites { set; get; }
        public ICollection<Review> Reviews { set; get; }
        public ICollection<Purchase> Purchases { set; get; }
        public ICollection<UserRole> UserRoles { set; get; }

    }
}
