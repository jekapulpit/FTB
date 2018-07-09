using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    class FighterRepository : IRepository<Fighter>
    {
        private GameContext _fight;

        public FighterRepository(GameContext context)
        {
            _fight = context;
        }

        public ObservableCollection<Fighter> GetAll(string username)
        {
            IQueryable<Fighter> Result = from p in _fight.Fighters
                                           where p.Username == username
                                           select p;
            ObservableCollection<Fighter> Res1 = new ObservableCollection<Fighter>();
            foreach (Fighter t in Result)
            {
                Res1.Add(t);
            }
            return Res1;
        }
        public void Update()
        {

        }
        public Fighter Find(int? id)
        {
            return _fight.Fighters.Find(id);
        }
        public void Remove(Fighter fighter)
        {
            _fight.Fighters.Remove(fighter);
            _fight.SaveChanges();
        }
        public void Add(Fighter fighter)
        {
            _fight.Fighters.Add(fighter);
            _fight.SaveChanges();
        }
        public void Dispose() { }
    }
}
