using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FightTheBoss.Skeleton
{
    class User
    {
        [Key]
        string Username { get; set; }
        string Password { get; set; }

        public ICollection<Fighter> Fighters { get; set; }
        public ICollection<Weapon> Weapons { get; set; }
    }
}
