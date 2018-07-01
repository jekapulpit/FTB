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
    class RegisterViewModel : INotifyPropertyChanged
    {

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
                        PasswordBox pass = obj as PasswordBox;
                        User NewUser = new User(Login, pass.Password);
                        try
                        {
                            using (UserContext T = new UserContext())
                            {
                                T.Users.Add(NewUser);
                                T.SaveChanges();
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        
                    }
                    ));
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
