using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FightTheBoss.Skeleton;

namespace FightTheBoss.Races
{
    class Ghoblin : Fighter
    {
        public Ghoblin(string name, string Race) : base(name, "Гоблин")
        {
            Armor = 3;
            Health = 20;
        }
        public override void TakeAShield()
        {
            Armor = 10;
        }
        public override void GetHealed()
        {
            Health = 20;
        }

    }
}
