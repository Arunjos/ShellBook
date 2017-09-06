using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Data.SQLite;
using System.Globalization;
namespace aron
{
    public partial class Form12 : Form
    {
        public Dictionary<int, bool> dic;
        public int rowIndex, check=-1,diff=24,z=0;
        public int[] row;
        public double[] sum;
        public string inputFile, dbConnection;
        public DataTable dataTable;
        public SQLiteDataAdapter ad;
        public SQLiteConnection cnn;
        public string a, b, nam;
        public Form12(int rpt)
        {
            diff = rpt;
            InitializeComponent();
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
          tableLayoutPanel1.CellPaint += tableLayoutPanel_CellPaint;
          dataGridView1.RowHeadersVisible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.Doc_PrintPag;
          ((ToolStripButton)((ToolStrip)printPreviewDialog1.Controls[1]).Items[0]).Enabled
         = false;//disable the direct print from printpreview.as when we click that Print button PrintPage event fires again.
             printPreviewDialog1.Document = doc;
            // doc.DefaultPageSettings.PaperSize = new PaperSize("papersize", 150, 500); ;
             rect_y = 0;
             printPreviewDialog1.ShowDialog();

        }

         private void button2_Click(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.Doc_PrintPag;
            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;
            
            //a = 1;
            rect_y = 0;
            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }



        public TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
      
       public int rect_y=0,pdf_rect_y =0;
        
private void Doc_PrintPag(object sender, PrintPageEventArgs e)
         {
            
             float x = e.MarginBounds.Left;
              float y = e.MarginBounds.Top;
              if (rect_y + 800 < tableLayoutPanel1.Height)
              {
                  System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, rect_y, tableLayoutPanel1.Width, 800);
                  Bitmap bmp1 = new Bitmap(tableLayoutPanel1.Width, tableLayoutPanel1.Height);
                  tableLayoutPanel1.DrawToBitmap(bmp1, new System.Drawing.Rectangle(0, 0, bmp1.Width, bmp1.Height));
                 Bitmap bmp2 = bmp1.Clone(rect, bmp1.PixelFormat);
                  Bitmap bmp = new Bitmap(bmp2, new Size(550, 800));
                  e.Graphics.DrawImage((System.Drawing.Image)bmp, x, y);
                  rect_y = rect_y + 800;
                  e.HasMorePages = true; //e.HasMorePages raised the PrintPage event once per page .           
                  return;//It will call PrintPage event again   
              }
              else if(rect_y + 800 >= tableLayoutPanel1.Height)
              {
                  System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, rect_y, tableLayoutPanel1.Width, (tableLayoutPanel1.Height - rect_y));
                  Bitmap bmp1 = new Bitmap(tableLayoutPanel1.Width, tableLayoutPanel1.Height);
                  tableLayoutPanel1.DrawToBitmap(bmp1, new System.Drawing.Rectangle(0, 0, bmp1.Width, bmp1.Height));
                 Bitmap bmp2 = bmp1.Clone(rect, bmp1.PixelFormat);
                 Bitmap bmp = new Bitmap(bmp2, new Size(550, (tableLayoutPanel1.Height - rect_y)));
                  e.Graphics.DrawImage((System.Drawing.Image)bmp, x, y);
                  rect_y = rect_y + 800;
                  e.HasMorePages = false; //e.HasMorePages raised the PrintPage event once per page .           
                         }
        }


