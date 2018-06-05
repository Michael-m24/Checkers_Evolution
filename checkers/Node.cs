using System.Collections.Generic;
using System;
namespace checkers
{
    public abstract class Node
    {
        public static int MAX_SIZE = 5;
        public static int SIZE = 1;
        public static int SIZE2 = 0;
        private List<double> results;
        public string sign;
        public double value;
        public List<Node> childNodes;
        public Node father;
        abstract public void eval();
        public void eval_tree(Node cur)
        {
            
                if (cur.childNodes != null)
                {
                    
                    foreach (Node i in cur.childNodes)
                        if(i!=null)
                            eval_tree(i);
                    
                }
                cur.eval(); 
        }

        public double etiFunc(List<double> results)
        {
            this.results = results;
            eval_tree(this);
            return this.value; 
        }

        public List<double> getResults() { return results; }

        abstract public void build_sons();
        public void print_tree(Node cur)
        {
           
            if (cur.GetType() == typeof(Leaf)|| cur.GetType() == typeof(Const) || cur.GetType() == typeof(Eval_func))
                return;
            

            if (cur.childNodes !=null)
            {
                Console.WriteLine("Father: " + cur.sign + " has " + (cur.childNodes).Count + " childes");
                
    
                Console.WriteLine("childes: ");
                foreach (Node i in cur.childNodes)
                {

                    Console.WriteLine(i.sign);
                    print_tree(i);
                }
            }
          
        }
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandom(int min, int max)
        {
            
             lock (syncLock)
            {  //synchronize

            return random.Next(min, max);
            }
        }
        public static class Util
        {
            
            public static int GetRandom()
            {
                return random.Next();
            }
        }
        public void bulid_tree(Node cur)
        {

            if (cur.GetType() == typeof(Leaf) || cur.GetType() == typeof(Const) || cur.GetType() == typeof(Eval_func))
                return;

            if (cur.childNodes != null)
            {

                SIZE2 = 0;
                foreach (Node i in cur.childNodes)
                {
                    SIZE2++;
                    

                    i.build_sons();
                    if (SIZE2 == ((cur.childNodes).Count)-1)
                        SIZE++;
                    bulid_tree(i);

                }
            }
                

        }

        public Node mutation(Node second_tree)
        {
            //##########//Random place in seconde tree##########////
            Node runner = second_tree;
            Node runnerF = second_tree;
            int runnerR = 0;

            Node thisRunner = this;
            Node thisRunnerF = this;

            int thisRunnerR = 0;

            int dep = GetRandom(1, SIZE-1);//Until which depth to go

            //While there are childes
            while (runner.childNodes != null && runner.childNodes.Count != 0)  
            {
                
                if (SIZE + dep-- <= SIZE)
                    break;
                runnerF = runner;
                //Go to random chiled
                runnerR = GetRandom(0, runner.childNodes.Count);
                runner = runner.childNodes[runnerR];
            }
            

            //##########Random place in this tree##########//
            dep = GetRandom(1, SIZE-1);
            //While there are childes
            while (thisRunner.childNodes!= null && thisRunner.childNodes.Count!=0)
            {
                
                if (SIZE + dep-- <= SIZE)
                    break;
                thisRunnerF = thisRunner;
                //Go to random chiled
                thisRunnerR = GetRandom(0, thisRunner.childNodes.Count);
                thisRunner = thisRunner.childNodes[thisRunnerR];
            }

            //One of the roots are cond_node and one is not- need to fix this!
            if ( (thisRunner.GetType() == typeof(checkers.cond_node) || runner.GetType() == typeof(checkers.cond_node) ) && thisRunner.GetType() != runner.GetType())
                if (thisRunner.GetType() == typeof(checkers.cond_node))
                    thisRunner = thisRunner.childNodes[GetRandom(0, thisRunner.childNodes.Count)];
                else
                    runner = runner.childNodes[GetRandom(0, runner.childNodes.Count)];


            //randomly chose who is added to who:
            if(GetRandom(0, 100)%2 == 0)
            {
                runnerF.childNodes[runnerR] = thisRunner;
                return runner;
            }
               
            else
            {
                thisRunnerF.childNodes[thisRunnerR] = runner;
                return thisRunner;
            }
        }


    }
}
