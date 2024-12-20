using Project17_;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Minesweeper
{
    // Represents the game grid
    class Grid
    {
        // Constants for grid sizes
        public static readonly int EasyWid = 400;
        public static readonly int MedWid = 600;
        public static readonly int HardWid = 800;

        // Grid properties
        private int x, y;
        private Cell[,] cells;
        private int Columns;
        private int Rows;
        private bool gameOver;
        private int MineCount;
        

        private Form2 parent;
        Form1 ms = new Form1();

        public static Random rng = new Random();


        // Constructor for initializing the grid
        public Grid(int columns, int rows, int mineCount, int windowWidth, int x, int y, Form2 parent)
        {
            Columns = columns;
            Rows = rows;
            MineCount = mineCount;
            this.x = x;
            this.y = y;

            this.parent = parent;
            gameOver = false;
            cells = new Cell[Columns, Rows];

            InitializeCells(windowWidth / columns);
            GenerateMines();
            AddCellsToParent();
        }


        // Static method to create a new grid based on difficulty
        public static Grid NewGrid(string difficulty, Form2 _parent)
        {
            int cols, rows, mineCount, width;

            switch (difficulty)
            {
                case "easy":
                    cols = 10;
                    rows = 10;
                    mineCount = 1;
                    width = EasyWid;
                    break;

                case "medium":
                    cols = 20;
                    rows = 12;
                    mineCount = 30;
                    width = MedWid;
                    break;

                case "hard":
                    cols = 24;
                    rows = 20;
                    mineCount = 80;
                    width = HardWid;
                    break;

                default:
                    throw new InvalidOperationException("Invalid difficulty");
            }

            Grid grid = new Grid(cols, rows, mineCount, width, _parent.Width / 2 - width / 2, 100, _parent);

            return grid;
        }

        // Getters for cells, mine count, and main parent
        public Cell[,] GetCells()
        {
            return cells;
        }

        private void InitializeCells(int cellWidth)
        {
            for (int c = 0; c < Columns; c++)
            {
                for (int r = 0; r < Rows; r++)
                {
                    cells[c, r] = new Cell(c * cellWidth, r * cellWidth, cellWidth, this, c, r);
                }
            }
        }

        // Check if the game is over
        public bool IsGameFin()
        {
            return gameOver;
        }

        public int GetMineCount()
        {
            return MineCount;
        }

        public Form2 GetMainParent()
        {
            return parent;
        }

        public void GenerateMines()
        {
            Random rng = new Random();

            var allPositions = Enumerable.Range(0, Columns)
                .SelectMany(x => Enumerable.Range(0, Rows).Select(y => (x, y)))
                .OrderBy(_ => rng.Next())
                .Take(MineCount);

            foreach (var (rx, ry) in allPositions)
            {
                Cell current = cells[rx, ry];
                current.SetAsMine();
            }
        }

        // Add cells to the parent form
        public void AddCellsToParent()
        {
            foreach (Cell cell in cells)
            {
                AddCellToParent(cell, x, y);
            }
        }

        private void AddCellToParent(Cell cell, int offsetX, int offsetY)
        {
            cell.SetOffset(offsetX, offsetY);
            parent.Controls.Add(cell.GetControl());
        }

        // Check if the player has won the game
        public void CheckGameResult()
        {
            bool allNonMinesRevealed = true;

            foreach (Cell c in cells)
            {
                if (!c.IsMine() && !c.IsRevealed())
                {
                    allNonMinesRevealed = false;
                    // Don't break here to continue checking other cells
                }
            }

            // Check if all non-mines are revealed and no flags left
            if (allNonMinesRevealed && !gameOver)
            {
                EndGame(true); // Winner!
            }
            
        }



        // End the game and show the result
        public void EndGame(bool gameWon)
        {
            gameOver = true;

            string message = gameWon ? "You Won" : "You Lost";

            foreach (Cell cell in cells)
            {
                if (cell.IsMine())
                {
                    cell.Reveal();
                }
                else if (cell.IsFlagged())
                {
                    cell.SetBackColourWhite();
                }
            }

            var playAgainResult = MessageBox.Show($"{message}\n Do you want to Play Again?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.None);

            if (playAgainResult == DialogResult.No)
            {
                parent.Close();
            }
            else
            {
                parent.Close();
                ms.StartNewGame();
            }
        }

        public int GetCols()
        {
            return Columns;
        }


        public int GetRows()
        {
            return Rows;
        }
    }
}



