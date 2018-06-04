using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace checkers
{
    class Player
    {
        public Node tree;
        public int color;
        public int direction; //1 for player 1 who moves 0 to 7, -1 for player 2 who moves 7 to 0.
        public int BoardValue;
        public Form1 f;
        public int wins; //score in this tourament iteration.
        Random rnd = new Random();

        public Player()
        {

        }

        public Player(Form1 form)
        {
            f = form;
            try
            {
                //this.tree = checkers.createTree.buildTree(); //todo: enable this

            } catch(Exception e) { f.printMessageGui("tree exception catch!"); } //TODO: remove this.
        }


        

        
        public virtual AMove ChooseMove(AMove[] options, Board b,Form1 f, Player[] players, int depth)
        {
            if (options == null || options.Length == 0)
                return null;
            //temporary hotfix.
            //int a = rnd.Next(0, options.Length);
            AMove a = Minimax(options, b, depth, players);

            return a;
        }

        public AMove Minimax(AMove[] options , Board b, int depth, Player[] players)
        {
            
            GameMaster g = new GameMaster();
            Board[] boards = new Board[options.Length];
            int[] score = new int[options.Length];
            int bestMove = 0;
            for (int i = 0; i < boards.Length; i++)
            {
                //Console.WriteLine("i is " + i);
                //options[i].printAMove();
                boards[i] = new Board(b);
                g.PerformMove2(boards[i], options[i], color);
                if (players[0] == this)
                    score[i] = Minimax2(boards[i], depth - 1, players, 1);
                else score[i] = Minimax2(boards[i], depth - 1, players, 0);
                if (score[i] > score[bestMove])
                    bestMove = i;
            }
            //Console.WriteLine("score[bestMove] is " + score[bestMove]);
            //Console.Write("options[bestMove] is ");
            //options[bestMove].printAMove();

            return options[bestMove];
            

        }

        public int Minimax2( Board b, int depth, Player[] players,int turn)
        {
            int a;
            GameMaster g = new GameMaster();
            AMove[] options = g.GetAllMoves(b,players[turn]);
            Board[] boards = new Board[options.Length];
            
            for (int i = 0; i < boards.Length; i++)
            {
                boards[i] = new Board(b);
                g.PerformMove2(boards[i], options[i], players[turn].color);
            }

            if (depth <= 0)
            {
                //stoping point for the recursion 
                //call to the heuristic evaluation function to get the value of the board
                // BoardValue = heuristic_evaluation_function(b); //aka eti function
                BoardValue = rnd.Next(-20, 20); // TODO: call eti function!!! //public double etiFunction(Board b) runs evaluation funcs on a board according to tree.
                //Console.WriteLine("depth is "+depth+"val is "+BoardValue);
                return BoardValue;
            }
            //if its the current player's turn
            if (this.color == players[turn].color)
            {

                a = int.MinValue;//-infinity
                for (int i = 0; i < boards.Length; i++)
                {
                    a = Math.Max(a, Minimax2(boards[i], depth - 1,players, 1 - turn));
                }
                return a;
            }
            //if its the enemy's turn
            else
            {
                
                a = int.MaxValue;//+infinity
                for (int i = 0; i < boards.Length; i++)
                {
                    a = Math.Min(a, Minimax2(boards[i], depth - 1, players ,1 - turn));
                }
                return a;

            }

        }

    }
}
