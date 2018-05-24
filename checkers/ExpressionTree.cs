using System;

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

        bool has_right_son(int index) { if ((index + 1) * 3 < tree_arr_size) return true; else return false; }

        bool has_middle_son(int index) { if (index * 3 + 2 < tree_arr_size) return true; else return false; }
        bool has_left_son(int index) { if (index * 3 + 1 < tree_arr_size) return true; else return false; }


        public ExpressionTree()
        {
            Random rnd = new Random();
            tree_arr_size = 20;
            tree_arr = new string[tree_arr_size];
            //Initialize the values array
            for (int i = 0; i < tree_arr_size; i++)
            {
                //If this index is an index of a leaf- put an integer(probability of 30%) or an eval func(probability of 70%)
                if (!(has_right_son(i) || has_middle_son(i) || has_left_son(i)))
                    //70% the leaf will be an eval func:
                    if (rnd.Next(0, 100) < 71)
                        this.tree_arr[i] = eval_funcs[i % 6];
                    //30% the leaf will be a randon number (0-100)
                    else
                        this.tree_arr[i] = rnd.Next(0, 100).ToString();
                //this index is not an index of a leaf- put an operator
                else
                    this.tree_arr[i] = operations[i % 6];
            }

            //Allocate & Initialize the root
            root = new TreeNode(tree_arr[0], true, null, null, null, null);

            build_expression_tree(0, root);

        }
        public void build_expression_tree(int i, TreeNode runner)
        {
            Random rnd = new Random();

            if ((i < tree_arr_size) && (runner != null))
            {
                //Temporary value- the index of the eval func, for leaves the value supposot to be the eval func return value
                if (!(has_right_son(i) || has_middle_son(i) || has_left_son(i)))
                    if (root.get_is_eval_func())
                        runner.value = int.Parse(tree_arr[i]);
                    else
                        runner.value = 1;//need to call to the eval func to get a double.

                //Create left son if exist
                if (has_left_son(i))
                {

                    runner.set_left_son(new TreeNode(tree_arr[i * 3 + 1], true, null, null, null, runner));
                    if (!(is_operator((runner.left).data)))
                        (runner.left).set_is_eval_func(true);

                    runner.set_is_leaf(false);
                    build_expression_tree(i * 3 + 1, runner.left);
                }

                //Create middle son if exist
                if (has_middle_son(i))
                {

                    runner.set_middle_son(new TreeNode(tree_arr[i * 3 + 2], true, null, null, null, runner));
                    if (!(is_operator((runner.middle).data)))
                        (runner.middle).set_is_eval_func(true);

                    runner.set_is_leaf(false);
                    build_expression_tree(i * 3 + 2, runner.middle);
                }

                //Create right son if exist
                if (has_right_son(i))
                {
                    runner.set_right_son(new TreeNode(tree_arr[i * 3 + 3], true, null, null, null, runner));
                    if (!(is_operator((runner.right).data)))
                        (runner.right).set_is_eval_func(true);

                    runner.set_is_leaf(false);
                    build_expression_tree((i + 1) * 3, runner.right);
                }


            }

        }

        public void print_expression_tree(int i, TreeNode runner)
        {
            if (i < tree_arr_size && runner != null)
            {
                if (!(has_right_son(i) || has_middle_son(i) || has_left_son(i)))
                    Console.WriteLine(i + " " + runner.data + " VAL: " + runner.value);
                else
                    Console.WriteLine(i + " " + runner.data + " VAL: " + runner.value);

                if (has_left_son(i))
                    print_expression_tree(i * 3 + 1, runner.left);

                if (has_middle_son(i))
                    print_expression_tree(i * 3 + 2, runner.middle);

                if (has_right_son(i))
                    print_expression_tree((i + 1) * 3, runner.right);
            }

        }

        public void print_tree_arr()
        {
            for (int i = 0; i < tree_arr_size; i++)
            {
                Console.WriteLine(tree_arr[i] + " ");

            }
        }

        public void calc_expression_tree(int i, TreeNode runner)
        {
            if (i < tree_arr_size && runner != null)
            {
                if (!(has_right_son(i) || has_middle_son(i) || has_left_son(i)))
                    Console.WriteLine(i + " " + runner.data + "VAL: " + runner.value);
                else
                    Console.WriteLine(i + " " + runner.data);

                if (has_left_son(i))
                {
                    print_expression_tree(i * 2 + 1, runner.left);
                    runner.value += runner.left.value;
                }


                if (has_middle_son(i))
                {
                    print_expression_tree(i * 2 + 2, runner.middle);
                    runner.value += runner.middle.value;
                }


                if (has_right_son(i))
                {
                    print_expression_tree((i + 1) * 2, runner.right);
                    runner.value += runner.right.value;
                }

            }

        }

        public bool is_operator(string op)
        {
            foreach (string x in operations)
                if (op.Contains(x))
                    return true;
            return false;
        }
    }
}
