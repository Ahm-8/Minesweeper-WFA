using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {

        private string diff = "easy";
        Grid g;
        public Form1()
        {
            InitializeComponent();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(diff);
            f2.Show();

        }

        public void StartNewGame()
        {
            Form2 f2 = new Form2(diff);
            f2.Show();
        }


        //add the code to change the difficult here!!!!!!!!! 
        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStart.BackColor = Color.Lime;
            diff = "easy";



        }
        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStart.BackColor = Color.Orange;
            diff = "medium";

        }
        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStart.BackColor = Color.Red;
            diff = "hard";

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Some squares contain hidden mines.\r\nThe objective is to clear the board without detonating any mines.\r\nYou can reveal squares by clicking on them.\r\nNumbers displayed on revealed squares indicate the number of mines adjacent to that square.\r\nUse the numbers to deduce the location of mines.\r\nYou can mark potential mine locations with flags to keep track.\r\nIf you reveal a square containing a mine, you lose the game.\r\nClear all non-mine squares to win the game.");
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(diff);
            f2.Show();

        }

        private void creatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Alexander Gordon & Ahmid Omarzada created this game.\r\n Hope you enjoy it.");
        }
    }
}
