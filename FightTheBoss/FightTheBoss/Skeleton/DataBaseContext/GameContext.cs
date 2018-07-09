using FightTheBoss.ArmorElements;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    class GameContext : DbContext
    {
        public GameContext() : base("connect")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Helmet> Helmets { get; set; }
        public DbSet<BodyArmor> BodyArmors { get; set; }
        public DbSet<FeetArmor> FeetArmors { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Fighter> Fighters { get; set; }

    }
}
