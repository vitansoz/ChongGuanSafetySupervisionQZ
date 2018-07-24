using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ChongGuanSafetySupervisionQZ.View.WPF.Pages
{
    /// <summary>
    /// NewTalkPage.xaml 的交互逻辑
    /// </summary>
    public partial class NewTalkPage : Page
    {
        private bool _isConfimPartyInfo = false;

        public NewTalkPage()
        {
            InitializeComponent();

            //TextBox_HomeAddress.DataContext = GlobalData.NewTalkViewModel;
            Grid_Hardware_Status.DataContext = GlobalData.CheckingHardwareViewModel;
            GlobalData.NewTalkViewModel = new ViewModel.NewTalkViewModel();
            GroupBox_PartyInfo.DataContext = GlobalData.NewTalkViewModel;


            this.Loaded += NewTalkPage_Loaded;
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


        }

        private void Button_Confim_Click(object sender, RoutedEventArgs e)
        {
            if (!_isConfimPartyInfo)
            {
                GlobalData.NewTalkViewModel.SavePartyInfo(GlobalData.CurrentDeparment.DeparmentId,GlobalData.CurrentUser.UserId);

                GroupBox_PoliceInfo.Visibility = Visibility.Visible;
                Button_Confim.Content = "开始谈话";
                Button_ReGet.Content = "取消谈话";
                _isConfimPartyInfo = true;
            }
            else
            {

            }
        }

        private void Button_ReGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (!_isConfimPartyInfo)
            {
                GlobalData.NewTalkViewModel.GetPartyInfoByIdCard("");
            }
            else
            {
                (Window.GetWindow(this) as HomeWindow).GoHome();
            }

        }
    }
}
