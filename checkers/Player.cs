using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class Player
    {
        
        public int color;
        public int direction; //1 for player 1 who moves 0 to 7, -1 for player 2 who moves 7 to 0.


        Random rnd = new Random();
        public virtual AMove ChooseMove(AMove[] options, Board b,Form1 f)
        {
            if (options == null || options.Length == 0)
                return null;
            //temporary hotfix.
            int a = rnd.Next(0, options.Length);

            //System.Threading.Thread.Sleep(100);

            return options[a];
        }
        
    }
}
