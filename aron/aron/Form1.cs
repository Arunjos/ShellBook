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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string inputFile, dbConnection;
        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            button1.BackColor = Color.DarkBlue;
            button1.ForeColor = Color.White;
            button10.BackColor = Color.White;
            button10.ForeColor = Color.Black;
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;        
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel7.Visible = false;
            panel1.Visible = false;
            panel4.Visible = false;

            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
            button10.BackColor = Color.White;
            button10.ForeColor = Color.Black;
            button3.BackColor = Color.DarkBlue;
            button3.ForeColor = Color.White;
            
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile); button3_Click(null, null);
            //  string dbConnection;  
      //  string inputFile = "database.s3db";
       //  dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);
         // string sql = "CREATE TABLE clientdetails (slno integer primary key, name varchar(1000), addr varchar(10000),phno varchar(1000), email varchar(1000), tob varchar(1000),camt varchar(1000),date varchar(1000),period varchar(1000));";//table syntax
         // string sql = "CREATE TABLE incomehddetails (slno integer , imainhd varchar(1000), isubhd varchar(10000));";
            //string sql = "CREATE TABLE exphddetails (slno integer , emainhd varchar(1000), esubhd varchar(10000));";
          // string sql = "Select Max(slno) From incomehddetails";
          // string sql = "insert into incomehddetails (imainhd ,isubhd) values ('arrr ',' arun ');";
          // string sql = " DROP Table 'incomehddetails';";
          // string sql = "ALTER TABLE exphddetails ADD COLUMN nam_o_cont typ_o_wrk catagory;";
           // string sql = "ALTER TABLE addexdetails ADD COLUMN  subcont_dwage d_wage d_num d_name d_cat;";
           // string sql = "CREATE TABLE addindetails (id integer primary key,date varchar(1000),name varchar(1000), inacc varchar(10000),inhd varchar(1000), des varchar(1000),amnt varchar(1000),mthd varchar(1000),bnkname varchar(1000),chqno varchar(1000),chqdt varchar(1000));";//table syntax
            // string sql = "CREATE TABLE incomehddetails (slno integer , imainhd varchar(1000), isubhd varchar(10000));";
     //    string sql = "CREATE TABLE addexdetails (id integer primary key,date varchar(1000),name varchar(1000), exacc varchar(10000),item varchar(1000), des varchar(1000),amnt varchar(1000),mthd varchar(1000),bnkname varchar(1000),chqno varchar(1000),chqdt varchar(1000),mat_trans_mat_pur varchar(1000),frm_shop varchar(1000),frm_site varchar(1000),mat_quan varchar(1000),mat_unit varchar(1000),nam_contract varchar(1000),typ_wrk varchar(1000));";
         //string sql = " DROP Table 'addexdetails'";
     // SQLiteConnection cnn = new SQLiteConnection(dbConnection);
  //  cnn.Open();
   //   SQLiteCommand mycommand = new SQLiteCommand(cnn);
   //  mycommand.CommandText = sql;
           // object val = mycommand.ExecuteScalar();
           // int maxId = int.Parse(val.ToString());
    //   int rowsUpdated = mycommand.ExecuteNonQuery();
           // MessageBox.Show(" large" + maxId);
        }



        private void createNewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Tag = this;
            form3.ShowDialog(this);
        }

        private void addExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            contextMenuStrip1.Show(button4, new Point(0, button4.Height));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form10 form = new Form10();
            form.Tag = this;
            form.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form11 form = new Form11();
            form.Tag = this;
            form.ShowDialog(this);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Tag = this;
            form5.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Tag = this;
            form6.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel1.Visible = false;
            panel3.Visible = false;
            panel7.Visible = false;

            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
            button10.BackColor = Color.White;
            button10.ForeColor = Color.Black;
            button2.BackColor = Color.DarkBlue;
            button2.ForeColor = Color.White;
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Tag = this;
            form2.ShowDialog(this);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //contextMenuStrip1.Show(button8, new Point(0, button8.Height));
            int inputNo = int.Parse( textBox1.Text);

  if (inputNo == 0)
     textBox1.Text= "Zero";

  int[] numbers = new int[4];
  int first = 0;
  int u, h, t;
  System.Text.StringBuilder sb = new System.Text.StringBuilder();

  if (inputNo < 0)
  {
      sb.Append("Minus ");
      inputNo = -inputNo;
  }

  string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
          "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
  string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
          "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
  string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
          "Seventy ","Eighty ", "Ninety "};
  string[] words3 = { "Thousand ", "Lakh ", "Crore " };

  numbers[0] = inputNo % 1000; // units
  numbers[1] = inputNo / 1000;
  numbers[2] = inputNo / 100000;
  numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
  numbers[3] = inputNo / 10000000; // crores
  numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

  for (int i = 3; i > 0; i--)
  {
      if (numbers[i] != 0)
      {
          first = i;
          break;
      }
  }
  for (int i = first; i >= 0; i--)
  {
      if (numbers[i] == 0) continue;
      u = numbers[i] % 10; // ones
      t = numbers[i] / 10;
      h = numbers[i] / 100; // hundreds
      t = t - 10 * h; // tens
      if (h > 0) sb.Append(words0[h] + "Hundred ");
      if (u > 0 || t > 0)
      {
          if ((h > 0 || i == 0)&&inputNo>99) sb.Append("and ");
          if (t == 0)
              sb.Append(words0[u]);
          else if (t == 1)
              sb.Append(words1[u]);
          else
              sb.Append(words2[t - 2] + words0[u]);
      }
      if (i != 0) sb.Append(words3[i - 1]);
  }
  textBox1.Text = sb.ToString().TrimEnd();

        }

        private void arunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Tag = this;
             form3.ShowDialog(this);
        }

        private void hiranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.Tag = this;
            form.ShowDialog(this);
        }

        private void himaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 form = new Form7();
            form.Tag = this;
            form.ShowDialog(this);
        }

        private void aDDEXPENSEACOUNTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.Tag = this;
            form.ShowDialog(this);
        }

        private void aDDEXPENSESUBHEADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 form = new Form9();
            form.Tag = this;
            form.ShowDialog(this);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(1);
            form.Tag = this;
            form.ShowDialog(this);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(button9, new Point(0, button9.Height));
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            contextMenuStrip3.Show(button7, new Point(0, button7.Height));
        }

        private void addIncomeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form21 form = new Form21();
            form.Tag = this;
            form.ShowDialog(this);
           
        }

        private void addExpenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form22 form = new Form22();
            form.Tag = this;
            form.ShowDialog(this);
            
        }

        private void viewIncomeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void viewExpenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

        }

        private void button11_Click(object sender, EventArgs e)
        {
            contextMenuStrip4.Show(button11, new Point(0, button11.Height));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            contextMenuStrip5.Show(button14, new Point(0, button14.Height));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(1);
            form.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(11);
            form.ShowDialog();
        }

        private void incomeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(12);
            form.ShowDialog();
        }

        private void accountDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(13);
            form.ShowDialog();
        }

        private void chartOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(14);
            form.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(2);
            form.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(21);
            form.ShowDialog();
        }

        private void expenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(22);
            form.ShowDialog();
        }

        private void accountDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(23);
            form.ShowDialog();
        }

        private void chartOfAccountsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(24);
            form.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12(3);
            form.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
            button10.BackColor = Color.DarkBlue;
            button10.ForeColor = Color.White;
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
            
        }

        private void button16_Click(object sender, EventArgs e)
        {
            contextMenuStrip6.Show(button16, new Point(0, button16.Height));
        }

        private void button15_Click(object sender, EventArgs e)
        {
            contextMenuStrip7.Show(button15, new Point(0, button15.Height));
        }

        private void allUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from clientdetails";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from addindetails";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from addexdetails ";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            MessageBox.Show("Erased succesfully");
        }

        private void allMainHeadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from incomehddetails ";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from exphddetails";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "insert into exphddetails (slno,emainhd,esubhd) values ('1 ','materials','" + null + "');";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "insert into exphddetails (slno,emainhd,esubhd) values ('2','labour charge','" + null + "');";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "insert into exphddetails (slno,emainhd,esubhd) values ('3','sub contractor','" + null + "');";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from addindetails";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from addexdetails ";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            MessageBox.Show("Erased succesfully");
        }

        private void allSubHeadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from exphddetails where esubhd !='" + null+ "' ";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from incomehddetails where  isubhd ='" + null + "' ";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }

            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from addindetails";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
            {
                string sql = "delete from addexdetails ";

                SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn);
                cnn.Open();
                int suc = sqlCmd.ExecuteNonQuery();
            }
            MessageBox.Show("Erased succesfully");
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form20 form = new Form20(1);
            form.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Form20 form = new Form20(2);
            form.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Form20 form = new Form20(3);
            form.ShowDialog();

        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Form20 form = new Form20(4);
            form.ShowDialog();

        }

        private void selectDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form20 form = new Form20(5);
            form.ShowDialog();

        }

        private void deleteDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form20 form = new Form20(6);
            form.ShowDialog();
        }
    }

     
    }

