using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTheBoss.Skeleton;


namespace FightTheBoss.Concrete
{
    class Protect : Command
    {

        Fighter Goal;
        
        public Protect(Fighter Goal)
        {
            this.Goal = Goal; 
        }

        public override void Execute()
        {
            Goal.TakeAShield();
        }

        public override void Undo()
        {

        }
    }
}
