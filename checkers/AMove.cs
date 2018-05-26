using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class AMove
    {




        public int[] From = new int[2];
        public int[] To = new int[2];

        public AMove(int f1, int f2, int t1, int t2)
        {
            From = new int[] { f1, f2 };
            To = new int[] { t1, t2 };



        }


        public void printAMove(AMove a)
        {
            Console.WriteLine("(" + a.From[0] + "," + a.From[1] + ") => (" + a.To[0] + "," + a.To[1] + ")");

        }

    }
}   

