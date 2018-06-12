using System.Collections.Generic;
using System;
namespace checkers
{
    public abstract class Node
    {
        public static int MAX_SIZE = 5;
        public static int SIZE = 1;
        public static int SIZE2 = 0;
        public static List<double> results;
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
            //this.results = null;
           // this.results = new List<double>();
            setResults(results);
            eval_tree(this);
            return this.value; 
        }
        public static void setResults(List<double> results)
        {
            Node.results = results;
        }
        public List<double> getResults() { return Node.results; }

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

                if(min>max)
                {
                    int tmp = min;
                    min = max;
                    max = tmp;
                }
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

            return Create_mutation_tree(runner, runnerF, thisRunner, thisRunnerF, runnerR, thisRunnerR);


        }
        
      public Node Create_mutation_tree(Node runner, Node runnerF, Node thisRunner, Node thisRunnerF, int runnerR, int thisRunnerR)
        {


            //randomly chose who is added to who:
            if (GetRandom(0, 100) % 2 == 0)
            {

                return copy_tree(runnerF, new Math_op()).childNodes[runnerR] = thisRunner;/////////////////////////////////////
                
            }

            else
            {
                return copy_tree(thisRunnerF,new Math_op()).childNodes[thisRunnerR] = runner;
               
            }
        }
        public Node copy_tree(Node Old, Node New)
        {

            if (Old.GetType() == typeof(Const))
                New=(new Const((Const)Old));
 
            else if (Old.GetType() == typeof(Eval_func))
                New=(new  Eval_func((Eval_func)Old));

            else if (Old.GetType() == typeof(If))
                New=((new If((If)Old)));

            else if (Old.GetType() == typeof(Math_op))
                New=(new Math_op((Math_op)Old));

            else if(Old.GetType()==typeof(cond_node))
                New=(new cond_node((cond_node)Old));

            int amout_of_childes = Old.childNodes != null ? Old.childNodes.Count : 0;
           
            for(int j=0; j< amout_of_childes; j++)
                    New.childNodes.Add(copy_tree(Old.childNodes[j], new Math_op()));
                
            return New;
        }

    }
    
}

/*
   if (cur.GetType() == typeof(Const))
            {
                Const A= new Const();
                A.value = cur.value;
                A.sign = cur.sign;
                return A;
            }
                
            if (cur.GetType() == typeof(Eval_func))
            {
                Eval_func A = new Eval_func();
                A.value = cur.value;
                A.sign = cur.sign;
                return A;
            }
 */
