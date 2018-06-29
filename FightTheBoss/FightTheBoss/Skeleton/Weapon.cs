using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton
{
    abstract class Weapon
    {
        public int damage { get; set; }
        public string Call { get; set; }

        public Weapon(int damage)
        {
            this.damage = damage;
        }
        public abstract void Attackeffect(Fighter Goal);
    }
}
