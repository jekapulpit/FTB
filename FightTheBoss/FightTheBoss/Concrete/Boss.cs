using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTheBoss.Skeleton;


namespace FightTheBoss.Concrete
{
    class Boss : Fighter
    {
        public Boss(string name, string race) : base("Босс","Босс")
        {
            Armor = 1;
            Health = 100;
        }
        public override void TakeAShield()
        {
            
        }

        public override void GetHealed()
        {
            Health = 100;
        }
       


    }
}
