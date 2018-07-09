using FightTheBoss.ArmorElements;
using FightTheBoss.Skeleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FightTheBoss
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel currviewmodel;
        public MainWindow(User currentuser)
        {
            InitializeComponent();
            currviewmodel = new ViewModel(currentuser);
            DataContext = currviewmodel;
        }

        private void weaponBut_Click(object sender, RoutedEventArgs e)
        {
            WeaponList.Visibility = Visibility.Visible;
            WeaponList.Children.Clear();
            foreach (Weapon weapon in currviewmodel.Weapons)
            {
                if(!weapon.IsWearing)
                    WeaponList.Children.Add(SetAWeaponElement(weapon.Id));
            }

        }
       
        public Button SetAWeaponElement(int id)
        {
            Button WeaponImageBlock = new Button();
            WeaponImageBlock.Height = 90;
            WeaponImageBlock.Width = 90;
            WeaponImageBlock.Content = "Оружие";
            WeaponImageBlock.Background = null;
            WeaponImageBlock.BorderBrush = Brushes.Black;
            WeaponImageBlock.Name = "id" + id;
            //WeaponImageBlock.CommandParameter = 
            WeaponImageBlock.Click += Chooseweapon;

            return WeaponImageBlock;
        }
        public void Chooseweapon(object sender, RoutedEventArgs e)
        {
            using (WeaponContext T = new WeaponContext())
            {
                Weapon ChosenWeapon = T.Weapons.Find(Convert.ToInt32(((Button)sender).Name.Substring(2)));
                using(FighterContext M = new FighterContext())
                {

                    Fighter fighter = M.Fighters.Find(currviewmodel.SelectedFighter.FighterId);
                    if (fighter.WeaponId != null)
                    {
                        Weapon currweapon = T.Weapons.Find(fighter.WeaponId);
                        currweapon.IsWearing = false;
                    }
                    fighter.WeaponId = ChosenWeapon.Id;
                    M.SaveChanges();
                }
                currviewmodel.SelectedFighter.GetWeapon(ChosenWeapon);
                currviewmodel.SelectedFighter = currviewmodel.SelectedFighter;
                ChosenWeapon.IsWearing = true;
                T.SaveChanges();
                WeaponList.Visibility = Visibility.Hidden;
            }
        }

        public Button SetAArmorElement(Armor armor)
        {
            Button ArmorImageBlock = new Button();
            ArmorImageBlock.Height = 90;
            ArmorImageBlock.Width = 90;
            ArmorImageBlock.Content = armor.Call;
            ArmorImageBlock.Background = null;
            ArmorImageBlock.BorderBrush = Brushes.Black;
            ArmorImageBlock.Name = "ad" + armor.Id;
            //WeaponImageBlock.CommandParameter = 
            switch(armor.type)
            {
                case "Helmet":
                    ArmorImageBlock.Click += ChooseHelmet;

                    break;
                case "BodyArmor":
                    ArmorImageBlock.Click += ChooseBodyArmor;

                    break;
                case "FeetArmor":
                    ArmorImageBlock.Click += ChooseFeetArmor;

                    break;
                default: break;

            }

            return ArmorImageBlock;
        }

        public void ChooseHelmet(object sender, RoutedEventArgs e)
        {
            using (ArmorContext T = new ArmorContext())
            {
                Armor ChosenHelmet = T.Helmets.Find(Convert.ToInt32(((Button)sender).Name.Substring(2)));

                using (FighterContext M = new FighterContext())
                {
                    Fighter fighter = M.Fighters.Find(currviewmodel.SelectedFighter.FighterId);

                    if (fighter.HelmetId != null)
                    {
                        Armor currarmor = T.Helmets.Find(fighter.HelmetId);
                        currarmor.IsWearing = false;
                        fighter.Armor -= currarmor.ArmorPoints;
                    }
                    fighter.HelmetId = ChosenHelmet.Id;
                    fighter.Armor += ChosenHelmet.ArmorPoints;
                    M.SaveChanges();
                }
                currviewmodel.SelectedFighter.Helmet = ChosenHelmet as Helmet;
                currviewmodel.SelectedFighter = currviewmodel.SelectedFighter;
                ChosenHelmet.IsWearing = true;
                T.SaveChanges();
                WeaponList.Visibility = Visibility.Hidden;
            }
        }

        public void ChooseBodyArmor(object sender, RoutedEventArgs e)
        {
            using (ArmorContext T = new ArmorContext())
            {
                Armor ChosenBodyArmor = T.BodyArmors.Find(Convert.ToInt32(((Button)sender).Name.Substring(2)));

                using (FighterContext M = new FighterContext())
                {
                    Fighter fighter = M.Fighters.Find(currviewmodel.SelectedFighter.FighterId);

                    if (fighter.BodyArmorId != null)
                    {
                        Armor currarmor = T.BodyArmors.Find(fighter.HelmetId);
                        currarmor.IsWearing = false;
                        fighter.Armor -= currarmor.ArmorPoints;
                    }
                    fighter.BodyArmorId = ChosenBodyArmor.Id;
                    fighter.Armor += ChosenBodyArmor.ArmorPoints;
                    M.SaveChanges();
                }
                currviewmodel.SelectedFighter.BodyArmor = ChosenBodyArmor as BodyArmor;
                currviewmodel.SelectedFighter = currviewmodel.SelectedFighter;
                ChosenBodyArmor.IsWearing = true;
                T.SaveChanges();
                WeaponList.Visibility = Visibility.Hidden;
            }
        }

        public void ChooseFeetArmor(object sender, RoutedEventArgs e)
        {
            using (ArmorContext T = new ArmorContext())
            {
                Armor ChosenFeetArmor = T.FeetArmors.Find(Convert.ToInt32(((Button)sender).Name.Substring(2)));

                using (FighterContext M = new FighterContext())
                {
                    Fighter fighter = M.Fighters.Find(currviewmodel.SelectedFighter.FighterId);

                    if (fighter.FeetArmorId != null)
                    {
                        Armor currarmor = T.FeetArmors.Find(fighter.HelmetId);
                        currarmor.IsWearing = false;
                        fighter.Armor -= currarmor.ArmorPoints; 
                    }
                    fighter.BodyArmorId = ChosenFeetArmor.Id;
                    fighter.Armor += ChosenFeetArmor.ArmorPoints;
                    M.SaveChanges();
                }
                currviewmodel.SelectedFighter.FeetArmor = ChosenFeetArmor as FeetArmor;
                currviewmodel.SelectedFighter = currviewmodel.SelectedFighter;
                ChosenFeetArmor.IsWearing = true;
                T.SaveChanges();
                WeaponList.Visibility = Visibility.Hidden;
            }
        }


        private void FeetArmor_Click(object sender, RoutedEventArgs e)
        {
            WeaponList.Visibility = Visibility.Visible;
            WeaponList.Children.Clear();
            IEnumerable<Armor> ArmorElements = from p in currviewmodel.FeetArmors
                                               select p;
            foreach (Armor armor in ArmorElements)
            {
                if (!armor.IsWearing)
                    WeaponList.Children.Add(SetAArmorElement(armor));
            }
        }

        private void BodyArmor_Click(object sender, RoutedEventArgs e)
        {
            WeaponList.Visibility = Visibility.Visible;
            WeaponList.Children.Clear();
            IEnumerable<Armor> ArmorElements = from p in currviewmodel.BodyArmors
                                               select p;
            foreach (Armor armor in ArmorElements)
            {
                if (!armor.IsWearing)
                    WeaponList.Children.Add(SetAArmorElement(armor));
            }
        }

        private void Helmet_Click_1(object sender, RoutedEventArgs e)
        {
            WeaponList.Visibility = Visibility.Visible;
            WeaponList.Children.Clear();
            IEnumerable<Armor> ArmorElements = from p in currviewmodel.Helmets
                                               select p;
            foreach (Armor armor in ArmorElements)
            {
                if (!armor.IsWearing)
                    WeaponList.Children.Add(SetAArmorElement(armor));
            }
        }
    }
}
