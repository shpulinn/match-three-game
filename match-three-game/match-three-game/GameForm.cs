using System;
using System.Drawing;
using System.Windows.Forms;

namespace match_three_game {
    public partial class GameForm : Form {

        Random rand = new Random();
        int[,] gameBoardArray = new int[8, 8]; // GAME BOARD
        int gameTime = 60; // GAME TIME IN SECONDS
        int score = 0; // SCORE VARIABLE
        DataGridViewCell currentCell; 
        bool moved = false;
        Point[] delPoints = new Point[3]; // Meant to detect line of 3 exact tiles for their removing (don't work :c )

        public GameForm() {
            InitializeComponent();
            gameGrid.RowCount = 8; // SET 8 ROWS IN DATAGRIDVIEW
        }

        private void GameForm_Load(object sender, EventArgs e) {

            FillBoard();
            currentCell = gameGrid.CurrentCell;
            gameTimer.Start(); //  Start countdown from (gameTime) to 0

        }

        // Places image in DataGridView based on number in gameBoardArray
        private void UpdateGrid() {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    switch (gameBoardArray[i, j]) {
                        case 0: // banana
                        ((DataGridViewImageCell)gameGrid.Rows[i].Cells[j]).Value = Properties.Resources.banana;
                        break;

                        case 1: //orange
                        ((DataGridViewImageCell)gameGrid.Rows[i].Cells[j]).Value = Properties.Resources.orange;
                        break;

                        case 2: //apple
                        ((DataGridViewImageCell)gameGrid.Rows[i].Cells[j]).Value = Properties.Resources.apple;
                        break;

                        case 3: //grape
                        ((DataGridViewImageCell)gameGrid.Rows[i].Cells[j]).Value = Properties.Resources.grape;
                        break;

                        case 4: //pear
                        ((DataGridViewImageCell)gameGrid.Rows[i].Cells[j]).Value = Properties.Resources.pear;
                        break;
                    }
                }
            }
        }

        private void FillBoard() {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {

                    gameBoardArray[i, j] = rand.Next(0, 5); // Fill gameBoardArray by random int (from 0 to 4)
                    if (i > 0) {
                        if (gameBoardArray[i - 1, j] == gameBoardArray[i, j]) {
                            while (gameBoardArray[i - 1, j] == gameBoardArray[i, j]) {
                                gameBoardArray[i - 1, j] = rand.Next(0, 5);
                            } 
                        }
                    }
                    if (j > 0) {
                        if (gameBoardArray[i, j - 1] == gameBoardArray[i, j]) {
                            while(gameBoardArray[i, j - 1] == gameBoardArray[i, j]) {
                                gameBoardArray[i, j - 1] = rand.Next(0, 5);
                            }
                        }
                    }
                }
            }
            UpdateGrid();
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        // Countdown from gameTime to 0, ticks every 1000 ms
        private void GameTimer_Tick(object sender, EventArgs e) {
            if (gameTime > 0) {
                gameTime -= 1;
                timeLabel.Text = gameTime.ToString(); 
                gameGrid.CurrentCell.Selected = true;
                scoreLabel.Text = score.ToString();
            }
            else {
                gameTimer.Stop();
                gameGrid.Visible = false;
                gameOverLabel.Visible = true;
                timeLabel.Text = "Time's up!";
                menuButton.Visible = true;
            }
        }

        // Open MenuForm, hide GameForm
        private void MenuButton_Click(object sender, EventArgs e) {
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
            this.Hide();
        }

        private void gameGrid_SelectionChanged(object sender, EventArgs e) {

            SwapTiles(); // Swap 2 near tiles 
            CheckGrid(); // Should check the grid for matches of 3 and more tiles in line

        }

        private void CheckGrid() {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (i > 0) {
                        
                        if (gameBoardArray[i, j] == gameBoardArray[i - 1, j]) {
                            switch (delPoints.Length) {
                                case 0:
                                    delPoints[0].X = i;
                                    delPoints[0].Y = j;
                                break;
                                case 1:
                                    delPoints[1].X = i;
                                    delPoints[1].Y = j;
                                break;
                                case 2:
                                    delPoints[2].X = i;
                                    delPoints[2].Y = j;
                                break;
                            }

                            if (delPoints.Length == 3) {
                                gameBoardArray[delPoints[0].X, delPoints[0].Y] = 5;
                                gameBoardArray[delPoints[1].X, delPoints[1].Y] = 5;
                                gameBoardArray[delPoints[2].X, delPoints[2].Y] = 5;
                            }
                        }
                    }
                    if (j > 0) {

                        if (gameBoardArray[i, j] == gameBoardArray[i, j - 1]) {
                            switch (delPoints.Length) {
                                case 0:
                                delPoints[0].X = i;
                                delPoints[0].Y = j;
                                break;
                                case 1:
                                delPoints[1].X = i;
                                delPoints[1].Y = j;
                                break;
                                case 2:
                                delPoints[2].X = i;
                                delPoints[2].Y = j;
                                break;
                            }

                            if (delPoints.Length == 3) {
                                gameBoardArray[delPoints[0].X, delPoints[0].Y] = 5;
                                gameBoardArray[delPoints[1].X, delPoints[1].Y] = 5;
                                gameBoardArray[delPoints[2].X, delPoints[2].Y] = 5;
                            }
                        }
                    }

                }
            }

            UpdateGrid();
        }

        private void gameGrid_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (moved && currentCell == null || currentCell != gameGrid.CurrentCell) {
                currentCell = gameGrid.CurrentCell; 
                gameGrid.ClearSelection();
            }
            else currentCell = null; // cancel seletion
        }

        private void SwapTiles() {
            if (currentCell != null) {
                if (currentCell != gameGrid.CurrentCell) {
                    // checking the possibility of changing tiles in places
                    if (Math.Abs(gameGrid.CurrentCell.RowIndex - currentCell.RowIndex) > 1 || Math.Abs(gameGrid.CurrentCell.ColumnIndex - currentCell.ColumnIndex) > 1) {
                        currentCell = null;
                        gameGrid.ClearSelection();
                        moved = false;

                        gameGrid.CurrentCell.Selected = false;
                        return;
                    }
                    // swap tiles 
                    else {
                        int tempValue = gameBoardArray[currentCell.RowIndex, currentCell.ColumnIndex];
                        gameBoardArray[currentCell.RowIndex, currentCell.ColumnIndex] = gameBoardArray[gameGrid.CurrentCell.RowIndex, gameGrid.CurrentCell.ColumnIndex];
                        gameBoardArray[gameGrid.CurrentCell.RowIndex, gameGrid.CurrentCell.ColumnIndex] = tempValue;
                        currentCell = null;
                        gameGrid.ClearSelection();
                        UpdateGrid();
                        moved = true;
                        gameGrid.CurrentCell.Selected = false;
                        score++; // +1 point for each move 
                    }
                }
            }
            else return;
        }
    }
}
