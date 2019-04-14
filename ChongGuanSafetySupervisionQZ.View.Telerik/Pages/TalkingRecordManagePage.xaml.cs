using ChongGuanSafetySupervisionQZ.Model;
using ChongGuanSafetySupervisionQZ.ViewModel.BussinessModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using Telerik.Windows.Controls;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Pages
{
    /// <summary>
    /// TalkingRecordManagePage.xaml 的交互逻辑
    /// </summary>
    public partial class TalkingRecordManagePage : Page
    {
        public TalkingRecordManagePage()
        {
            StyleManager.ApplicationTheme = new FluentTheme();

            InitializeComponent();


            this.Loaded += TalkingRecordManagePage_Loaded;
            GlobalData.TalkingRecordManageViewModel = new ViewModel.TalkingRecordManageViewModel();
        }

        private void TalkingRecordManagePage_Loaded(object sender, RoutedEventArgs e)
        {
            ChongGuanSafetySupervisionQZ.DAL.TalkTypeDAL talkTypeDAL = new DAL.TalkTypeDAL();

            var result_t = talkTypeDAL.Query();

            if (result_t.IsSuccessed)
            {
                List<QZ_TalkType> talkTypes = result_t.Data.ToList();

                talkTypes.Insert(0, new QZ_TalkType { TalkTypeId = -1, TalkTypeName = "请选择" });
                this.ComboBox_TalkTpye.ItemsSource = talkTypes;
                this.ComboBox_TalkTpye.SelectedIndex = 0;
            }

            GlobalData.TalkingRecordManageViewModel.SearchAll(SystemParameters.PrimaryScreenWidth / 4 - 80);
            //GlobalData.TalkingRecordManageViewModel.Search(SystemParameters.PrimaryScreenWidth / 4 - 80, 0, "张三");

            this.DataContext = GlobalData.TalkingRecordManageViewModel;
            //this.ListBox_TalkingRecords.ItemsSource = GlobalData.TalkingRecordManageViewModel.CurrentInquiryAndPartyList;
        }

        private void LayoutControl_SelectionChanged(object sender, Telerik.Windows.Controls.LayoutControl.LayoutControlSelectionChangedEventArgs e)
        {
            MessageBox.Show("fuck");
            Debug.WriteLine(LayoutControl.SelectedItem.ToString());
        }

        private void Hyperlink_PrePage_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.TalkingRecordManageViewModel.PrePage();
        }

        private void Hyperlink_NextPage_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.TalkingRecordManageViewModel.NextPage();
            //(RootLayoutTabGroup.Items[1] as LayoutControlTabGroupItem).IsSelected = true;
        }

        private void LayoutControlTabGroupItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //GlobalData.TalkingRecordManageViewModel.Search(SystemParameters.PrimaryScreenWidth / 4 - 80);
        }

        private void Hyperlink_PrePageBySeacrh_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.TalkingRecordManageViewModel.PrePageBySearch();
        }

        private void Hyperlink_NextPageBySeacrh_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.TalkingRecordManageViewModel.NextPageBySearch();
        }

        private void Button_ReGetInfo_Click(object sender, RoutedEventArgs e)
        {
            this.TextBox_PartyNameForSearch.Text = string.Empty;
            this.TextBox_PartyNumber.Text = string.Empty;

            this.ComboBox_TalkTpye.SelectedIndex = 0;
        }

        private void Button_Confim_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.TalkingRecordManageViewModel.Search(SystemParameters.PrimaryScreenWidth / 4 - 80, 0, this.TextBox_PartyNameForSearch.Text, this.TextBox_PartyNumber.Text, this.ComboBox_TalkTpye.Text == "请选择" ? "" : this.ComboBox_TalkTpye.Text);
            (RootLayoutTabGroup.Items[2] as LayoutControlTabGroupItem).IsSelected = true;
        }

        private void ListBox_TalkingRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                GlobalData.TalkingRecordManageViewModel.CurrentInquiryAndParty = e.AddedItems[0] as InquiryAndPartyModel;
                (RootLayoutTabGroup.Items[3] as LayoutControlTabGroupItem).IsSelected = true;

                GlobalData.TalkingRecordManageViewModel.LoadMessageList();

                MediaElement_Video.Source = new Uri(GlobalData.TalkingRecordManageViewModel.CurrentInquiryAndParty.InquiryVideo1FilePath);
                Image_Mask.Visibility = Visibility.Visible;
            }
        }

        private void ListBox_TalkingRecordsBySearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {

                GlobalData.TalkingRecordManageViewModel.CurrentInquiryAndParty = e.AddedItems[0] as InquiryAndPartyModel;
                (RootLayoutTabGroup.Items[3] as LayoutControlTabGroupItem).IsSelected = true;

                GlobalData.TalkingRecordManageViewModel.LoadMessageList();

                MediaElement_Video.Source = new Uri(GlobalData.TalkingRecordManageViewModel.CurrentInquiryAndParty.InquiryVideo1FilePath);
                Image_Mask.Visibility = Visibility.Visible;
            }
        }

        private void Button_PlayVideo_Click(object sender, RoutedEventArgs e)
        {
            Image_Mask.Visibility = Visibility.Collapsed;
            MediaElement_Video.Play();
        }

        private void Button_PauseVideo_Click(object sender, RoutedEventArgs e)
        {
            MediaElement_Video.Pause();
        }

        private void Button_StopVideo_Click(object sender, RoutedEventArgs e)
        {
            MediaElement_Video.Stop();
        }

        private void MediaElement_Video_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElement_Video.Stop();
        }

        private void Button_OpenLawBook_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(GlobalData.TalkingRecordManageViewModel.CurrentInquiryAndParty.InquiryLawBookFilePath))
            {
                Process.Start(GlobalData.TalkingRecordManageViewModel.CurrentInquiryAndParty.InquiryLawBookFilePath);
            }
            else
            {
                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "提示", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = "该谈话没有生成文书", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                    Owner = Window.GetWindow(this),
                    Theme = new MaterialTheme()
                });
            }
        }
    }
}
