using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Globalization;
namespace aron
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        public string dbConnection;
        public string inputFile;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

            combo_load();
          
        }
        public void combo_load()
        {
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM clientdetails where name='"+comboBox1.Text+"';", cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                
                sqlReader.Read();
              //  slno integer primary key, name varchar(1000), addr varchar(10000),phno varchar(1000), email varchar(1000), tob varchar(1000),camt varchar(1000),date varchar(1000),period
                textBox1.Text = sqlReader["slno"].ToString(); textBox3.Text = sqlReader["addr"].ToString(); textBox4.Text = sqlReader["phno"].ToString();
                textBox5.Text = sqlReader["email"].ToString(); textBox6.Text = sqlReader["tob"].ToString(); textBox7.Text = sqlReader["camt"].ToString();
                dateTimePicker1.Text = sqlReader["date"].ToString();
                textBox8.Text = sqlReader["period"].ToString();
                sqlReader.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && comboBox1.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0)
            {
                string sql = "update  clientdetails set  name ='" + comboBox1.Text + "',addr ='" + textBox3.Text + "',phno ='" + textBox4.Text + "',email ='" + textBox5.Text + "',tob ='" + textBox6.Text + "',camt ='" + textBox7.Text + "',date ='" +dateTimePicker1.Text + "',period ='" + textBox8.Text + "' where slno ='" + textBox1.Text + "'";

                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                {
                    SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                    cnn.Open();
                    int rowsUpdated1 = sqlCmd.ExecuteNonQuery();
                }
                MessageBox.Show("Update succesfully");
                comboBox1.Items.Clear();
                combo_load();
            }
            else
                MessageBox.Show("Please Select or Complete Fill Data");
        }
    }
}
