using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton
{
    public abstract class Weapon : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
