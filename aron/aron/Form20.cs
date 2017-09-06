using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace aron
{
    public partial class Form20 : Form
    {
        public int check;
        public Form20(int c)
        {
            check = c;
            InitializeComponent();
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(textBox1.Text));

                //get hash result after compute it
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    strBuilder.Append(result[i].ToString("x2"));
                }


                StreamReader sr = new StreamReader(@"safe\p_login.txt");
                string pas = sr.ReadLine();
                sr.Close();
                if (pas == strBuilder.ToString())
                {
                    if (check == 1)
                    { panel6.Visible = false;
                    panel3.Visible = true;
                    panel1.Visible = false;
                    panel4.Visible = false;
                    panel7.Visible = false;
                    panel8.Visible = false;
                    }
                    else if (check == 2)
                    {
                        panel6.Visible = false;
                        panel3.Visible =false;
                        panel1.Visible = true;
                        panel4.Visible = false;
                        panel7.Visible = false;
                        panel8.Visible = false;
                        StreamReader sr1 = new StreamReader(@"safe\n_adrs.txt");
                        textBox2.Text = sr1.ReadLine();
                        textBox5.Text = sr1.ReadLine();
                        textBox6.Text = sr1.ReadLine();
                        sr1.Close();
                    }
                    else if (check == 3)
                    {
                        panel6.Visible = false;
                        panel3.Visible = false;
                        panel4.Visible = true;
                        panel1.Visible = false;
                        panel7.Visible = false;
                        panel8.Visible = false;
                        //Directory.GetCurrentDirectory()
                        DirectoryInfo dinfo = new DirectoryInfo(@"databases\");
                        FileInfo[] Files = dinfo.GetFiles("*.s3db");
                        foreach (FileInfo file in Files)
                        {
                            string a = Path.GetFileNameWithoutExtension(file.Name);
                             comboBox1.Items.Add(a);
                        }
                    }
                    else if (check == 4)
                    {
                        panel6.Visible = false;
                        panel3.Visible = false;
                        panel7.Visible = true;
                        panel1.Visible = false;
                        panel4.Visible = false;
                        panel8.Visible = false;
                    }
                    else if (check == 5)
                    {
                        panel6.Visible = false;
                        panel3.Visible = false;
                        panel7.Visible =false;
                        panel1.Visible = false;
                        panel4.Visible = false;
                        panel8.Visible = true;
                        button9.Visible = true;
                        button8.Visible = false;
                        DirectoryInfo dinfo = new DirectoryInfo(@"databases\");
                        FileInfo[] Files = dinfo.GetFiles("*.s3db");
                        foreach (FileInfo file in Files)
                        {
                            string a = Path.GetFileNameWithoutExtension(file.Name);
                             comboBox2.Items.Add(a);
                        }
                        StreamReader sr1 = new StreamReader(@"safe\slt_db.txt");
                       comboBox2.SelectedItem = sr1.ReadLine();
                       sr1.Close();
                    }
                    else if (check == 6)
                    {
                        panel6.Visible = false;
                        panel3.Visible = false;
                        panel7.Visible = false;
                        panel1.Visible = false;
                        panel4.Visible = false;
                        panel8.Visible = true;
                        button9.Visible = false;
                        button8.Visible = true;
                        DirectoryInfo dinfo = new DirectoryInfo(@"databases\");
                        FileInfo[] Files = dinfo.GetFiles("*.s3db");
                        foreach (FileInfo file in Files)
                        {
                            string a = Path.GetFileNameWithoutExtension(file.Name);
                            comboBox2.Items.Add(a);
                        }
                        StreamReader sr1 = new StreamReader(@"safe\slt_db.txt");
                        comboBox2.SelectedItem = sr1.ReadLine();
                        sr1.Close();
                    }
                    else
                    {
                        Form1 form = new Form1();
                        form.FormClosed += new FormClosedEventHandler(frm2_FormClosed);
                        this.Hide();
                        form.Show();
                    }
                }
                else
                { MessageBox.Show("INCORRECT PASSWORD"); }
                 
            }
            catch { MessageBox.Show("INCORRECT PASSWORD"); }

        }

        private void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox4.Text)
            { MessageBox.Show("Retyped Password is not matched"); }
            else if (textBox3.Text == "")
            { MessageBox.Show("Password Cannot be empty"); }
            else
            {                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(textBox3.Text));

                //get hash result after compute it
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    strBuilder.Append(result[i].ToString("x2"));
                }
                System.IO.File.WriteAllText(@"safe\p_login.txt", string.Empty);

                StreamWriter sw = new StreamWriter(@"safe\p_login.txt", true, Encoding.ASCII);

                sw.Write(strBuilder.ToString());
                sw.Close();
                MessageBox.Show("PASSWORD RESET SUCCESSFULLY");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(@"safe\n_adrs.txt", string.Empty);

            StreamWriter sw = new StreamWriter(@"safe\n_adrs.txt", true, Encoding.ASCII);

            sw.Write(textBox2.Text.ToString()+"\n");
            sw.Write(textBox5.Text.ToString()+"\n");
            sw.Write(textBox6.Text.ToString()+"\n");
            sw.Close();
            MessageBox.Show("UPDATION SAVED SUCCESSFULLY");
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            textBox7.Text = fbd.SelectedPath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && textBox7.Text != "")
            {
                try { System.IO.File.Copy(@"databases\" + comboBox1.Text + ".s3db", @textBox7.Text+ "\\" + comboBox1.Text + ".s3db",true);
                MessageBox.Show("Exported Successfully");
                }
                catch (Exception es) { MessageBox.Show("error "+es); }
            }
            else
            { MessageBox.Show("Please Fill Data"); } 

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "(*.s3db) | *.s3db;";
            if (file.ShowDialog() == DialogResult.OK)
            {
               textBox8.Text = file.FileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                try
                {
                    System.IO.File.Copy(@textBox8.Text, @"databases\" + Path.GetFileName(textBox8.Text), false);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception es) { MessageBox.Show("FileName Already exist try other one \n" + es); }
            }
            else
            { MessageBox.Show("Please Fill Data"); } 

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr1 = new StreamReader(@"safe\slt_db.txt");
                 System.IO.File.Copy(@Directory.GetCurrentDirectory() + "\\database.s3db", @"databases\" + sr1.ReadLine() + ".s3db", true);
                 sr1.Close();
                System.IO.File.Copy(@"databases\" + comboBox2.Text + ".s3db",@Directory.GetCurrentDirectory()+"\\database.s3db", true);
                System.IO.File.WriteAllText(@"safe\slt_db.txt", string.Empty);
                StreamWriter sw = new StreamWriter(@"safe\slt_db.txt", true, Encoding.ASCII);
                sw.Write(comboBox2.Text);
                sw.Close();
                MessageBox.Show(comboBox2.Text + "  SELECTED");
            }
            catch (Exception es) { MessageBox.Show("" + es); }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr1 = new StreamReader(@"safe\slt_db.txt");
                if (sr1.ReadLine() == comboBox2.Text)
                { MessageBox.Show(comboBox2.Text + " IS CURRENTLY SELECTED DATABSE CAN'T BE DELETED");
                }
              else
                {
                    File.Delete(@"databases\" + comboBox2.Text + ".s3db");
                    MessageBox.Show(comboBox2.Text + " DELETED SUCCESSFULLY");
                    comboBox2.Items.Clear();
                    DirectoryInfo dinfo = new DirectoryInfo(@"databases\");
                    FileInfo[] Files = dinfo.GetFiles("*.s3db");
                    foreach (FileInfo file in Files)
                    {
                        string a = Path.GetFileNameWithoutExtension(file.Name);
                        comboBox2.Items.Add(a);
                    }

                } sr1.Close();
            }
            catch (Exception es) { MessageBox.Show("" + es); }
          
        }

       

    }
}
