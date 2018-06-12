using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    public class Const:Leaf
    {
        public Const()
        {
            
            this.value = GetRandom(0, 100);
            this.sign = (this.value).ToString();
        }

        public Const(Const old)
        {

            this.value = old.value;
            this.sign =old.sign;
        }

        public override void eval()
        {
           
        }
    }
}
