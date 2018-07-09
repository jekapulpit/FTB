using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    public interface IRepository<T> : IDisposable
    {
        ObservableCollection<T> GetAll(string username);
        T Find(int? id);
        void Update();

    }
}
