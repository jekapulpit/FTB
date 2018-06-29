using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FightTheBoss.Skeleton;


namespace FightTheBoss.Weapons
{
    class MiniGun : Weapon
    {


        public MiniGun() : base(10)
        {
            Call = "Пулемет";
        }

        public override void Attackeffect(Fighter Goal)
        {
            MessageBox.Show(Goal.Name + " Был расстрелян из минигана!!!");

            Goal.Health -= (damage - Goal.Armor);
        }

    }
}
