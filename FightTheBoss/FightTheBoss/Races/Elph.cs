using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTheBoss.Skeleton;

namespace FightTheBoss.Races
{
    class Elph:Fighter
    {
        public Elph(string name, string race) : base(name, "Эльф")

        {
            BaseArmor = 2;
            Damage = 3;
            Armor = 1;
            Health = 25;
        }
        public Elph() {

        }

        public override void TakeAShield()
        {
            Armor = 4;
        }
        public override void GetHealed()
        {
            Health = 25;
        }

    }
}
