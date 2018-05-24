using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class TreeNode
    {
        bool is_leaf;
        public string data;
        public double value;
        public TreeNode right, middle, left, father;
        bool is_eval_func;
        public TreeNode()
        {
            is_eval_func = false;
            is_leaf = true;
            right = null;
            middle = null;
            left = null;
            father = null;
        }

        public TreeNode(string data, bool is_leaf, TreeNode right, TreeNode middle, TreeNode left, TreeNode father)
        {
            this.data = data;
            this.is_leaf = is_leaf;
            this.right = right;
            this.middle = middle;
            this.left = left;
            this.father = father;
            this.value = 0;
            this.is_eval_func = false;
        }

        public TreeNode get_father() { return this.father; }
        public void set_right_son(TreeNode son) { this.right = son; }
        public void set_middle_son(TreeNode son) { this.middle = son; }
        public void set_left_son(TreeNode son) { this.left = son; }
        public void set_is_leaf(bool is_leaf) { this.is_leaf = is_leaf; }
        public void set_data(string op) { this.data = op; }
        public bool get_is_eval_func() { return this.is_eval_func; }
        public void set_is_eval_func(bool is_eval_func) { this.is_eval_func = is_eval_func; }

    }

}
