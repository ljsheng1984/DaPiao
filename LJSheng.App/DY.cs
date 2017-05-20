using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;

namespace LJSheng.App
{
    public partial class DY : Form
    {
        public DY()
        {
            InitializeComponent();
        }

        private void dybt_Click(object sender, EventArgs e)
        {
            WordHelp WH = new WordHelp();
            WH.Open(@"D:\打印\POS.doc");
            WH.Replace("[商户号码]", "[商家名称]\r\nsdasdasd\r\n             X   1");
            //WH.SaveAsHtml(@"D:\打印\POS.html");
            //WH.Save();
            WH.SaveAs(@"D:\打印\POS111.doc");
            WH.PrintViewWord(@"D:\打印\POS111.doc");
        }

        public static void byDoc(String time, String uid)
        {
            Microsoft.Office.Interop.Word.Application app = null;
            Microsoft.Office.Interop.Word.Document doc = null;

            object missing = System.Reflection.Missing.Value;
            object templateFile = @"D:\打印\POS.doc";
            try
            {
                app = new Microsoft.Office.Interop.Word.ApplicationClass();
                doc = app.Documents.Add(ref templateFile, ref missing, ref missing, ref missing);

                try
                {
                    foreach (Microsoft.Office.Interop.Word.Bookmark bm in doc.Bookmarks)
                    {
                        bm.Select();

                        string item = bm.Name;

                        if (item.Equals("A"))
                        {
                            bm.Range.Text = time == null ? "" : time.ToString();
                        }
                        else if (item.Equals("B"))
                        {
                            bm.Range.Text = uid == null ? "" : uid.ToString();
                        }
                    }
                }
                catch
                {
                }

                //打印
                doc.PrintOut(ref missing, ref missing, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref missing);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
                //MessageBox.Show(exp.Message, this.Text);
            }

            //销毁word进程
            finally
            {
                object saveChange = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                if (doc != null)
                    doc.Close(ref saveChange, ref missing, ref missing);

                if (app != null)
                    app.Quit(ref missing, ref missing, ref missing);
            }
        }

        private void kehubt_Click(object sender, EventArgs e)
        {
            //商品读取
            //Test test = new Test(33);
            //List<List<Goods>> resulttest = test.GetAllSelection();
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < resulttest.Count; i++)
            //{
            //    sb.Append("\r\n");
            //    for (int j = 0; j < resulttest[i].Count; j++)
            //    {
            //        sb.Append(resulttest[i][j].Price.ToString() + ",");
            //    }
            //}
            //MessageBox.Show(sb.ToString());
            using (OpenFileDialog lvse = new OpenFileDialog())
            {
                lvse.Title = "请选择客户表格";
                lvse.InitialDirectory = "";
                lvse.Filter = "Excel表格|*.xlsx;*.xls";
                lvse.FilterIndex = 1;

                if (lvse.ShowDialog() == DialogResult.OK)
                {
                    //lvse.FileName
                    object missing = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//lauch excel application
                    if (excel == null)
                    {
                        MessageBox.Show("请选择正确excel");
                    }
                    else
                    {
                        excel.Visible = false; excel.UserControl = true;
                        // 以只读的形式打开EXCEL文件
                        Workbook wb = excel.Application.Workbooks.Open(lvse.FileName, missing, true, missing, missing, missing,
                         missing, missing, missing, true, missing, missing, missing, missing, missing);
                        //取得第一个工作薄
                        Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);


                        //取得总记录行数   (包括标题列)
                        int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
                        //int columnsint = mySheet.UsedRange.Cells.Columns.Count;//得到列数
                        //MessageBox.Show(ws.get_Range("A2", "A2").Text);
                        // 列表集合将作为comboBox1的数据源
                        List<MyItem> list = new List<MyItem>();
                        for (int i = 1; i <= rowsint - 1; i++)
                        {
                            list.Add(new MyItem(ws.get_Range("A" + i.ToString(), "A2").Text, ws.get_Range("A" + i.ToString(), "A3").Text));
                        }
                        // 绑定
                        KHCB.DataSource = list;
                        // 在comboBox1中显示MyItem的Name属性
                        KHCB.DisplayMember = "Name";
                        ////取得数据范围区域 (不包括标题列) 
                        //Range rng1 = ws.Cells.get_Range("B2", "B" + rowsint);   //item


                        //Range rng2 = ws.Cells.get_Range("K2", "K" + rowsint); //Customer
                        //object[,] arryItem = (object[,])rng1.Value2;   //get range's value
                        //object[,] arryCus = (object[,])rng2.Value2;
                        //将新值赋给一个数组
                        //string[,] arry = new string[rowsint - 1, 2];
                        //for (int i = 1; i <= rowsint - 1; i++)
                        //{
                        //    //Item_Code列
                        //    arry[i - 1, 0] = arryItem[i, 1].ToString();
                        //    //Customer_Name列
                        //    arry[i - 1, 1] = arryCus[i, 1].ToString();
                        //}
                        //MessageBox.Show(arry[0, 0] + " / " + arry[0, 1] + "#" + arry[rowsint - 2, 0] + " / " + arry[rowsint - 2, 1]);
                    }
                    excel.Quit(); excel = null;
                    Process[] procs = Process.GetProcessesByName("excel");


                    foreach (Process pro in procs)
                    {
                        pro.Kill();//没有更好的方法,只有杀掉进程
                    }
                    GC.Collect();
                }
            }
        }

        private void KHCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 将被选中的项目强制转换为MyItem
            MyItem item = comboBox1.SelectedItem as MyItem;
            // 显示被选中项的值
            label1.Text = string.Format("Value = {0}", item.Value);
        }
    }

    public class MyItem
    {
        public MyItem(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; private set; }
        public string Value { get; private set; }
    }
}
