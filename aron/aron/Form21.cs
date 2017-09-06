using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
namespace aron
{
    public partial class Form21 : Form
    {
        public Form21()
        {
            InitializeComponent();
        }
        public int rowIndex;
        public string inputFile, dbConnection;
        public DataTable dataTable;
        public SQLiteDataAdapter ad;
        public SQLiteConnection cnn;
        public string a, b, nam;
        private void Form21_Load(object sender, EventArgs e)
        {
            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);
          
            cnn = new SQLiteConnection(dbConnection);
            
            cnn.Open();

            ad = new SQLiteDataAdapter("SELECT * FROM addindetails order by date desc", cnn);
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder(ad);

            dataTable = new DataTable();
            ad.Fill(dataTable);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
           

        }

     

        private void button3_Click(object sender, EventArgs e)
        {
            try
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    ad.Update(dataTable);
                }
                catch (Exception exceptionObj)
                {
                    MessageBox.Show(exceptionObj.Message.ToString());
                }
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                rowIndex = dataGridView1.CurrentRow.Index;
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                // MessageBox.Show(row.Cells[0].Value.ToString());
                Form14 a = new Form14(row);
                a.ShowDialog();
                refresh();
           
        }
        public void refresh()
        {
            using (cnn = new SQLiteConnection(dbConnection))
            {
                cnn.Open();
                ad = new SQLiteDataAdapter("SELECT * FROM addindetails order by date desc", cnn);
                SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder(ad);
                dataTable = new DataTable();
                ad.Fill(dataTable);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dataTable;
                dataGridView1.DataSource = bindingSource;

            }
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[0];
            dataGridView1.Rows[rowIndex].Selected = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form13 form = new Form13();
            form.Tag = this;
            form.ShowDialog(this);
            refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Tag = this;
            form5.ShowDialog(this);
            refresh();
        }

    }
}
