using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTheBoss.Skeleton;

namespace FightTheBoss.Concrete
{
    class Attack : Command
    {
        public Weapon weapon;
        public Fighter Goal;
        public Attack(Weapon weapon, Fighter Goal)
        {

            this.weapon = weapon;
            this.Goal = Goal;
        }

        public override void Execute()
        {
            weapon.Attackeffect(Goal);
        }

        public override void Undo()
        {
           
        }

    }
}
