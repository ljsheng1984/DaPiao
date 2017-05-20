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

        #region 打印WORD
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
        #endregion

        #region 客户表格导入
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
                        if (rowsint > 0)
                        {
                            //int columnsint = mySheet.UsedRange.Cells.Columns.Count;//得到列数
                            //Range rng = (Microsoft.Office.Interop.Excel.Range)ws.Cells[3, 2];
                            //MessageBox.Show(rng.Text);
                            // 列表集合将作为comboBox1的数据源
                            List<MyItem> list = new List<MyItem>();
                            for (int i = 2; i <= rowsint; i++)
                            {
                                list.Add(new MyItem(((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 1]).Text, ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 2]).Text+"@"+ ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 3]).Text + "@" + ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 4]).Text + "@" + ((Microsoft.Office.Interop.Excel.Range)ws.Cells[i, 5]).Text));
                            }
                            // 绑定
                            KHCB.DataSource = list;
                            // 在comboBox1中显示MyItem的Name属性
                            KHCB.DisplayMember = "Name";
                        }
                        else
                        {
                            MessageBox.Show("表格里没有数据");
                        }
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
        #endregion

        #region 客户名单选中事件
        private void KHCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 将被选中的项目强制转换为MyItem
            MyItem item = KHCB.SelectedItem as MyItem;
            // 显示被选中项的值
            tb.Text = string.Format("Value = {0}", item.Value);
        }
        #endregion

        #region 只能输入数字
        private void NumCheck(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void DY_Load(object sender, EventArgs e)
        {
            #region 设置默认值
            KHCB.SelectedText = "请选一个客户";
            MBCB.SelectedText = "请选一个模板";
            FLCB.SelectedText = "请选商家分类";
            SJCB.SelectedText = "请选商家名称";
            SPFLCB.SelectedText = "请选商品分类";
            SPCB.SelectedText = "请选一个商品";
            #endregion

            #region 设置打印时间
            //年份
            for (int i = 2015; i <= 2025; i++)
            {
                NCB.Items.Add(i.ToString());
            }
            //月份
            for (int i = 1; i <= 12; i++)
            {
                YCB.Items.Add(i.ToString().PadLeft(2, '0'));
            }
            //日
            for (int i = 1; i <= 31; i++)
            {
                RCB.Items.Add(i.ToString().PadLeft(2, '0'));
            }
            //时
            for (int i = 0; i <= 23; i++)
            {
                SCB.Items.Add(i.ToString().PadLeft(2, '0'));
            }
            //分秒
            for (int i = 0; i <= 59; i++)
            {
                FCB.Items.Add(i.ToString().PadLeft(2, '0'));
                MCB.Items.Add(i.ToString().PadLeft(2, '0'));
            }
            //设置当前默认日期
            NCB.SelectedText = DateTime.Now.Year.ToString();
            YCB.SelectedText = DateTime.Now.Month.ToString().PadLeft(2, '0');
            RCB.SelectedText = DateTime.Now.Day.ToString().PadLeft(2, '0');
            SCB.SelectedText = DateTime.Now.Hour.ToString().PadLeft(2, '0');
            FCB.SelectedText = DateTime.Now.Minute.ToString().PadLeft(2, '0');
            MCB.SelectedText = DateTime.Now.Second.ToString().PadLeft(2, '0');
            #endregion

            #region 设置商品列表
            GV.Columns.Add("name", "名称");
            GV.Columns.Add("num", "数量");
            GV.Columns.Add("rmb", "价格");
            GV.Columns[0].Width = 150;
            GV.Columns[1].Width = 38;
            GV.Columns[2].Width = 65;
            #endregion
        }
        #region 选择商家
        private void SJCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GV.RowsDefaultCellStyle.Font = new Font("宋体", 20);
            GV.Rows.Clear();
            DataSet ds;
            //if (ds.Count > 0)
            //{
            //    foreach (var l in ds)
            //    {
            //        try
            //        {
            //            string[] s = l.Split(',');
            //            GV.Rows.Add(s[0], s[1], s[2], s[3]);
            //        }
            //        catch { }
            //    }
            //}
        }
        #endregion
    }

    #region 自定义集合类
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
    #endregion
}
