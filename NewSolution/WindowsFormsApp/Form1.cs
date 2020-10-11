using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        string[] suffixStr = new string[] { ".xls", ".xlsx" };
        public Form1()
        {
            InitializeComponent();
            BindCombox();
        }
        private void BindCombox()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("id");
            DataColumn dc2 = new DataColumn("name");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            DataRow dr1 = dt.NewRow();
            dr1["id"] = "1";
            dr1["name"] = "URL地址";

            DataRow dr2 = dt.NewRow();
            dr2["id"] = "2";
            dr2["name"] = "Base64";

            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);

            cbBoxType.DataSource = dt;
            cbBoxType.ValueMember = "id";
            cbBoxType.DisplayMember = "name";
            cbBoxType.SelectedIndex = 0;
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(fileDialog.FileName);
                if (!suffixStr.Contains(extension))
                {
                    return;
                }
                FileInfo file = new FileInfo(fileDialog.FileName);
                ExcelPackage package = new ExcelPackage(new FileStream(fileDialog.FileName, FileMode.Open));

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];//选定 指定页

                int maxColumnNum = worksheet.Dimension.End.Column;//最大列
                int minColumnNum = worksheet.Dimension.Start.Column;//最小列


                int maxRowNum = worksheet.Dimension.End.Row;//最小行
                int minRowNum = worksheet.Dimension.Start.Row;//最大行

                string[] str = txtFileName.Text.Split('|');

                string fileName = string.Empty;
                string source = string.Empty;

                for(int n = 2; n <= maxRowNum; n++)
                {
                    for (int m = 1; m <= maxColumnNum; m++)
                    {
                        string value = worksheet.Cells[n, m].Value.ToString();
                        Console.WriteLine(value);
                        if (str[0].Contains(m.ToString()))
                            fileName += value + "-";
                        if (m.ToString() == str[1])
                            source = value;
                    }
                    fileName = fileName.TrimEnd('-');
                    if (cbBoxType.SelectedIndex == 0)
                    {
                        string extend = source.Substring(source.LastIndexOf("."));
                        string directory = $"c:\\url";
                        string filepath = $"{directory}\\{fileName}.{extend}";
                        if (!Directory.Exists(directory))
                            Directory.CreateDirectory(directory);
                        WebClient mywebclient = new WebClient();
                        mywebclient.DownloadFile(source, filepath);
                    }
                    else
                    {
                        string directory = $"c:\\base64";
                        if (!Directory.Exists(directory))
                            Directory.CreateDirectory(directory);
                        string filePath = Path.Combine(directory, fileName + ".png");
                        //Base64StringToImage("data:image/png;base64," + source, filePath);
                        Base64StringToImage(source, filePath);
                        Console.WriteLine(filePath);
                    }
                    
                }
            }
        }

        public void Base64StringToImage(string strbase64,string filePath)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(strbase64);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                bmp.Save(filePath, ImageFormat.Png);
                ms.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("123");
        }
    }
}
