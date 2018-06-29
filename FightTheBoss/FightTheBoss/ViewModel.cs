using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using FightTheBoss.Concrete;
using FightTheBoss.Races;
using FightTheBoss.Skeleton;
using FightTheBoss.Weapons;
using FightTheBoss.States;
using System.Windows;

namespace FightTheBoss
{
    class ViewModel : INotifyPropertyChanged
    {

        Fighter selectedFighter;
        public Fighter SelectedFighter
        {
            get
            {
                return selectedFighter;
            }
            set
            {
                selectedFighter = value;
                OnPropertyChanged("SelectedFighter");
            }
        }

        Fighter selectedGoal;
        public Fighter SelectedGoal
        {
            get
            {
                return selectedGoal;
            }
            set
            {
                selectedGoal = value;
                OnPropertyChanged("SelectedGoal");
            }
        }

        Weapon selectedWeapon;
        public Weapon SelectedWeapon
        {
            get
            {
                return selectedWeapon;
            }
            set
            {
                selectedWeapon = value;
                OnPropertyChanged("SelectedWeapon");
            }
        }

        public Fighter Boss = new Boss("Смелов ");


        public ObservableCollection<Weapon> Weapons { get; set; }
        public ObservableCollection<Fighter> Fighters { get; set; }
        public ObservableCollection<Fighter> Goals { get; set; }
        public ObservableCollection<string> Log { get; set; }

        private RelayCommand _AddHero;
        public RelayCommand AddHero
        {
            get
            {
                return _AddHero ??
                    (_AddHero = new RelayCommand(obj =>
                    {
                       
                    }
                    ));
            }

        }

        private RelayCommand _ChooseWeapon;
        public RelayCommand ChooseWeapon
        {
            get
            {
                return _ChooseWeapon ??
                    (_ChooseWeapon = new RelayCommand(obj =>
                    {
                        Weapon N = obj as Weapon;
                        SelectedFighter.GetWeapon(N);
                        Log.Add("Игроку " + SelectedFighter.Name + " было выдано оружие " + N.Call);
                    }
                    ));
            }

        }

