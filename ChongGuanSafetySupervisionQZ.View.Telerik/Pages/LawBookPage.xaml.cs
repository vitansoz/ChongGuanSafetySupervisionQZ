using ChongGuanDotNetUtils.Helpers;
using ChongGuanSafetySupervisionQZ.DAL;
using ChongGuanSafetySupervisionQZ.Hardware;
using ChongGuanSafetySupervisionQZ.View.WPF.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using Telerik.Windows.Documents.FormatProviders.Xaml;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Lists;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Model.Styles;
using Telerik.Windows.Documents.Proofing;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Pages
{
    /// <summary>
    /// LawBookPage.xaml 的交互逻辑
    /// </summary>
    public partial class LawBookPage : Page
    {

        private int hw_device_ok_button = 17;
        private int hw_device_cancel_button = 18;
        private IntPtr m_hDevice = IntPtr.Zero;
        private IntPtr _windowHandle = IntPtr.Zero;

        private int m_nWidth = 0;
        private int m_nHeight = 0;
        private byte[] m_pImageBuffer = new byte[307200];
        private int m_nSize = 307200;
        private int nRet = -1;
        public int cachedKey = 0;
        public int disMsg = 0;
        public int hw_rv_ok = 0;
        private HwClass.HWPacket pkt;
        private System.Drawing.Point ptStart;
        private System.Drawing.Point ptEnd;
        private HwClass.HWDeviceInfo devInfo;

        private byte[] _capTmp = new byte[2048];
        private int _cbCapTmp = 2048;
        private byte[] _fPBuffer;

        private DispatcherTimer _fingerImageTimer = new DispatcherTimer();
        private DispatcherTimer _signImageTimer = new DispatcherTimer();

        private HomeWindow _homeWindow;

        private string _flagFinger = ConfigurationManager.AppSettings["FingerFlag"];

        Bitmap b;
        Graphics g;

        public LawBookPage()
        {
            //StyleManager.ApplicationTheme = new Office2016Theme();

            InitializeComponent();

            this.Loaded += LawBookPage_Loaded;

            //StyleManager.SetTheme(this, new Office2016Theme());
        }

        private void LawBookPage_Loaded(object sender, RoutedEventArgs e)
        {
            //this.radRichTextBox
            //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            //((DocumentSpellChecker)this.radRichTextBox.SpellChecker).SpellCheckingCulture = new CultureInfo("zh-CN");
            //radRichTextBox.Language = 

            using (Stream stream = System.Windows.Application.GetResourceStream(new Uri("/ChongGuanSafetySupervisionQZ.View.WPF;component/Resources/xxx.docx", UriKind.Relative)).Stream)
            {
                DocxFormatProvider provider = new DocxFormatProvider();
                this.radRichTextBox.Document = provider.Import(stream);

            }

            //this.PictureBox_Finger.Image = new Bitmap(@"C:\Users\PETTO\Desktop\102823659548517125.jpg");

            //var t = this.radRichTextBox.Document.GetAllBookmarks();

            //foreach (var t1 in t)
            //{
            //    Debug.WriteLine(t1.Name);
            //}

            //this.radRichTextBox.Document.GoToBookmark("bwxAddress");

            //StyleDefinition currentSpanStyle = this.radRichTextBox.Document.CaretPosition.GetCurrentSpanBox().AssociatedDocumentElement.Style;

            ////this.radRichTextBox.Document.Insert("fuck", currentSpanStyle);
            //this.radRichTextBox.Document.GoToBookmark("endDay");
            //this.radRichTextBox.Insert("SHIT");

            InsertInfo("year", GlobalData.CurrentTalkingStartTime.Year.ToString());
            InsertInfo("startMonth", GlobalData.CurrentTalkingStartTime.Month.ToString());
            InsertInfo("startDay", GlobalData.CurrentTalkingStartTime.Day.ToString());
            InsertInfo("startHour", GlobalData.CurrentTalkingStartTime.Hour.ToString());
            InsertInfo("startMinute", GlobalData.CurrentTalkingStartTime.Minute.ToString());
            InsertInfo("endMonth", GlobalData.CurrentTalkingEndTime.Month.ToString());
            InsertInfo("endDay", GlobalData.CurrentTalkingEndTime.Day.ToString());
            InsertInfo("endHour", GlobalData.CurrentTalkingEndTime.Hour.ToString());
            InsertInfo("endMinute", GlobalData.CurrentTalkingEndTime.Minute.ToString());

            InsertInfo("wxAddress", GlobalData.NewTalkViewModel.Inquiry.InquiryAddress);
            InsertInfo("bwxName", GlobalData.NewTalkViewModel.Party.PartyName);
            InsertInfo("bwxSex", GlobalData.NewTalkViewModel.Party.PartySex);
            InsertInfo("bwxAge", (DateTime.Now.Year - DateTime.Parse(GlobalData.NewTalkViewModel.Party.PartyBirth).Year).ToString());
            InsertInfo("bwxCard", GlobalData.NewTalkViewModel.Party.PartyCard);
            InsertInfo("bwxAddress", GlobalData.NewTalkViewModel.Party.PartyAddress);
            InsertInfo("xwrName", GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceName);
            InsertInfo("xwrNumber", GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceNumber);
            InsertInfo("talkingType", GlobalData.NewTalkViewModel.Inquiry.InquiryTalkType);
            InsertInfo("talkingRemark", GlobalData.NewTalkViewModel.Inquiry.InquiryRemarks);

            StringBuilder sb = new StringBuilder();
            foreach (var item in GlobalData.TalkingPageViewModel.MessageList)
            {
                sb.AppendLine((item.MessageTypeIsParty ? "回答：" : "提问：") + item.MessageContent);
            }

            sb.AppendLine();
            InsertInfo("talkingContent", sb.ToString());
            //InsertInfo("xwrNumber", GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceNumber);
            //InsertInfo("xwrNumber", GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceNumber);
            //InsertInfo("xwrNumber", GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceNumber);
            //InsertInfo("xwrNumber", GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceNumber);
            //InsertInfo("endMinute", GlobalData.CurrentTalkingEndTime.Minute.ToString());



            //_windowHandle = new WindowInteropHelper(this).Handle;
            _windowHandle = ((HwndSource)PresentationSource.FromVisual(this)).Handle;

            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);

            _homeWindow = (Window.GetWindow(this) as HomeWindow);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Handle messages...

            if (msg == this.disMsg && HwClass.HWPacketsGet(1, ref this.pkt) > (ushort)0)
            {
                if (this.pkt.nButton > 0)
                {
                    if (this.pkt.nButton != this.hw_device_ok_button && this.pkt.nButton == this.hw_device_cancel_button)
                        this.PictureBox_Sign.Refresh();
                }
                else
                {
                    //System.Drawing.Graphics graphics = this.PictureBox_Sign.CreateGraphics();
                    //System.Drawing.Graphics graphics = Graphics.FromImage(this.PictureBox_Sign.Image); ;
                    //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


                    int nPress = this.pkt.nPress;
                    int num = nPress <= 0 ? 0 : 1;
                    System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(20, 83, 148), (float)nPress / 400f);
                    this.ptEnd.X = this.pkt.nXPos * this.PictureBox_Sign.Width / this.devInfo.nXExt;
                    this.ptEnd.Y = this.pkt.nYPos * this.PictureBox_Sign.Height / this.devInfo.nYExt;
                    if (this.cachedKey == 0 && num == 1)
                    {
                        this.ptStart = this.ptEnd;
                    }
                    else if (this.cachedKey == 1 && num == 1)
                    {
                        g.DrawLine(pen, this.ptStart, this.ptEnd);
                    }
                    else if (this.cachedKey == 1 && num == 0)
                    {
                        g.DrawLine(pen, this.ptStart, this.ptEnd);
                    }


                    this.cachedKey = num;
                    this.ptStart = this.ptEnd;

                    this.PictureBox_Sign.Image = b;
                }
            }

            return IntPtr.Zero;
        }


        public void InsertInfo(string bookmarkStr, string insertContent)
        {
            //return;

            BookmarkRangeStart bookmark = this.radRichTextBox.Document.GetBookmarkByName(bookmarkStr);
            this.radRichTextBox.Document.GoToBookmark(bookmark);
            this.radRichTextBox.Document.Selection.SelectAnnotationRange(bookmark);

            this.radRichTextBox.Insert(insertContent);
        }

        public void InsertImage(string bookmarkStr, System.Drawing.Image image)
        {
            try
            {
                BookmarkRangeStart bookmark = this.radRichTextBox.Document.GetBookmarkByName(bookmarkStr);

                if (bookmark != null)
                {
                    this.radRichTextBox.Document.GoToBookmark(bookmark);
                    //this.radRichTextBox.Document.Selection.SelectAnnotationRange(bookmark);

                    using (Stream stream = new System.IO.MemoryStream())
                    {
                        image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Position = 0;

                        this.radRichTextBox.InsertImage(stream, "png");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                LoggerHelper.Log(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public RadDocument RadDocument
        {
            get
            {
                return this.radRichTextBox.Document;
            }
            set
            {
                SetupNewDocument(value);
                this.radRichTextBox.Document = value;
            }
        }

        private void SetupNewDocument(RadDocument document)
        {
            document.LayoutMode = DocumentLayoutMode.Paged;
            document.ParagraphDefaultSpacingAfter = 10;
            document.SectionDefaultPageMargin = new Telerik.Windows.Documents.Layout.Padding(95);
        }

        private bool IsSupportedImageFormat(string extension)
        {
            if (extension != null)
            {
                extension = extension.ToLower();
            }

            return extension == ".jpg" ||
                extension == ".jpeg" ||
                extension == ".png" ||
                extension == ".bmp" ||
                extension == ".tif" ||
                extension == ".tiff" ||
                extension == ".ico" ||
                extension == ".gif" ||
                extension == ".wdp" ||
                extension == ".hdp";
        }

        private void Button_PrintLawBook_Click(object sender, RoutedEventArgs e)
        {
            this.radRichTextBox.Print("文书", Telerik.Windows.Documents.UI.PrintMode.Native);
        }

        private void Button_CreatePdfLawBook_Click(object sender, RoutedEventArgs e)
        {
            string file = ExportContent(".pdf", "PDF File (*.pdf)|*.pdf", new PdfFormatProvider());


            if (File.Exists(file))
            {
                Process.Start(file);
            }
        }

        private async void Button_SaveLawBook_Click(object sender, RoutedEventArgs e)
        {
            PdfFormatProvider pdfFormatProvider = new PdfFormatProvider();

            string pdfPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "InquiryData", GlobalData.NewTalkViewModel.Inquiry.InquiryId, "lawbook.pdf");
            using (Stream stream = File.OpenWrite(pdfPath))
            {
                pdfFormatProvider.Export(this.radRichTextBox.Document, stream);
            }

            GlobalData.NewTalkViewModel.Inquiry.InquiryLawBookFilePath = pdfPath;
            InquiryDAL inquiryDAL = new InquiryDAL();
            await inquiryDAL.Update(GlobalData.NewTalkViewModel.Inquiry);
        }

        private void Button_CreateDocxLawBook_Click(object sender, RoutedEventArgs e)
        {
            string file = ExportContent(".docx", "DOCX File (*.docx)|*.docx", new DocxFormatProvider());


            if (File.Exists(file))
            {
                Process.Start(file);
            }
        }

        public string ExportContent(string defaultExtension,
                           string filter,
                           IDocumentFormatProvider formatProvider)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = defaultExtension,
                Filter = filter
            };

            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == true)
            {
                using (var outputStream = saveFileDialog.OpenFile())
                {
                    formatProvider.Export(radRichTextBox.Document, outputStream);
                }
            }

            return saveFileDialog.FileName;
        }

        private void PictureBox_Finger_DoubleClick(object sender, EventArgs e)
        {

        }

        private void Button_InputFinger_Click(object sender, RoutedEventArgs e)
        {

            if (_flagFinger == "1")
            {
                if (this.m_hDevice == IntPtr.Zero || (uint)this.nRet > 0U)
                {
                    if (IntPtr.Zero != this.m_hDevice)
                    {
                        ZwClass.ZKFPModule_Disconnect(this.m_hDevice);
                    }

                    this.m_hDevice = ZwClass.ZKFPModule_Connect("protocol=USB,vendor-id=6997,product-id=289");
                }

            }
            else
            {
                if (this.m_hDevice == IntPtr.Zero || (uint)this.nRet > 0U)
                {
                    if (IntPtr.Zero != this.m_hDevice)
                    {
                        FingerClass.CloseDevice(this.m_hDevice);
                    }

                    FingerClass.Init();
                    this.m_hDevice = FingerClass.OpenDevice(0);
                    byte[] numArray = new byte[4];
                    int size = 4;
                    FingerClass.GetParameters(this.m_hDevice, 1, numArray, ref size);
                    FingerClass.ByteArray2Int(numArray, ref this.m_nWidth);
                    FingerClass.GetParameters(this.m_hDevice, 2, numArray, ref size);
                    FingerClass.ByteArray2Int(numArray, ref this.m_nHeight);
                    this._fPBuffer = new byte[this.m_nWidth * this.m_nHeight];
                }
            }

            this.PictureBox_Finger.Refresh();
            if (this.PictureBox_Finger.Image != null)
            {
                this.PictureBox_Finger.Image.Dispose();
                this.PictureBox_Finger.Image = null;
            }

            _fingerImageTimer.Stop();
            _fingerImageTimer.Tick -= _fingerImageTimer_Tick;
            _fingerImageTimer.Tick += _fingerImageTimer_Tick;
            _fingerImageTimer.Interval = TimeSpan.FromSeconds(1);
            _fingerImageTimer.Start();
        }

        private void _fingerImageTimer_Tick(object sender, EventArgs e)
        {
            _fingerImageTimer.Stop();

            _cbCapTmp = 2048;

            if (_flagFinger == "1")
            {
                this.nRet = ZwClass.ZKFPModule_GetFingerImage(this.m_hDevice, ref this.m_nWidth,
                    ref this.m_nHeight, this.m_pImageBuffer, ref this.m_nSize);
                if (this.nRet == 0)
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        BitmapFormat.WriteBitmap(this.m_pImageBuffer, this.m_nWidth, this.m_nHeight);
                        BitmapFormat.GetBitmap(this.m_pImageBuffer, this.m_nWidth , this.m_nHeight, ref ms);
                        Bitmap bitmap = new Bitmap((Stream)ms);
                        this.PictureBox_Finger.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.PictureBox_Finger.Image = (System.Drawing.Image)bitmap;

                        SaveFingerOrSignImage(ms);
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.Log(ex.Message);
                    }
                }
                else
                {
                    LoggerHelper.Log("this.nRet:" + this.nRet);
                }
            }
            else
            {
                this.nRet = FingerClass.AcquireFingerprint(this.m_hDevice, this._fPBuffer, this._capTmp, ref this._cbCapTmp);
                if (this.nRet == 0)
                {
                    MemoryStream ms = new MemoryStream();
                    FingerBitmapFormat.GetBitmap(this._fPBuffer, this.m_nWidth, this.m_nHeight, ref ms);
                    Bitmap bitmap = new Bitmap((Stream)ms);
                    this.PictureBox_Finger.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.PictureBox_Finger.Image = (System.Drawing.Image)bitmap;
                    SaveFingerOrSignImage(ms);

                    ms.Dispose();
                }
            }
            _fingerImageTimer.Start();
        }

        private void Button_InputSign_Click(object sender, RoutedEventArgs e)
        {
            HwClass.HWInit(ref this.devInfo, ref this.disMsg, this._windowHandle);

            int num = (int)HwClass.HWClearSig();

            this.PictureBox_Sign.Refresh();
            if (this.PictureBox_Sign.Image != null)
            {
                this.PictureBox_Sign.Image.Dispose();
                this.PictureBox_Sign.Image = null;
            }

            b = new Bitmap(PictureBox_Sign.Width, PictureBox_Sign.Height);
            g = Graphics.FromImage(b);
        }

        private void SaveFingerOrSignImage(Stream stream)
        {
            string tempDir = AppDomain.CurrentDomain.BaseDirectory + "temp";

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }

            if (File.Exists(System.IO.Path.Combine(tempDir, "zhiwen.jpg")))
            {
                File.Delete(System.IO.Path.Combine(tempDir, "zhiwen.jpg"));
            }

            Bitmap bitmap = new Bitmap(stream);
            System.Drawing.Image image = bitmap;
            image.Save(System.IO.Path.Combine(tempDir, "zhiwen.jpg"));
            image.Dispose();
            stream.Dispose();
        }

        private void Button_InsertFingerToParty_Click(object sender, RoutedEventArgs e)
        {
            //InsertImage("partyZhiWen", new System.Drawing.Bitmap(@"C:\Users\PETTO\Desktop\images\team-5.jpg"));

            if (this.PictureBox_Finger.Image != null)
            {
                InsertImage("partyZhiWen", new System.Drawing.Bitmap(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp", "zhiwen.jpg")));
            }
            else
            {
                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "提示", FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = "请先录入指纹", FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), IsHitTestVisible = false },

                    Owner = Window.GetWindow(this),
                    Theme = new MaterialTheme()
                });

                return;
            }

        }

        private void Button_InsertFingerToPolic_Click(object sender, RoutedEventArgs e)
        {
            if (this.PictureBox_Finger.Image != null)
            {
                InsertImage("policeZhiWen", this.PictureBox_Finger.Image);
            }
            else
            {
                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "提示", FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = "请先录入指纹", FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), IsHitTestVisible = false },

                    Owner = Window.GetWindow(this),
                    Theme = new MaterialTheme()
                });

                return;
            }

        }


        private void Button_InsertSignToParty_Click(object sender, RoutedEventArgs e)
        {

            if (this.PictureBox_Sign.Image != null)
            {
                InsertImage("partyQianMing", this.PictureBox_Sign.Image);
            }
            else
            {
                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "提示", FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = "请先录入签名", FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), IsHitTestVisible = false },

                    Owner = Window.GetWindow(this),
                    Theme = new MaterialTheme()
                });

                return;
            }

        }

        private void Button_InsertSignToPolic_Click(object sender, RoutedEventArgs e)
        {

            if (this.PictureBox_Sign.Image != null)
            {
                InsertImage("policeQianMing", this.PictureBox_Sign.Image);
            }
            else
            {
                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "提示", FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = "请先录入签名", FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), IsHitTestVisible = false },

                    Owner = Window.GetWindow(this),
                    Theme = new MaterialTheme()
                });

                return;
            }

        }

        private void Button_ConfimAndSave_Click(object sender, RoutedEventArgs e)
        {
            Button_SaveLawBook_Click(sender, e);

            if (_homeWindow.Frame_BusinessPage.CanGoBack)
            {
                _homeWindow.Frame_BusinessPage.RemoveBackEntry();
            }

            _homeWindow.GoHome();
        }
    }
}
