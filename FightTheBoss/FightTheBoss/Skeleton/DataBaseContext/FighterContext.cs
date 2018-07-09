using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton
{
    class FighterContext : DbContext
    {
        public FighterContext() : base("connect")
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Fighter> Fighters { get; set; }

    }
}
