using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
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
using Telerik.Windows.Controls;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Pages
{
    /// <summary>
    /// NewTalkPage.xaml 的交互逻辑
    /// </summary>
    public partial class NewTalkPage : Page
    {
        private bool _isConfimPartyInfo = false;
        private HomeWindow _homeWindow;
        private Thread _getCardInfoThread = null;

        public NewTalkPage()
        {
            InitializeComponent();

            //TextBox_HomeAddress.DataContext = GlobalData.NewTalkViewModel;
            Grid_Hardware_Status.DataContext = GlobalData.CheckingHardwareViewModel;
            GlobalData.NewTalkViewModel = new ViewModel.NewTalkViewModel();
            GroupBox_PartyInfo.DataContext = GlobalData.NewTalkViewModel;
            GroupBox_PoliceInfo.DataContext = GlobalData.NewTalkViewModel;


            this.Loaded += NewTalkPage_Loaded;
            this.Unloaded += NewTalkPage_Unloaded;
        }

        private void NewTalkPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= NewTalkPage_Loaded;
        }

        private void NewTalkPage_Loaded(object sender, RoutedEventArgs e)
        {
            _isConfimPartyInfo = false;
            ChongGuanSafetySupervisionQZ.DAL.TalkTypeDAL talkTypeDAL = new DAL.TalkTypeDAL();

            var result_t = talkTypeDAL.Query();

            if (result_t.IsSuccessed)
            {
                List<QZ_TalkType> talkTypes = result_t.Data.ToList();

                this.ComboBox_TalkTpye.ItemsSource = talkTypes;
                this.ComboBox_TalkTpye.SelectedIndex = 0;
            }

            _homeWindow = (Window.GetWindow(this) as HomeWindow);

            _getCardInfoThread = new Thread(new ThreadStart(GlobalData.NewTalkViewModel.ReadCDCard));
            _getCardInfoThread.IsBackground = true;
            _getCardInfoThread.Start();
        }

        private void Button_Confim_Click(object sender, RoutedEventArgs e)
        {
            if (!_isConfimPartyInfo)
            {
                if (!CheckPartyInfoIsEmpty())
                {
                    RadWindow.Alert(new DialogParameters
                    {
                        Header = new TextBlock { Text = "提示", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                        Content = new TextBlock { Text = "请把信息填写完整", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                        Owner = Window.GetWindow(this),
                        Theme = new MaterialTheme()
                    });

                    return;
                }

                GlobalData.NewTalkViewModel.SavePartyInfo(GlobalData.CurrentDeparment?.DeparmentId.ToString(),
                    GlobalData.CurrentUser?.UserId.ToString());

                GroupBox_PoliceInfo.Visibility = Visibility.Visible;
                Button_Confim.Content = "开始谈话";
                Button_ReGet.Content = "取消谈话";
                _isConfimPartyInfo = true;

                //GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceName = "詹彦晶";
                //GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceNumber = "110";
                //GlobalData.NewTalkViewModel.Inquiry.InquiryRemarks = "安徽杀手大会傻傻的哈稍等哈说的话萨迪克";
                //GlobalData.NewTalkViewModel.Inquiry.InquiryAddress = "茅坑厕所拉屎地";

                GlobalData.NewTalkViewModel.Inquiry = GlobalData.NewTalkViewModel.Inquiry;
            }
            else
            {
                if (!CheckInquiryInfoIsEmpty())
                {
                    RadWindow.Alert(new DialogParameters
                    {
                        Header = new TextBlock { Text = "提示", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                        Content = new TextBlock { Text = "请把信息填写完整", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                        Owner = Window.GetWindow(this),
                        Theme = new MaterialTheme()
                    });

                    return;
                }

                GlobalData.NewTalkViewModel.Inquiry.PartyId = GlobalData.NewTalkViewModel.Party.PartyId;

                GlobalData.NewTalkViewModel.SaveInquiryInfo(GlobalData.CurrentDeparment?.DeparmentId.ToString(),
                    GlobalData.CurrentUser?.UserId.ToString());

                if (_homeWindow.Frame_BusinessPage.CanGoBack)
                {
                    _homeWindow.Frame_BusinessPage.RemoveBackEntry();
                }

                _homeWindow.Frame_BusinessPage.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

                _homeWindow.Grid_MainNavigationButtons.Visibility = Visibility.Collapsed;

                _homeWindow.Frame_BusinessPage.Navigate(new Uri("Pages/TalkingPage.xaml", UriKind.Relative));
            }
        }



        private bool CheckPartyInfoIsEmpty()
        {
            return !(string.IsNullOrWhiteSpace(this.TextBox_PartyName.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_Birthplace.Text) ||
                string.IsNullOrWhiteSpace(this.DatePicker_Birthday.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_IdNnumber.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_HomeAddress.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_CustNumber.Text) ||
                string.IsNullOrWhiteSpace(this.DatePicker_DetentionDate.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_Supervisory.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_SupervisionArea.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_SupervisionRoom.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_DetentionReason.Text));
        }

        private bool CheckInquiryInfoIsEmpty()
        {
            return !(string.IsNullOrWhiteSpace(this.TextBox_PoliceName.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_PoliceNumber.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_RespondentNumber.Text) ||
                string.IsNullOrWhiteSpace(this.DatePicker_TalkingDate.Text) ||
                string.IsNullOrWhiteSpace(this.TextBox_TalkingAddress.Text));
        }

        private bool CheckInfoIsEmpty(Visual myVisual, IEnumerable<string> exception)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);
                if (childVisual != null)
                {
                    if ((childVisual is TextBox) && string.IsNullOrWhiteSpace((childVisual as TextBox).Text) && !exception.Contains((childVisual as Control).Name))
                    {
                        RadWindow.Alert(new DialogParameters
                        {
                            Header = new TextBlock { Text = "提示", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                            Content = new TextBlock { Text = "请把信息填写完整" + (childVisual as Control).Name, FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                            Owner = Window.GetWindow(this),
                            Theme = new MaterialTheme()
                        });

                        return false;
                    }

                    if ((childVisual is DatePicker) && string.IsNullOrWhiteSpace((childVisual as DatePicker).Text) && !exception.Contains((childVisual as Control).Name))
                    {
                        RadWindow.Alert(new DialogParameters
                        {
                            Header = new TextBlock { Text = "提示", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                            Content = new TextBlock { Text = "请把信息填写完整" + (childVisual as Control).Name, FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                            Owner = Window.GetWindow(this),
                            Theme = new MaterialTheme()
                        });

                        return false;
                    }

                    if (!CheckInfoIsEmpty(childVisual, new string[] { "" }))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void EnumVisual(Visual myVisual)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);
                if (childVisual != null)
                {
                    if (childVisual is TextBox) (childVisual as TextBox).Text = "Winner";
                    EnumVisual(childVisual);
                }
            }
        }

        private void Button_ReGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (!_isConfimPartyInfo)
            {
                GlobalData.NewTalkViewModel.GetPartyInfoByIdCard(this.TextBox_IdNnumber.Text);
            }
            else
            {
                (Window.GetWindow(this) as HomeWindow).GoHome();
            }

        }

        private void Button_Test_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.NewTalkViewModel.Party.PartyName = "张三";
            GlobalData.NewTalkViewModel.Party.PartySex = "男";
            GlobalData.NewTalkViewModel.Party.PartyNational = "汉";
            GlobalData.NewTalkViewModel.Party.PartyBirth = "1990/01/01";
            GlobalData.NewTalkViewModel.Party.PartyCard = "11011000000000000";
            GlobalData.NewTalkViewModel.Party.PartyAddress = "北京市朝阳区xxxxxxxx街道xxxx号";
            GlobalData.NewTalkViewModel.Party.PartyNumber = "123";
            GlobalData.NewTalkViewModel.Party.PartyDetentionDate = "2018/09/01";
            GlobalData.NewTalkViewModel.Party.PartyPrison = "xxx监所";
            GlobalData.NewTalkViewModel.Party.PartyMonitoringArea = "xxx监区";
            GlobalData.NewTalkViewModel.Party.PartyMonitoringRoom = "xxx监室";
            GlobalData.NewTalkViewModel.Party.PartyDetentionReason = "xxxxxxxxxxxxxxxxx";

            GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceName = "李警官";
            GlobalData.NewTalkViewModel.Inquiry.InquiryPoliceNumber = "1234";
            GlobalData.NewTalkViewModel.Inquiry.InquiryRemarks = "xxxxxxxxxxxxxxx";
            GlobalData.NewTalkViewModel.Inquiry.InquiryAddress = "xxx看守所xxx谈话室";

            GlobalData.NewTalkViewModel.Inquiry = GlobalData.NewTalkViewModel.Inquiry;
            GlobalData.NewTalkViewModel.Party = GlobalData.NewTalkViewModel.Party;


        }
    }
}
