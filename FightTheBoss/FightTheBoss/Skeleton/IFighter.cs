using FightTheBoss.ArmorElements;
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
    public abstract class Fighter : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FighterId { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public int Xp { get; set; }
        public int AbilityPoints { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Lvl { get; set; }
        public int Damage { get; set; }
        public int BaseArmor { get; set; }
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

        [ForeignKey("Weapon")]
        public int? WeaponId { get; set; }
        public Weapon Weapon { get; set; }

        [ForeignKey("Helmet")]
        public int? HelmetId { get; set; }
        public Helmet Helmet { get; set; }

        [ForeignKey("FeetArmor")]
        public int? FeetArmorId { get; set; }
        public FeetArmor FeetArmor { get; set; }

        [ForeignKey("BodyArmor")]
        public int? BodyArmorId { get; set; }
        public BodyArmor BodyArmor { get; set; }

        public abstract void TakeAShield();
        public abstract void GetHealed();


        public void GetWeapon(Weapon weapon)
        {
            this.Weapon = weapon;
            this.WeaponId = weapon.Id;
        }

       
        public void GetHelmet (Helmet armor)
        {
            this.Helmet = armor;
        }
        public void GetBodyArmor(BodyArmor armor)
        {
            this.BodyArmor = armor;
        }
        public void GetFeetArmor(FeetArmor armor)
        {
            this.FeetArmor = armor;
        }
        public void ThrowArmor(Armor armor)
        {
            if(armor != null)
                this.Armor -= armor.ArmorPoints;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
