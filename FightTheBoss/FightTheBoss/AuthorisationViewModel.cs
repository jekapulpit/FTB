using FightTheBoss.Skeleton;
using FightTheBoss.Skeleton.DataBaseContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FightTheBoss
{
    class AuthorisationViewModel : INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }
        public MainWindow Profile { get; set; }
        public Action CloseAction { get; set; }

        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }




        private RelayCommand _LoginButton;
        public RelayCommand LoginButton
        {
            get
            {
                return _LoginButton ??
                    (_LoginButton = new RelayCommand(obj =>
                    {
                        PasswordBox pass = obj as PasswordBox;
                        try
                        {
                            using (UnitOfWork T = new UnitOfWork())
                            {

                                CurrentUser = T.GetUsers().FindUser(Login);
                                if (!(T.GetUsers().Check(CurrentUser.Username, pass.Password.GetHashCode().ToString())))
                                    throw new NullReferenceException();
                            }
                            Profile = new MainWindow(CurrentUser);
                            Profile.Show();
                            CloseAction();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    ));
            }
        }

        private RelayCommand _RegisterButton;
        public RelayCommand RegisterButton
        {
            get
            {
                return _RegisterButton ??
                    (_RegisterButton = new RelayCommand(obj =>
                    {

                    }
                    ));
            }
        }

        public AuthorisationViewModel()
        {
            Login = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
