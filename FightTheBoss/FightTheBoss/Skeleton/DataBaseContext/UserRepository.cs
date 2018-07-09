using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    class UserRepository : IRepository<User>
    {
        private GameContext _usr;

        public UserRepository(GameContext context)
        {
            _usr = context;
        }

        public ObservableCollection<User> GetAll(string username)
        {
            IQueryable<User> Result = from p in _usr.Users
                                      select p;
            return (Result as ObservableCollection<User>);
        }
        public void Update()
        {

        }
        public User Find(int? id)
        {
            return null;
        }
       
        public bool Check(string username, string pass)
        {
            if ((_usr.Users.Find(username)).Password == pass)
            {
                return true;
            }
            else return false;
        }
        public User FindUser(string Username)
        {
            return _usr.Users.Find(Username);
        }
        public void Add(User user)
        {
            _usr.Users.Add(user);
            _usr.SaveChanges();
        }
        public void Dispose() { }
    }
}
