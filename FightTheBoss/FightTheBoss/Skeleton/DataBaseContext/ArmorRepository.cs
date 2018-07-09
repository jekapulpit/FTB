using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton.DataBaseContext
{
    public abstract class ArmorRepository<T> : IRepository<T>
    {
        public void Dispose() { }
    }
}
