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
using Telerik.Windows.Controls;

namespace ChongGuanSafetySupervisionQZ.View.WPF
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private CheckingHardwareWindow checkingHardwareWindow = new CheckingHardwareWindow();
        private HomeWindow homeWindow = new HomeWindow();
        public LoginWindow()
        {
            InitializeComponent();

            //test
            this.TextBox_UserName.Text = "admin";
            this.RadPasswordBox_Password.Password = "123456";
        }

        private bool CheckInputIsShitOrFuck()
        {
            if (string.IsNullOrWhiteSpace(this.TextBox_UserName.Text))
            {
                this.TextBox_UserName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.RadPasswordBox_Password.Password))
            {
                this.RadPasswordBox_Password.Focus();
                return false;
            }

            return true;
        }

        private void IsLoginningOrDone(bool isLoginning)
        {
            if (isLoginning)
            {
                this.Button_Confim.IsEnabled = false;
                this.ProgressBar_Login.Visibility = Visibility.Visible;
            }
            else
            {
                this.Button_Confim.IsEnabled = true;
                this.ProgressBar_Login.Visibility = Visibility.Collapsed;

            }

        }


        private async void Button_Confim_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInputIsShitOrFuck())
            {
                return;
            }

            IsLoginningOrDone(true);

            //await Task.Delay(1000);            

            ChongGuanSafetySupervisionQZ.DAL.UserDAL userDAL = new DAL.UserDAL();
            var loginResult = await userDAL.Login(new Model.QZ_User { LoginName = this.TextBox_UserName.Text, LoginPwd = this.RadPasswordBox_Password.Password });

            if (!loginResult.IsSuccessed)
            {
                IsLoginningOrDone(false);

                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "登录失败", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = loginResult.Message, FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                    Owner = this,
                    Theme = new MaterialTheme()
                });

                return;
            }

            GlobalData.CurrentUser = loginResult.Data;

            ChongGuanSafetySupervisionQZ.DAL.Role_UserDAL role_UserDAL = new DAL.Role_UserDAL();
            var role_UserResult = role_UserDAL.QueryRoleByUser(GlobalData.CurrentUser);

            if (!role_UserResult.IsSuccessed)
            {
                IsLoginningOrDone(false);

                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "登录失败", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = role_UserResult.Message, FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                    Owner = this,
                    Theme = new MaterialTheme()
                });

                return;
            }

            GlobalData.CurrnetRole = role_UserResult.Data;

            ChongGuanSafetySupervisionQZ.DAL.Deparment_UserDAL deparment_UserDAL = new DAL.Deparment_UserDAL();
            var deparment_UserResult = deparment_UserDAL.QueryDeparmentByUser(GlobalData.CurrentUser);

            if (!deparment_UserResult.IsSuccessed)
            {
                IsLoginningOrDone(false);

                RadWindow.Alert(new DialogParameters
                {
                    Header = new TextBlock { Text = "登录失败", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                    Content = new TextBlock { Text = deparment_UserResult.Message, FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                    Owner = this,
                    Theme = new MaterialTheme()
                });

                return;
            }

            GlobalData.CurrentDeparment = deparment_UserResult.Data;

            //dXRibbonMainWindow.Show();
            checkingHardwareWindow.Show();
            //homeWindow.Show();
            this.Close();

            IsLoginningOrDone(false);

            //NavigationService..Navigate(new Uri("NavigateDemoPage.xaml", UriKind.Relative));
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm(new DialogParameters
            {
                Header = new TextBlock { Text = "退出", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                Content = new TextBlock { Text = "确定退出本系统吗？", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                Owner = this,
                Theme = new MaterialTheme(),

                Closed = (_, __) =>
                {
                    if (__.DialogResult.Value)
                    {
                        Application.Current.Shutdown();
                    }
                }
            });
        }
    }
}
