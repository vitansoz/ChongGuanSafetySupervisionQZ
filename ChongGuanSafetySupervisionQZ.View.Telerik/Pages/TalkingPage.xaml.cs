using ChongGuanSafetySupervisionQZ.DAL;
using ChongGuanSafetySupervisionQZ.Hardware;
using ChongGuanSafetySupervisionQZ.View.WPF.Common;
using ChongGuanSafetySupervisionQZ.View.WPF.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Pages
{
    /// <summary>
    /// TalkingPage.xaml 的交互逻辑
    /// </summary>
    public partial class TalkingPage : Page
    {
        private CameraHelper _cameraHelperFont = new CameraHelper();
        private CameraHelper _cameraHelperBack = new CameraHelper();
        private AudioHelper _audioHelper = new AudioHelper();

        private DispatcherTimer _timer = new DispatcherTimer();


        private ConcurrentQueue<byte[]> _dataQueue;
        private Thread _voiceThread;
        private bool _isVoiceReady;
        private bool _isVoiceConverterStart;

        private byte[] _voiceData = new byte[320000];
        private int _voiceDataLength;

        private FileStream _voiceFileStream;
        private DirectXSoundHelper _voiceRecorder;

        private string _lastRealContent;

        private HomeWindow _homeWindow;

        public TalkingPage()
        {
            InitializeComponent();

            this.GroupBox_PartyInfo.DataContext = GlobalData.NewTalkViewModel;

            GlobalData.TalkingPageViewModel = new ViewModel.TalkingPageViewModel();
            this.DataContext = GlobalData.TalkingPageViewModel;

            this.Loaded += TalkingPage_Loaded;
            this.Unloaded += TalkingPage_Unloaded;

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;

            _audioHelper.AudioDataAvailable += _audioHelper_AudioDataAvailable;

        }

        private void TalkingPage_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _timer.Tick -= _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            GlobalData.TalkingPageViewModel.DurationTime = GlobalData.TalkingPageViewModel.DurationTime.Add(TimeSpan.FromSeconds(1));
        }

        private void TalkingPage_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.TalkingPageViewModel.SetTalkingState(ViewModel.Enum.TalkState.None);

            _cameraHelperFont.IsStartPicture = true;
            _cameraHelperFont.IsDisplay = true;
            _cameraHelperFont.SourcePlayer = VideoSourcePlayer_Front;
            _cameraHelperFont.UpdateCameraDevices();

            if (_cameraHelperFont.CameraDevices.Count > 0)
            {
                string videoFullPath = $"{AppDomain.CurrentDomain.BaseDirectory}InquiryData\\{GlobalData.NewTalkViewModel.Inquiry.InquiryId}\\front.mp4";
                string pictureFullPath = $"{AppDomain.CurrentDomain.BaseDirectory}InquiryData\\{GlobalData.NewTalkViewModel.Inquiry.InquiryId}\\mainpic.jpg";

                _cameraHelperFont.SetCameraDevice(0, videoFullPath, pictureFullPath, true, true, System.Drawing.RotateFlipType.RotateNoneFlipX);

                GlobalData.NewTalkViewModel.Inquiry.InquiryVideo1FilePath = videoFullPath;
                GlobalData.NewTalkViewModel.Inquiry.InquiryPictureFilePath = pictureFullPath;
            }

            _cameraHelperBack.IsDisplay = true;
            _cameraHelperBack.SourcePlayer = VideoSourcePlayer_Back;
            _cameraHelperBack.UpdateCameraDevices();

            if (_cameraHelperBack.CameraDevices.Count > 1)
            {
                string videoFullPath = $"{AppDomain.CurrentDomain.BaseDirectory}InquiryData\\{GlobalData.NewTalkViewModel.Inquiry.InquiryId}\\back.mp4";
                string pictureFullPath = $"{AppDomain.CurrentDomain.BaseDirectory}InquiryData\\{GlobalData.NewTalkViewModel.Inquiry.InquiryId}\\mainpic.jpg";

                //_cameraHelperBack.SetCameraDevice(1, videoFullPath, string.Empty, true, false, System.Drawing.RotateFlipType.RotateNoneFlipX);
                _cameraHelperBack.SetCameraDevice(1, videoFullPath, pictureFullPath, true, true, System.Drawing.RotateFlipType.RotateNoneFlipX);

                GlobalData.NewTalkViewModel.Inquiry.InquiryVideo2FilePath = videoFullPath;
            }

            int t = ASR_Recog.Init();
            if (t >= 0)
            {
                _isVoiceReady = true;
                _dataQueue = new ConcurrentQueue<byte[]>();

                _voiceThread = new Thread(new ThreadStart(this.StartRecog));
                _voiceThread.IsBackground = true;
            }

            _homeWindow = (Window.GetWindow(this) as HomeWindow);
        }


        private void _audioHelper_AudioDataAvailable(object sender, AudioDataAvailableEventArgs e)
        {
            Slider_AudioPeak.Value = e.MasterPeakValue * 100;
            ProgressBar_AudioPeak.Value = e.MasterPeakValue * 100;
        }

        private void StartRecog()
        {
            while (true)
            {
                if (this._dataQueue.IsEmpty)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    byte[] dataTmp = (byte[])null;
                    if (!this._dataQueue.TryDequeue(out dataTmp))
                    {
                        Thread.Sleep(100);
                    }
                    else
                    {
                        int ASRRealTimeEnd = 0;
                        if (ASR_Recog.GetRealtime() == "yes")
                        {
                            string str_t = ASR_Recog.RealtimeRecog(dataTmp, dataTmp.Length, out ASRRealTimeEnd);
                            Dispatcher.BeginInvoke((Action)delegate ()
                            {
                                this.TextBox_CurrentTalkingCountent.AppendText(str_t);
                            });
                        }
                        //this.memoEdit2.BeginInvoke((Delegate)(() => this.memoEdit2.Text += ASR_Recog.RealtimeRecog(dataTmp, dataTmp.Length, out ASRRealTimeEnd)));
                        else if (ASR_Recog.GetRealtime() == "rt")
                        {
                            string result = ASR_Recog.RealtimeRecog(dataTmp, dataTmp.Length, out ASRRealTimeEnd);

                            VoiceConverterModel voiceConverterModel_t = JsonHelper.DeserializeObjectFromJson<VoiceConverterModel>(result);
                            if (voiceConverterModel_t == null)
                            {
                                continue;
                            }

                            Dispatcher.BeginInvoke((Action)delegate ()
                            {
                                this.TextBox_CurrentTalkingCountent.Text = _lastRealContent;
                                for (int i = 0; i < voiceConverterModel_t.SegmentCount; i++)
                                {
                                    this.TextBox_CurrentTalkingCountent.Text = this.TextBox_CurrentTalkingCountent.Text + voiceConverterModel_t.Segment[i].Text;

                                }

                                this.Button_Submit.IsEnabled = false;
                                this.TextBox_CurrentTalkingCountent.SelectionStart = this.TextBox_CurrentTalkingCountent.Text.Length;

                            });
                            //this.memoEdit2.BeginInvoke((Delegate)(() => this.ParseJson(result)));
                            if (ASRRealTimeEnd == 1)
                            {
                                //this.memoEdit2.BeginInvoke((Delegate)(() => this.last_content_ = this.memoEdit2.Text));                                

                                Dispatcher.BeginInvoke((Action)delegate ()
                                {
                                    for (int i = 0; i < voiceConverterModel_t.SegmentCount; i++)
                                    {
                                        this.TextBox_CurrentTalkingCountent.Text = _lastRealContent + voiceConverterModel_t.Segment[i].Text;
                                    }

                                    _lastRealContent = this.TextBox_CurrentTalkingCountent.Text;
                                    this.TextBox_CurrentTalkingCountent.SelectionStart = this.TextBox_CurrentTalkingCountent.Text.Length;
                                    this.Button_Submit.IsEnabled = true;

                                });
                            }
                        }
                    }
                }
            }
        }

        public void RecogStop()
        {
            try
            {
                _isVoiceConverterStart = false;

                _voiceRecorder.SoundDataGet -= _voiceRecorder_SoundDataGet;
                _voiceRecorder.RecStop();
                _voiceRecorder = null;
                _voiceFileStream.Close();
                _voiceFileStream = null;
            }
            catch (Exception ex)
            {
                _isVoiceConverterStart = false;
            }
        }

        private void ToggleButton_OpenCloseVoiceConversion_Checked(object sender, RoutedEventArgs e)
        {
            if (_isVoiceReady)
            {
                if (!_voiceThread.IsAlive)
                {
                    _voiceThread.Start();
                }
                try
                {
                    if (_voiceFileStream == null)
                    {
                        _voiceFileStream = File.OpenWrite(".\\tmp.pcm");
                        _voiceFileStream.Position = _voiceFileStream.Length;
                    }

                    _isVoiceConverterStart = true;
                    _voiceRecorder = new DirectXSoundHelper();
                    _voiceRecorder.Init();
                    _voiceRecorder.SoundDataGet -= _voiceRecorder_SoundDataGet;
                    _voiceRecorder.SoundDataGet += _voiceRecorder_SoundDataGet;
                    _voiceRecorder.SetFileName("tmp.pcm");
                    _voiceRecorder.RecStart();
                }
                catch (Exception)
                {

                    throw;
                }
            }



            Debug.WriteLine(ToggleButton_OpenCloseVoiceConversion.IsChecked);
        }

        private void _voiceRecorder_SoundDataGet(object sender, Common.DirectXSoundEventArgs e)
        {
            if (!_isVoiceConverterStart)
                return;

            if (_voiceDataLength >= 16000)
            {
                _voiceFileStream.Write(_voiceData, 0, _voiceDataLength);
                byte[] numArray = new byte[this._voiceDataLength];
                Array.Copy((Array)_voiceData, 0, (Array)numArray, 0, this._voiceDataLength);
                this._dataQueue.Enqueue(numArray);
                Array.Clear((Array)_voiceData, 0, this._voiceDataLength);
                this._voiceDataLength = 0;
            }

            e.AudioData.CopyTo((Array)this._voiceData, this._voiceDataLength);
            this._voiceDataLength += e.AudioData.Length;
        }

        private void ToggleButton_OpenCloseVoiceConversion_UnChecked(object sender, RoutedEventArgs e)
        {
            //_voiceThread.Abort();
            if (_isVoiceConverterStart)
            {
                RecogStop();
            }

            Debug.WriteLine(ToggleButton_OpenCloseVoiceConversion.IsChecked);
        }

        private void Button_StartTalking_Click(object sender, RoutedEventArgs e)
        {
            //GlobalData.TalkingPageViewModel.IsRunning = true;

            GlobalData.TalkingPageViewModel.SetTalkingState(ViewModel.Enum.TalkState.Running);

            _cameraHelperBack.IsStartRec = true;
            _cameraHelperFont.IsStartRec = true;

            _cameraHelperBack.CaptureImage();

            string audioFullPath = $"{AppDomain.CurrentDomain.BaseDirectory}InquiryData\\{GlobalData.NewTalkViewModel.Inquiry.InquiryId}\\audio.wav";
            GlobalData.NewTalkViewModel.Inquiry.InquiryAudioFilePath = audioFullPath;

            if (_audioHelper.IsPause)
            {
                _audioHelper.Resume();
            }
            else
            {
                _audioHelper.StartRec(audioFullPath);
                GlobalData.CurrentTalkingStartTime = DateTime.Now;
            }
            _timer.Start();

            _voiceRecorder?.RecResume();
        }

        private void Button_PauseTalking_Click(object sender, RoutedEventArgs e)
        {
            //GlobalData.TalkingPageViewModel.IsRunning = false;

            GlobalData.TalkingPageViewModel.SetTalkingState(ViewModel.Enum.TalkState.Pause);

            _cameraHelperBack.IsStartRec = false;
            _cameraHelperFont.IsStartRec = false;

            _audioHelper.Pause();

            _timer.Stop();

            _voiceRecorder?.RecPause();
        }

        private async void Button_EndTalking_Click(object sender, RoutedEventArgs e)
        {
            //GlobalData.TalkingPageViewModel.IsRunning = false;

            GlobalData.TalkingPageViewModel.SetTalkingState(ViewModel.Enum.TalkState.Stopped);
            GlobalData.CurrentTalkingEndTime = DateTime.Now;

            _cameraHelperBack.IsStartRec = false;
            _cameraHelperFont.IsStartRec = false;

            _timer.Stop();
            _audioHelper.StopRec();

            _cameraHelperFont.CloseDevice();
            _cameraHelperBack.CloseDevice();

            if (_isVoiceConverterStart)
            {
                RecogStop();
            }

            string talkingMessageListFullPath = $"{AppDomain.CurrentDomain.BaseDirectory}InquiryData\\{GlobalData.NewTalkViewModel.Inquiry.InquiryId}\\talkingMessageList.json";
            GlobalData.TalkingPageViewModel.SaveTalkingMessageList(talkingMessageListFullPath);

            GlobalData.NewTalkViewModel.Inquiry.InquiryChatFilePath = talkingMessageListFullPath;

            InquiryDAL inquiryDAL = new InquiryDAL();
            await inquiryDAL.Update(GlobalData.NewTalkViewModel.Inquiry);

        }

        private async void Button_CreateBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ASR_Recog.Release();

                //GlobalData.TalkingPageViewModel.Test();

                //GlobalData.TalkingPageViewModel.IsOpenVoiceConversion = !GlobalData.TalkingPageViewModel.IsOpenVoiceConversion;
                //GlobalData.TalkingPageViewModel.IsPartySpeeking = !GlobalData.TalkingPageViewModel.IsPartySpeeking;
                GlobalData.TalkingPageViewModel.SetTalkingState(ViewModel.Enum.TalkState.CreateingBook);
                this.StarkPanel_CreateLawbook.Visibility = Visibility.Visible;
                this.Grid_Mask.Visibility = Visibility.Visible;
                this.Grid_Video.Visibility = Visibility.Collapsed;

                await Task.Delay(TimeSpan.FromSeconds(1));

                if (_homeWindow.Frame_BusinessPage.CanGoBack)
                {
                    _homeWindow.Frame_BusinessPage.RemoveBackEntry();
                }

                _homeWindow.Frame_BusinessPage.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                _homeWindow.Grid_MainNavigationButtons.Visibility = Visibility.Collapsed;
                _homeWindow.Frame_BusinessPage.Navigate(new Uri("Pages/LawBookPage.xaml", UriKind.Relative));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.TextBox_CurrentTalkingCountent.Text))
            {
                return;
            }

            GlobalData.TalkingPageViewModel.AddNewMessage(new ViewModel.TalkingMessageModel
            {
                MessageContent = this.TextBox_CurrentTalkingCountent.Text.Trim(),
                MessageTime = DateTime.Now.ToString(),
                MessageTypeIsParty = GlobalData.TalkingPageViewModel.IsPartySpeeking
            });

            try
            {
                //清理当前
                this.TextBox_CurrentTalkingCountent.Clear();
                _lastRealContent = string.Empty;

                byte[] dataTmp = (byte[])null;
                this._dataQueue.TryDequeue(out dataTmp);

                Array.Clear(this._voiceData, 0, this._voiceData.Length);
                this._voiceDataLength = 0;
                //--
            }
            catch { }

            GlobalData.TalkingPageViewModel.IsPartySpeeking = !GlobalData.TalkingPageViewModel.IsPartySpeeking;

            ListBox_MessageList.ScrollIntoView(ListBox_MessageList.Items[ListBox_MessageList.Items.Count - 1]);
        }

        private void TextBox_CurrentTalkingCountent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Button_Submit_Click(new object(), new RoutedEventArgs());
            }
            else
            {
                _lastRealContent = this.TextBox_CurrentTalkingCountent.Text;
            }
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.TextBox_CurrentTalkingCountent.Clear();
                _lastRealContent = string.Empty;

                byte[] dataTmp = (byte[])null;
                this._dataQueue.TryDequeue(out dataTmp);

                Array.Clear(this._voiceData, 0, this._voiceData.Length);
                this._voiceDataLength = 0;
            }
            catch { }
        }
    }
}