        private RelayCommand _Attack;
        public RelayCommand Attack
        {
            get
            {
                return _Attack ??
                    (_Attack = new RelayCommand(obj =>
                    {
                        Fighter goal = obj as Fighter;
                        try
                        {
                            SelectedFighter.SetCommand(new Attack(SelectedFighter.weapon, goal));
                            SelectedFighter.Run();
                            int dam = (SelectedFighter.weapon.damage - goal.Armor);
                            if (dam < 0) dam = 0;
                            Log.Add("Игрок " + SelectedFighter.Name + " Атаковал " + goal.Name + " (-" + dam + ")");

                            Goals.Remove(goal);
                            if (goal != Boss)
                                Goals.Insert(1, goal);
                            else
                                Goals.Insert(0, goal);
                            SelectedGoal = goal;

                            if(goal.Health <= 0 && goal != Boss)
                            {
                                Log.Add("Игрок " + goal.Name + " был убит");
                                Goals.Remove(goal);
                                Fighters.Remove(goal);
                            }
                            else if(goal.Health <= 0 && goal == Boss)
                            {
                                Log.Add("Босс был убит! Игроки победили босса!");
                                Goals.Remove(goal);


                            }
                            else
                            {
                                int n = Fighters.Count;
                                for (int i = 0; i < n; i++)
                                {
                                    Fighter T = Fighters[n - 1];
                                    Boss.SetCommand(new Attack(Boss.weapon, T));
                                    Boss.Run();
                                    dam = (Boss.weapon.damage - T.Armor);
                                    if (dam < 0) dam = 0;
                                    Log.Add("Босс атаковал игрока " + T.Name + " (-" + dam + ")");
                                    Fighters.Remove(T);
                                    Fighters.Insert(0, T);
                                    Goals.Remove(T);
                                    Goals.Add(T);
                                    if (T.Health <= 0 && T != Boss)
                                    {
                                        Log.Add("Игрок " + T.Name + " был убит");
                                        Goals.Remove(T);
                                        Fighters.Remove(T);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Выберите оружие!");
                        }
                       

                    }
                    ));
            }

        }

        private RelayCommand _Heal;
        public RelayCommand Heal
        {
            get
            {
                return _Heal ??
                    (_Heal = new RelayCommand(obj =>
                    {
                        try
                        {
                        Fighter goal = obj as Fighter;
                       
                            SelectedFighter.SetCommand(new Heal(goal));
                            int beg = goal.Health;  
                            SelectedFighter.Run();
                            int dam = goal.Health - beg;
                            
                            Log.Add("Игрок " + SelectedFighter.Name + " вылечил " + goal.Name + " (+" + dam + ")");
                            Goals.Remove(goal);
                            if (goal != Boss)
                                Goals.Insert(1, goal);
                            else
                                Goals.Insert(0, goal);
                            SelectedGoal = goal;
                            int n = Fighters.Count;
                            for (int i = 0; i < n; i++)
                            {
                                Fighter T = Fighters[n - 1];
                                Boss.SetCommand(new Attack(Boss.weapon, T));
                                Boss.Run();
                                dam = (Boss.weapon.damage - T.Armor);
                                if (dam < 0) dam = 0;
                                Log.Add("Босс атаковал игрока " + T.Name + " (-" + dam + ")");
                                Fighters.Remove(T);
                                Fighters.Insert(0, T);
                                Goals.Remove(T);
                                Goals.Add(T);
                                if (T.Health <= 0 && T != Boss)
                                {
                                    Log.Add("Игрок " + T.Name + " был убит");
                                    Goals.Remove(T);
                                    Fighters.Remove(T);
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Выберите цель!");
                        }
                    }
                    ));
            }

        }

        private RelayCommand _GetShield;
        public RelayCommand GetShield
        {
            get
            {
                return _GetShield ??
                    (_GetShield = new RelayCommand(obj =>
                    {
                       
                        try
                        {
                            Fighter sel = selectedFighter;
                            SelectedFighter.SetCommand(new Protect(SelectedFighter));
                            SelectedFighter.Run();
                            if (sel is Berzerk) throw new Exception();
                            Log.Add("Игрок " + SelectedFighter.Name + " укрепился ");
                            Goals.Remove(sel);
                            Goals.Insert(0, sel);
                            Fighters.Remove(sel);
                            Fighters.Insert(0, sel);
                            SelectedFighter = sel;
                            SelectedGoal = Boss;
                            int n = Fighters.Count;
                            int dam;
                            for (int i = 0; i < n; i++)
                            {
                                Fighter T = Fighters[n - 1];
                                Boss.SetCommand(new Attack(Boss.weapon, T));
                                Boss.Run();
                                dam = (Boss.weapon.damage - T.Armor);
                                if (dam < 0) dam = 0;
                                Log.Add("Босс атаковал игрока " + T.Name + " (-" + dam + ")");
                                Fighters.Remove(T);
                                Fighters.Insert(0, T);
                                Goals.Remove(T);
                                Goals.Add(T);
                                if (T.Health <= 0 && T != Boss)
                                {
                                    Log.Add("Игрок " + T.Name + " был убит");
                                    Goals.Remove(T);
                                    Fighters.Remove(T);
                                }
                            }
                        }
                        catch
                        {
                           
                        }
                    }
                    ));
            }

        }

        public ViewModel()
        {
            Log = new ObservableCollection<string>();
            Weapons = new ObservableCollection<Weapon>()
            {
                new Bow(),
                new MiniGun(),
                new Granate(),
                new Knife() 
            };
            Fighters = new ObservableCollection<Fighter>()
            {
                new Berzerk("Стиви"),
                new Elph("Джон"),
                new Ghoblin("Жека")
            };
            Goals = new ObservableCollection<Fighter>();
            Goals.Add(Boss);
            foreach (Fighter T in Fighters)
                Goals.Add(T);
           
            Boss.weapon = new BossHand();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
