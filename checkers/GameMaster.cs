using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers
{
    internal class GameMaster
    {
        //
        private Board b;
        private Player player1;
        private Player player2;
        private int turn;
        private Form1 f;
        Random rnd = new Random();
        public GameMaster(Form1 frm)
        {
            f = frm;
        }
        public GameMaster()
        {
            //defult constructor
        }

        public void tmpp(Player p1, Player p2)
        {
            b = new Board();
            b.PrintBoard2(f);
            player1 = p1;
            player2 = p2;
            player1.color = 1;
            player1.direction = 1;
            player2.color = 2;
            player2.direction = -1;

            Player[] players = { p1, p2 };
            Player ans;
            turn = 1;

            p1.Minimax(GetAllMoves(b, p1), b, 3, players);
            
        }

        /*public void HvP(Human h,Player p)
        {   

            turn= rnd.Next(2);
            while (true)
            {
                Console.Write("Please enter your move:");
                string line = Console.ReadLine();
                int a = int.Parse(line);
                line = Console.ReadLine();
                int b = int.Parse(line);

                AMove[] MA = GetAllMoves(b, players[turn]);
                AMove m = players[turn].ChooseMove(MA, b, f, players);
                while (MA != null && m != null && !IsLegalMove(b, m, players[turn])) //will only shoot in case of human player making an illegal move
                {
                    f.msg("the move " + m.From[0] + "," + m.From[1] + " to " + m.To[0] + "," + m.To[1] + " isn't legal.");
                    m = players[turn].ChooseMove(GetAllMoves(b, players[turn]), b, f, players);
                }

                turn = Math.Abs(turn - 1);
            }
        }*/


        public Player PvP(Player p1, Player p2)
        {
            Console.WriteLine("game start!");
            b = new Board();
            b.PrintBoard2(f);
            player1 = p1;
            player2 = p2;
            player1.color = 1;
            player1.direction = 1;
            player2.color = 2;
            player2.direction = -1;
            AMove[] logAMoves = new AMove[400];
            int logInd = 0;

            Player[] players = { p1, p2 };
            Player ans;
            turn = rnd.Next(2);
            while (true) //TODO: put in its own thread
            {
                b.PrintBoard2(f);
                AMove[] MA = GetAllMoves(b, players[turn]);
                AMove m = players[turn].ChooseMove(MA, b, f, players);
                while (MA != null && m != null && !IsLegalMove(b, m, players[turn])) //will only shoot in case of human player making an illegal move
                {
                    f.printMessageGui("the move " + m.From[0] + "," + m.From[1] + " to " + m.To[0] + "," + m.To[1] + " isn't legal.");
                    Thread.Sleep(2000);
                    m = players[turn].ChooseMove(GetAllMoves(b, players[turn]), b, f , players);
                }
                if (m == null) //the player currently playing has no legal move to make
                {

                    //Console.WriteLine("player " + (Math.Abs(turn - 1)+1)+" is the winner! ");
                    f.printMessageGui("player " + (Math.Abs(turn - 1) + 1) + " is the winner! ");
                    for (int i = 0; i < logInd; i++)
                        logAMoves[i].printAMove();
                    return players[Math.Abs(turn - 1)]; //return the player who hasent lost
                }
                PerformMove(b, m, players[turn]);
                logAMoves[logInd] = m;
                logInd++;
                turn = Math.Abs(turn - 1);
            }

        }

        public bool IsLegalMove(Board b, AMove a, Player p)
        {
            bool ans = true;

            if (a.From[0] % 2 == a.From[1] % 2 || a.To[0] % 2 == a.To[1] % 2) //not from a black position or not to a black position
            {
                return false;
            }
            if (a.From[0] < 0 || a.From[0] > 7 || a.From[1] < 0 || a.From[1] > 7) //move from out of board
            {
                return false;
            }
            if (a.To[0] < 0 || a.To[0] > 7 || a.To[1] < 0 || a.To[1] > 7) //move to out of board
            {
                return false;
            }

            if ((b.BoardArray[a.From[0], a.From[1]].color != p.color)) //not from a player positioned piece
            {
                return false;
            }
            if ((b.BoardArray[a.To[0], a.To[1]] != null)) //not to an open space
            {
                return false;
            }

            if ((Math.Abs(a.From[0] - a.To[0])) != (Math.Abs(a.From[1] - a.To[1]))) //not diagonal move
            {
                return false;
            }

            if (b.BoardArray[a.From[0], a.From[1]].GetType() == typeof(Piece)) //its a regular soldier yo!
            {
                if (p.direction * (a.From[1] - a.To[1]) > -1) //not moving in the right direction for the player
                {
                    return false;
                }
                if ((Math.Abs(a.From[0] - a.To[0]) == 2) && (Math.Abs(a.From[1] - a.To[1])) == 2)
                //piece eating step
                {
                    Piece skipped = b.BoardArray[(a.From[0] + a.To[0]) / 2, (a.From[1] + a.To[1]) / 2];
                    if (skipped == null || skipped.color == p.color)
                        //the position being skipped is empty, or occupied by the player currently playing
                        return false;
                }
                if ((Math.Abs(a.From[0] - a.To[0]) > 2) || (Math.Abs(a.From[1] - a.To[1])) > 2)
                // To furter than 2 from From
                {
                    return false;
                }
            }
            else //its a queen yo!
            {
                int dis = Math.Abs(a.To[0] - a.From[0]); //from (3,5) to (0,2) is 3 distance
                int dirx = Math.Sign(a.To[0] - a.From[0]); //in example its negative direction on x axis
                int diry = Math.Sign(a.To[1] - a.From[1]); //in example its negative direction on y axis
                for (int i = 1; i < dis - 1; i++) //start from 1 because the starting position was already checked
                {
                    if (b.BoardArray[a.From[0] + dirx * i, a.From[1] + diry * i] != null) return false; //one of the tiles along the path tha isnt legal for eating was occupied
                }
                if (dis > 1) //there was at least one tile skipped
                {
                    Piece skipped = b.BoardArray[a.To[0] - dirx, a.To[1] - diry];
                    if (skipped != null && skipped.color == p.color)
                        return false; //the tile right before the stopping tile is auccupied by a player piece
                }
            }


            return ans;
        }

        public void PerformMove(Board b1, AMove a, Player p)
        {
            //change the board according to a planned ove. assuming the move is legal.
            if (b1.BoardArray[a.From[0], a.From[1]].GetType() == typeof(Piece)) //if its a regular piece and not a queen
            {

                b1.BoardArray[a.To[0], a.To[1]] = b1.BoardArray[a.From[0], a.From[1]];
                b1.BoardArray[a.From[0], a.From[1]] = null;
                if ((Math.Abs(a.From[0] - a.To[0]) == 2) && (Math.Abs(a.From[1] - a.To[1])) == 2) //regular piece eating step
                {
                    b1.BoardArray[(a.From[0] + a.To[0]) / 2, (a.From[0] + a.To[0]) / 2] = null;
                }
                if (a.To[1] == 0 || a.To[1] == 7) //reaching the furthest row
                {
                    b1.BoardArray[a.To[0], a.To[1]] = new Queen(p.color); //transform the piece to a queen
                }
            }
            else //if its a queen
            {
                b1.BoardArray[a.To[0], a.To[1]] = b1.BoardArray[a.From[0], a.From[1]];
                b1.BoardArray[a.From[0], a.From[1]] = null;
                int x = a.From[0] + Math.Sign(a.To[0] - a.From[0]);
                int y = a.From[1] + Math.Sign(a.To[1] - a.From[1]);
                b1.BoardArray[x, y] = null; //the tile before the To tile is cleared. if there was a piece there it died.

            }


        }

        public void PerformMove2(Board b2, AMove a, int color)
        {
            //change the board according to a planned ove. assuming the move is legal.
            if (b2.BoardArray[a.From[0], a.From[1]].GetType() == typeof(Piece)) //if its a regular piece and not a queen
            {

                b2.BoardArray[a.To[0], a.To[1]] = b2.BoardArray[a.From[0], a.From[1]];
                b2.BoardArray[a.From[0], a.From[1]] = null;
                if ((Math.Abs(a.From[0] - a.To[0]) == 2) && (Math.Abs(a.From[1] - a.To[1])) == 2) //regular piece eating step
                {
                    b2.BoardArray[(a.From[0] + a.To[0]) / 2, (a.From[0] + a.To[0]) / 2] = null;
                }
                if (a.To[1] == 0 || a.To[1] == 7) //reaching the furthest row
                {
                    b2.BoardArray[a.To[0], a.To[1]] = new Queen(color); //transform the piece to a queen
                }
            }
            else //if its a queen
            {
                b2.BoardArray[a.To[0], a.To[1]] = b2.BoardArray[a.From[0], a.From[1]];
                b2.BoardArray[a.From[0], a.From[1]] = null;
                int x = a.From[0] + Math.Sign(a.To[0] - a.From[0]);
                int y = a.From[1] + Math.Sign(a.To[1] - a.From[1]);
                b2.BoardArray[x, y] = null; //the tile before the To tile is cleared. if there was a piece there it died.

            }


        }

        public AMove[] GetAllMoves(Board b, Player p)
        {
            //return an array with all the legal moves a player can make based on the board inputed.
            int ind = 0;
            AMove[] ans = new AMove[200]; //array of possible legal moves

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (b.BoardArray[i, j] != null && b.BoardArray[i, j].color == p.color) //for every piece of the player currently playing
                    {
                        if (b.BoardArray[i, j].GetType() == typeof(Piece)) //its a regular piece
                        {
                            AMove[] possible = new AMove[4];
                            possible[0] = new AMove(i, j, i + 1, j + p.direction); //regular moves
                            possible[1] = new AMove(i, j, i - 1, j + p.direction);
                            possible[2] = new AMove(i, j, i + 2, j + 2 * p.direction); //eating
                            possible[3] = new AMove(i, j, i - 2, j + 2 * p.direction);

                            for (int k = 0; k < 4; k++) //add the legal moves to the ans array
                            {
                                if (possible[k] != null && IsLegalMove(b, possible[k], p))
                                {
                                    ans[ind] = possible[k];
                                    ind++;
                                }
                            }
                        }
                        else //its a queen
                        {
                            int ind2 = 0;
                            AMove[] possible = new AMove[32];
                            for (int r = 1; r < 8; r++) //consider all the possible moves availabe for a queen in (i,j) //TODO: can be  optimised
                            {
                                possible[ind2] = new AMove(i, j, i + r, j + r); ind2++;
                                possible[ind2] = new AMove(i, j, i + r, j - r); ind2++;
                                possible[ind2] = new AMove(i, j, i - r, j + r); ind2++;
                                possible[ind2] = new AMove(i, j, i - r, j - r); ind2++;
                            }
                            for (int k = 0; k < ind2; k++) //add the legal moves to the ans array
                            {
                                if (possible[k] != null && IsLegalMove(b, possible[k], p))
                                {
                                    ans[ind] = possible[k];
                                    ind++;
                                }
                            }
                        }

                    }
                }
            //copy the legal moves array to a tmp array so there are non null slots
            AMove[] tmp = new AMove[ind];
            for (int i = 0; i < ind; i++)
            {
                tmp[i] = ans[i];
            }
            //Console.WriteLine("possible moves for player "+(turn+1)+": "+tmp.Length);
            return tmp;
        }


    }
}


