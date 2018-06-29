using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTheBoss.Skeleton;


enum HeroStates {
    InFight,
    Hialing
}

namespace FightTheBoss.States
{
    abstract class State
    {
        public abstract void Handle(Fighter fighter);
    }
}
