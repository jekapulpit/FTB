using FightTheBoss.ArmorElements;
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
            _fight.Fighters.Remove(_fight.Fighters.Find(fighter.FighterId));
            _fight.SaveChanges();
        }
        public void Add(Fighter fighter)
        {
            _fight.Fighters.Add(fighter);
            _fight.SaveChanges();
        }
        public void Equip(Fighter fighter, int id)
        {
            Fighter Fighter = _fight.Fighters.Find(fighter.FighterId);
            Weapon Weapon = _fight.Weapons.Find(id);
            if(Fighter.WeaponId != null)
            {
                Weapon Weapon1 = _fight.Weapons.Find(Fighter.WeaponId);
                Weapon1.IsWearing = false;
            }
            Fighter.WeaponId = Weapon.Id;
            Weapon.IsWearing = true;

            _fight.SaveChanges();
        }
        public void TakeOn(Fighter fighter, Armor armor)
        {
            Armor Armor;
            Fighter Fighter = _fight.Fighters.Find(fighter.FighterId);

            switch (armor.type)
            {
                case ("Helmet"):
                    Armor = _fight.Helmets.Find(armor.Id);
                    if (Fighter.HelmetId != null)
                    {
                        Helmet Helmet = _fight.Helmets.Find(Fighter.HelmetId);
                        Helmet.IsWearing = false;
                    }
                    Fighter.HelmetId = Armor.Id;
                    Armor.IsWearing = true;
                    break;
                case ("BodyArmor"):
                    Armor = _fight.BodyArmors.Find(armor.Id);
                    if (Fighter.BodyArmorId != null)
                    {
                        BodyArmor bodyArmor = _fight.BodyArmors.Find(Fighter.BodyArmorId);
                        bodyArmor.IsWearing = false;
                    }
                    Fighter.BodyArmorId = Armor.Id;
                    Armor.IsWearing = true;
                    break;
                case ("FeetArmor"):
                    Armor = _fight.FeetArmors.Find(armor.Id);
                    if (Fighter.FeetArmorId != null)
                    {
                        FeetArmor feet = _fight.FeetArmors.Find(Fighter.FeetArmorId);
                        feet.IsWearing = false;
                    }
                    Fighter.FeetArmorId = Armor.Id;
                    Armor.IsWearing = true;
                    break;
                default: break;
            }
            _fight.SaveChanges();

        }
        public void Dispose() { }
    }
}
