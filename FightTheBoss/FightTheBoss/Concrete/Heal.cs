using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTheBoss.Skeleton;


namespace FightTheBoss.Concrete
{
    class Heal : Command
    {
        public Fighter Goal;
        public Heal(Fighter Goal)
        {
            this.Goal = Goal;
        }

        public override void Execute()
        {
            Goal.GetHealed();
        }

        public override void Undo()
        {

        }
    }
}
