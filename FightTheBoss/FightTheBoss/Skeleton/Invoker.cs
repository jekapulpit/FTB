using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton
{
    class Invoker
    {
        public List<Command> CommandQueue = new List<Command>(); // Очередь комманд или последовательность действий  

        public void SetCommand(Command c) // добавить действие
        {
            CommandQueue.Add(c);
        }

        public void Run()  // запустить 
        {
           foreach (Command command in CommandQueue)
            {
                command.Execute();
            }
        }

        public void Cancel(Command command) // отменить ход
        {
            CommandQueue.Clear();
        }
    }
}
