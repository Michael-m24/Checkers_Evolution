using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    public class Eval_func:Leaf
    {
        List<string> eval_funcs = new List<string> { "f1", "f2", "f3" };

        public Eval_func()
        {
                this.sign = eval_funcs[GetRandom(0, 100) % 3];
            
        }
        public override void eval()//Need to call to the eval func and return it's returned value;
        {
            this.value = 1;
        }
    }
}
