using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class ExpressionTree
    {
        //The internal nodes
        string[] operations = { "+", "-", "*", "/", "sin", "cos" };
        //The Variables- Only Leaves
        string[] eval_funcs = { "f1", "f2", "f3", "f4", "f5", "f6" };

        public int tree_arr_size;
        public string[] tree_arr;
        public TreeNode root;

        bool has_right_son(int index) { if ((index + 1) * 2 < tree_arr_size) return true; else return false; }
        bool has_left_son(int index) { if (index * 2 + 1 < tree_arr_size) return true; else return false; }


        public ExpressionTree()
        {
            tree_arr_size = 10;
            tree_arr = new string[tree_arr_size];
            //Initialize the values array
            for (int i = 0; i < tree_arr_size; i++)
            {
                if (!(has_right_son(i) || has_left_son(i)))
                    this.tree_arr[i] = eval_funcs[i % 6];
                else
                    this.tree_arr[i] = operations[i % 6];
            }

            //Allocate & Initialize the root
            root = new TreeNode(tree_arr[0], true, null, null, null);

            build_expression_tree(0, root);
        }
        public void build_expression_tree(int i, TreeNode runner)
        {

            if (i < tree_arr_size && runner != null)
            {
                //Temporary value of leaves is the index of the eval func
                if (!(has_right_son(i) || has_right_son(i)))
                    runner.value = runner.data[1];/////////////

                //Create right son if exist
                if (has_right_son(i))
                {
                    runner.set_right_son(new TreeNode(tree_arr[(i + 1) * 2], true, null, null, runner));
                    runner.set_is_leaf(false);
                    build_expression_tree((i + 1) * 2, runner.right);
                }

                //Create right son if exist
                if (has_left_son(i))
                {
                    runner.set_left_son(new TreeNode(tree_arr[i * 2 + 1], true, null, null, runner));
                    runner.set_is_leaf(false);
                    build_expression_tree(i * 2 + 1, runner.left);
                }
            }

        }

        public void print_expression_tree(int i, TreeNode runner)
        {
            if (i < tree_arr_size && runner != null)
            {
                if (!(has_right_son(i) || has_right_son(i)))
                    Console.WriteLine(i + " " + runner.data + "VAL: " + runner.value);
                else
                    Console.WriteLine(i + " " + runner.data);

                if (has_left_son(i))
                    print_expression_tree(i * 2 + 1, runner.left);

                if (has_right_son(i))
                    print_expression_tree((i + 1) * 2, runner.right);
            }

        }

        public void print()
        {
            for (int i = 0; i < tree_arr_size; i++)
            {
                Console.WriteLine(tree_arr[i] + " ");

            }
        }
    }
}
