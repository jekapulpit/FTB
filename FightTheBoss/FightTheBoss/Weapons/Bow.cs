using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FightTheBoss.Skeleton;


namespace FightTheBoss.Weapons
{
    class Bow : Weapon
    {
       public Bow():base(3)
        {
            Call = "Лук";
        }
        public override void Attackeffect(Fighter Goal)
        {
            MessageBox.Show("в '" + Goal.Name + "' была выпущена стрела!");
            Goal.Health -= (damage - Goal.Armor);
        }

    }
}
