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
                WeaponList.Children.Add(SetAWeaponElement(weapon.Id));
            }

        }
        public Button SetAWeaponElement(int id)
        {
            Button WeaponImageBlock = new Button();
            WeaponImageBlock.Height = 90;
            WeaponImageBlock.Width = 90;
            WeaponImageBlock.Content = "ор1" + id;
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
                currviewmodel.SelectedFighter.GetWeapon(ChosenWeapon);
                currviewmodel.SelectedFighter = currviewmodel.SelectedFighter;
                T.SaveChanges();
                WeaponList.Visibility = Visibility.Hidden;
            }
        }
    }
}
