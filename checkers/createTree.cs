using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{

    class createTree
    {
           checkers.Node. = 0;
            Math_op root = new Math_op();

        root.childNodes = new List<Node>();
            root.build_sons();
            root.bulid_tree(root);
            root.print_tree(root);
            Console.WriteLine("================================================  " + mycheckers.Node.SIZE + " LEVELS  ================================================  ");
            Console.WriteLine("=============================>>>> Total Calculate: " + root.value + " <<<<=============================");
            root.eval_tree(root);

            return root;

    }
}
