using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FightTheBoss.Skeleton;


namespace FightTheBoss.Weapons
{
    class Granate : Weapon
    {


        public Granate():base(15)
        {
            Call = "Граната";
        }
        
        public override void Attackeffect(Fighter Goal)
        {
            MessageBox.Show("в '" + Goal.Name + "' кинули гранату!!!");

            Goal.Health -= (damage - Goal.Armor);
        }
    }
}
