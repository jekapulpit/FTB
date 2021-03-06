﻿using FightTheBoss.ArmorElements;
using FightTheBoss.Skeleton;
using FightTheBoss.Skeleton.DataBaseContext;
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
            using(UnitOfWork T = new UnitOfWork())
            {
                foreach(Weapon weapon in T.GetWeapons().GetAll(currviewmodel.CurrentUser.Username))
                {
                    if (!(weapon.IsWearing))
                       WeaponList.Children.Add(SetAWeaponElement(weapon));
                }
            }

        }
        private void FeetArmor_Click(object sender, RoutedEventArgs e)
        {
            WeaponList.Visibility = Visibility.Visible;
            WeaponList.Children.Clear();
            using (UnitOfWork T = new UnitOfWork())
            {
                foreach (FeetArmor helmet in T.GetFeetArmor().GetAll(currviewmodel.CurrentUser.Username))
                {
                    if (!(helmet.IsWearing))
                        WeaponList.Children.Add(SetAArmorElement(helmet));
                }
            }
        }
        private void BodyArmor_Click(object sender, RoutedEventArgs e)
        {
            WeaponList.Visibility = Visibility.Visible;
            WeaponList.Children.Clear();
            using (UnitOfWork T = new UnitOfWork())
            {
                foreach (BodyArmor helmet in T.GetBodyArmor().GetAll(currviewmodel.CurrentUser.Username))
                {
                    if (!(helmet.IsWearing))
                        WeaponList.Children.Add(SetAArmorElement(helmet));
                }
            }
        }
        private void Helmet_Click_1(object sender, RoutedEventArgs e)
        {
            WeaponList.Visibility = Visibility.Visible;
            WeaponList.Children.Clear();
            using (UnitOfWork T = new UnitOfWork())
            {
                foreach (Helmet helmet in T.GetHelmets().GetAll(currviewmodel.CurrentUser.Username))
                {
                    if (!(helmet.IsWearing))
                        WeaponList.Children.Add(SetAArmorElement(helmet));
                }
            }
        }
        public void Chooseweapon(object sender, RoutedEventArgs e)
        {
            using(UnitOfWork T = new UnitOfWork())
            {
                T.GetFighters().Equip(currviewmodel.SelectedFighter, Convert.ToInt32(((Button)sender).Name.Substring(2)));
            }
            currviewmodel.UpdateLists();
            WeaponList.Visibility = Visibility.Hidden;
        }

       

        public void ChooseHelmet(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork T = new UnitOfWork())
            {
                T.GetFighters().TakeOn(currviewmodel.SelectedFighter, T.GetHelmets().Find(Convert.ToInt32(((Button)sender).Name.Substring(2))));
            }
            currviewmodel.UpdateLists();

            WeaponList.Visibility = Visibility.Hidden;
        }

        public void ChooseBodyArmor(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork T = new UnitOfWork())
            {
                T.GetFighters().TakeOn(currviewmodel.SelectedFighter, T.GetBodyArmor().Find(Convert.ToInt32(((Button)sender).Name.Substring(2))));
            }
            currviewmodel.UpdateLists();

            WeaponList.Visibility = Visibility.Hidden;
        }

        public void ChooseFeetArmor(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork T = new UnitOfWork())
            {
                T.GetFighters().TakeOn(currviewmodel.SelectedFighter, T.GetFeetArmor().Find(Convert.ToInt32(((Button)sender).Name.Substring(2))));
            }
            currviewmodel.UpdateLists();

            WeaponList.Visibility = Visibility.Hidden;
        }


       

       
        public Button SetAArmorElement(Armor armor)
        {
            Button ArmorImageBlock = new Button();
            ArmorImageBlock.Height = 90;
            ArmorImageBlock.Width = 90;
            ArmorImageBlock.Content = armor.Call;
            ArmorImageBlock.Background = null;
            ArmorImageBlock.BorderBrush = Brushes.Black;
            ToolTip hint = new ToolTip()
            {
                Width = 170,
                Height = 100
            };
            StackPanel hintcontent = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Canvas img = new Canvas()
            {
                Width = 70,
                Height = 70,
                Background = new SolidColorBrush(Colors.Red)
            };
            StackPanel quals = new StackPanel()
            {
                Orientation = Orientation.Vertical,

            };
            TextBlock text1 = new TextBlock()
            {
                Margin = new Thickness(5, 5, 5, 5),
                Text = armor.Call,
                FontSize = 16

            };
            TextBlock text2 = new TextBlock()
            {
                Margin = new Thickness(5, 5, 5, 5),
                Text = "Броня: " + armor.ArmorPoints,
                FontSize = 13
            };
            quals.Children.Add(text1);
            quals.Children.Add(text2);
            hintcontent.Children.Add(img);
            hintcontent.Children.Add(quals);
            hint.Content = hintcontent;
            ArmorImageBlock.ToolTip = hint;
            //WeaponImageBlock.CommandParameter = 
            switch (armor.type)
            {
                case "Helmet":
                    ArmorImageBlock.Click += ChooseHelmet;
                    ArmorImageBlock.Name = "ah" + armor.Id;


                    break;
                case "BodyArmor":
                    ArmorImageBlock.Click += ChooseBodyArmor;
                    ArmorImageBlock.Name = "ab" + armor.Id;

                    break;
                case "FeetArmor":
                    ArmorImageBlock.Click += ChooseFeetArmor;
                    ArmorImageBlock.Name = "af" + armor.Id;

                    break;
                default: break;

            }

            return ArmorImageBlock;
        }
        public Button SetAWeaponElement(Weapon weapon)
        {
            Button WeaponImageBlock = new Button();
            WeaponImageBlock.Height = 90;
            WeaponImageBlock.Width = 90;
            WeaponImageBlock.Content = weapon.Call;
            WeaponImageBlock.Background = null;
            WeaponImageBlock.BorderBrush = Brushes.Black;
            WeaponImageBlock.Name = "id" + weapon.Id;
            ToolTip hint = new ToolTip()
            {
                Width = 170,
                Height = 100
            };
            StackPanel hintcontent = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Canvas img = new Canvas()
            {
                Width = 70,
                Height = 70,
                Background = new SolidColorBrush(Colors.Red)
            };
            StackPanel quals = new StackPanel()
            {
                Orientation = Orientation.Vertical,

            };
            TextBlock text1 = new TextBlock()
            {
                Margin = new Thickness(5, 5, 5, 5),
                Text = weapon.Call,
                FontSize  = 16

            };
            TextBlock text2 = new TextBlock()
            {
                Margin = new Thickness(5, 5, 5, 5),
                Text = "Урон: " + weapon.damage,
                FontSize = 13
            };
            quals.Children.Add(text1);
            quals.Children.Add(text2);
            hintcontent.Children.Add(img);
            hintcontent.Children.Add(quals);
            hint.Content = hintcontent;
            WeaponImageBlock.ToolTip = hint;
            //WeaponImageBlock.CommandParameter = 
            WeaponImageBlock.Click += Chooseweapon;

            return WeaponImageBlock;
        }
    }
}
