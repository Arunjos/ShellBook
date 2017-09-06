using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;
namespace aron
{   
    public partial class Form15 : Form
    {
        public string[] print1;
        public Form15(string[] print)
        {
            print1 = print;
            InitializeComponent();
        }

        private void Form15_Load(object sender, EventArgs e)
        {
            StreamReader sr1 = new StreamReader(@"safe\n_adrs.txt");
            sr1.ReadLine();
            label4.Text = sr1.ReadLine();
            label14.Text = sr1.ReadLine();
            sr1.Close();
           // MessageBox.Show(print1[3]);
            label13.Text = print1[0];
            label7.Text = print1[1];
            label12.Text = print1[2];
            //label14.Text = print1[3];
            try
            {
                int inputNo = int.Parse(print1[2]);
                if (inputNo == 0)
                    label8.Text = "Zero";

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
                        if ((h > 0 || i == 0) && inputNo > 99) sb.Append("and ");
                        if (t == 0)
                            sb.Append(words0[u]);
                        else if (t == 1)
                            sb.Append(words1[u]);
                        else
                            sb.Append(words2[t - 2] + words0[u]);
                    }
                    if (i != 0) sb.Append(words3[i - 1]);
                }
                label8.Text = sb.ToString().TrimEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The Amount is not in Correct Format");
            } 
           try
           {

           StreamReader sr = new StreamReader(@"i_sl.txt");
         string sl = sr.ReadLine();
         sr.Close();
         int slno = int.Parse(sl);
         slno++;
           System.IO.File.WriteAllText(@"i_sl.txt", string.Empty);
           
               StreamWriter sw = new StreamWriter(@"i_sl.txt", true, Encoding.ASCII);

               sw.Write(slno.ToString());
                sw.Close();
                label9.Text = sl;
           }
           catch (Exception ex)
           {
               MessageBox.Show("error" + ex);
           }

           if (print1[3] == "CASH")
           { 
           panel3.Location = new Point(0,230);
           panel3.Visible = true;
           panel4.Visible = false;
           panel5.Visible = false;
           }
           else if (print1[3] == "BANK")
           {
               label5.Text = print1[4];
               panel4.Location = new Point(0, 230);
               panel4.Visible = true;
               panel3.Visible = false;
               panel5.Visible = false;
           }
           else
           {
               label16.Text = print1[4];
               label17.Text = print1[5];
               label26.Text = print1[6];
               panel5.Location = new Point(0, 230);
               panel5.Visible = true;
               panel3.Visible = false;
               panel4.Visible = false;
           }

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.Doc_PrintPag;
            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;
            ((ToolStripButton)((ToolStrip)printPreviewDialog1.Controls[1]).Items[0]).Enabled
          = false;//disable the direct print from printpreview.as when we click that Print button PrintPage event fires again.
            printPreviewDialog1.Document = doc;
            // doc.DefaultPageSettings.PaperSize = new PaperSize("papersize", 150, 500); ;
            printPreviewDialog1.ShowDialog();
            //a = 1;
           // if (dlgSettings.ShowDialog() == DialogResult.OK)
           // {
           //     doc.Print();
           // }
        }
        private void Doc_PrintPag(object sender, PrintPageEventArgs e)
        {

            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            // this.DrawToBitmap(bmp, new Rectangle(tableLayoutPanel1.Location.X, tableLayoutPanel1.Location.Y, tableLayoutPanel1.Width, tableLayoutPanel1.Height));
            panel1.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));
            e.Graphics.DrawImage((System.Drawing.Image)bmp, 0, y);


            /*   
             * PRINT MULTIPLE PAGES (see tutorial folder)
             * if (a == 1)
                { a = 0; e.HasMorePages = true; return; }
                else
                    e.HasMorePages = false;*/
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.Doc_PrintPag;
            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;
            ((ToolStripButton)((ToolStrip)printPreviewDialog1.Controls[1]).Items[0]).Enabled
          = false;//disable the direct print from printpreview.as when we click that Print button PrintPage event fires again.
            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        private void etractToPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            // this.DrawToBitmap(bmp, new Rectangle(tableLayoutPanel1.Location.X, tableLayoutPanel1.Location.Y, tableLayoutPanel1.Width, tableLayoutPanel1.Height));
            panel1.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));
            
           
            Document doc = new Document();
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(@"../rcpt/receipt"+label9.Text+".pdf", FileMode.Create));
                doc.Open();

                //doc.Add(new Paragraph("GIF"));
                // Image gif = Image.GetInstance(imagepath + "/mikesdotnetting.gif");
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Bmp);
                img.ScaleAbsolute(159f, 159f);
                PdfPTable table = new PdfPTable(1);
                table.AddCell(img);
                doc.Add(table);
                //doc.Add(a);
                MessageBox.Show("Pdf created Suuccesfully");
            }
            catch (Exception ex)
            {
                //Log error;
                MessageBox.Show("error" + ex);
            }
            finally
            {
                doc.Close();
            }

        }
        
    }
}
