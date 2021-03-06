﻿using System;
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
using FightTheBoss.Skeleton.DataBaseContext;

namespace FightTheBoss
{
    class ViewModel : INotifyPropertyChanged
    {
        #region properties

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

        string weaponname;
        public string WeaponName
        {
            get
            {
                return weaponname;
            }
            set
            {
                weaponname = value;
                OnPropertyChanged("WeaponName");
            }
        }

        string bodyarmorname;
        public string BodyArmorName
        {
            get
            {
                return bodyarmorname;
            }
            set
            {
                bodyarmorname = value;
                OnPropertyChanged("BodyArmorName");
            }
        }

        string feetarmorname;
        public string FeetArmorName
        {
            get
            {
                return feetarmorname;
            }
            set
            {
                feetarmorname = value;
                OnPropertyChanged("FeetArmorName");
            }
        }

        string helmetname;
        public string HelmetName
        {
            get
            {
                return helmetname;
            }
            set
            {
                helmetname = value;
                OnPropertyChanged("HelmetName");
            }
        }

        public User CurrentUser { get; set; }

        public AddHero AddHeroWindow { get; set; }

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
                if (selectedFighter != null)
                {
                    Progress = selectedFighter.Xp.ToString() + "/100";
                }
                UpdateLists();

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

        #endregion

        public ObservableCollection<Fighter> Fighters { get; set; }
        public ObservableCollection<Fighter> Goals { get; set; }

        #region commands
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
                            using (UnitOfWork T = new UnitOfWork())
                            {
                                T.GetFighters().UnEquip(SelectedFighter, SelectedFighter.WeaponId);
                                T.GetFighters().TakeOff(SelectedFighter, SelectedFeetArmor);
                                T.GetFighters().TakeOff(SelectedFighter, SelectedBodyArmor);
                                T.GetFighters().TakeOff(SelectedFighter, SelectedHelmet);
                                T.GetFighters().Remove(SelectedFighter);
                            }
                            for (int i = 0; i < Fighters.Count(); i++)
                            {
                                if (Fighters[i].FighterId == SelectedFighter.FighterId)
                                {
                                    Fighters.Remove(Fighters[i]);
                                    break;
                                }
                            }
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

        private RelayCommand _TakeOffWeapon;
        public RelayCommand TakeOffWeapon
        {
            get
            {
                return _TakeOffWeapon ??
                    (_TakeOffWeapon = new RelayCommand(obj =>
                    {
                        using (UnitOfWork T = new UnitOfWork())
                        {
                            T.GetFighters().UnEquip(SelectedFighter, SelectedWeapon.Id);
                            UpdateLists();
                        }
                    }
                    ));
            }
        }

        private RelayCommand _TakeOffHelmet;
        public RelayCommand TakeOffHelmet
        {
            get
            {
                return _TakeOffHelmet ??
                    (_TakeOffHelmet = new RelayCommand(obj =>
                    {
                        using (UnitOfWork T = new UnitOfWork())
                        {
                            T.GetFighters().TakeOff(SelectedFighter, SelectedHelmet);
                            UpdateLists();
                        }
                    }
                    ));
            }
        }

        private RelayCommand _TakeOffBodyArmor;
        public RelayCommand TakeOffBodyArmor
        {
            get
            {
                return _TakeOffBodyArmor ??
                    (_TakeOffBodyArmor = new RelayCommand(obj =>
                    {
                        using (UnitOfWork T = new UnitOfWork())
                        {
                            T.GetFighters().TakeOff(SelectedFighter, SelectedBodyArmor);
                            UpdateLists();
                        }
                    }
                    ));
            }
        }

        private RelayCommand _TakeOffFeetArmor;
        public RelayCommand TakeOffFeetArmor
        {
            get
            {
                return _TakeOffFeetArmor ??
                    (_TakeOffFeetArmor = new RelayCommand(obj =>
                    {
                        using (UnitOfWork T = new UnitOfWork())
                        {
                            T.GetFighters().TakeOff(SelectedFighter, SelectedFeetArmor);
                            UpdateLists();
                        }
                    }
                    ));
            }
        }

        #endregion

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
                SelectedFighter = Fighters[0];
                using (UnitOfWork T = new UnitOfWork())
                {
                    T.GetFighters().Add(NewHero);
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
           
            using (UnitOfWork T = new UnitOfWork())
            {
                Fighters = T.GetFighters().GetAll(CurrentUser.Username);
            }

        }

        public void UpdateLists()
        {
            try
            {
                using (UnitOfWork T = new UnitOfWork())
                {
                    //T.Weapons.Add(new Bow() { Call = "assd", Username = CurrentUser.Username });
                    //T.Weapons.Add(new Bow() { Call = "4len", Username = CurrentUser.Username });
                    //T.Weapons.Add(new MiniGun() { Call = "hehe", Username = CurrentUser.Username });
                    //T.Weapons.Add(new Bow() { Call = "aga", Username = CurrentUser.Username });
                    //T.SaveChanges();
                   if(selectedFighter != null)
                    {
                        selectedFighter = T.GetFighters().Find(selectedFighter.FighterId);
                        SelectedWeapon = (T.GetWeapons().Find(selectedFighter.WeaponId)) ?? (SelectedWeapon = new Bow() {Call = "Без оружия"});
                        SelectedHelmet = (T.GetHelmets().Find(selectedFighter.HelmetId)) ?? (SelectedHelmet = new Helmet() { Call = "Без шлема" }); 
                        SelectedBodyArmor = (T.GetBodyArmor().Find(selectedFighter.BodyArmorId)) ?? (SelectedBodyArmor = new BodyArmor() { Call = "Без брони" }); 
                        SelectedFeetArmor = (T.GetFeetArmor().Find(selectedFighter.FeetArmorId)) ?? (SelectedFeetArmor = new FeetArmor() { Call = "Без ботинок" }); 
                        HelmetName = SelectedHelmet.Call;
                        BodyArmorName = SelectedBodyArmor.Call;
                        FeetArmorName = SelectedFeetArmor.Call;
                        WeaponName = SelectedWeapon.Call;
                        OnPropertyChanged("SelectedFighter");
                    }
                }
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
            }
            catch (NullReferenceException)
            {

            }
            catch (Exception Ex)
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
