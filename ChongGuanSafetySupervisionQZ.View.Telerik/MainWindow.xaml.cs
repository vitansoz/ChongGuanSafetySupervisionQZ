using ChongGuanDotNetUtils.Helpers;
using ChongGuanSafetySupervisionQZ.Model;
using ChongGuanSafetySupervisionQZ.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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

namespace ChongGuanSafetySupervisionQZ.View.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CheckingHardwareWindow checkingHardwareWindow = new CheckingHardwareWindow();

        public MainWindow()
        {
            //StyleManager.ApplicationTheme = new MaterialTheme();

            //StyleManager.SetTheme(this, new MaterialTheme());

            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private ChinaCitiesViewModel _chinaCitiesViewModel;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ChongGuanSafetySupervisionQZ.DAL.AreasDAL areasDAL = new DAL.AreasDAL();
            var areaList = areasDAL.Query();

            List<QZ_Areas> tt = new List<QZ_Areas>(areaList.Data.ToList());

            _chinaCitiesViewModel = new ChinaCitiesViewModel(tt);
            this.DataContext = _chinaCitiesViewModel;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _chinaCitiesViewModel.SelectedItem = e.NewValue as Location;
            this.Grid_AreaTreeView.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Grid_AreaTreeView.Visibility = Visibility.Visible;

            e.Handled = true;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Grid_AreaTreeView.Visibility == Visibility.Visible)
            {
                this.Grid_AreaTreeView.Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_AreaTreeView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private bool CheckInputIsShitOrFuck()
        {
            if (this.TextBlock_SelectedArea.Text.Contains("请选择"))
            {
                this.Grid_AreaTreeView.Visibility = Visibility.Visible;
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.TextBox_Department.Text))
            {
                this.TextBox_Department.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.TextBox_AdminUserName.Text))
            {
                this.TextBox_AdminUserName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.RadPasswordBox_AdminPassword.Password))
            {
                this.RadPasswordBox_AdminPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.RadPasswordBox_AdminPasswordConfim.Password))
            {
                this.RadPasswordBox_AdminPasswordConfim.Focus();
                return false;
            }

            return true;
        }


        private async void Button_Confim_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInputIsShitOrFuck())
            {
                return;
            }

            ChongGuanSafetySupervisionQZ.DAL.UserDAL userDAL = new DAL.UserDAL();
            var addUser_result = await userDAL.Add(new QZ_User
            {
                AreaCode = _chinaCitiesViewModel.SelectedItem.AreaId,
                CreateTime = DateTime.Now.ToString(),
                IsDeleteId = 0,
                IsForbidden = 0,
                LoginName = this.TextBox_AdminUserName.Text,
                LoginPwd = this.RadPasswordBox_AdminPassword.Password,
                ModifyTime = DateTime.Now.ToString(),
                UserCode = Guid.NewGuid().ToString("N"),
                UserName = "超级管理员",
                UserAge = "0",
                UserCard = "",
                UserEmail = "",
                UserFingerImageFilePath = "",
                UserLawCard = "",
                UserPhone = "",
                UserPhotoFilePath = "",
                UserSex = ""
            });

            if (!addUser_result.IsSuccessed)
            {
                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "添加管理员失败", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = addUser_result.Message, FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                    Owner = this,
                    Theme = new MaterialTheme()
                });

                return;
            }

            GlobalData.CurrentUser = addUser_result.Data;

            ChongGuanSafetySupervisionQZ.DAL.DeparmentDAL deparmentDAL = new DAL.DeparmentDAL();
            var addDeparment_result = await deparmentDAL.Add(new QZ_Deparment
            {
                AreaCode = _chinaCitiesViewModel.SelectedItem.AreaId,
                DeparmentCode = Guid.NewGuid().ToString("N"),
                DeparmentName = this.TextBox_Department.Text,
                DeparmentParentCode = "",
                IsDeleteId = 0
            });

            if (!addDeparment_result.IsSuccessed)
            {
                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "添加部门信息失败", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = addDeparment_result.Message, FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },


                    Owner = this,
                    Theme = new MaterialTheme()
                });

                return;
            }

            GlobalData.CurrentDeparment = addDeparment_result.Data;

            ChongGuanSafetySupervisionQZ.DAL.Deparment_UserDAL deparment_UserDAL = new DAL.Deparment_UserDAL();
            await deparment_UserDAL.Add(GlobalData.CurrentDeparment, GlobalData.CurrentUser);

            ChongGuanSafetySupervisionQZ.DAL.Role_UserDAL role_UserDAL = new DAL.Role_UserDAL();
            await role_UserDAL.Add(new QZ_Role { RoleId = 1 }, GlobalData.CurrentUser);

            GlobalData.CurrnetRole = new QZ_Role { RoleId = 1 };

            //RegistryHelper.SetRegistryData(@"ChongGuan\ChongGuanSafetySupervisionQZ",
            //    "Registered", "1");

            CreateRegFile();

            checkingHardwareWindow.Show();
            //dXRibbonMainWindow.Show();
            this.Close();
        }

        private void CreateRegFile()
        {
            if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}chongguanData\\"))
            {
                Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}chongguanData\\");
            }

            File.Create($"{AppDomain.CurrentDomain.BaseDirectory}chongguanData\\isreg.lock");
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            //string alertText = "The employee photo has been uploaded.";

            RadWindow.Confirm(new DialogParameters
            {
                Header = new TextBlock { Text = "退出", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                Content = new TextBlock { Text = "确定退出本系统吗？", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                //Closed = new EventHandler<WindowClosedEventArgs>(OnClosed),
                Owner = this,
                Theme = new MaterialTheme(),

                Closed = (_, __) =>
                {
                    if (__.DialogResult.Value)
                    {
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        //MessageBox.Show("FUCK");
                    }
                }
            });
        }

        private void RadPasswordBox_AdminPasswordConfim_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RadPasswordBox_AdminPasswordConfim.Password) && RadPasswordBox_AdminPassword.Password != RadPasswordBox_AdminPasswordConfim.Password)
            {
                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "提示", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = "两次密码输入不一致", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },
                    Closed = (s1, e1) =>
                    {
                        RadPasswordBox_AdminPasswordConfim.Clear();
                        RadPasswordBox_AdminPasswordConfim.Focus();
                    },
                    Owner = this,
                    Theme = new MaterialTheme()
                });
            }
        }
    }
}
