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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }
        public string  inputFile,dbConnection;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                panel1.Enabled = true;
                radioButton2.Checked = false; panel2.Enabled = false;
            radioButton3.Checked = false; panel3.Enabled = false;
            radioButton4.Checked = false; panel4.Enabled = false;
            radioButton5.Checked = false; panel5.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                panel2.Enabled = true;
                radioButton1.Checked = false; panel1.Enabled = false;
                radioButton3.Checked = false; panel3.Enabled = false;
                radioButton4.Checked = false; panel4.Enabled = false;
                radioButton5.Checked = false; panel5.Enabled = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                panel3.Enabled = true;
                radioButton2.Checked = false; panel2.Enabled = false;
                radioButton1.Checked = false; panel1.Enabled = false;
                radioButton4.Checked = false; panel4.Enabled = false;
                radioButton5.Checked = false; panel5.Enabled = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                panel4.Enabled = true;
                radioButton2.Checked = false; panel2.Enabled = false;
                radioButton3.Checked = false; panel3.Enabled = false;
                radioButton1.Checked = false; panel1.Enabled = false;
                radioButton5.Checked = false; panel5.Enabled = false;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                panel5.Enabled = true;
                radioButton2.Checked = false; panel2.Enabled = false;
                radioButton3.Checked = false; panel3.Enabled = false;
                radioButton4.Checked = false; panel4.Enabled = false;
                radioButton1.Checked = false; panel1.Enabled = false;
            }
        }

        private void Form11_Load(object sender, EventArgs e)
        {

            combo_load();
        }
        public void combo_load()
        {
            client_load();
            emainhd_load();
            imainhd_load();
        }

        public void client_load()
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
        public void emainhd_load()
        {
            comboBox5.Items.Clear(); comboBox6.Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM exphddetails where esubhd= '" + null + "';";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox5.Items.Add(sqlReader["emainhd"].ToString());
                    comboBox6.Items.Add(sqlReader["emainhd"].ToString());
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
                    comboBox3.Items.Add(sqlReader["imainhd"].ToString());
                }

                sqlReader.Close();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM incomehddetails where isubhd != '" + null + "' and imainhd = '" +comboBox3.Text + "' ;";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox4.Items.Add(sqlReader["isubhd"].ToString());
                }

                sqlReader.Close();
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM exphddetails where esubhd != '" + null + "' and emainhd = '" + comboBox6.Text + "' ;";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox7.Items.Add(sqlReader["esubhd"].ToString());
                }

                sqlReader.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                if (comboBox1.Text.Length > 0)
                {
                    
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        string sql = "delete from clientdetails where name ='" + comboBox1.Text + "'";
                       
                        SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                        cnn.Open();
                        int suc = sqlCmd.ExecuteNonQuery();
                    }
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        string sql = "delete from addindetails where name ='" + comboBox1.Text + "'";

                        SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                        cnn.Open();
                        int suc = sqlCmd.ExecuteNonQuery();
                    }
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        string sql = "delete from addexdetails where name ='" + comboBox1.Text + "'";

                        SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                        cnn.Open();
                        int suc = sqlCmd.ExecuteNonQuery();
                    }

                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM clientdetails", cnn);
                        cnn.Open();
                        SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                        int count = 1;
                        while (sqlReader.Read())
                        {
                            

                                SQLiteCommand sqlCmd1 = new SQLiteCommand("update clientdetails set slno ='" + count + "' where name ='" + sqlReader["name"].ToString() + "'", cnn);
                                 sqlCmd1.ExecuteNonQuery();

                            
                            count++;
                        }

                        sqlReader.Close();
                    }

                    client_load();
                }
                else
                { MessageBox.Show("please select data"); }
            
              }
            else if (radioButton2.Checked == true)
            {
                if (comboBox2.Text.Length > 0)
                {

                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        string sql = "delete from incomehddetails where imainhd ='" + comboBox2.Text + "'";
                       
                        SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                        cnn.Open();
                        int suc = sqlCmd.ExecuteNonQuery();
                    }

                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM incomehddetails  where isubhd = ''", cnn);
                        cnn.Open();
                        SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                        int count = 1;
                        while (sqlReader.Read())
                        {


                            SQLiteCommand sqlCmd1 = new SQLiteCommand("update incomehddetails set slno ='" + count + "' where imainhd ='" + sqlReader["imainhd"].ToString() + "'", cnn);
                            sqlCmd1.ExecuteNonQuery();
                           // MessageBox.Show(count + "  " + sqlReader["imainhd"].ToString());

                            count++;
                        }

                        sqlReader.Close();
                    }
                    imainhd_load();
                }
                else
                { MessageBox.Show("please select data"); }
            
            
            }
            else if (radioButton3.Checked == true)
            {
                if (comboBox3.Text.Length > 0 && comboBox4.Text.Length > 0)
                {

                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        string sql = "delete from incomehddetails where imainhd ='" + comboBox3.Text + "' and isubhd ='" + comboBox4.Text + "' ";

                        SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                        cnn.Open();
                        int suc = sqlCmd.ExecuteNonQuery();
                    }
                    imainhd_load();
                     comboBox3_SelectedIndexChanged(null,null);
                }
                else
                { MessageBox.Show("please select data"); }
            
            
            }
            else if (radioButton4.Checked == true)
            {
                if (comboBox5.Text.Length > 0)
                {
                    if (comboBox5.Text == "materials" || comboBox5.Text == "labour charge")
                    { MessageBox.Show("The EXPENSE MAINHEAD are DEFALUT can't be Deleted"); }
                    else
                    {
                        using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                        {
                            string sql = "delete from exphddetails where emainhd ='" + comboBox5.Text + "'";

                            SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                            cnn.Open();
                            int suc = sqlCmd.ExecuteNonQuery();
                        }

                        using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                        {
                            SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM exphddetails  where esubhd = ''", cnn);
                            cnn.Open();
                            SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                            int count = 1;
                            while (sqlReader.Read())
                            {


                                SQLiteCommand sqlCmd1 = new SQLiteCommand("update exphddetails set slno ='" + count + "' where emainhd ='" + sqlReader["emainhd"].ToString() + "'", cnn);
                                sqlCmd1.ExecuteNonQuery();
                                //MessageBox.Show(count + "  " + sqlReader["emainhd"].ToString());

                                count++;
                            }

                            sqlReader.Close();
                        }
                        emainhd_load();
                    }
                }
                else
                { MessageBox.Show("please select data"); }
            
            
            
            }
           
            else if (radioButton5.Checked == true)
            {
                if (comboBox6.Text.Length > 0 && comboBox7.Text.Length > 0)
                {

                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        string sql = "delete from exphddetails where emainhd ='" + comboBox6.Text + "' and esubhd ='" + comboBox7.Text + "' ";

                        SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                        cnn.Open();
                        int suc = sqlCmd.ExecuteNonQuery();
                    }
                    emainhd_load();
                    comboBox6_SelectedIndexChanged(null, null);
                }
                else
                { MessageBox.Show("please select data"); }
            
            }

            
        }



    }
}
