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
                Console.WriteLine("Round "+i+" ! FIGHT!");
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
                for (int j = i; j < contenders.Length; j++)
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
            Console.WriteLine();
            foreach (Player var in arr)
            {
                Console.Write(var.wins + "  ");
            }
            Console.WriteLine("win array before");
            Array.Sort(arr,
                delegate(Player x, Player y) { return y.wins.CompareTo(x.wins); });
            foreach (Player var in arr)
            {
                Console.Write(var.wins+"  ");
            }
            Console.WriteLine("win array after \n");
            return arr;
        }

        public Player[] NextGen(Player[] arr)
        {
            //TODO: test, add eti mutation?

            Player[] ans = new Player[contenders.Length];
            ans = contenders;
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
             */
            for (int i = 0; i < contenders.Length; i++)
            {
                int a = rnd.Next(0, 4);
                ans[i] = new Player(f,contenders[a].tree);
            }




            
            return ans;
        }

        public void tmp()
        {
            Board b=new Board();
            //b.Empty();

            //b.BoardArray[1, 0] = new Piece(2);
            //b.BoardArray[0, 1] = new Piece(1);

            Player player1 = new Player(f);
            Player player2 = new Player(f);
            player1.color = 1;
            player1.direction = 1;
            player2.color = 2;
            player2.direction = -1;

            Console.WriteLine("1exp="+b.Exposure(player1,b));
            Console.WriteLine("2exp=" + b.Exposure(player2, b));
        }

    }
}
