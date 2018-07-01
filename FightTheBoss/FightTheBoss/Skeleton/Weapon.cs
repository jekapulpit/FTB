using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton
{
    abstract class Weapon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;
        public int damage { get; set; }
        public string Call { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }
        public User User { get; set; }

        public Weapon()
        {
            
        }

        public Weapon(int damage)
        {
            this.damage = damage;
        }
        public abstract void Attackeffect(Fighter Goal);
    }
}
