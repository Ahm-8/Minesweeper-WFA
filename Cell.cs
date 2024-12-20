using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Minesweeper
{
    // Represents an individual cell in the Minesweeper grid
    class Cell
    {
        private int i, j, k; // x and y represent the position, w represents the width of the cell
        private int Column, Row; // col and row represent the column and row in the grid
        private int offsetX, offsetY; // Offset for positioning the cell in the grid

        private bool revealed; // Indicates whether the cell is revealed
        private bool flagged; // Indicates whether the cell is flagged
        private bool hasMine; // Indicates whether the cell contains a mine

        private Label revealUi; // Label to display the cell on the UI

        private Grid parent; // Reference to the parent grid

        // Constructor for creating a cell
        public Cell(int x, int y, int w, Grid _parent, int col, int row)
        {
            i = x;
            j = y;
            k = w;

            Column = col;
            Row = row;

            revealed = false;
            flagged = false;
            hasMine = false;

            parent = _parent;

            SetOffset(0, 0);
        }

        // Getters for cell properties
        public int GetX() { return i; }
        public int GetY() { return j; }
        public int GetRow() { return Row; }
        public int GetColumn() { return Column; }
        public Label GetControl() { return revealUi; }

        // Sets the offset for the cell based on grid position
        public void SetOffset(int _x, int _y)
        {
            offsetX = _x;
            offsetY = _y;

            InitLabel();
        }

        // Checks if the cell is revealed
        public bool IsRevealed() { return revealed; }

        // Checks if the cell is flagged
        public bool IsFlagged() { return flagged; }

        // Checks if the cell contains a mine
        public bool IsMine() { return hasMine; }

        // Initializes the label for the cell
        private void InitLabel()
        {
            revealUi = new Label
            {
                Width = k,
                Height = k,
                Location = new Point(i + offsetX, j + offsetY),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(192, 192, 192), // Light gray background color
                Font = new Font("Arial", 12, FontStyle.Bold), // Larger and bold font
                ForeColor = Color.Black, // Black text color
                Cursor = Cursors.Hand // Change cursor to hand when hovering over the label
            };

            void revealLabel_MouseClick(object sender, MouseEventArgs e)
            {
                if (parent.IsGameFin()) return;

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (!revealed)
                            Reveal();
                        break;

                    case MouseButtons.Right:
                        if (!revealed)
                            MarkasFlag();
                        break;
                }

                parent.CheckGameResult();
            }

            revealUi.MouseClick += revealLabel_MouseClick;

        }

        public bool SetAsMine()
        {
            if (hasMine) return false;

            hasMine = true;
            return true;
        }

        // Toggles the flag on the cell
        public void MarkasFlag()
        {
            if (!revealed)
            {
                // Check if the flag is being added or removed
                if (!flagged)
                {
                    // Check if there are remaining flags to mark
                    if (true)
                    {
                        flagged = true;
                        revealUi.Text = "🚩";
                    }
                }
                else
                {
                    flagged = false;
                    revealUi.Text = "";
                    
                }

                parent.CheckGameResult(); // Check for a win after each flag operation
            }
        }

        // Reveals the cell
        public void Reveal()
        {
            if (revealed || flagged) return;

            if (IsMine())
            {
                revealUi.BackColor = Color.Red;
                revealUi.Text = "💣";
                if (!parent.IsGameFin())
                {
                    parent.EndGame(false);
                }
                return;
            }

            Cell[,] cells = parent.GetCells();

            FloodFillReveal(cells, Column, Row);

            parent.CheckGameResult();
        }

        // New recursive function for flood fill reveal
        private void FloodFillReveal(Cell[,] cells, int currentCol, int currentRow)
        {
            if (currentCol < 0 || currentCol >= parent.GetCols() || currentRow < 0 || currentRow >= parent.GetRows())
                return;

            Cell currentCell = cells[currentCol, currentRow];

            if (currentCell.revealed || currentCell.flagged || currentCell.IsMine())
                return;

            currentCell.revealUi.BackColor = Color.White;
            currentCell.revealed = true;

            int surroundingMines = currentCell.CountMines();

            if (surroundingMines == 0)
            {
                currentCell.revealUi.Text = "";

                for (int a = -1; a <= 1; a++)
                {
                    for (int b = -1; b <= 1; b++)
                    {
                        if (!(a == 0 && b == 0))
                        {
                            try
                            {
                                FloodFillReveal(cells, currentCol + a, currentRow + b);
                            }
                            catch (IndexOutOfRangeException e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }
                }
            }
            else
            {
                currentCell.revealUi.Text = $"{surroundingMines}";
            }
        }


        // Counts the number of mines surrounding the cell
        public int CountMines()
        {
            int total = 0;

            Cell[,] cells = parent.GetCells();
            int cols = cells.GetLength(0);
            int rows = cells.GetLength(1);

            // Define relative positions of neighboring cells
            List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>
            {
                Tuple.Create(-1, -1), Tuple.Create(-1, 0), Tuple.Create(-1, 1),
                Tuple.Create(0, -1), Tuple.Create(0, 1),
                Tuple.Create(1, -1), Tuple.Create(1, 0), Tuple.Create(1, 1)
            };

            int currentCol = Column;
            int currentRow = Row;

            foreach (var neighbor in neighbors)
            {
                int checkCol = currentCol + neighbor.Item1;
                int checkRow = currentRow + neighbor.Item2;

                if (checkCol >= 0 && checkCol < cols && checkRow >= 0 && checkRow < rows)
                {
                    if (cells[checkCol, checkRow].IsMine())
                    {
                        total++;
                    }
                }
            }

            return total;
        }



        // Sets the background color of the cell to white
        public void SetBackColourWhite()
        {
            revealUi.BackColor = Color.GhostWhite;
        }
    }
}
