﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LJSheng.App
{
    public static class ExcelHelp
    {
        //读取EXCEL的方法   (用范围区域读取数据)
        private static void OpenExcel(string strFileName)
        {
            object missing = System.Reflection.Missing.Value;
            Application excel = new Application();//lauch excel application
            if (excel == null)
            {
                //Response.Write("<script>alert('Can't access excel')</script>");
            }
            else
            {
                excel.Visible = false; excel.UserControl = true;
                // 以只读的形式打开EXCEL文件
                Workbook wb = excel.Application.Workbooks.Open(strFileName, missing, true, missing, missing, missing,
                 missing, missing, missing, true, missing, missing, missing, missing, missing);
                //取得第一个工作薄
                Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);


                //取得总记录行数   (包括标题列)
                int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
                                                             //int columnsint = mySheet.UsedRange.Cells.Columns.Count;//得到列数


                //取得数据范围区域 (不包括标题列) 
                Range rng1 = ws.Cells.get_Range("B2", "B" + rowsint);   //item


                Range rng2 = ws.Cells.get_Range("K2", "K" + rowsint); //Customer
                object[,] arryItem = (object[,])rng1.Value2;   //get range's value
                object[,] arryCus = (object[,])rng2.Value2;
                //将新值赋给一个数组
                string[,] arry = new string[rowsint - 1, 2];
                for (int i = 1; i <= rowsint - 1; i++)
                {
                    //Item_Code列
                    arry[i - 1, 0] = arryItem[i, 1].ToString();
                    //Customer_Name列
                    arry[i - 1, 1] = arryCus[i, 1].ToString();
                }
                //Response.Write(arry[0, 0] + " / " + arry[0, 1] + "#" + arry[rowsint - 2, 0] + " / " + arry[rowsint - 2, 1]);
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
