using System;
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
        int gameTime = 7;
        int score = 0;
        bool inGame = true;

        public GameForm() {
            InitializeComponent();
            gameGrid.RowCount = 8;
        }

        private void GameForm_Load(object sender, EventArgs e) {

            gameGrid.CurrentCell = null;
            FillBoard();
            gameTimer.Start();
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
            var curCell = gameGrid.CurrentCell;
            for (int i = 0; i < randInt.Length; i++) {
                for (int j = 0; j < randInt.Length; j++) {
                    if (copyArr[i,j] != randInt[i,j]) {

                    }
                    copyArr[i, j] = randInt[i, j];
                }
            }

        }

        private void UpdateGrid() {
            for (int i = 0; i < randInt.Length; i++) {
                for (int j = 0; j < randInt.Length; j++) {
                    // костылЬ!!
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
                    }
                }
            }

        }

        private void FillBoard() {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    randInt[i, j] = rand.Next(0, 5); // заполнение массива числами от 0 до 4
                    //randInt[i, j] = 1;
                }
            }

            // ======================================================= добавление в DataGridView иконок в зависимости от случ. числа
            for (int i = 0; i < gameGrid.ColumnCount; i++) {
                for (int j = 0; j < gameGrid.RowCount; j++) {
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
                    }
                }
            }
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void GameTimer_Tick(object sender, EventArgs e) {
            if (gameTime > 0) {
                gameTime -= 1;
                timeLabel.Text = gameTime.ToString();
            }
            else {
                gameTimer.Stop();
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

        private void gameGrid_MouseClick(object sender, MouseEventArgs e) {
            string msg = String.Format("Row: {0}, Column: {1}",
        gameGrid.CurrentCell.RowIndex,
        gameGrid.CurrentCell.ColumnIndex);
            MessageBox.Show(msg, "Current Cell");
        }
    }
}
