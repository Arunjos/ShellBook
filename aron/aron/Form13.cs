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
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
           
        }
        public int rowIndex;
        public string inputFile, dbConnection;
        public DataTable dataTable;
        public SQLiteDataAdapter ad;
        public SQLiteConnection cnn;
        public string a, b,nam;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length > 0)
            {
                inputFile = "database.s3db";
                dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);
                //using (
                cnn = new SQLiteConnection(dbConnection);
                 //   )
               // {
                    //  string sql = "SELECT * FROM incomehddetails where isubhd != '" + null + "' and imainhd = '" + comboBox3.Text + "' ;";
                    //  SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                    cnn.Open();
                    // SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                    nam = comboBox1.Text;
                     a = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                     b = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                    ad = new SQLiteDataAdapter("SELECT * FROM addindetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + comboBox1.Text + "'", cnn);
                    SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder(ad);
                    
                    dataTable = new DataTable();
                    ad.Fill(dataTable);
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = dataTable;

                    dataGridView1.DataSource = bindingSource;
                   // DataTable dt = new DataTable();
                  //  ad.Fill(dt);
                  //  dataGridView1.DataSource = dt;
                    //  sqlReader.Close();
                //}
                    
            }
            else
            { MessageBox.Show("please fill data"); }
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);

            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM clientdetails", cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox1.Items.Add(sqlReader["name"].ToString());
                }

                sqlReader.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length > 0)
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
        }else
            { MessageBox.Show("please fill data"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length > 0)
            {
                 rowIndex = dataGridView1.CurrentRow.Index;
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                // MessageBox.Show(row.Cells[0].Value.ToString());
                Form14 a = new Form14(row);
                a.ShowDialog();
                refresh();
            }
            else
            { MessageBox.Show("please fill data"); }
        }

        public void refresh()
        {
            using (cnn = new SQLiteConnection(dbConnection))
            {
                cnn.Open();
                ad = new SQLiteDataAdapter("SELECT * FROM addindetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + nam + "'", cnn);
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

       

    }
}
