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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        public string inputFile, dbConnection;
        public int rowsUpdated;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);

            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM exphddetails where esubhd= '" + null + "';";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox1.Items.Add(sqlReader["emainhd"].ToString());
                }

                sqlReader.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length > 0)
            {
                if (comboBox1.Text == "labour charge")
                {
                    if (radioButton4.Checked == true)
                    {
                        if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
                        {
                            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                            {
                                string sql = "SELECT count(*) FROM exphddetails where nam_o_cont ='" + textBox2.Text + "' and typ_o_wrk ='" + textBox3.Text + "' ;";
                                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                                cnn.Open();
                                rowsUpdated = Convert.ToInt32(sqlCmd.ExecuteScalar());
                            }
                            if (rowsUpdated > 0)
                            { MessageBox.Show("THE ENTERD EXPENSE SUBHEAD ALREADY EXIST "); }
                            else
                            {
                                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                                {
                                    string sql = "insert into exphddetails (emainhd,esubhd,nam_o_cont,typ_o_wrk) values ('" + comboBox1.Text + "','sub contract','" + textBox2.Text + "','" + textBox3.Text + "');";
                                    SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                                    cnn.Open();
                                    int rowsUpdated1 = sqlCmd.ExecuteNonQuery();
                                    MessageBox.Show("Saved successfully");
                                    textBox1.Text = "";
                                }
                            }
                        }
                        else { MessageBox.Show("please fill data"); }
                    }
                    else
                    {
                        if (textBox5.Text.Length > 0)
                        {
                            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                            {
                                string sql = "SELECT count(*) FROM exphddetails where catagory ='" + textBox5.Text + "' ;";
                                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                                cnn.Open();
                                rowsUpdated = Convert.ToInt32(sqlCmd.ExecuteScalar());
                            }
                            if (rowsUpdated > 0)
                            { MessageBox.Show("THE ENTERD EXPENSE SUBHEAD ALREADY EXIST "); }
                            else
                            {
                                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                                {
                                    string sql = "insert into exphddetails (emainhd,esubhd,catagory) values ('" + comboBox1.Text + "','daily wages','" + textBox5.Text + "');";
                                    SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                                    cnn.Open();
                                    int rowsUpdated1 = sqlCmd.ExecuteNonQuery();
                                    MessageBox.Show("Saved successfully");
                                    textBox1.Text = "";
                                }
                            }
                        }
                        else { MessageBox.Show("please fill data"); }
                    }
                    
                }
                else
                {
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        string sql = "SELECT count(*) FROM exphddetails where emainhd ='" + comboBox1.Text + "' and esubhd ='" + textBox1.Text + "' ;";
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
                            string sql = "insert into exphddetails (emainhd,esubhd) values ('" + comboBox1.Text + "','" + textBox1.Text + "');";
                            SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                            cnn.Open();
                            int rowsUpdated1 = sqlCmd.ExecuteNonQuery();
                            MessageBox.Show("Saved successfully");
                            textBox1.Text = "";
                        }
                    }
                }
               
            }
            else { MessageBox.Show("please filldata"); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (comboBox1.Text == "labour charge")
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = true;
                panel4.Visible = false;
            }
            else
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = true;
                panel4.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel4.Visible = true;
                panel3.Visible = false;
            }
        }
    }
}
