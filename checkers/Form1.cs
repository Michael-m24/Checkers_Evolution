using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers 

{
    public partial class Form1 : Form
    {
        private BackgroundWorker backgroundWorker1;
        private BackgroundWorker backgroundWorker2;
        private bool moveIsClicked=false;
        private Human me = new Human();
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 8; i++) label1.Text += i + Environment.NewLine;  //Y axis numbering label
           

        }
        delegate void StringPrintDelegate(string text);
        delegate void clearTextBoxes();
        private void button1_Click(object sender, EventArgs e)
        {
            //Board Board1=new Board();
            //Board1.PrintBoard();
            TournamentMaster TM=new TournamentMaster(100,10,this);
            GameMaster GM = new GameMaster(this);
            //GM.PvP(new Player(), new Player());
            //GM.tmpp(new Player(), new Player());
            backgroundWorker2 = new BackgroundWorker();
            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker2.DoWork += backgroundWorker2_DoWork;
            if (backgroundWorker2.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker2.RunWorkerAsync(GM);
            }

        }

        public void Print(string txt)
        {

            if (this.textBox1.InvokeRequired)
            {
                StringPrintDelegate d = new StringPrintDelegate(Print);
                this.Invoke(d, new object[] { txt });
                
            }
            else
            {
                richTextBox1.Text = txt.Replace("\n", Environment.NewLine);
                richTextBox1.Text = txt.Replace(" ", "  ");
                richTextBox1.Text = string.Format("{0,2}", richTextBox1.Text);
            }

           /* richTextBox1.Invoke(new Action(() => richTextBox1.Text = txt.Replace("\n", Environment.NewLine)));
           // richTextBox1.Text = txt.Replace("\n", Environment.NewLine); //formating /n to fit form syntax
            richTextBox1.Invoke(new Action(() => richTextBox1.Text = txt.Replace(" ", "  "))); //formating /n to fit form syntax
            richTextBox1.Invoke(new Action(() => richTextBox1.Text = string.Format("{0,2}", richTextBox1.Text))); //formatting to spacing to be uniform
            */
        }


        public void printMessageGui(string txt)
        {
            if (this.textBox3.InvokeRequired)
            {
                StringPrintDelegate d = new StringPrintDelegate(printMessageGui);
                this.Invoke(d, new object[] { txt });

            }
            else
            {
                textBox3.Clear();
                this.msg(txt);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //me.stored = new AMove(textBox1.Text[0], textBox1.Text[1], textBox2.Text[0], textBox2.Text[1]);
            this.moveIsClicked = true;
        }

        public void msg(string s)
        {
        
            textBox3.Text = s;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GameMaster GM = new GameMaster(this);
            //Thread t =new Thread (new ParameterizedThreadStart(run_game_hvp));
            //t.Start(GM);
            //t.Join();
            //GM.PvP(new Player(), me);
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync(GM);
            }

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            GameMaster GM = (GameMaster)e.Argument;
           GM.PvP(new Player(this), this.me);

            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            GameMaster GM = (GameMaster)e.Argument;
            GM.PvP(new Player(this), new Player(this));

            if (backgroundWorker2.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker2.CancelAsync();
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }

        public void clearTextBox()
        {
            if (this.textBox1.InvokeRequired)
            {
                clearTextBoxes d = new clearTextBoxes(clearTextBox);
                this.Invoke(d, new object[] { });

            }
            else
            {
                this.textBox1.Clear();
                this.textBox2.Clear();
            }

        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void setMoveIsClicked(bool isClicked) {this.moveIsClicked = isClicked; }
        public bool getMoveIsClicked() { return this.moveIsClicked; }
        public string getTextBox1(){ return textBox1.Text; }
        public string getTextBox2() { return textBox2.Text; }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //TODO: human player mechanic. 4 hr.
        //TODO: testing game mechanics. 8 hr.
        //TODO: turnament mechanic. 4hr.
        //TODO: assesment functions. 1 hr.
        //TODO: tree building. 4 hr.
        //TODO: genetic cross & genetic mutation. 12 hr.

    }
}
