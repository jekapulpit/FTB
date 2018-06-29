using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FightTheBoss.Skeleton;

namespace FightTheBoss.Weapons
{
    class BossHand:Weapon
    {
        public BossHand() : base(10)
        {
            Call = "Рука";
        }

        public override void Attackeffect(Fighter Goal)
        {
            Goal.Health -= (damage - Goal.Armor);
        }
    }
}

