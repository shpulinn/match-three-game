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
        ImageList imageList = new ImageList();

        public enum Icons {
             Orange = 1
        }

        public GameForm() {
            InitializeComponent();
            //dataGridView1.ColumnCount = 8;
            dataGridView1.RowCount = 8;
        }


        private void GameForm_Load(object sender, EventArgs e) {

            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn() {
                
            };

           //for (int i = 0; i < 7; i++) {
            //dataGridView1.Columns.Add(imgColumn);
            

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    randInt[i, j] = rand.Next(0, 5);
                    //randInt[i, j] = 1;
                }
            }

            // ======================================================= Просто заполнение числами
            for (int i = 0; i < dataGridView1.ColumnCount; i++) {
                for (int j = 0; j < dataGridView1.RowCount; j++) {

                    switch (randInt[i,j]) {
                        case 0:
                            ((DataGridViewImageCell)dataGridView1.Rows[i].Cells[j]).Value = Properties.Resources.banana;
                        break;
                            
                        case 1:
                            ((DataGridViewImageCell)dataGridView1.Rows[i].Cells[j]).Value = Properties.Resources.orange;
                        break;

                        case 2:
                            ((DataGridViewImageCell)dataGridView1.Rows[i].Cells[j]).Value = Properties.Resources.apple;
                        break;

                        case 3:
                        ((DataGridViewImageCell)dataGridView1.Rows[i].Cells[j]).Value = Properties.Resources.grape;
                        break;

                        case 4:
                        ((DataGridViewImageCell)dataGridView1.Rows[i].Cells[j]).Value = Properties.Resources.pear;
                        break;
                    }

                    //if (randInt[i, j].Equals(1)) {
                    //    ((DataGridViewImageCell)dataGridView1.Rows[i].Cells[j]).Value = Properties.Resources.orange;
                    //}
                    //dataGridView1.Rows[i].Cells[j].Value = randInt[i, j];

                }
            }

            //for (int i = 0; i < dataGridView1.ColumnCount; i++) {
            //    for (int j = 0; j < dataGridView1.RowCount; j++) {

            //        if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "1") {
            //            dataGridView1.Rows[i].Cells[j].Value = Properties.Resources.orange;
            //        }

            //    }
            //}

            /*
            mage image = new Bitmap("C:\\Users\\shpul\\Desktop\\orange.png");
            for (int row = 0; row <= dataGridView1.Rows.Count - 1; row++) {
                ((DataGridViewImageCell)dataGridView1.Rows[row].Cells[1]).Value = image;
            }*/

            // ======================================================= Просто заполнение числами
            //for (int i = 0; i < dataGridView1.ColumnCount; i++) {
            //    for (int j = 0; j < dataGridView1.RowCount; j++) {

            //        //dataGridView1.Rows.Add($"{i.ToString()}");
            //        //dataGridView1.Rows[i].Cells[j].Value = i;
            //    }
            //}

            // ================================================================== не работает но можно попробовать

            //DataGridViewImageColumn imgColumn = new DataGridViewImageColumn() {
            //    Name = "Images"
            //};

            ////dataGridView1.Columns.AddRange(imgColumn);

            //Image image = new Bitmap("C:\\Users\\shpul\\Desktop\\orange.png");
            //imgColumn.Image = image;
            ////for (int i = 0; i < dataGridView1.Rows.Count; i++) {
            ////    dataGridView1.Rows[i].Cells["Images"].Value = image;
            ////}
            //for (int i = 0; i < dataGridView1.Rows.Count; i++) {
            //    for (int j = 0; j < dataGridView1.Columns.Count; j++) {
            //        dataGridView1.Rows[i].Cells[j].Value = image;
            //        //dataGridView1.Columns.Insert(j, imgColumn);
            //    }
            //}

            /*
            Image image = new Bitmap("C:\\Users\\shpul\\Desktop\\orange.png");
            imageList1.Images.Add(image);

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            int icIndex = dataGridView1.Columns.Add(imgCol);
            
            MessageBox.Show(icIndex.ToString());
            dataGridView1.Columns[icIndex].Name = "Image";

            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (row.Index < imageList1.Images.Count)    // *
                    row.Cells["Image"].Value = imageList1.Images[row.Index];

            //Image image = new Bitmap("C:\\Users\\shpul\\Desktop\\orange.png");
            for (int i = 0; i < dataGridView1.Rows.Count; i++) {
                dataGridView1.Rows[i].Cells["Image"].Value = image;
            }*/

        }



        private void GameForm_FormClosed(object sender, FormClosedEventArgs e) {
            //MenuForm menuForm = new MenuForm();
            //menuForm.Show();
            //this.Hide();
            Application.Exit();
        }
    }
}
