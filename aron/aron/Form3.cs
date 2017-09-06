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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public int maxId;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0)
            {
                string dbConnection;
                string inputFile = "database.s3db";
                dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                cnn.Open();
                string sql = "SELECT count(*) FROM clientdetails where name ='" + textBox2.Text+ "';";

                SQLiteCommand mycommand = new SQLiteCommand(cnn);
                mycommand.CommandText = sql;
                
                int rowsUpdated = Convert.ToInt32(mycommand.ExecuteScalar());
                if (rowsUpdated > 0)
                { MessageBox.Show("THE ENTERD NAMEIS ALREADY EXIST "); }
                else
                {
                    string sql1 = "insert into clientdetails (name,addr ,phno,email,tob,camt,date,period) values ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + dateTimePicker1.Text + "','" + textBox8.Text + "');";
                    SQLiteCommand mycommand1 = new SQLiteCommand(cnn);
                    mycommand1.CommandText = sql1;
                    int rowsUpdated1 = mycommand1.ExecuteNonQuery();
                    cnn.Close();
                    MessageBox.Show("SAVED SUCCESFULLY ");
                    slnoload();
                    textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = "";
                    textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = "";   
                }
               
            }
            else
            { MessageBox.Show("Please Fill Mandotry Fields"); }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            slnoload();
        }
        public void slnoload()
        {
            string inputFile = "database.s3db";
            string dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);

            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "Select Max(slno) From clientdetails";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open(); object val = sqlCmd.ExecuteScalar();
                try
                {
                    maxId = int.Parse(val.ToString()) + 1;
                    textBox1.Text = maxId.ToString();
                }
                catch (Exception ex)
                {
                    maxId = 1;
                    textBox1.Text = maxId.ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form11 form = new Form11();
            form.Tag = this;
            form.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form10 form = new Form10();
            form.Tag = this;
            form.ShowDialog(this);
        }
    }
}
