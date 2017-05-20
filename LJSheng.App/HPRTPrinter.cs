using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace LJSheng.App
{
    public class PrinterState
    {
        //Printer Status
        public static readonly uint STS_NORMAL = 0x00;
        public static readonly uint STS_PAPEREMPTY = 1;
        public static readonly uint STS_COVEROPEN = 2;
        public static readonly uint STS_PAPERNEAREND = 4;
        public static readonly uint STS_MSR_READY = 8;
        public static readonly uint STS_SMARTCARD_READY = 16;
        public static readonly uint STS_ERROR = 32;
        public static readonly uint STS_NOT_OPEN = 64;
        public static readonly uint STS_OFFLINE = 128;

        uint m_status = 0;

        public PrinterState()
        {
            m_status = 0;
        }
        public uint State
        {
            get { return m_status; }
            set { m_status = value; }
        }
        public bool IsNormal
        {
            get { return (STS_NORMAL == m_status); }
        }
        public bool InError
        {
            get
            {
                return (PAPEREMPTY || COVEROPEN || PAPERNEAREND || MSR_READY || SMARTCARD_READY || ERROR || NOT_OPEN || OFFLINE);
            }
        }
        public bool PAPEREMPTY
        {
            get { return ((STS_PAPEREMPTY & m_status) > 0); }
        }
        public bool COVEROPEN
        {
            get { return ((STS_COVEROPEN & m_status) > 0); }
        }
        public bool PAPERNEAREND
        {
            get { return ((STS_PAPERNEAREND & m_status) > 0); }
        }
        public bool MSR_READY
        {
            get { return ((STS_MSR_READY & m_status) > 0); }
        }
        public bool SMARTCARD_READY
        {
            get { return ((STS_SMARTCARD_READY & m_status) > 0); }
        }
        public bool ERROR
        {
            get { return ((STS_ERROR & m_status) > 0); }
        }
        public bool NOT_OPEN
        {
            get { return ((STS_NOT_OPEN & m_status) > 0); }
        }
        public bool OFFLINE
        {
            get { return ((STS_OFFLINE & m_status) > 0); }
        }

    }

    public class HPRTPrinter
    {
      
#if WindowsCE
        public const string HPRTDIR = "HprtPrinter_CE.dll";
        public const CharSet charSet = CharSet.Unicode;
#else
        public const string HPRTDIR = "ESC_SDK.dll";
        public const CharSet charSet = CharSet.Ansi;
#endif

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtFormatError(int error_no,int langid,byte[] buf,int pos,int bufSize);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPrinterCreator(ref IntPtr printer,string model);

        [DllImport(HPRTDIR)]
        private static extern int PrtPrinterDestroy(IntPtr printer);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPortOpen(IntPtr printer, string portSetting);

        [DllImport(HPRTDIR)]
        private static extern int PrtPortClose(IntPtr printer);

        [DllImport(HPRTDIR)]
        private static extern int PrtPrinterInitialize(IntPtr printer);

        [DllImport(HPRTDIR)]
        private static extern int PrtFeedLine(IntPtr printer, int nFeed);

        [DllImport(HPRTDIR)]
        private static extern int PrtSetAlign(IntPtr printer, int align);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPrintText(IntPtr printer, byte[] text, int alignment, int attribute, int textSize);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPrintBarCode(IntPtr printer, int bcType, string bcData, int width, int height, int alignment, int hriPosition);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPrintSymbol(IntPtr printer, int type, string bcData, int errLevel, int width, int height, int alignment);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtDefineDownloadedImageCompatible(IntPtr printer, string imagePath);

        [DllImport(HPRTDIR)]
        private static extern int PrtPrintDownloadedImageCompatible(IntPtr printer, int scaleMode);

        [DllImport(HPRTDIR, CharSet = CharSet.Unicode)]
        private static extern int PrtDefineNVImageCompatible(IntPtr printer, string[] imagePath, int imageCnt);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPrintNVImageCompatible(IntPtr printer, int imageNo, int scaleMode);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtDefineDownloadedImage(IntPtr printer, string imagePath, byte kc1, byte kc2);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPrintDownloadedImage(IntPtr printer, byte kc1, byte kc2);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtDefineBufferedImage(IntPtr printer, string imagePath);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPrintBufferedImage(IntPtr printer);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtDefineNVImage(IntPtr printer, string imagePath, byte kc1, byte kc2);

        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPrintNVImage(IntPtr printer, byte kc1, byte kc2, int horizontal, int vertical);
        [DllImport(HPRTDIR, CharSet = charSet)]
        private static extern int PrtPrintImage(IntPtr printer, string filePath, int scaleMode);

        [DllImport(HPRTDIR)]
        private static extern int PrtCutPaper(IntPtr printer, int cutMode, int distance);

        [DllImport(HPRTDIR)]
        private static extern int PrtOpenDrawer(IntPtr printer, int pinMode, int onTime,int offTime);

        [DllImport(HPRTDIR)]
        private static extern int PrtSelectStandardMode(IntPtr printer);

        [DllImport(HPRTDIR)]
        private static extern int PrtSetTextLineSpace(IntPtr printer, int lineSpace);

        
        [DllImport(HPRTDIR)]
        private static extern int PrtSetTextFont(IntPtr printer, int font);

        [DllImport(HPRTDIR)]
        private static extern int PrtSetTextPosition(IntPtr printer, int position);

        [DllImport(HPRTDIR)]
        private static extern int PrtSelectPageMode(IntPtr printer);

        [DllImport(HPRTDIR)]
        private static extern int PrtSetPrintAreaInPageMode(IntPtr printer, int horizontal
        , int vertical, int width, int height);

        [DllImport(HPRTDIR)]
        private static extern int PrtCancelPrintDataInPageMode(IntPtr printer);

        [DllImport(HPRTDIR)]
        private static extern int PrtSelectPrintDirectionInPageMode(IntPtr printer, int direction);

        [DllImport(HPRTDIR)]
        private static extern int PrtSetAbsolutePrintPosition(IntPtr printer, int position);

        [DllImport(HPRTDIR)]
        private static extern int PrtSetAbsoluteVerticalPrintPositionInPageMode(IntPtr printer, int position);

        [DllImport(HPRTDIR)]
        private static extern int PrtSetPrintAndReturnStandardMode(IntPtr printer);

        [DllImport(HPRTDIR)]
        private static extern int PrtPrintDataInPageMode(IntPtr printer);

        [DllImport(HPRTDIR)]
        private static extern int PrtCheckPrinterState(IntPtr printer, ref uint printerState);

        [DllImport(HPRTDIR)]
        private static extern int PrtDirectIO(IntPtr printer, byte[] writeData, uint writenum, byte[] readData, uint readNum, ref uint readedNum);

        [DllImport(HPRTDIR)]
        private static extern int PrtSetCharacterSet(IntPtr printer, int characterSet);

        [DllImport(HPRTDIR)]
        private static extern int PrtGetPrinterVersion(IntPtr printer, int[] version, int versionLen );

        [DllImport(HPRTDIR)]
        private static extern int PrtPositionNextLabel(IntPtr printer);

        [DllImport(HPRTDIR)]
        private static extern int PrtPrintTwoQRCode(IntPtr printer, string data1, int width1, int hAlign1, int vAlign1, string data2, int width2, int hAlign2, int vAlign2);

        [DllImport(HPRTDIR)]
        private static extern int PrtPrintTwoQRCodeS(IntPtr printer, int height, string data1, string data2);

        private IntPtr printer;
        private int langid = 0;
        int E_SUCCESS = Constants.E_SUCCESS;
        int E_INVALID_PARAMETER = Constants.E_INVALID_PARAMETER;
        PrinterState state = new PrinterState();
        string modelname = "";
        public HPRTPrinter(string model)
        {
            int result=E_SUCCESS;
            printer = IntPtr.Zero;
            if (E_SUCCESS != (result=PrtPrinterCreator(ref printer, model)))
            {
                string errorMsg=FormatError(result);
                printer = IntPtr.Zero;
                throw new Exception(errorMsg);
            }
            modelname = model;
        }
        public string FormatError(int error_no)
        {
            byte[] temp = new byte[512];

            PrtFormatError(error_no, langid, temp, 0, 512);

#if WindowsCE
            return System.Text.Encoding.Unicode.GetString(temp, 0, 512);
#else
            return System.Text.Encoding.Default.GetString(temp,0,512);
#endif
        }
        public string Model
        {
            get { return modelname; }
            set
            {
                if(value.ToUpper().Trim()!=modelname.ToUpper().Trim())
                {
                    IntPtr p = IntPtr.Zero;
                    if(Constants.E_SUCCESS== PrtPrinterCreator(ref p, value))
                    {
                        PrtPrinterDestroy(printer);
                        printer = p;
                        modelname = value;
                    }
                }
            }
        }
        public int ChangeModel(string modelName)
        {
            int result=Constants.E_SUCCESS;

            return result;
        }
        public int PortOpen(string uri)
        {
            if (printer != IntPtr.Zero)
                return PrtPortOpen(printer, uri);
            else
                return Constants.E_BAD_HANDLE;
        }

        public int PortClose()
        {
            int result = E_SUCCESS;
            result=PrtPortClose(printer);
            return result;
        }

        public int Initialize()
        {
            return PrtPrinterInitialize(printer);
        }

        public int PrintText(string text, int alignment, int attribute, int textsize)
        {
            int len = text.Length;
            byte[] data = Encoding.GetEncoding("GB2312").GetBytes(text);
            return PrtPrintText(printer, data, alignment, attribute, textsize);
        }

        public int PrintBarCode(int bcType, string bcData, int width, int height, int alignment, int hriPosition)
        {
            return PrtPrintBarCode(printer, bcType, bcData, width, height, alignment, hriPosition);
        }

        public int PrintSymbol(int type, string data, int errLevel, int width, int height, int alignment)
        {
            return PrtPrintSymbol(printer, type, data, errLevel, width, height, alignment);
        }

        public int DefineDownloadedImageCompatible(string imagePath)
        {
            return PrtDefineDownloadedImageCompatible(printer, imagePath);
        }

        public int PrintDownloadedImageCompatible(int scaleMode)
        {
            return PrtPrintDownloadedImageCompatible(printer, scaleMode);
        }

        public int PrintImage(string imagePath, int scaleMode)
        {
            return PrtPrintImage(printer, imagePath, scaleMode);
        }

        public int DefineNVImageCompatible(string[] fileList, int ImageQty)
        {
            return PrtDefineNVImageCompatible(printer, fileList, ImageQty);
        }

        public int PrintNVImageCompatible(int imageNo, int scaleMode)
        {
            return PrtPrintNVImageCompatible(printer, imageNo, scaleMode);
        }

        public int DefineDownloadedImage(string imagePath, byte kc1, byte kc2)
        {
            return PrtDefineDownloadedImage(printer, imagePath, kc1, kc2);
        }

        public int PrintDownloadedImage(byte kc1, byte kc2)
        {
            return PrtPrintDownloadedImage(printer, kc1, kc2);
        }

        public int DefineBufferedImage(string imagePath)
        {
            return PrtDefineBufferedImage(printer, imagePath);
        }

        public int PrintBufferedImage()
        {
            return PrtPrintBufferedImage(printer);
        }

        public int DefineNVImage(string imagePath, byte kc1, byte kc2)
        {
            return PrtDefineNVImage(printer, imagePath, kc1, kc2);
        }

        public int PrintNVImage(byte kc1, byte kc2)
        {
            return PrtPrintNVImage(printer, kc1, kc2, 1, 1);
        }
        public int FeedLine(int lines)
        {
            return PrtFeedLine(printer, lines);
        }

        public int SetAlign(int align)
        {
            return PrtSetAlign(printer, align);
        }

        public int CutPaper(int cutMode, int distance)
        {
            return PrtCutPaper(printer, cutMode, distance);
        }

        public int OpenDrawer(int pinMode, int onTime,int offTime)
        {
            return PrtOpenDrawer(printer, pinMode, onTime, offTime);
        }

        public int SelectStandardMode()
        {
            return PrtSelectStandardMode(printer);
        }

        public int SetTextLineSpacing(int spacing)
        {
            return PrtSetTextLineSpace(printer, spacing);
        }


        public int SetTextFont(int font)
        {
            return PrtSetTextFont(printer, font);
        }


        public int SetTextPosition(int position)
        {
            return PrtSetTextPosition(printer, position);
        }

        public int SelectPageMode()
        {
            return PrtSelectPageMode(printer);
        }

        public int SetPageModePrintArea(int horizontal
        , int vertical, int width, int height)
        {
            return PrtSetPrintAreaInPageMode(printer, horizontal, vertical
            , width, height);
        }

        public int CancelPrintData()
        {
            return PrtCancelPrintDataInPageMode(printer);
        }

        public int SetPageModePrintDirection(int direction)
        {
            return PrtSelectPrintDirectionInPageMode(printer, direction);
        }

        public int SetPageModeHorizontalPosition(int position)
        {
            return PrtSetAbsolutePrintPosition(printer, position);
        }

        public int SetPageModeVerticalPosition(int position)
        {
            return PrtSetAbsoluteVerticalPrintPositionInPageMode(printer, position);
        }

        public int PrintAndReturnStandardMode()
        {
            return PrtSetPrintAndReturnStandardMode(printer);
        }

        public int PositionNextLabel()
        {
            return PrtPositionNextLabel(printer);
        }

        public int PrintDataInPageMode()
        {
            return PrtPrintDataInPageMode(printer);
        }

        public int CheckPrinterState(ref uint printerState)
        {
            return PrtCheckPrinterState(printer, ref printerState);
        }
        public int GetState(ref PrinterState printerState)
        {
            uint s = 0;
            int result = E_SUCCESS;
            result = CheckPrinterState(ref s);
            if (E_SUCCESS == result)
            {
                printerState.State = s;
                state.State = s;
            }
            else
            {
                printerState.State |= PrinterState.STS_ERROR;
                state.State |= PrinterState.STS_ERROR;
            }

            return result;
        }
        public int DirectIO(byte[] writedata, byte[] readdata, int readnum, ref int readednum)
        {
            if (writedata == null || writedata.Length == 0)
            {
                MessageBox.Show("No data to write!");
                return E_INVALID_PARAMETER;
            }
            //if (readdata.Length < readnum)
            //{
            //    MessageBox.Show("No enough buffer!");
            //    return E_INVALID_PARAMETER;
            //}
            //else
            {
                uint readedcnt = 0;
                int errorno = PrtDirectIO(printer, writedata, (uint)writedata.Length, readdata, (uint)readnum, ref readedcnt);
                if (E_SUCCESS == errorno)
                    readednum = (int)readedcnt;
                return errorno;
            }
        }

        public int SetCharacterSet(int CharacterSet)
        {
            return PrtSetCharacterSet(printer, CharacterSet);
        }
         
        public void PrintText2Image(string path,string text,FontStyle font_mode,int font_size)
        {

            int bmp_height = font_size*2;
            System.Drawing.Bitmap bmp = new Bitmap(384, bmp_height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            SolidBrush brush = new SolidBrush(Color.Black);
            Font font = new Font(FontFamily.GenericSerif, font_size, font_mode);
            g.DrawString(text, font, brush, 0, 5);
            bmp.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
            font.Dispose();
            g.Dispose();
            PrtPrintImage(printer, path, Constants.PRINT_IMAGE_NORMAL);
        }

        public int GetPrinterVersion(int[] Version)
        {
            return PrtGetPrinterVersion(printer, Version, Version.Length);
        }

        public int PrintTwoQRCode(string data1, int width1, int hAlign1, int vAlign1, string data2, int width2, int hAlign2, int vAlign2)
        {
            return PrtPrintTwoQRCode(printer, data1, width1, hAlign1, vAlign1, data2, width2, hAlign2, vAlign2);
        }

        public int PrintTwoQRCodeS(int height, string data1, string data2)
        {
            return PrtPrintTwoQRCodeS(printer, height, data1, data2);
        }
    }
}
