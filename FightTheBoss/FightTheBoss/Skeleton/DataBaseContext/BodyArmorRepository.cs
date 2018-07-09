using FightTheBoss.ArmorElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    class BodyArmorRepository : IRepository<BodyArmor>
    {
        private GameContext _arm;

        public BodyArmorRepository(GameContext context)
        {
            _arm = context;
        }

        public ObservableCollection<BodyArmor> GetAll(string username)
        {
            IQueryable<BodyArmor> Result = from p in _arm.BodyArmors
                                                     where p.Username == username
                                                     select p;
            ObservableCollection<BodyArmor> Res1 = new ObservableCollection<BodyArmor>();
            foreach (BodyArmor t in Result)
            {
                Res1.Add(t);
            }
            return Res1;
        }
        public void Update()
        {

        }
        public BodyArmor Find(int? id)
        {
            return _arm.BodyArmors.Find(id);
        }

        public void Dispose() { }
       
    }
}
