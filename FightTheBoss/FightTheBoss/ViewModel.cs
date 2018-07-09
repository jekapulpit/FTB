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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FightTheBoss.ArmorElements;

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

                if (selectedFighter != null)
                {
                    Progress = selectedFighter.Xp.ToString() + "/100";
                    using(WeaponContext T = new WeaponContext())
                    {
                        SelectedWeapon = T.Weapons.Find(SelectedFighter.WeaponId);
                    }
                    OnPropertyChanged("SelectedWeapon");
                    using(ArmorContext T = new ArmorContext())
                    {
                        SelectedHelmet = T.Helmets.Find(SelectedFighter.HelmetId);
                        SelectedBodyArmor = T.BodyArmors.Find(SelectedFighter.BodyArmorId);
                        SelectedFeetArmor = T.FeetArmors.Find(SelectedFighter.FeetArmorId);
                    }
                    OnPropertyChanged("SelectedHelmet");
                    OnPropertyChanged("SelectedBodyArmor");
                    OnPropertyChanged("SelectedFeetArmor");
                }

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

        Helmet selectedHelmet;
        public Helmet SelectedHelmet
        {
            get
            {
                return selectedHelmet;
            }
            set
            {
                selectedHelmet = value;
                OnPropertyChanged("SelectedHelmet");
            }
        }

        BodyArmor selectedBodyArmor;
        public BodyArmor SelectedBodyArmor
        {
            get
            {
                return selectedBodyArmor;
            }
            set
            {
                selectedBodyArmor = value;
                OnPropertyChanged("SelectedBodyArmor");
            }
        }

        FeetArmor selectedFeetArmor;
        public FeetArmor SelectedFeetArmor
        {
            get
            {
                return selectedFeetArmor;
            }
            set
            {
                selectedFeetArmor = value;
                OnPropertyChanged("SelectedFeetArmor");
            }
        }

        public ObservableCollection<Weapon> Weapons { get; set; }
        public ObservableCollection<Fighter> Fighters { get; set; }
        public ObservableCollection<Fighter> Goals { get; set; }
        public ObservableCollection<Armor> Helmets { get; set; }
        public ObservableCollection<Armor> BodyArmors { get; set; }
        public ObservableCollection<Armor> FeetArmors { get; set; }

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
                                T.Fighters.Remove(T.Fighters.Find(SelectedFighter.FighterId));
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
                    case "Гоблин":
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            AddHeroWindow.Close();



        }

        public ViewModel(User currentuser)
        {
            CurrentUser = currentuser;
            Fighters = new ObservableCollection<Fighter>();
            Weapons = new ObservableCollection<Weapon>();
            Helmets = new ObservableCollection<Armor>();
            BodyArmors = new ObservableCollection<Armor>();
            FeetArmors = new ObservableCollection<Armor>();
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
                }
                using (WeaponContext T = new WeaponContext())
                {
                    //T.Weapons.Add(new Bow() { Call = "assd", Username = CurrentUser.Username });
                    //T.Weapons.Add(new Bow() { Call = "4len", Username = CurrentUser.Username });
                    //T.Weapons.Add(new MiniGun() { Call = "hehe", Username = CurrentUser.Username });
                    //T.Weapons.Add(new Bow() { Call = "aga", Username = CurrentUser.Username });
                    //T.SaveChanges();
                    Weapons.Clear();
                    IEnumerable<Weapon> allw = from p in T.Weapons
                                               where p.Username == CurrentUser.Username 
                                               select p;
                    foreach (Weapon weapon in allw)
                    {
                        Weapons.Add(weapon);
                    }
                }
                using (ArmorContext T = new ArmorContext())
                {
                    //T.Helmets.Add(new Helmet() { Call = "sosat", Username = CurrentUser.Username });
                    //T.Helmets.Add(new Helmet() { Call = "sosat1", Username = CurrentUser.Username });
                    //T.Helmets.Add(new Helmet() { Call = "sosat2", Username = CurrentUser.Username });
                    //T.BodyArmors.Add(new BodyArmor() { Call = "Sos", Username = CurrentUser.Username });
                    //T.BodyArmors.Add(new BodyArmor() { Call = "Sos1", Username = CurrentUser.Username });
                    //T.BodyArmors.Add(new BodyArmor() { Call = "Sos2", Username = CurrentUser.Username });
                    //T.FeetArmors.Add(new FeetArmor() { Call = "nog", Username = CurrentUser.Username });
                    //T.FeetArmors.Add(new FeetArmor() { Call = "nog1", Username = CurrentUser.Username });
                    //T.FeetArmors.Add(new FeetArmor() { Call = "nog2", Username = CurrentUser.Username });
                    //T.SaveChanges();
                    Helmets.Clear();
                    BodyArmors.Clear();
                    FeetArmors.Clear();
                    IEnumerable<Armor> allHelmets = from p in T.Helmets
                                                    where p.Username == CurrentUser.Username
                                                    select p;
                    IEnumerable<Armor> allBodyArmors = from p in T.BodyArmors
                                                        where p.Username == CurrentUser.Username
                                                        select p;
                    IEnumerable<Armor> allFeetArmors = from p in T.FeetArmors
                                                        where p.Username == CurrentUser.Username
                                                        select p;
                    foreach (Armor armor in allHelmets)
                    {
                        Helmets.Add(armor);
                    }
                    foreach (Armor armor in allBodyArmors)
                    {
                        BodyArmors.Add(armor);
                    }
                    foreach (Armor armor in allFeetArmors)
                    {
                        FeetArmors.Add(armor);
                    }
                }
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
            

        }

       

       


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
