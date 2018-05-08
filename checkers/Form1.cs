using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers
{
    public partial class Form1 : Form
    {
        private Human me = new Human();
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 8; i++) label1.Text += i + Environment.NewLine;  //Y axis numbering label

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Board Board1=new Board();
            //Board1.PrintBoard();
            GameMaster GM = new GameMaster(this);
            GM.PvP(new Player(), new Player());
            //GM.tmpp(new Player(), new Player());
            


        }

        public void Print(string txt)
        {
            richTextBox1.Text = txt.Replace("\n", Environment.NewLine); //formating /n to fit form syntax
            richTextBox1.Text = txt.Replace(" ", "  "); //formating /n to fit form syntax
            richTextBox1.Text = string.Format("{0,2}", richTextBox1.Text); //formatting to spacing to be uniform

        }

        private void button2_Click(object sender, EventArgs e)
        {
            me.stored = new AMove(textBox1.Text[0], textBox1.Text[1], textBox2.Text[0], textBox2.Text[1]);
        }

        public void msg(string s)
        {
            textBox3.Text = s;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GameMaster GM = new GameMaster(this);
            GM.PvP(new Player(), me);
            
        }

        //TODO: human player mechanic. 4 hr.
        //TODO: testing game mechanics. 8 hr.
        //TODO: turnament mechanic. 4hr.
        //TODO: assesment functions. 1 hr.
        //TODO: tree building. 4 hr.
        //TODO: genetic cross & genetic mutation. 12 hr.

    }
}
