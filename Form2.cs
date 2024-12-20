using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form2 : Form
    {
        string diff;
        Grid g;
        public Form2(string y)
        {
            InitializeComponent();
            diff = y;

        }

        private void flagCounterLabel_Click(object sender, EventArgs e)
        {

        }

        private void MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeComponent();

            g = Grid.NewGrid(diff, this);

            
        }

        public bool StartNewGame()
        {
            if (MessageBox.Show("Are you sure you want to start a new game?", "New Game", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
            {
                foreach (var cell in g.GetCells())
                {
                    Controls.Remove(cell.GetControl());
                }
                return true;
            }
            g = Grid.NewGrid(diff, this);


            return false;
        }
    }
}
