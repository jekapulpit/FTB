using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton
{
    class WeaponContext : DbContext
    {
        public WeaponContext() : base("connect")
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }

    }
}
