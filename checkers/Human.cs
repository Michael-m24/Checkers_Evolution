using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class Human : Player
    {
        public AMove stored;
        public override  AMove ChooseMove(AMove[] options, Board b, Form1 f)
        {
            AMove ans;
            while (stored == null) //TODO: this is all wrong. find another way to wait for user input
            {
                f.msg("Please enter a move");
                System.Threading.Thread.Sleep(1000);
            }
            ans = stored;
            stored = null;
            return ans;

        }
    }
}
