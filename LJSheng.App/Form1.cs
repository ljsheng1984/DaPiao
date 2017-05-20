using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LJSheng.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("     益民停车场管理系统  \n");
            sb.Append("*************************************\n");
            sb.Append("进场时间：" + DateTime.Now.ToString() + "\n");
            sb.Append("出场时间：" + DateTime.Now.AddHours(2).ToString() + "\n");
            sb.Append("停车时长：   2   小时\n");
            sb.Append("停车收费：   5     元\n");
            sb.Append("*************************************\n");
            Print(sb.ToString());
        }


        //定义一个字符串流，用来接收所要打印的数据
        private StringReader sr;
        //str要打印的数据
        public bool Print(string sb)
        {
            bool result = true;
            try
            {
                sr = new StringReader(sb.ToString());
                PrintDocument pd = new PrintDocument();
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                pd.DefaultPageSettings.Margins.Top = 2;
                pd.DefaultPageSettings.Margins.Left = 0;
                pd.DefaultPageSettings.PaperSize.Width = 320;
                pd.DefaultPageSettings.PaperSize.Height = 5150;
                pd.PrinterSettings.PrinterName = pd.DefaultPageSettings.PrinterSettings.PrinterName;//默认打印机
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                pd.Print();
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return result;
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Font printFont = new Font("Arial", 9);//打印字体
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = "";
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
            while (count < linesPerPage && ((line = sr.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }
            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }
        string imagePath;
        HPRTPrinter printer = null;
        bool isPageModePrinter;
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files|*.bmp;*.gif;*.jpg;*.png;";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imagePath = dlg.FileName;
            }
            //if (Constants.E_SUCCESS == result)
            //{
            //    MessageBox.Show("DownLoad Succeed.");
            //}
            //else
            //{
            //    MessageBox.Show("DownLoad Failed." + result.ToString());
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Index = 0;
            switch (Index)
            {
                case 0:
                    printer.PrintImage(imagePath, 0);
                    break;
                case 1:
                    printer.PrintBufferedImage();
                    break;
                case 2: printer.PrintNVImage((byte)'1', (byte)'1'); break;
                case 3: printer.PrintNVImageCompatible(1, 0); break;
                case 4: printer.PrintDownloadedImageCompatible(0); break;
                default:
                    break;
            }
        }

        private void setButtonEnable(bool isEnable)
        {
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string modelsetting = "111";
            string portsetting = "USB";
            int result = 0;

            if (null == printer)
            {
                if (null == (printer = new HPRTPrinter(modelsetting)))
                {
                    MessageBox.Show("Creator Model Failed!");
                    return;
                }
            }
            else
            {
                printer.Model = modelsetting;
            }
            if (Constants.E_SUCCESS == (result = printer.PortOpen(portsetting)))
            {
                //设置字符集
                //printer.SetCharacterSet(Constants.CHARACTERSET_DEFAULT);

                setButtonEnable(true);
                ////判断是否支持页模式
                //isPageModePrinter = PageModePrinterList.Contains(modelsetting);
                //if (!isPageModePrinter)
                //{
                //    this.btnPrintLabel.Enabled = false;
                //}
            }
            else
                MessageBox.Show("Port Failed!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            printer.PortClose();
            setButtonEnable(false);
        }







        //public bool Print()
        //{
        //    bool bolIsSuccess = false;
        //    PrintDocument pd = new PrintDocument();

        //    PaperSize ps = new PaperSize("Measure", 350, 650);
        //    pd.DefaultPageSettings.PaperSize = ps;
        //    pd.DefaultPageSettings.PrinterSettings.Copies = 1;
        //    pd.DefaultPageSettings.PrinterSettings.MaximumPage = 1;
        //    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
        //    pd.Print();
        //    bolIsSuccess = true;
        //    return bolIsSuccess;
        //}

        //void pd_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    Font titleFont = new Font("宋体", 14, FontStyle.Bold);//标题字体 
        //    Font fntTxt = new Font("宋体", 12, FontStyle.Regular);//正文文字 
        //    Brush brush = new SolidBrush(Color.Black);//画刷 
        //    Pen pen = new Pen(Color.Black);           //线条颜色 
        //    Point poTitle = new Point(40, 200);
        //    Point poTime = new Point(70, 210);
        //    Point poTxt = new Point(20, 235);
        //    StringBuilder sb = GetPrintSW();

        //    string strTime = DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日   " + DateTime.Now.ToString("HH:mm:ss");
        //    try
        //    {
        //        e.Graphics.DrawString("\r\n\r\n\r\n\r\n\r\n\r\nXXXXXXX计量单", fntTxt, new SolidBrush(Color.White), new Point(45, 100));
        //        e.Graphics.DrawString("\r\n\r\n\r\n\r\n\r\n\r\nXXXXXXXX计量单", fntTxt, brush, poTitle);
        //        e.Graphics.DrawString("\r\n\r\n\r\n\r\n\r\n\r\n\r\n" + strTime, fntTxt, brush, poTime);
        //        e.Graphics.DrawString(sb.ToString(), fntTxt, brush, poTxt);   //DrawString方式进行打印。 

        //        if (dt.Rows.Count > 0)
        //        {
        //            string strMeaDocId = dt.Rows[0]["C_MeasureDocID"].ToString() ?? "";
        //            if (!string.IsNullOrEmpty(strMeaDocId))
        //            {
        //                barcodeControl.Data = strMeaDocId;
        //            }
        //        }
        //        barcodeControl.CopyRight = "";
        //        BarcodeType bt = BarcodeType.CODE39;
        //        barcodeControl.BarcodeType = bt;
        //        Graphics g = e.Graphics;
        //        Rectangle rect = barcodeControl.ClientRectangle;
        //        rect = new Rectangle(90, 550, 230, 50);
        //        //打印
        //        barcodeControl.Draw(g, rect, GraphicsUnit.Inch, 0.01f, 0, null);
        //        g.Dispose();

        //    }
        //    catch (Exception ex)
        //    {
        //        SaveLog.SaveErrLog("打印小票出错：" + ex.Message);
        //        return;
        //    }
        //}
    }
}
