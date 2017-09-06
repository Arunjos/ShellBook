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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string inputFile, dbConnection,sql;
        public string[] print,mat_con_lab;
        public int q = 1;
        private void button3_Click(object sender, EventArgs e)
        {
            int k = 0;
            if (textBox2.Text.Length > 0 && comboBox1.Text.Length > 0 && comboBox2.Text.Length > 0  && textBox7.Text.Length > 0)
            {
                if (comboBox2.Text == "materials")
                {
                    if ( comboBox4.Text.Length > 0 && textBox12.Text.Length > 0 && textBox13.Text.Length > 0)
                    {
                        if (radioButton4.Checked == true)
                            mat_con_lab = new String[] { radioButton4.Text, textBox10.Text, "", comboBox4.Text, textBox12.Text, textBox13.Text, "", "", "", "", "", "", "" };
                        else
                            mat_con_lab = new String[] { radioButton5.Text, "",comboBox8.Text, comboBox4.Text, textBox12.Text, textBox13.Text, "", "", "", "", "", "", "" };
                    }
                    else { k = 1; }
            }
            else if( comboBox2.Text == "labour charge" )
                {
                    if ((comboBox6.Text.Length > 0 && textBox3.Text.Length > 0) || (textBox11.Text.Length > 0 && comboBox5.Text.Length > 0 && textBox14.Text.Length > 0 && textBox5.Text.Length > 0))
                    {
                        if (radioButton7.Checked == true)
                            mat_con_lab = new String[] { "", "", "", "", "", "", comboBox6.Text, textBox3.Text,"","","","","sub contract" };
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
                else{k=1;}

            }
                if (radioButton2.Checked == true)
                {
                    if (textBox1.Text.Length == 0 && textBox8.Text.Length == 0 && textBox9.Text.Length == 0)
                    { k = 1; }
                    sql = "insert into addexdetails (date,name,exacc,item,des,amnt,bnkname,chqno,chqdt,mthd,mat_trans_mat_pur,frm_shop,frm_site,mat_quan,mat_unit,nam_contract ,typ_wrk,subcont_dwage, d_wage, d_num, d_name, d_cat) values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + mat_con_lab[3] + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox1.Text + "','CHECK','" + mat_con_lab[0] + "','" + mat_con_lab[1] + "','" + mat_con_lab[2] + "','" + mat_con_lab[4] + "','" + mat_con_lab[5] + "','" + mat_con_lab[6] + "','" + mat_con_lab[7] + "','" + mat_con_lab[12] + "','" + mat_con_lab[11] + "','" + mat_con_lab[10] + "','" + mat_con_lab[9] + "','" + mat_con_lab[8] +"');";
                    print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), comboBox1.Text, textBox7.Text, "CHECK", comboBox2.Text, comboBox3.Text, textBox9.Text, textBox1.Text, textBox8.Text };
                }
                else if (radioButton3.Checked == true)
                {
                    if (textBox8.Text.Length == 0)
                    {
                        k = 1;
                    }
                    sql = "insert into addexdetails (date,name,exacc,item,des,amnt,bnkname,mthd,mat_trans_mat_pur,frm_shop,frm_site,mat_quan,mat_unit,nam_contract ,typ_wrk,subcont_dwage, d_wage, d_num, d_name, d_cat) values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + mat_con_lab[3] + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','BANK','" + mat_con_lab[0] + "','" + mat_con_lab[1] + "','" + mat_con_lab[2] + "','" + mat_con_lab[4] + "','" + mat_con_lab[5] + "','" + mat_con_lab[6] + "','" + mat_con_lab[7] + "','" + mat_con_lab[12] + "','" + mat_con_lab[11] + "','" + mat_con_lab[10] + "','" + mat_con_lab[9] + "','" + mat_con_lab[8] + "');";
                    print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), comboBox1.Text, textBox7.Text, "BANK", comboBox2.Text, comboBox3.Text, textBox8.Text };
                }
                else
                {
                    sql = "insert into addexdetails (date,name,exacc,item,des,amnt,mthd,mat_trans_mat_pur,frm_shop,frm_site,mat_quan,mat_unit,nam_contract,typ_wrk,subcont_dwage, d_wage, d_num, d_name, d_cat) values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + mat_con_lab[3] + "','" + textBox6.Text + "','" + textBox7.Text + "','CASH','" + mat_con_lab[0] + "','" + mat_con_lab[1] + "','" + mat_con_lab[2] + "','" + mat_con_lab[4] + "','" + mat_con_lab[5] + "','" + mat_con_lab[6] + "','" + mat_con_lab[7] + "','" + mat_con_lab[12] + "','" + mat_con_lab[11] + "','" + mat_con_lab[10] + "','" + mat_con_lab[9] + "','" + mat_con_lab[8] + "');";
                    print = new string[] { dateTimePicker1.Value.ToString("dd-MM-yyyy"), comboBox1.Text, textBox7.Text, "CASH", comboBox2.Text, comboBox3.Text };
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
                            MessageBox.Show("SAVED SUCCEFULLY");
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
     

        private void Form4_Load(object sender, EventArgs e)
        {
            combo_load();
            imainhd_load();
        }
        public void combo_load()
        {
            comboBox1.Items.Clear();
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
                    comboBox1.Items.Add(sqlReader["name"].ToString());
                    comboBox8.Items.Add(sqlReader["name"].ToString());
                }

                sqlReader.Close();
            }
        }
        public void imainhd_load()
        {
            comboBox2.Items.Clear(); comboBox3.Items.Clear(); comboBox4.Items.Clear();
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

     

        private void button2_Click(object sender, EventArgs e)
        {
            q = 0;
            button3_Click(null, null);
        }

       

       
        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            if (comboBox4.Text == "Purchased")
            {
                label14.Visible = true;
                label15.Visible = false;
            }
            else
            {
                label14.Visible = false;
                label15.Visible = true;
            }
        }

       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
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
               // comboBox7.Items.Clear();
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
            else if( comboBox2.Text == "labour charge")
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
                panel6.Visible = false;
                // comboBox4.SelectedIndex = 1;
                panel4.Location = new Point(0, 228);
                comboBox3.Visible = true;
                label5.Visible = true;
            }
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
         //   comboBox7.Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "SELECT * FROM exphddetails where esubhd == 'sub contract' and nam_o_cont = '" + comboBox6.Text + "' ;";
                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                sqlReader.Read();
              //  while (sqlReader.Read())
               // {
                   textBox3.Text = (sqlReader["typ_o_wrk"].ToString());
              //  }
                sqlReader.Close();
            }
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
        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            double amount;
            if (textBox14.Text != "" && textBox11.Text != "")
            {
                amount = float.Parse(textBox11.Text, CultureInfo.InvariantCulture.NumberFormat) * float.Parse(textBox14.Text, CultureInfo.InvariantCulture.NumberFormat);
                textBox7.Text = amount.ToString();
            }

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            double amount;
            if (textBox14.Text != "" && textBox11.Text != "")
            {
                amount = float.Parse(textBox11.Text, CultureInfo.InvariantCulture.NumberFormat) * float.Parse(textBox14.Text, CultureInfo.InvariantCulture.NumberFormat);
                textBox7.Text = amount.ToString();
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

       
    }
}
