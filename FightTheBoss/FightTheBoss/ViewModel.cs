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
using System.Windows;

namespace FightTheBoss
{
    class ViewModel : INotifyPropertyChanged
    {

        List<string> Races = new List<string>()
        {
            "Гоблин",
            "Берсерк",
            "Эльф"
        };

        string progress;

        public string Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                OnPropertyChanged("Progress");

            }
        }


public User CurrentUser { get; set; }
        public AddHero AddHeroWindow { get; set; }

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
                Progress = selectedFighter.Xp.ToString() + "/100";

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



        public ObservableCollection<Weapon> Weapons { get; set; }
        public ObservableCollection<Fighter> Fighters { get; set; }
        public ObservableCollection<Fighter> Goals { get; set; }

        private RelayCommand _AddHero;
        public RelayCommand AddHero
        {
            get
            {
                return _AddHero ??
                    (_AddHero = new RelayCommand(obj =>
                    {

                        AddHeroWindow = new AddHero();
                        AddHeroWindow.AddButton.Click += AddNewHero;
                        foreach(string race in Races)
                        {
                            AddHeroWindow.Races.Items.Add(race);
                        }
                        AddHeroWindow.Show();

                    }
                    ));
            }

        }


        private RelayCommand _DeleteHero;
        public RelayCommand DeleteHero
        {
            get
            {
                return _DeleteHero ??
                    (_DeleteHero = new RelayCommand(obj =>
                    {
                        MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить персонажа?\nВосстановление прогресса будет невозможно!",
                                          "Подтверждение",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            using (FighterContext T = new FighterContext())
                            {
                                T.Fighters.Remove(T.Fighters.Find(SelectedFighter.Id));
                                T.SaveChanges();
                            }
                            Fighters.Remove(SelectedFighter);
                            if (Fighters.Count() != 0) SelectedFighter = Fighters[0];
                        }
                    },
                    obj => { return SelectedFighter != null; }
                    ));
            }

        }

        public void AddNewHero(object sender, RoutedEventArgs e)
        {
            Fighter NewHero = new Ghoblin();
            string Race = AddHeroWindow.Races.SelectedItem.ToString();
            try
            {
                switch (Race)
                {
                    case "Берсерк":
                        NewHero = new Berzerk(AddHeroWindow.HeroName.Text, "Берсерк");
                        break;
                    case "Эльф":
                        NewHero = new Elph(AddHeroWindow.HeroName.Text, "Эльф");
                        break;
                    case "Гном":
                        NewHero = new Ghoblin(AddHeroWindow.HeroName.Text, "Гоблин");
                        break;
                    default: throw new Exception();
                       
                }
                NewHero.Username = CurrentUser.Username;
                Fighters.Insert(0, NewHero);
                SelectedFighter = NewHero;
                using (FighterContext T = new FighterContext())
                {
                    T.Fighters.Add(NewHero);
                    T.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            AddHeroWindow.Close();
           


        }

        private RelayCommand _ChooseWeapon;
        public RelayCommand ChooseWeapon
        {
            get
            {
                return _ChooseWeapon ??
                    (_ChooseWeapon = new RelayCommand(obj =>
                    {
                        
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
                       
                    }
                    ));
            }

        }

        public ViewModel(User currentuser)
        {
            CurrentUser = currentuser;
            Fighters = new ObservableCollection<Fighter>();

            try
            {
                using (FighterContext T = new FighterContext())
                {
                    IEnumerable<Fighter> allf = from p in T.Fighters
                                                where p.Username == CurrentUser.Username
                                                select p;
                    foreach(Fighter fighter in allf)
                    {
                        Fighters.Add(fighter);
                    }
                    T.SaveChanges();
                }
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
            Weapons = new ObservableCollection<Weapon>()
            {
               
            };
          

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
