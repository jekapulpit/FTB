using FightTheBoss.ArmorElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    class HelmetRepository : IRepository<Helmet>
    {

        private GameContext _arm;

        public HelmetRepository(GameContext context)
        {
            _arm = context;
        }

        public ObservableCollection<Helmet> GetAll(string username)
        {
            IQueryable<Helmet> Result = from p in _arm.Helmets
                                           where p.Username == username
                                           select p;
            ObservableCollection<Helmet> Res1 = new ObservableCollection<Helmet>();
            foreach (Helmet t in Result)
            {
                Res1.Add(t);
            }
            return Res1;
        }
        public void Update()
        {

        }
        public Helmet Find(int? id)
        {
            return _arm.Helmets.Find(id);
        }

        public void Dispose() { }
    }
}
