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
    public partial class Form18 : Form
    {
        public Form18(DataGridViewRow row1)
        {
            row = row1;
            InitializeComponent();
        }
        public DataGridViewRow row;
        public string inputFile,dbConnection;
        public string[] print;
        public int q = 1;
        public string sql;
        public string[]  mat_con_lab;
      

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear(); comboBox4.Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM exphddetails where esubhd != '" + null + "' and emainhd = '" + comboBox2.Text + "' ;";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox3.Items.Add(sqlReader["esubhd"].ToString());
                    comboBox4.Items.Add(sqlReader["esubhd"].ToString());
                }

                sqlReader.Close();
            }


            if (comboBox2.Text == "labour charge")
            {
                //comboBox7.Items.Clear();
                comboBox6.Items.Clear();
                comboBox5.Items.Clear();
                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                {
                    string sql = "SELECT * FROM exphddetails where esubhd == 'sub contract' and emainhd = '" + comboBox2.Text + "' ;";
                    SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                    cnn.Open();
                    SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox6.Items.Add(sqlReader["nam_o_cont"].ToString());
                        // comboBox7.Items.Add(sqlReader["typ_o_wrk"].ToString());
                    }
                    sqlReader.Close();
                }

                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                {
                    string sql = "SELECT * FROM exphddetails where esubhd == 'daily wages' and emainhd = '" + comboBox2.Text + "' ;";
                    SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                    cnn.Open();
                    SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox5.Items.Add(sqlReader["catagory"].ToString());
                        // comboBox7.Items.Add(sqlReader["typ_o_wrk"].ToString());
                    }
                    sqlReader.Close();
                }
            }

            if (comboBox2.Text == "materials")
            {
                this.Size = new Size(538, 653);
                panel6.Visible = false;
                panel4.Location = new Point(0, 328);
                panel2.Location = new Point(25, 190);
                comboBox3.Visible = false;
                label5.Visible = false;
                panel2.Visible = true;
            }
            else if (comboBox2.Text == "labour charge")
            {
                this.Size = new Size(538, 653);
                panel2.Visible = false;
                panel4.Location = new Point(0, 328);
                panel6.Location = new Point(25, 190);
                comboBox3.Visible = false;
                label5.Visible = false;
                panel3.Visible = true;
                panel5.Visible = false;
                radioButton7.Checked = true;
                panel6.Visible = true;
            }
            else
            {
                this.Size = new Size(538, 554);

                panel2.Visible = false;
                panel3.Visible = false;
                // comboBox4.SelectedIndex = 1;
                panel4.Location = new Point(0, 228);
                comboBox3.Visible = true;
                label5.Visible = true;
            }

        }

        private void Form18_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(row.Cells[0].Value.ToString());
            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);
            dateTimePicker1.Value = DateTime.Parse(row.Cells[1].Value.ToString(), new CultureInfo("en-CA"));
            textBox5.Text = row.Cells[2].Value.ToString();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM clientdetails where name='" + row.Cells[2].Value.ToString() + "';", cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                sqlReader.Read();
                textBox2.Text = sqlReader["slno"].ToString();
                sqlReader.Close();
            }
            combo_load();
            imainhd_load();
            comboBox2.SelectedItem = row.Cells[3].Value.ToString();
            comboBox3.SelectedItem = row.Cells[4].Value.ToString();
            comboBox4.SelectedItem = row.Cells[4].Value.ToString();
            textBox6.Text = row.Cells[5].Value.ToString();
            textBox7.Text = row.Cells[6].Value.ToString();
            if (row.Cells[7].Value.ToString() != "CASH")
            {
                if (row.Cells[7].Value.ToString() == "BANK")
                { radioButton3.Checked = true; textBox8.Text = row.Cells[8].Value.ToString(); }
                else
                {
                    radioButton2.Checked = true; textBox8.Text = row.Cells[8].Value.ToString();
                    textBox9.Text = row.Cells[9].Value.ToString();
                    textBox1.Text = row.Cells[10].Value.ToString();
                }

            }
            string[] mat_con_lab;
            int k=0;
            if (comboBox2.Text == "materials")
            {
                if (radioButton4.Text == row.Cells[11].Value.ToString())
                { radioButton4.Checked = true; textBox10.Text = row.Cells[12].Value.ToString();
                }
                else
                {
                    radioButton5.Checked = true; comboBox8.SelectedItem = row.Cells[13].Value.ToString();
                }
                textBox12.Text = row.Cells[14].Value.ToString(); textBox13.Text = row.Cells[15].Value.ToString();

            }
            else if (comboBox2.Text == "labour charge")
            {
                if (row.Cells[22].Value.ToString() == "sub contract")
                {
                    radioButton7.Checked = true;
                    comboBox6.SelectedItem = row.Cells[16].Value.ToString();
                  textBox11.Text = row.Cells[17].Value.ToString();
                }
                else if (row.Cells[22].Value.ToString() == "daily wages")
                {
                    radioButton6.Checked = true;
                    comboBox5.SelectedItem = row.Cells[18].Value.ToString(); textBox3.Text = row.Cells[19].Value.ToString();
                    textBox11.Text = row.Cells[20].Value.ToString(); textBox14.Text = row.Cells[21].Value.ToString();
                }
            }
            else
            {
                if (comboBox3.Text.Length > 0)
                { mat_con_lab = new String[] { "", "", "", comboBox3.Text, "", "", "", "" }; }
                else { k = 1; }

            }
        }
        public void combo_load()
        {
           
            comboBox8.Items.Clear();
            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);

            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM clientdetails", cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                   
                    comboBox8.Items.Add(sqlReader["name"].ToString());
                }

                sqlReader.Close();
            }
        }
        public void imainhd_load()
        {
            comboBox2.Items.Clear(); comboBox3.Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM exphddetails where esubhd= '" + null + "';";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    comboBox2.Items.Add(sqlReader["emainhd"].ToString());

                }

                sqlReader.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int k = 0;
            if (textBox2.Text.Length > 0 && textBox5.Text.Length > 0 && comboBox2.Text.Length > 0  && textBox7.Text.Length > 0)
            {
                if (comboBox2.Text == "materials")
                {
                    if ( comboBox4.Text.Length > 0 && textBox12.Text.Length > 0 && textBox13.Text.Length > 0)
                    {
                        if (radioButton4.Checked == true)
                            mat_con_lab = new String[] { radioButton4.Text, textBox10.Text, "", comboBox4.Text, textBox12.Text, textBox13.Text, "", "", "", "", "", "", "" };
                        else
                            mat_con_lab = new String[] { radioButton5.Text, "", comboBox8.Text, comboBox4.Text, textBox12.Text, textBox13.Text, "", "", "", "", "", "", "" };
                    }
                    else { k = 1; }
                }
                else if (comboBox2.Text == "labour charge")
                {
                    if ((comboBox6.Text.Length > 0 &&  textBox4.Text.Length > 0) || (textBox11.Text.Length > 0 && comboBox5.Text.Length > 0 && textBox14.Text.Length > 0 && textBox5.Text.Length > 0))
                    {
                        if (radioButton7.Checked == true)
                            mat_con_lab = new String[] { "", "", "", "", "", "", comboBox6.Text, textBox4.Text, "", "", "", "", "sub contract" };
                        else
                            mat_con_lab = new String[] { "", "", "", "", "", "", "", "", comboBox5.Text, textBox5.Text, textBox11.Text, textBox14.Text, "daily wages" };

                    }
                    else
                    { k = 1; }
                }
                else
                {
                    if (comboBox3.Text.Length > 0)
                    { mat_con_lab = new String[] { "", "", "", comboBox3.Text, "", "", "", "", "", "", "", "", "" }; }
                    else { k = 1; }

                }
                if (radioButton2.Checked == true)
                {
                    if (textBox1.Text.Length == 0 && textBox8.Text.Length == 0 && textBox9.Text.Length == 0)
                    { k = 1; }
                    sql = "update  addexdetails set  date ='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',name ='" + textBox5.Text + "',exacc ='" + comboBox2.Text + "',item ='" + mat_con_lab[3] + "',des ='" + textBox6.Text + "',amnt ='" + textBox7.Text + "',bnkname ='" + textBox8.Text + "',chqno ='" + textBox9.Text + "',chqdt ='" + textBox1.Text + "',mthd ='CHECK',mat_trans_mat_pur ='" + mat_con_lab[0] + "',frm_shop ='" + mat_con_lab[1] + "',frm_site= '" + mat_con_lab[2] + "',mat_quan='" + mat_con_lab[4] + "',mat_unit ='" + mat_con_lab[5] + "',nam_contract = '" + mat_con_lab[6] + "' ,typ_wrk = '" + mat_con_lab[7] + "',subcont_dwage = '" + mat_con_lab[12] + "', d_wage = '" + mat_con_lab[11] + "', d_num = '" + mat_con_lab[10] + "', d_name = '" + mat_con_lab[9] + "', d_cat = '" + mat_con_lab[8] + "' where id ='" + row.Cells[0].Value.ToString() + "'";
                    print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), textBox2.Text, textBox7.Text, "CHECK", comboBox2.Text, comboBox3.Text, textBox9.Text, textBox1.Text, textBox8.Text };
                }
                else if (radioButton3.Checked == true)
                {
                    if (textBox8.Text.Length == 0)
                    {
                        k = 1;
                    }
                    sql = "update  addexdetails set  date ='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',name ='" + textBox5.Text + "',exacc ='" + comboBox2.Text + "',item ='" + mat_con_lab[3] + "',des ='" + textBox6.Text + "',amnt ='" + textBox7.Text + "',bnkname ='" + textBox8.Text + "',mthd ='BANK',mat_trans_mat_pur ='" + mat_con_lab[0] + "',frm_shop ='" + mat_con_lab[1] + "',frm_site= '" + mat_con_lab[2] + "',mat_quan='" + mat_con_lab[4] + "',mat_unit ='" + mat_con_lab[5] + "',nam_contract = '" + mat_con_lab[6] + "' ,typ_wrk = '" + mat_con_lab[7] + "',subcont_dwage = '" + mat_con_lab[12] + "', d_wage = '" + mat_con_lab[11] + "', d_num = '" + mat_con_lab[10] + "', d_name = '" + mat_con_lab[9] + "', d_cat = '" + mat_con_lab[8] + "' where id ='" + row.Cells[0].Value.ToString() + "'";
                    print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), textBox2.Text, textBox7.Text, "BANK", comboBox2.Text, comboBox3.Text, textBox8.Text };
                }
                else
                {
                    sql = "update  addexdetails set  date ='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',name ='" + textBox5.Text + "',exacc ='" + comboBox2.Text + "',item ='" + mat_con_lab[3] + "',des ='" + textBox6.Text + "',amnt ='" + textBox7.Text + "',mthd ='CASH',mat_trans_mat_pur ='" + mat_con_lab[0] + "',frm_shop ='" + mat_con_lab[1] + "',frm_site= '" + mat_con_lab[2] + "',mat_quan='" + mat_con_lab[4] + "',mat_unit ='" + mat_con_lab[5] + "',nam_contract = '" + mat_con_lab[6] + "' ,typ_wrk = '" + mat_con_lab[7] + "',subcont_dwage = '" + mat_con_lab[12] + "', d_wage = '" + mat_con_lab[11] + "', d_num = '" + mat_con_lab[10] + "', d_name = '" + mat_con_lab[9] + "', d_cat = '" + mat_con_lab[8] + "' where id ='" + row.Cells[0].Value.ToString() + "'";
                    print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), textBox2.Text, textBox7.Text, "CASH", comboBox2.Text, comboBox3.Text };
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
                            MessageBox.Show("SAVED SUCCESSFULLY");
                        }
                        else
                        {
                            Form16 form16 = new Form16(print,mat_con_lab);
                            form16.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            { label15.Visible = true; label14.Visible = false; comboBox8.Visible = true; textBox10.Visible = false; }
           
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            { label14.Visible = true; label15.Visible = false; comboBox8.Visible = false; textBox10.Visible = true; }
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

            panel1.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            { panel3.Visible = true; panel5.Visible = false; }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton6.Checked == true)
            { panel5.Visible = true; panel3.Visible = false; }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox7.Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM exphddetails where esubhd == 'sub contract' and nam_o_cont = '" + comboBox6.Text + "' ;";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                sqlReader.Read();
              //  while (sqlReader.Read())
               // {
                   textBox4.Text = (sqlReader["typ_o_wrk"].ToString());
               // }
                sqlReader.Close();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
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

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
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

      

       
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
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
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            double amount;
            if (textBox14.Text != "" && textBox11.Text != "") { amount = float.Parse(textBox11.Text, CultureInfo.InvariantCulture.NumberFormat) * float.Parse(textBox14.Text, CultureInfo.InvariantCulture.NumberFormat);
            textBox7.Text = amount.ToString();
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            double amount;
            if (textBox14.Text != "" && textBox11.Text != "")
            {
                amount = float.Parse(textBox11.Text, CultureInfo.InvariantCulture.NumberFormat) * float.Parse(textBox14.Text, CultureInfo.InvariantCulture.NumberFormat);
                textBox7.Text = amount.ToString();
            }
        }

        
    }
}
