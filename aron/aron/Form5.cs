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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public string inputFile,dbConnection;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public string sql;
        public string[] print;
        public int q=1;
        private void button3_Click(object sender, EventArgs e)
        {
            int k = 0;
            if (textBox2.Text.Length > 0 && comboBox1.Text.Length > 0 && comboBox2.Text.Length > 0 && comboBox3.Text.Length > 0  && textBox7.Text.Length > 0)
           {
               if (radioButton2.Checked == true)
               {
                   if (textBox1.Text.Length == 0 && textBox8.Text.Length == 0 && textBox9.Text.Length == 0)
                   { k = 1; }
               sql = "insert into addindetails (date,name,inacc,inhd,des,amnt,bnkname,chqno,chqdt,mthd) values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox1.Text + "','CHECK');";
               print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), comboBox1.Text, textBox7.Text, "CHECK", textBox9.Text, textBox1.Text, textBox8.Text };
               
               }
               else if (radioButton3.Checked == true)
               {
                   if (textBox8.Text.Length == 0)
                   {
                       k = 1;
                   }
                   sql = "insert into addindetails (date,name,inacc,inhd,des,amnt,bnkname,mthd) values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','BANK TRANSFER');";
                   print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), comboBox1.Text, textBox7.Text, "BANK", textBox8.Text };
               }
               else
               {
                   sql = "insert into addindetails (date,name,inacc,inhd,des,amnt,mthd) values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','CASH');";
                   print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), comboBox1.Text, textBox7.Text, "CASH" };
               }
                if (k == 0)
               {
                   using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                   {
                       SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                       cnn.Open();
                       int a = sqlCmd.ExecuteNonQuery();
                     
                textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; textBox9.Text = ""; textBox1.Text = "";
                 textBox2.Text = "";
                 
              combo_load();
                 imainhd_load();
                comboBox2_SelectedIndexChanged(null, null);
                 //comboBox2.ResetText();
                if (q == 1)
                {
                    MessageBox.Show("SAVED SUCCEFULLY");}else{ Form15 form15 = new Form15(print);
                    form15.ShowDialog();
                }
                q = 1;
                   }
}
               else
               { MessageBox.Show("Completely Fill data"); }
           }
            else
            { MessageBox.Show("Completely Fill data"); }

           
        }
       
        private void Form4_Load(object sender, EventArgs e)
        {
            combo_load();
            imainhd_load();
        }
        public void combo_load()
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
        public void imainhd_load()
        {
            comboBox2.Items.Clear(); comboBox3.Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM incomehddetails where isubhd= '" + null + "';";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox2.Items.Add(sqlReader["imainhd"].ToString());
                    
                }

                sqlReader.Close();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label9.Visible = false;
            textBox1.Visible = false;
            label10.Visible = true;
            label13.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = false;
            label12.Visible = false;
            panel1.Visible = true;
            label11.Visible = false;
        }

       

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = false;
            panel1.Visible = true;


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            panel1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM clientdetails where name='" + comboBox1.Text + "';", cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
               sqlReader.Read();
               textBox2.Text = sqlReader["slno"].ToString();
                sqlReader.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM incomehddetails where isubhd != '" + null + "' and imainhd = '" + comboBox2.Text + "' ;";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox3.Items.Add(sqlReader["isubhd"].ToString());
                }

                sqlReader.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            q = 0;
             button3_Click(null,null);
            
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

       

    }
}
