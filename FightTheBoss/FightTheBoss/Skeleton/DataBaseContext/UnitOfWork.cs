using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    class UnitOfWork : IDisposable
    {
        private BodyArmorRepository bodyArmor;
        private FeetArmorRepository feetArmor;
        private HelmetRepository helmet;
        private FighterRepository fighter;
        private WeaponRepository weapon;
        private UserRepository user;

        private GameContext _db = new GameContext();

        public BodyArmorRepository GetBodyArmor()
        {
            return bodyArmor ?? (bodyArmor = new BodyArmorRepository(_db));
        }
        public FeetArmorRepository GetFeetArmor()
        {
            return feetArmor ?? (feetArmor = new FeetArmorRepository(_db));
        }
        public HelmetRepository GetHelmets()
        {
            return helmet ?? (helmet = new HelmetRepository(_db));
        }
        public FighterRepository GetFighters()
        {
            return fighter ?? (fighter = new FighterRepository(_db));
        }
        public WeaponRepository GetWeapons()
        {
            return weapon ?? (weapon = new WeaponRepository(_db));
        }
        public UserRepository GetUsers()
        {
            return user ?? (user = new UserRepository(_db));
        }

        public void Save()
        {
            _db.SaveChanges(); 
        }
        public void Dispose() { }
    }
}
