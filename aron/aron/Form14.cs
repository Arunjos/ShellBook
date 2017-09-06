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
    public partial class Form14 : Form
    {
        public DataGridViewRow row;
        public Form14(DataGridViewRow row1)
        {
            row = row1;
            InitializeComponent();
        }
        public string inputFile,dbConnection;
        public string[] print;
        public int q = 1;
        private void Form14_Load(object sender, EventArgs e)
        {//
            //MessageBox.Show(row.Cells[0].Value.ToString());
            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);
           dateTimePicker1.Value = DateTime.Parse(row.Cells[1].Value.ToString(), new CultureInfo("en-CA"));
            textBox3.Text = row.Cells[2].Value.ToString();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM clientdetails where name='" + row.Cells[2].Value.ToString() + "';", cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                sqlReader.Read();
                textBox2.Text = sqlReader["slno"].ToString();
                sqlReader.Close();
            }
            imainhd_load();
            comboBox2.SelectedItem = row.Cells[3].Value.ToString();
            comboBox3.SelectedItem = row.Cells[4].Value.ToString();
            textBox6.Text = row.Cells[5].Value.ToString();
            textBox7.Text = row.Cells[6].Value.ToString();
            if (row.Cells[7].Value.ToString() != "CASH")
            {
                if (row.Cells[7].Value.ToString() == "BANK TRANSFER")
                { radioButton3.Checked = true; textBox4.Text = row.Cells[8].Value.ToString(); }
                else { radioButton2.Checked = true; textBox8.Text = row.Cells[8].Value.ToString();
                textBox9.Text = row.Cells[9].Value.ToString();
                textBox1.Text = row.Cells[10].Value.ToString();
                  }
            
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
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label9.Visible = false;
            textBox1.Visible = false;
            label10.Visible = true;
            label13.Visible = true;
            textBox4.Visible = true;
            textBox8.Visible = false;
            textBox9.Visible = false;
            label12.Visible = false;
            panel1.Visible = true;
            label11.Visible = false;
        }



        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox4.Visible = false;
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
            
            panel1.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {//string sql = "update  clientdetails set  name ='" + comboBox1.Text + "',addr ='" + textBox3.Text + "',phno ='" + textBox4.Text + "',email ='" + textBox5.Text + "',tob ='" + textBox6.Text + "',camt ='" + textBox7.Text + "',date ='" +dateTimePicker1.Text + "',period ='" + textBox8.Text + "' where slno ='" + textBox1.Text + "'";

            int k = 0;
            string sql;
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && comboBox2.Text.Length > 0 && comboBox3.Text.Length > 0 && textBox7.Text.Length > 0)
            {
                if (radioButton2.Checked == true)
                {
                    if (textBox1.Text.Length == 0 && textBox8.Text.Length == 0 && textBox9.Text.Length == 0)
                    { k = 1; }

                    sql = "update  addindetails set  date ='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',name ='" + textBox3.Text + "',inacc ='" + comboBox2.Text + "',inhd ='" + comboBox3.Text + "',des ='" + textBox6.Text + "',amnt ='" + textBox7.Text + "',bnkname ='" + textBox8.Text + "',chqno ='" + textBox9.Text + "',chqdt ='" + textBox1.Text + "',mthd ='CHECK' where id ='" + row.Cells[0].Value.ToString() + "'";
                    print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), textBox3.Text, textBox7.Text, "CHECK", textBox9.Text, textBox1.Text, textBox8.Text, };
                }
                else if (radioButton3.Checked == true)
                {
                    if (textBox4.Text.Length == 0)
                    {
                        k = 1;
                    }
                    sql = "update  addindetails set  date ='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',name ='" + textBox3.Text + "',inacc ='" + comboBox2.Text + "',inhd ='" + comboBox3.Text + "',des ='" + textBox6.Text + "',amnt ='" + textBox7.Text + "',bnkname ='" + textBox4.Text + "',mthd ='BANK TRANSFER' where id ='" + row.Cells[0].Value.ToString() + "'";
                    print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), textBox3.Text, textBox7.Text, "BANK",textBox4.Text };
                }
                else
                {
                    sql = "update  addindetails set  date ='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',name ='" + textBox3.Text + "',inacc ='" + comboBox2.Text + "',inhd ='" + comboBox3.Text + "',des ='" + textBox6.Text + "',amnt ='" + textBox7.Text + "',mthd ='CASH' where id ='" + row.Cells[0].Value.ToString() + "'";
                    print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), textBox3.Text, textBox7.Text, "CASH" };
                }
                    if (k == 0)
                    {
                        using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                        {
                            SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                            cnn.Open();
                            int a = sqlCmd.ExecuteNonQuery();
                            if (q == 1)
                            {
                                MessageBox.Show("SAVED SUCCEFULLY");
                            }
                            else
                            {
                                Form15 form15 = new Form15(print);
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

        private void button2_Click(object sender, EventArgs e)
        {
            q = 0;
            button3_Click(null, null);
           
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