private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
{
    try
    {
        if (dic[e.RowIndex])
            dic[e.RowIndex] = false;
        else
            dic[e.RowIndex] = true;

    foreach (KeyValuePair<int, bool> kvp in dic)
    {
        if (kvp.Value == false)
            dataGridView1.Rows[kvp.Key].Selected = false;
        else
            dataGridView1.Rows[kvp.Key].Selected = true;
    }
    }
    catch (Exception ex)
    { dataGridView1.SelectAll(); }
}

        private void button1_Click(object sender, EventArgs e)
        {
            // TableLayoutPanel Initialization
           // tableLayoutPanel1 are = new tableLayoutPanel1();
            // TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel(); 
            a = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            b = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            tableLayoutPanel1.Location = new Point(13, 175);
            label6.Text = "For"+a+"Through"+b;
tableLayoutPanel1.ColumnCount = 3;
tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.LightBlue;
tableLayoutPanel1.Size = new System.Drawing.Size(600, 50);
tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
tableLayoutPanel1.Controls.Add(new Label() { Text = "Address" ,Size= new Size(50,20) }, 0, 0);
tableLayoutPanel1.Controls.Add(new Label() { Text = "Contact No" }, 1, 0);
tableLayoutPanel1.Controls.Add(new Label() { Text = "Email ID" }, 2, 0);

// For Add New Row (Loop this code for add multiple rows)
tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
tableLayoutPanel1.Size = new System.Drawing.Size(600, 100);
tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
tableLayoutPanel1.Controls.Add(new Label() { Text = "Street, City, State" }, 0, tableLayoutPanel1.RowCount - 1);
tableLayoutPanel1.Controls.Add(new Label() { Text = "888888888888" }, 1, tableLayoutPanel1.RowCount - 1);
tableLayoutPanel1.Controls.Add(new Label() { Text = "xxxxxxx@gmail.com" }, 2, tableLayoutPanel1.RowCount - 1);

tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
tableLayoutPanel1.Size = new System.Drawing.Size(600, 150);
tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
tableLayoutPanel1.Controls.Add(new Label() { Text = "Street, City, State" }, 0, tableLayoutPanel1.RowCount - 1);
tableLayoutPanel1.Controls.Add(new Label() { Text = "888888888888" }, 1, tableLayoutPanel1.RowCount - 1);
tableLayoutPanel1.Controls.Add(new Label() { Text = "xxxxxxx@gmail.com" }, 2, tableLayoutPanel1.RowCount - 1);

tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
tableLayoutPanel1.Size = new System.Drawing.Size(600, 200);
tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
tableLayoutPanel1.Controls.Add(new Label() { Text = "Street, City, State" }, 0, tableLayoutPanel1.RowCount - 1);
tableLayoutPanel1.Controls.Add(new Label() { Text = "888888888888" }, 1, tableLayoutPanel1.RowCount - 1);
tableLayoutPanel1.Controls.Add(new Label() { Text = "xxxxxxx@gmail.com" }, 2, tableLayoutPanel1.RowCount - 1);

tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
tableLayoutPanel1.Size = new System.Drawing.Size(600, 250);
tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
tableLayoutPanel1.Controls.Add(new Label() { Text = "Street, City, State" }, 0, tableLayoutPanel1.RowCount - 1);
tableLayoutPanel1.Controls.Add(new Label() { Text = "888888888888" }, 1, tableLayoutPanel1.RowCount - 1);
tableLayoutPanel1.Controls.Add(new Label() { Text = "xxxxxxx@gmail.com" }, 2, tableLayoutPanel1.RowCount - 1);

Controls.Add(tableLayoutPanel1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string sl="";
            try
            {

                StreamReader sr = new StreamReader(@"rpt_sl.txt");
              sl = sr.ReadLine();
                sr.Close();
                int slno = int.Parse(sl);
                slno++;
                System.IO.File.WriteAllText(@"rpt_sl.txt", string.Empty);

                StreamWriter sw = new StreamWriter(@"rpt_sl.txt", true, Encoding.ASCII);

                sw.Write(slno.ToString());
                sw.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex);
            }
            try
            { 
            Document doc = new Document(PageSize.A4);
            PdfWriter.GetInstance(doc, new FileStream(@"../rpt/report" + sl + ".pdf", FileMode.Create));
            doc.Open();
            while (true)
            {
                if (pdf_rect_y + 800 < tableLayoutPanel1.Height)
                {
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, pdf_rect_y, tableLayoutPanel1.Width, 800);
                    Bitmap bmp1 = new Bitmap(tableLayoutPanel1.Width, tableLayoutPanel1.Height);
                    tableLayoutPanel1.DrawToBitmap(bmp1, new System.Drawing.Rectangle(0, 0, bmp1.Width, bmp1.Height));
                    Bitmap bmp2 = bmp1.Clone(rect, bmp1.PixelFormat);
                    Bitmap bmp = new Bitmap(bmp2, new Size(550, 800));

                    //doc.Add(new Paragraph("GIF"));
                    // Image gif = Image.GetInstance(imagepath + "/mikesdotnetting.gif");
                    iTextSharp.text.Image a = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Bmp);
                    doc.Add(a);
                    doc.NewPage();
                    pdf_rect_y = pdf_rect_y + 800;
                      
                }
                else if (pdf_rect_y + 800 >= tableLayoutPanel1.Height)
                {

                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, pdf_rect_y, tableLayoutPanel1.Width, (tableLayoutPanel1.Height - pdf_rect_y));
                    Bitmap bmp1 = new Bitmap(tableLayoutPanel1.Width, tableLayoutPanel1.Height);
                    tableLayoutPanel1.DrawToBitmap(bmp1, new System.Drawing.Rectangle(0, 0, bmp1.Width, bmp1.Height));
                    Bitmap bmp2 = bmp1.Clone(rect, bmp1.PixelFormat);
                    Bitmap bmp = new Bitmap(bmp2, new Size(550, (tableLayoutPanel1.Height - pdf_rect_y)));
                    iTextSharp.text.Image a = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Bmp);
                    doc.Add(a);
                    doc.Close();
                 break;         
                }

                
            }

                 //doc.SetPageSize(new Rectangle(0,0,(bmp.Width, bmp.Height)));

          
           MessageBox.Show("Extracted Successfully");
            }
            catch (Exception ex)
            {
                //Log error;
                MessageBox.Show("error"+ex);
            }
            
               
           

        }

        private void Form12_Load(object sender, EventArgs e)
        {
           
            inputFile = "database.s3db";
            dbConnection = String.Format("Data Source={0};Version=3;Password=KraQlin;", inputFile);
            //using (
            cnn = new SQLiteConnection(dbConnection);
           ;
            cnn.Open();
           
            ad = new SQLiteDataAdapter("SELECT name FROM clientdetails", cnn);
            SQLiteCommandBuilder sqlCommandBuilder = new SQLiteCommandBuilder(ad);

            dataTable = new DataTable();
            ad.Fill(dataTable);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;

            dataGridView1.DataSource = bindingSource;
            dataGridView1.ClearSelection();
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 240;
            dataGridView1.SelectAll();
            dic = new Dictionary<int, bool>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dic.Add(i, false);

            if (diff == 13)
            {
                label7.Visible = true;
                comboBox1.Visible = true; comboBox1.Items.Clear();
                using (SQLiteConnection cnn1 = new SQLiteConnection(dbConnection))
                {
                    string sql = "SELECT * FROM incomehddetails where isubhd= '" + null + "';";
                    SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn1);
                    cnn1.Open();
                    SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox1.Items.Add(sqlReader["imainhd"].ToString());

                    }
                    comboBox1.SelectedIndex = 0;
                    sqlReader.Close();
                }
            }
            if (diff == 23)
            {
                label7.Visible = true;
                comboBox1.Visible = true; comboBox1.Items.Clear();
                using (SQLiteConnection cnn1 = new SQLiteConnection(dbConnection))
                {
                    string sql = "SELECT * FROM exphddetails where esubhd= '" + null + "';";
                    SQLiteCommand sqlCmd = new SQLiteCommand(sql, cnn1);
                    cnn1.Open();
                    SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox1.Items.Add(sqlReader["emainhd"].ToString());

                    }
                    comboBox1.SelectedIndex = 0;
                    sqlReader.Close();
                }
            }
            if (diff == 24) {button1_Click_2(null,null); }
            if (diff == 14) { button1_Click_2(null, null); }

        }
        private void tableLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            
           if (e.Row == 3 || check == e.Row)
            {
              //  check = -1;
                Point p = new Point(e.CellBounds.Location.X - 3, e.CellBounds.Location.Y - 3);

                e.Graphics.DrawLine(Pens.Black, p, new Point(e.CellBounds.Right - 3, e.CellBounds.Top - 3));
            }
           for (int i = 0; i < z; i++)
           {
               if (e.Row == row[i])
               {
                   Point p = new Point(e.CellBounds.Location.X - 3, e.CellBounds.Location.Y - 3);

                   e.Graphics.DrawLine(Pens.Black, p, new Point(e.CellBounds.Right - 3, e.CellBounds.Top - 3));
               }
           }
        }
     public   DataTable t = new DataTable();
        private void button1_Click_2(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
          string  a1 = dateTimePicker1.Value.ToString("dd-MM-yyyy");
          string  b1 = dateTimePicker2.Value.ToString("dd-MM-yyyy");
                a = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                b = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            int add = 50;
          //  label5.Visible = true;
          //  label6.Visible = true;
            if (diff == 1)
            {// label5.Text = "INCOME SUMMARY REPORT";
                
                string slno;
                sum = new double[1000];
                int i = 0;
                double totalsum = 0;
                //int add = 50;
                ;
                tableLayoutPanel1.Location = new Point(100, 50);
               // label6.Text = "For " + a + " Through " + b;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "     INCOME SUMMARY REPORT", Size = new Size(250, 15) }, 1, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, 0);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "For " + a1 + " Through " + b1, Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Sl No.", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Client Name", Size = new Size(250, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Totals", Size = new Size(150, 12) }, 2, tableLayoutPanel1.RowCount - 1);

                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    sum[i] = 0;
                    check = -1;
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;

                    //  MessageBox.Show(ID.ToString());
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM addindetails  WHERE  date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "' order by date desc", cnn);
                        cnn.Open();
                        SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                        while (sqlReader.Read())
                        {
                            sum[i] = sum[i] + double.Parse(sqlReader["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        }

                        sqlReader.Close();

                    } using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM clientdetails WHERE name = '" + myRow[0].ToString() + "'", cnn);
                        cnn.Open();
                        SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                        sqlReader.Read();

                        slno = sqlReader["slno"].ToString();


                        sqlReader.Close();

                    }

                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = slno, Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = sum[i].ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                    i++;
                }
                check = i + 3;
                for (int j = 0; j < i; j++)
                    totalsum = totalsum + sum[j];
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Grand Total", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = totalsum.ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "SELECTED CLIENTS", Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text =  "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(50, 15)}, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                }
                for (int y = 100; y > 0; y--)
                {
                    
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                }
                panel1.Controls.Add(tableLayoutPanel1);

                //check = -1;
            }
            else if(diff ==11)
            {
                row = new int[1000];
                label5.Text = "INCOME BY ACCOUNT";
                int i = 1;
                double totalsum = 0;
                a = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                b = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                tableLayoutPanel1.Location = new Point(100, 50);
                label6.Text = "For " + a + " Through " + b;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));


                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "     INCOME BY ACCOUNT", Size = new Size(250, 15) }, 1, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, 0);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "For " + a1 + " Through " + b1, Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;

                tableLayoutPanel1.Controls.Add(new Label() { Text = "Date", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Name Of Account", Size = new Size(150, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Current Amount", Size = new Size(150, 12) }, 2, tableLayoutPanel1.RowCount - 1);

                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                {
                    SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM incomehddetails WHERE   isubhd =''", cnn);
                    cnn.Open();
                    SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                       tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = i++ +" - "+ sqlReader["imainhd"].ToString(), Size = new Size(150, 15),Font =new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                totalsum = 0;
               // DataTable t = new DataTable();
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {

                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                    using (SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM addindetails  WHERE date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "'AND inacc = '" + sqlReader["imainhd"].ToString() + "' order by date desc", cnn))
                        {
                             SQLiteDataAdapter a22 = new SQLiteDataAdapter(sqlCmd1);
                         a22.Fill(t);
                       //  t.Load(sqlCmd1.ExecuteReader());
                         // a22.Update(t);
                           // t.Rows.Add(a22);
                    SQLiteDataReader sqlReader1 = sqlCmd1.ExecuteReader();
                    
                  while (sqlReader1.Read())
                    {
                          tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                            add = add + 20;
                            tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["date"].ToString(), Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["inhd"].ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["amnt"].ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                            totalsum = totalsum + double.Parse(sqlReader1["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        
                    }
                    
              sqlReader1.Close();
           
                   }

                } t.DefaultView.Sort ="date";
                t = t.DefaultView.ToTable();
                //dataGridView2.DataSource = t;
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = totalsum.ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                row[z++] = tableLayoutPanel1.RowCount-1;

                    }

                    sqlReader.Close();

                }

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "SELECTED CLIENTS", Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                }

                panel1.Controls.Add(tableLayoutPanel1);
            }
            else if (diff == 12)
            {
                label5.Text = "INCOME DETAIL REPORT";

                sum = new double[1000];
                int i = 0,k=0;
               double totalsum = 0;
                //int add = 50;

                a = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                b = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                tableLayoutPanel1.Location = new Point(100, 50);
                label6.Text = "For " + a + " Through " + b;
                tableLayoutPanel1.ColumnCount =4;
                tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
             

                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "     INCOME DETAIL REPORT", Size = new Size(250, 15) }, 2, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 3, 0);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                
                tableLayoutPanel1.Controls.Add(new Label() { Text = "For " + a1 + " Through " + b1, Size = new Size(250, 15) }, 2, tableLayoutPanel1.RowCount - 1);
              
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;

                tableLayoutPanel1.Controls.Add(new Label() { Text = "Date", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Sl No.", Size = new Size(50, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Description", Size = new Size(150, 12) }, 2, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Amount", Size = new Size(150, 12) }, 3, tableLayoutPanel1.RowCount - 1);
                totalsum = 0;
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    
                    check = -1;
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;

                    //  MessageBox.Show(ID.ToString());
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM addindetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "'", cnn);
                        cnn.Open();
                        SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                        while (sqlReader.Read())
                        {
                            SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM incomehddetails WHERE imainhd = '" + sqlReader["inacc"].ToString() + "' AND isubhd = ''  ", cnn);
                            
                            SQLiteDataReader sqlReader1 = sqlCmd1.ExecuteReader();

                            sqlReader1.Read();
                            
                            totalsum = totalsum + double.Parse(sqlReader["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                            tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                            add = add + 20;
                            tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["date"].ToString(), Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["slno"].ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["inhd"].ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["amnt"].ToString(), Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);
                            i++;
                            sqlReader1.Close();
                        }

                        sqlReader.Close();

                    } 
                   

                }
                check = i + 3;
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Grand Total", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = totalsum.ToString(), Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "SELECTED CLIENTS", Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                }

                panel1.Controls.Add(tableLayoutPanel1);
            }
            else if (diff == 13)
            {
                if (comboBox1.Text != "")
                {
                    label5.Text = comboBox1.Text+" INCOME DETAIL REPORT";
                     sum = new double[1000];
                    int i = 0, k = 0;
                    double totalsum = 0;
                    //int add = 50;

                    tableLayoutPanel1.Location = new Point(100, 50);
                    label6.Text = "For " + a + " Through " + b;
                    tableLayoutPanel1.ColumnCount = 4;
                    tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                    // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                    tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, 0);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, 0);
                    tableLayoutPanel1.Controls.Add(new Label() { Text =  "      "+comboBox1.Text+" INCOME DETAIL REPORT", Size = new Size(250, 15) }, 2, 0);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 3, 0);

                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);

                    tableLayoutPanel1.Controls.Add(new Label() { Text = "For " + a1 + " Through " + b1, Size = new Size(250, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);

                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Date", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Sl No.", Size = new Size(50, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Description", Size = new Size(150, 12) }, 2, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Amount", Size = new Size(150, 12) }, 3, tableLayoutPanel1.RowCount - 1);
                    totalsum = 0;
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {

                        check = -1;
                        DataRow myRow = (r.DataBoundItem as DataRowView).Row;

                        //  MessageBox.Show(ID.ToString());
                        using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                        {
                            SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM addindetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "' AND inacc = '" + comboBox1.Text + "'", cnn);
                            cnn.Open();
                            SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                            while (sqlReader.Read())
                            {
                                SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM incomehddetails WHERE imainhd = '" + sqlReader["inacc"].ToString() + "' AND isubhd = ''  ", cnn);

                                SQLiteDataReader sqlReader1 = sqlCmd1.ExecuteReader();

                                sqlReader1.Read();
                                totalsum = totalsum + double.Parse(sqlReader["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                               // double.Parse("52.8725945", System.Globalization.CultureInfo.InvariantCulture);
                                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                                add = add + 20;
                                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["date"].ToString(), Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["slno"].ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["inhd"].ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["amnt"].ToString(), Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);
                                i++;
                            }

                            sqlReader.Close();

                        }


                    }
                    check = i + 3;
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Grand Total", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = totalsum.ToString(), Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);

                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "SELECTED CLIENTS", Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {
                        DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                        tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                        add = add + 20;
                        tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                        tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                    }

                    panel1.Controls.Add(tableLayoutPanel1);
                }
                else { MessageBox.Show("please fill data"); }
            }
            else if(diff == 14)
            {
                panel2.Visible = false;
                panel1.Location = new Point(12, 3);
                panel1.Size = new Size(700, 430);
                check =-1;
                tableLayoutPanel1.Location = new Point(100, 50);
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 12) }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(250, 12) }, 1, 0);
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "     CHART OF ACCOUNT", Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);
               
 tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;

                tableLayoutPanel1.Controls.Add(new Label() { Text = "SlNo", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Name Of Account", Size = new Size(250, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                


                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                {
                    SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM incomehddetails WHERE   isubhd =''", cnn);
                    cnn.Open();
                    SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                       tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["slno"].ToString(), Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["imainhd"].ToString(), Size = new Size(250, 15),Font =new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 1, tableLayoutPanel1.RowCount - 1);
                

                SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM incomehddetails WHERE   isubhd !='' AND imainhd ='"+sqlReader["imainhd"].ToString()+"'", cnn);
                
                SQLiteDataReader sqlReader1 = sqlCmd1.ExecuteReader();
                        int h=1;
                         while (sqlReader1.Read())
                    {
                             tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                            add = add + 20;
                            tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                            tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = "  "+h++ +"   " + sqlReader1["isubhd"].ToString(), Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                           
                        }
                    
              sqlReader1.Close();
                    }
                    sqlReader.Close();
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                  check = tableLayoutPanel1.RowCount -1   ;
                   add = add + 20;
                   tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                 tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                  tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                  tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                     }
               
                panel1.Controls.Add(tableLayoutPanel1);     
            }


            else if (diff == 2)
            {// label5.Text = "INCOME SUMMARY REPORT";

                string slno;
                sum = new double[1000];
                int i = 0;
                double totalsum = 0;
                //int add = 50;
                ;
                tableLayoutPanel1.Location = new Point(100, 50);
                // label6.Text = "For " + a + " Through " + b;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "     EXPENSE SUMMARY REPORT", Size = new Size(250, 15) }, 1, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, 0);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "For " + a1 + " Through " + b1, Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Sl No.", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Client Name", Size = new Size(250, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Totals", Size = new Size(150, 12) }, 2, tableLayoutPanel1.RowCount - 1);

                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    sum[i] = 0;
                    check = -1;
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;

                    //  MessageBox.Show(ID.ToString());
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM addexdetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "'", cnn);
                        cnn.Open();
                        SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                        while (sqlReader.Read())
                        {
                            sum[i] = sum[i] + double.Parse(sqlReader["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        }

                        sqlReader.Close();

                    } using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM clientdetails WHERE name = '" + myRow[0].ToString() + "'", cnn);
                        cnn.Open();
                        SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                        sqlReader.Read();

                        slno = sqlReader["slno"].ToString();


                        sqlReader.Close();

                    }

                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = slno, Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = sum[i].ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                    i++;
                }
                check = i + 3;
                for (int j = 0; j < i; j++)
                    totalsum = totalsum + sum[j];
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Grand Total", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = totalsum.ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "SELECTED CLIENTS", Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                }

                panel1.Controls.Add(tableLayoutPanel1);

                //check = -1;
            }
            else if (diff == 21)
            {
                row = new int[1000];
                label5.Text = "INCOME BY ACCOUNT";
                int i = 1;
                double totalsum = 0;
                a = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                b = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                tableLayoutPanel1.Location = new Point(100, 50);
                label6.Text = "For " + a + " Through " + b;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));


                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "     EXPENSE BY ACCOUNT", Size = new Size(250, 15) }, 1, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, 0);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "For " + a1 + " Through " + b1, Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;

                tableLayoutPanel1.Controls.Add(new Label() { Text = "Date", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Name Of Account", Size = new Size(150, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Current Amount", Size = new Size(150, 12) }, 2, tableLayoutPanel1.RowCount - 1);


                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                {
                    SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM exphddetails WHERE   esubhd =''", cnn);
                    cnn.Open();
                    SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                        add = add + 20;
                        tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                        tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = i++ + " - " + sqlReader["emainhd"].ToString(), Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 1, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                        totalsum = 0;
                        foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                        {

                            DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                            using (SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM addexdetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "'AND exacc = '" + sqlReader["emainhd"].ToString() + "'", cnn))
                            {
                                SQLiteDataReader sqlReader1 = sqlCmd1.ExecuteReader();

                                while (sqlReader1.Read())
                                {
                                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                                    add = add + 20;
                                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                                    tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["date"].ToString(), Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                                    tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["des"].ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                                    tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["amnt"].ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                                    totalsum = totalsum + double.Parse(sqlReader1["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);

                                }

                                sqlReader1.Close();
                            }

                        }
                        tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                        add = add + 20;
                        tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                        tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = totalsum.ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                        row[z++] = tableLayoutPanel1.RowCount - 1;

                    }

                    sqlReader.Close();

                }

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "SELECTED CLIENTS", Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                }

                panel1.Controls.Add(tableLayoutPanel1);
            }
            else if (diff == 22)
            {
                label5.Text = "INCOME DETAIL REPORT";

                sum = new double[1000];
                int i = 0, k = 0;
                double totalsum = 0;
                //int add = 50;

                a = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                b = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                tableLayoutPanel1.Location = new Point(100, 50);
                label6.Text = "For " + a + " Through " + b;
                tableLayoutPanel1.ColumnCount = 4;
                tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));


                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "     EXPENSE DETAIL REPORT", Size = new Size(250, 15) }, 2, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 3, 0);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.Controls.Add(new Label() { Text = "For " + a1 + " Through " + b1, Size = new Size(250, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;

                tableLayoutPanel1.Controls.Add(new Label() { Text = "Date", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Sl No.", Size = new Size(50, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Description", Size = new Size(150, 12) }, 2, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Amount", Size = new Size(150, 12) }, 3, tableLayoutPanel1.RowCount - 1);
                totalsum = 0;
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {

                    check = -1;
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;

                    //  MessageBox.Show(ID.ToString());
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM addexdetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "'", cnn);
                        cnn.Open();
                        SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                        while (sqlReader.Read())
                        {
                            SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM exphddetails WHERE emainhd = '" + sqlReader["exacc"].ToString() + "' AND esubhd = ''  ", cnn);

                            SQLiteDataReader sqlReader1 = sqlCmd1.ExecuteReader();

                            sqlReader1.Read();

                            totalsum = totalsum + double.Parse(sqlReader["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                            tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                            add = add + 20;
                            tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["date"].ToString(), Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["slno"].ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["des"].ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["amnt"].ToString(), Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);
                            i++;
                            sqlReader1.Close();
                        }

                        sqlReader.Close();

                    }


                }
                check = i + 3;
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Grand Total", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = totalsum.ToString(), Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "SELECTED CLIENTS", Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                }

                panel1.Controls.Add(tableLayoutPanel1);
            }
            else if (diff == 23)
            {
                if (comboBox1.Text != "")
                {
                    label5.Text = comboBox1.Text + " INCOME DETAIL REPORT";
                    sum = new double[1000];
                    int i = 0, k = 0;
                    double totalsum = 0;
                    //int add = 50;

                    tableLayoutPanel1.Location = new Point(100, 50);
                    label6.Text = "For " + a + " Through " + b;
                    tableLayoutPanel1.ColumnCount = 4;
                    tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                    // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                    tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, 0);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, 0);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "      " + comboBox1.Text + " EXPENSE DETAIL REPORT", Size = new Size(250, 15) }, 2, 0);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 3, 0);

                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);

                    tableLayoutPanel1.Controls.Add(new Label() { Text = "For " + a1 + " Through " + b1, Size = new Size(250, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);

                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Date", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Sl No.", Size = new Size(50, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Description", Size = new Size(150, 12) }, 2, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Amount", Size = new Size(150, 12) }, 3, tableLayoutPanel1.RowCount - 1);
                    totalsum = 0;
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {

                        check = -1;
                        DataRow myRow = (r.DataBoundItem as DataRowView).Row;

                        //  MessageBox.Show(ID.ToString());
                        using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                        {
                            SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM addexdetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "' AND exacc = '" + comboBox1.Text + "'", cnn);
                            cnn.Open();
                            SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                            while (sqlReader.Read())
                            {
                                SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM exphddetails WHERE emainhd = '" + sqlReader["exacc"].ToString() + "' AND esubhd = ''  ", cnn);

                                SQLiteDataReader sqlReader1 = sqlCmd1.ExecuteReader();

                                sqlReader1.Read();
                                totalsum = totalsum + double.Parse(sqlReader["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                // double.Parse("52.8725945", System.Globalization.CultureInfo.InvariantCulture);
                                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                                add = add + 20;
                                tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["date"].ToString(), Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["slno"].ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["des"].ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                                tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["amnt"].ToString(), Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);
                                i++;
                            }

                            sqlReader.Close();

                        }


                    }
                    check = i + 3;
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "Grand Total", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = totalsum.ToString(), Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);

                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "SELECTED CLIENTS", Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {
                        DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                        tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                        add = add + 20;
                        tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                        tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                    }

                    panel1.Controls.Add(tableLayoutPanel1);
                }
                else { MessageBox.Show("please fill data"); }
            }
            else if (diff == 24)
            {
                panel2.Visible = false;
                panel1.Location = new Point(12, 3);
                panel1.Size = new Size(700,430);
                check = -1;
                tableLayoutPanel1.Location = new Point(100, 50);
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                tableLayoutPanel1.Size = new System.Drawing.Size(500, 50);
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 12) }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(250, 12) }, 1, 0);
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "     CHART OF ACCOUNT", Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;

                tableLayoutPanel1.Controls.Add(new Label() { Text = "SlNo", Size = new Size(150, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Name Of Account", Size = new Size(250, 12) }, 1, tableLayoutPanel1.RowCount - 1);



                using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                {
                    SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM exphddetails WHERE   esubhd =''", cnn);
                    cnn.Open();
                    SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                        add = add + 20;
                        tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                        tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["slno"].ToString(), Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["emainhd"].ToString(), Size = new Size(250, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 1, tableLayoutPanel1.RowCount - 1);


                        SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM exphddetails WHERE   esubhd !='' AND emainhd ='" + sqlReader["emainhd"].ToString() + "'", cnn);

                        SQLiteDataReader sqlReader1 = sqlCmd1.ExecuteReader();
                        int h = 1;
                        while (sqlReader1.Read())
                        {
                            tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                            add = add + 20;
                            tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                            tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = "  " + h++ + "   " + sqlReader1["esubhd"].ToString(), Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);

                        }

                        sqlReader1.Close();
                    }
                    sqlReader.Close();
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    check = tableLayoutPanel1.RowCount - 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(250, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                }

                panel1.Controls.Add(tableLayoutPanel1);
            }
            else if (diff == 3)
            {
                label5.Text = "INCOME DETAIL REPORT";

                sum = new double[1000];
                int i = 0, k = 0;
                double totalsum = 0;
                //int add = 50;

                a = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                b = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                tableLayoutPanel1.Location = new Point(100, 50);
                label6.Text = "For " + a + " Through " + b;
                tableLayoutPanel1.ColumnCount = 7;
                tableLayoutPanel1.RowCount = 1; tableLayoutPanel1.BackColor = Color.White;  //Color.LightBlue;
                // tableLayoutPanel1.Padding = new Padding(5, 15, 4, 5);

                tableLayoutPanel1.Size = new System.Drawing.Size(800, 50);
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26F));


               tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 12) }, 0, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 12) }, 1,0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(60, 12) }, 2, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "     General Ledger", Size = new Size(150, 12) }, 3, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 12) }, 4,0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 12) }, 5, 0);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 12) }, 6, 0);

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(60, 12) }, 2, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "For " + a + " Through " + b, Size = new Size(250, 12) }, 3, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 12) }, 4, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 12) }, 5, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(50, 12) }, 6, tableLayoutPanel1.RowCount - 1);
               
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Date", Size = new Size(50, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Sub Head", Size = new Size(150, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Sl No.", Size = new Size(60, 12) }, 2, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Description", Size = new Size(150, 12) }, 3, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Income", Size = new Size(50, 12) }, 4, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Expense", Size = new Size(50, 12) }, 5, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Balance", Size = new Size(50, 12) }, 6, tableLayoutPanel1.RowCount - 1);
                totalsum = 0;
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {

                    check = -1;
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;

                    //  MessageBox.Show(ID.ToString());
                    using (SQLiteConnection cnn = new SQLiteConnection(dbConnection))
                    {
                        SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM addexdetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "'", cnn);
                        //cnn.Open();
                      //  SQLiteDataReader sqlReader = sqlCmd.ExecuteReader();
                        SQLiteDataAdapter a22 = new SQLiteDataAdapter(sqlCmd);
                        a22.Fill(t);
                      SQLiteCommand sqlCmd0 = new SQLiteCommand("SELECT * FROM addindetails WHERE    date >='" + a + "' AND date <= '" + b + "'AND name = '" + myRow[0].ToString() + "'", cnn);
                        
                        SQLiteDataAdapter a20 = new SQLiteDataAdapter(sqlCmd0);
                        a20.Fill(t);
                      /*  while (sqlReader.Read())
                        {
                            SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM exphddetails WHERE emainhd = '" + sqlReader["exacc"].ToString() + "' AND esubhd = ''  ", cnn);

                            SQLiteDataReader sqlReader1 = sqlCmd1.ExecuteReader();

                            sqlReader1.Read();

                            totalsum = totalsum + double.Parse(sqlReader["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                            tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                            add = add + 20;
                            tableLayoutPanel1.Size = new System.Drawing.Size(500, add);
                            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["date"].ToString(), Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["slno"].ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["des"].ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                            tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader["amnt"].ToString(), Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1);
                            i++;
                            sqlReader1.Close();
                        }
                        */
                        //sqlReader.Close();

                    }


                }
                t.DefaultView.Sort = "date";
                t = t.DefaultView.ToTable();
                //dataGridView2.DataSource = t;
                double ttlsum =0,income=0,expense=0;
                foreach (DataRow row in t.Rows)
                {
                    string subhead,amnt,amnt1;
                    SQLiteDataReader sqlReader1;
                    if (row["inhd"].ToString() != "")
                    {
                        SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM incomehddetails WHERE imainhd = '" + row["inacc"].ToString() + "' AND isubhd = ''  ", cnn);
                     sqlReader1 = sqlCmd1.ExecuteReader();
                        sqlReader1.Read();
                         subhead = row["inhd"].ToString();
                         amnt = row["amnt"].ToString();
                         amnt1 = "";
                         income = income + double.Parse(row["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                         ttlsum = ttlsum + double.Parse(row["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else
                    {
                        SQLiteCommand sqlCmd1 = new SQLiteCommand("SELECT * FROM exphddetails WHERE emainhd = '" + row["exacc"].ToString() + "' AND esubhd = ''  ", cnn);
                          sqlReader1 = sqlCmd1.ExecuteReader();

                        sqlReader1.Read();
                        amnt1 = row["amnt"].ToString();
                        amnt = "";
                         subhead = row["item"].ToString();
                         expense = expense + double.Parse(row["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                         ttlsum = ttlsum - double.Parse(row["amnt"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = row["date"].ToString(), Size = new Size(100, 12) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = ""+subhead+"", Size = new Size(150, 12) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = sqlReader1["slno"].ToString(), Size = new Size(60, 12) }, 2, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = row["des"].ToString(), Size = new Size(150, 12) }, 3, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = amnt, Size = new Size(150, 12) }, 4, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = amnt1, Size = new Size(150, 12) }, 5, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = ttlsum.ToString(), Size = new Size(150, 12) }, 6, tableLayoutPanel1.RowCount - 1);
                    
                }
                check = tableLayoutPanel1.RowCount;
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(800, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "Grand Total", Size = new Size(150, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 3, tableLayoutPanel1.RowCount - 1); 
                tableLayoutPanel1.Controls.Add(new Label() { Text = income.ToString(), Size = new Size(150, 12) }, 4, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text =expense.ToString(), Size = new Size(150, 12) }, 5, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = ttlsum.ToString(), Size = new Size(150, 12) }, 6, tableLayoutPanel1.RowCount - 1);
                    

                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                add = add + 20;
                tableLayoutPanel1.Size = new System.Drawing.Size(800, add);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel1.Controls.Add(new Label() { Text = "CLIENTS", Size = new Size(150, 15), Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    DataRow myRow = (r.DataBoundItem as DataRowView).Row;
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(800, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = myRow[0].ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = "", Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);

                }
                for (int y = 100; y > 0; y--)
                {

                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    add = add + 20;
                    tableLayoutPanel1.Size = new System.Drawing.Size(800, add);
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(150, 15) }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(150, 15) }, 2, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = y.ToString(), Size = new Size(50, 15) }, 0, tableLayoutPanel1.RowCount - 1);
                   }
                panel1.Controls.Add(tableLayoutPanel1);
            }

                  
            }


       
        }
      

       
    }

