using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FightTheBoss.Skeleton;


namespace FightTheBoss.Weapons
{
    class Knife : Weapon
    {


        public Knife() : base(7)
        {
            Call = "Нож";
        }

        public override void Attackeffect(Fighter Goal)
        {
            MessageBox.Show(Goal.Name + " пырнули ножом!!!");
            Goal.Health -= (damage - Goal.Armor);
        }
    }
}
