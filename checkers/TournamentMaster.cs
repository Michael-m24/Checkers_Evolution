using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers
{
    class TournamentMaster
    {
        public Player[] contenders;// = new Player[100];
        public int iterations;
        private Form1 f;
        private GameMaster GM;
        Random rnd = new Random();

        public TournamentMaster(int size, int itr,Form1 frm)
        {
            
            contenders=new Player[size];
            for (int i = 0; i < size; i++)
            {
                contenders[i]=new Player(frm);
            }
            iterations = itr;
            f = frm;
            GM=new GameMaster(frm);
        }

        public Player Go()
        {
            for (int i = 0; i < iterations; i++)
            {
                contenders = Round();
            }
            f.printMessageGui("The tournament is over!");
            return contenders[0];
        }
        public Player[] Round()
        {
            for (int i = 0; i < contenders.Length; i++)
            {
                contenders[i].wins = 0;
            }
            for (int i = 0; i < contenders.Length; i++)
            {
                for (int j = 0; j < contenders.Length; j++)
                {
                    if (i != j)//dont play against yourself lol
                    {
                        Player p = GM.PvP(contenders[i], contenders[j],false);
                        if (p != null && p == contenders[i]) contenders[i].wins++; //whoevr wins gets a point
                        if (p != null && p == contenders[j]) contenders[j].wins++;
                        //if its a null its a tie

                    }
                }
            }
            Player[] ans = OrderByWins(contenders);
            ans = NextGen(ans);
            return ans;

        }

        public Player[] OrderByWins(Player[] arr)
        { 
            Array.Sort(arr,
                delegate(Player x, Player y) { return y.wins.CompareTo(x.wins); });
            return arr;
        }

        public Player[] NextGen(Player[] arr)
        {
            //TODO: test, add eti mutation?

            Player[] ans = new Player[contenders.Length];
            /*
            for (int i = 0; i < contenders.Length*0.8; i++)
            {
                int a = rnd.Next(0, (int)contenders.Length*0.2);
                int b = rnd.Next(14, 99);
                ans[i] = EtiCross(contenders[a], contenders[b]);
            }
            for (int i = 0; i < contenders.Length*0.1; i++)
            {
                int a = rnd.Next(0, (int)contenders.Length*0.1);
                int b = rnd.Next(0, (int)contenders.Length*0.2);
                ans[i] = EtiCross(contenders[a], contenders[b]);
            }
            for (int i = 0; i < contenders.Length*0.1; i++)
            {
                int a = rnd.Next(0, 4);
                ans[i] = Etimutate(contenders[a]);
            }




            */
            return ans;
        }

        public void tmp()
        {
            for (int i = 0; i < contenders.Length; i++)
            {
                contenders[i].wins = rnd.Next(0, 20);
            }
            for (int i = 0; i < contenders.Length; i++)
            {
                Console.WriteLine(contenders[i].wins);
            }
            Console.WriteLine("order");
            OrderByWins(contenders);
            for (int i = 0; i < contenders.Length; i++)
            {
                Console.WriteLine(contenders[i].wins);
            }
        }

    }
}
