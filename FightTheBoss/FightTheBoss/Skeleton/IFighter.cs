using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FightTheBoss.States;

namespace FightTheBoss.Skeleton
{
    abstract class Fighter
    {
        public string Name { get; set; }
        int armor;
        int health;

        public int Armor
        {
            get
            {
                return armor;
            }
            set
            {
                armor = value;
                OnPropertyChanged("Armor");
            }
        }
        public Fighter(string name)
        {
            Name = name;
            OnPropertyChanged("Name");
            armor = 1;
            health = 30;
        }
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
                OnPropertyChanged("Health");
            }
        }

        public HeroStates state;

        public Weapon weapon;
        public State State;
        public Command command;

        public abstract void TakeAShield();
        public abstract void GetHealed();

        public void SetCommand(Command c)
        {
            command = c;
        }

        public void Run()
        {
            command.Execute();
        }

        public void Cancel()
        {
            command.Undo();
        }

        public void GetWeapon(Weapon weapon)
        {
            this.weapon = weapon;
            MessageBox.Show(this.Name + " взял в руки " + weapon.Call);
        }

        public void ChangeState()
        {
            this.State.Handle(this);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
