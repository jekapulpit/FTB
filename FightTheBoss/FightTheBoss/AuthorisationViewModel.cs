using FightTheBoss.Skeleton;
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
                        using(UserContext T = new UserContext())
                        {
                            try
                            {
                                CurrentUser = T.Users.Find(Login);
                                if (CurrentUser.Password != pass.Password.GetHashCode().ToString())
                                    throw new NullReferenceException();
                                else
                                    MessageBox.Show(CurrentUser.Username);
                            }
                            catch(NullReferenceException)
                            {
                                MessageBox.Show("Неверное имя пользователя или пароль!");
                            }
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
