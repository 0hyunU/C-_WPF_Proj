using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe
{
    public partial class Winner : Form
    {

        public Winner()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            tictactoeeee tic = new tictactoeeee();
            tic.Focus();

            if (tictactoeeee.winner == "X") textBox1.Text = "WINNER IS X!!!!";
            if (tictactoeeee.winner == "O") textBox1.Text = "WINNER IS O!!!!";
            if (tictactoeeee.winner == "OX") textBox1.Text = "DRAW!!!";
       
        }

        private void Winner_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
