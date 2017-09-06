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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        public string inputFile, dbConnection;
        public int rowsUpdated;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);

            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM incomehddetails where isubhd= '"+null+"';";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox1.Items.Add(sqlReader["imainhd"].ToString());
                }

                sqlReader.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                {
                    string sql = "SELECT count(*) FROM incomehddetails where imainhd ='" + comboBox1.Text + "' and isubhd ='" + textBox1.Text + "' ;";
                    SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                    cnn.Open();
                    rowsUpdated = Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
                if (rowsUpdated > 0)
                { MessageBox.Show("THE ENTERD INCOME SUBHEAD ALREADY EXIST "); }
                else
                {
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        string sql = "insert into incomehddetails (imainhd,isubhd) values ('" + comboBox1.Text + "','" + textBox1.Text + "');";
                        SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                        cnn.Open();
                        int rowsUpdated1 = sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Saved successfully");
                        textBox1.Text = "";
                    }
                }
            }
            else { MessageBox.Show("please filldata"); }
        }
    }
}
