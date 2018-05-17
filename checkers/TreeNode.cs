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
        public float value;
        public TreeNode right, left,middle, father;
        public TreeNode()
        {
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
        }

        public TreeNode get_father() { return this.father; }
        public void set_right_son(TreeNode son) { this.right = son; }
        public void set_middle_son(TreeNode son) { this.middle = son; }
        public void set_left_son(TreeNode son) { this.left = son; }
        public void set_is_leaf(bool is_leaf) { this.is_leaf = is_leaf; }

    }

}
