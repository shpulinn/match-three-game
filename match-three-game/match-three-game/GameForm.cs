﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace match_three_game {
    public partial class GameForm : Form {

        Random rand = new Random();
        int[,] randInt = new int[8, 8];
        int gameTime = 40; // GAME TIME IN SECONDS
        int score = 0;
        bool inGame = true;
        DataGridViewCell currentCell;
        bool moved = false;
        //Dictionary<int, int> positions = new Dictionary<int, int>();
        //int[,] positions = new int[3, 3];
        Point[] delPoints = new Point[3];

        public GameForm() {
            InitializeComponent();
            gameGrid.RowCount = 8;
        }

        private void GameForm_Load(object sender, EventArgs e) {

            FillBoard();
            currentCell = gameGrid.CurrentCell;
            gameTimer.Start();
            //positions[0, 0] = randInt[0, 0];
            // while inGame///
            //while (inGame) {
            //    PlayerMove();
            //}

        }

        private void PlayerMove() {
            int[,] copyArr = new int[8, 8]; // copy arr??
            string msg = String.Format("Row: {0}, Column: {1}",
                                        gameGrid.CurrentCell.RowIndex,
                                        gameGrid.CurrentCell.ColumnIndex);
            MessageBox.Show(msg, "Current Cell");
            currentCell = gameGrid.CurrentCell;
            for (int i = 0; i < randInt.Length; i++) {
                for (int j = 0; j < randInt.Length; j++) {
                    if (copyArr[i,j] != randInt[i,j]) {

                    }
                    copyArr[i, j] = randInt[i, j];
                }
            }

        }

        private void UpdateGrid() {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    switch (randInt[i, j]) {
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
                        case 5:
                        ((DataGridViewImageCell)gameGrid.Rows[i].Cells[j]).Value = null;
                        break;
                    }
                }
            }
        }

        private void FillBoard() {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {

                    randInt[i, j] = rand.Next(0, 5); // заполнение массива числами от 0 до 4
                    if (i > 0) {
                        if (randInt[i - 1, j] == randInt[i, j]) {
                            //randInt[i - 1, j] = rand.Next(randInt[i - 1, j] + 1, 5);
                            while (randInt[i - 1, j] == randInt[i, j]) {
                                randInt[i - 1, j] = rand.Next(0, 5);
                            } 
                        }
                    }
                    if (j > 0) {
                        if (randInt[i, j - 1] == randInt[i, j]) {
                            while(randInt[i, j - 1] == randInt[i, j]) {
                                randInt[i, j - 1] = rand.Next(0, 5);
                            }
                            //if (randInt[i, j - 1] > 4) {
                            //    randInt[i, j - 1] = 3;
                            //}
                        }
                    }
                    //randInt[i, j] = 1;
                }
            }
            // добавление в DataGridView иконок в зависимости от случ. числа
            UpdateGrid();
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void GameTimer_Tick(object sender, EventArgs e) {
            if (gameTime > 0) {
                gameTime -= 1;
                timeLabel.Text = gameTime.ToString();
                //label1.Text = "Current Row " + gameGrid.CurrentCell.RowIndex.ToString();
                //label2.Text = "Current Col " + gameGrid.CurrentCell.ColumnIndex.ToString();
                //if (gameGrid.CurrentCell != null) {
                    label1.Text = "Current Row " + gameGrid.CurrentCell.RowIndex.ToString();
                    label2.Text = "Current Col " + gameGrid.CurrentCell.ColumnIndex.ToString();
                    gameGrid.CurrentCell.Selected = true;
               // } 
            }
            else {
                gameTimer.Stop();
                gameGrid.Visible = false;
                gameOverLabel.Visible = true;
                timeLabel.Text = "Time's up!";
                menuButton.Visible = true;
                inGame = false;
            }
        }

        private void MenuButton_Click(object sender, EventArgs e) {
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
            this.Hide();
        }

        //private void gameGrid_MouseClick(object sender, MouseEventArgs e) {
            //if (moved && currentCell == null || currentCell != gameGrid.CurrentCell ) {
            //    currentCell = gameGrid.CurrentCell;
            //    //gameGrid.CurrentCell = null;
            //    //gameGrid.ClearSelection();
            //}
            //else currentCell = null;

       // }

        private void gameGrid_SelectionChanged(object sender, EventArgs e) {

            SwapTiles();
            CheckGrid();

        }

        private void CheckGrid() { // можно сделать bool и возвращать true если можно играть !
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (i > 0) {
                        
                        if (randInt[i, j] == randInt[i - 1, j]) {
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
                                randInt[delPoints[0].X, delPoints[0].Y] = 5;
                                randInt[delPoints[1].X, delPoints[1].Y] = 5;
                                randInt[delPoints[2].X, delPoints[2].Y] = 5;
                            }
                        }
                    }
                    if (j > 0) {

                        if (randInt[i, j] == randInt[i, j - 1]) {
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
                                randInt[delPoints[0].X, delPoints[0].Y] = 5;
                                randInt[delPoints[1].X, delPoints[1].Y] = 5;
                                randInt[delPoints[2].X, delPoints[2].Y] = 5;
                            }
                        }
                    }

                }
            }

            UpdateGrid();
        }

        private void clickTile() {
            DataGridViewCell target = null;
            bool firstPiece = false;// -- global

            if (firstPiece == false) {
                target = gameGrid.CurrentCell;
            }
            else if (target == gameGrid.CurrentCell) {
                target = null;
                //gameGrid.CurrentCell.vi
            }

        }

        private void gameGrid_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (moved && currentCell == null || currentCell != gameGrid.CurrentCell) {
                currentCell = gameGrid.CurrentCell;
                //gameGrid.CurrentCell = null;
                gameGrid.ClearSelection();
            }
            else currentCell = null;
        }

        private void SwapTiles() {
            if (currentCell != null) {
                if (currentCell != gameGrid.CurrentCell) {
                    int tempValue = randInt[currentCell.RowIndex, currentCell.ColumnIndex];
                    // меняю только массив
                    if (Math.Abs(gameGrid.CurrentCell.RowIndex - currentCell.RowIndex) > 1 || Math.Abs(gameGrid.CurrentCell.ColumnIndex - currentCell.ColumnIndex) > 1) {
                        currentCell = null;
                        gameGrid.ClearSelection();
                        moved = false;

                        gameGrid.CurrentCell.Selected = false;
                        return;
                    }
                    else {
                        randInt[currentCell.RowIndex, currentCell.ColumnIndex] = randInt[gameGrid.CurrentCell.RowIndex, gameGrid.CurrentCell.ColumnIndex];
                        randInt[gameGrid.CurrentCell.RowIndex, gameGrid.CurrentCell.ColumnIndex] = tempValue;
                        //UpdateGrid();
                        currentCell = null;
                        gameGrid.ClearSelection();
                        UpdateGrid();
                        moved = true;
                        gameGrid.CurrentCell.Selected = false;
                        MessageBox.Show(delPoints[0].ToString() + "  " + delPoints[1].ToString() + "  " + delPoints[2].ToString());
                    }
                }
            }
            else return;
        }

        // ------------
    }
}
