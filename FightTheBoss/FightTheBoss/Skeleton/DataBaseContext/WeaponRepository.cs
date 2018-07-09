using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    class WeaponRepository : IRepository<Weapon>
    {
        private GameContext _weap;

        public WeaponRepository(GameContext context)
        {
            _weap = context;
        }

        public ObservableCollection<Weapon> GetAll(string username)
        {
            IQueryable<Weapon> Result = from p in _weap.Weapons
                                           where p.Username == username
                                           select p;
            ObservableCollection<Weapon> Res1 = new ObservableCollection<Weapon>();
            foreach (Weapon t in Result)
            {
                Res1.Add(t);
            }
            return Res1;
        }
        public void Update()
        {

        }
        public Weapon Find(int? id)
        {
            return _weap.Weapons.Find(id);
        }

        public void Dispose() { }
    }
}
