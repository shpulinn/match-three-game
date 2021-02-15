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
    public partial class MenuForm : Form {
        public MenuForm() {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e) {
            GameForm gameForm = new GameForm();
            gameForm.Show();
            this.Hide();
        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
