﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FightTheBoss.Skeleton;
namespace FightTheBoss.Races
{
    class Berzerk : Fighter
    {
        public Berzerk(string name, string race) : base(name, "Берсерк")
        {
            Armor = 2;
            Health = 40;
        }
        public override void TakeAShield()
        {
            MessageBox.Show("Берсерк не может взять щит! Он же Дебил!");
        }
        public override void GetHealed()
        {
            Health = 40;
        }

    }
}
