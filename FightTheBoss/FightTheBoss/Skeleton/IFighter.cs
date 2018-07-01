using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace FightTheBoss.Skeleton
{
    abstract class Fighter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public int Xp { get; set; }
        public int AbilityPoints { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Lvl { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }


        public User User { get; set; }
        public Fighter(string name, string race)
        {
            Name = name;
            AbilityPoints = 0;
            Race = race;
            Xp = 0;
            Lvl = 1;
            OnPropertyChanged("Name");
            Armor = 1;
            Health = 30;
        }
        public Fighter() { }
        


        public Weapon weapon;

        public abstract void TakeAShield();
        public abstract void GetHealed();


        public void GetWeapon(Weapon weapon)
        {
            this.weapon = weapon;
            MessageBox.Show(this.Name + " взял в руки " + weapon.Call);
        }

       
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
