using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTheBoss.Skeleton;


namespace FightTheBoss.States
{
    class InFight : State
    {
        public override void Handle(Fighter fighter)
        {
            fighter.State = new Healing();
            fighter.state = HeroStates.Hialing;
        }
    }
}
