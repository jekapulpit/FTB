using FightTheBoss.ArmorElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    class FeetArmorRepository:IRepository<FeetArmor>
    {
        private GameContext _arm;

        public FeetArmorRepository(GameContext context)
        {
            _arm = context;
        }

        public ObservableCollection<FeetArmor> GetAll(string username)
        {
            IQueryable<FeetArmor> Result = from p in _arm.FeetArmors
                                           where p.Username == username
                                           select p;
            ObservableCollection<FeetArmor> Res1 = new ObservableCollection<FeetArmor>();
            foreach (FeetArmor t in Result)
            {
                Res1.Add(t);
            }
            return Res1;
        }
        public void Update()
        {

        }
        public FeetArmor Find(int? id)
        {
            return _arm.FeetArmors.Find(id);
        }

        public void Dispose() { }

    }
}
