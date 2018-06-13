﻿using System;
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

        public Player Go(int size, int itr)
        {

            contenders = new Player[size];
            for (int i = 0; i < size; i++)
            {
                contenders[i] = new Player(f);
            }
            iterations = itr;

            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine("Round "+i+" ! FIGHT!");
                contenders = RoundV2(i);
            }
            f.printMessageGui("The tournament is over!");
            contenders = OrderByRound(contenders);
            return contenders[0];
        }

        public Player[] RoundV1()
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
            ans = NextGenV1(ans);
            for (int i = 0; i < contenders.Length; i++)
            {
                contenders[i].wins = 0;
            }
            return ans;

        }

        public Player[] RoundV2(int round)
        {

            Player[] ans = NextGenV2(contenders,round);
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

        public Player[] OrderByRound(Player[] group)
        {
            Console.WriteLine("orderround start");
            for (int i = 0; i < group.Length; i++)
            {
                group[i].wins = 0;
            }
            for (int i = 0; i < group.Length; i++)
            {
                for (int j = i; j < group.Length; j++)
                {
                    if (i != j)//dont play against yourself lol
                    {
                        Player p = GM.PvP(group[i], group[j], false);
                        if (p != null && p == group[i]) group[i].wins++; //whoever wins gets a point
                        if (p != null && p == group[j]) group[j].wins++;
                        //if its a null its a tie
                        if(group[i].wins>4||group[j].wins>4)
                        {

                        }


                    }
                }
            }
            Player[] ans = OrderByWins(group);
            for (int i = 0; i < group.Length; i++)
            {
                group[i].wins = 0;
            }
            Console.WriteLine("orderround finish");
            return ans;
        }
   
        public Player[] NextGenV1(Player[] arr)
        {
            //TODO: test

            Player[] ans = new Player[contenders.Length];

            
            int all = contenders.Length;
            int top = (int)Math.Ceiling(contenders.Length * 0.1);
            int good = (int)Math.Ceiling(contenders.Length * 0.2) + top;
            int rest = all - good;

            Console.WriteLine("top " + top + " good " + good + " rest " + rest);
            for (int i = 0; i < all; i++)
            {
                ans[i] = new Player(f);
            }

            //ans = contenders;
            //chhose 5, best 2 chance cross chance mutation
            for (int i = good; i < all; i++)
            {
                int a = rnd.Next(all);
                int b = rnd.Next(good);
                ans[i].Plant(contenders[a].tree.mutation(contenders[b].tree));
            }

            for (int i = top; i < good; i++)
            {
                int a = rnd.Next(top);
                int b = rnd.Next(good);
                ans[i].Plant(contenders[a].tree.mutation(contenders[b].tree));
            }

            for (int i = 0; i < top; i++)
            {
                int a = rnd.Next(0, top);
                ans[i].Plant(contenders[a].tree.mutation(contenders[a].tree));
            }

            return ans;
        }

        public Player[] NextGenV2(Player[] arr,int round)
        {
            Console.WriteLine("gen start");
            int mutationChance = 40;
            int crossChance = 10;
            int size = contenders.Length;
            Player[] ans = new Player[size];

            for (int i = 0; i < size; i++)
            {
                ans[i] = new Player(f);
                
                Player[] grouping = new Player[5];
                int[] chosenForGroup = new int[size];
                for (int j = 0; j < 5; j++)
                {
                    int b = rnd.Next(size);
                    if (chosenForGroup[b] == 0)
                    {
                        Console.WriteLine(b + " picked! ");
                        chosenForGroup[b] = 1;
                        grouping[j] = contenders[b];
                        grouping[j].wins = 0;
                    }
                    else
                    {
                        Console.WriteLine(b + " picked already! ");
                        j--;
                    }
                }

                grouping = OrderByRound(grouping);

                Console.WriteLine("round "+round+" contender "+i+" chosen.");

                int a = rnd.Next(100);
                Console.WriteLine("randomized " + a);
                if (a < crossChance)
                {
                    ans[i].Plant(grouping[0].tree.mutation(grouping[1].tree)); //cross
                }
                else if (a < crossChance + mutationChance)
                {
                    ans[i].Plant(grouping[0].tree.mutation(grouping[0].tree)); //mutation
                }
                else
                {
                    ans[i].Plant(ans[0].tree.copy_tree(ans[0].tree, new Math_op())); //elitism-ish
                }
                Console.WriteLine("finished spawning");
            }
            Console.WriteLine("gen finish");
            return ans;
        }



        public void tmp(Player[] arr, int round)
        {
            
        }

    }
}
