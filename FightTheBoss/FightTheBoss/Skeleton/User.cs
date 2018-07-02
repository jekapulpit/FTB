using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FightTheBoss.Skeleton
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(string user, string password)
        {
            Username = user;
            Password = password.GetHashCode().ToString();
        }
        public ICollection<Fighter> Fighters { get; set; }
        public ICollection<Weapon> Weapons { get; set; }
    }
}
