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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace ChongGuanSafetySupervisionQZ.View.WPF
{
    /// <summary>
    /// CheckingHardwareWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CheckingHardwareWindow : Window
    {
        private HomeWindow homeWindow = new HomeWindow();
        public CheckingHardwareWindow()
        {
            InitializeComponent();

            this.Loaded += CheckingHardwareWindow_Loaded;
            GlobalData.CheckingHardwareViewModel = new ViewModel.CheckingHardwareViewModel { CheckingProgress = 0, IsFingerprintGood = true, IsIdentificationGood = false, IsSceneCameraGood = true, IsSignatureGood = false };
            GlobalData.CheckingHardwareViewModel.CheckFinished += CheckingHardwareViewModel_CheckFinished;
        }

        private void CheckingHardwareViewModel_CheckFinished(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GlobalData.CheckingHardwareViewModel.ErrorHardwareMessage))
            {
                Dispatcher.Invoke(() =>
                {
                    RadWindow.Confirm(new DialogParameters
                    {
                        Header = new TextBlock { TextWrapping = TextWrapping.WrapWithOverflow, Text = "初始化错误", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false, Foreground = new SolidColorBrush(Colors.White) },
                        Content = new TextBlock { TextWrapping = TextWrapping.WrapWithOverflow, Text = GlobalData.CheckingHardwareViewModel.ErrorHardwareMessage + "初始化失败\n请选择重新检测或直接进入系统", FontFamily = new FontFamily("微软雅黑"), IsHitTestVisible = false },

                        CancelButtonContent = new TextBlock { TextWrapping = TextWrapping.Wrap, Text = "进入系统", FontFamily = new FontFamily("微软雅黑") },
                        OkButtonContent = new TextBlock { TextWrapping = TextWrapping.Wrap, Text = "重新检测", FontFamily = new FontFamily("微软雅黑") },

                        Owner = this,
                        Theme = new MaterialTheme(),

                        Closed = (_, __) =>
                        {
                            if (__.DialogResult.Value)
                            {
                                GlobalData.CheckingHardwareViewModel.InitHardware(new WindowInteropHelper(this).Handle);
                            }
                            else
                            {
                                homeWindow.Show();
                                this.Close();
                            }
                        }
                    });
                });
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    homeWindow.Show();
                    this.Close();
                });
            }
        }

        private void CheckingHardwareWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = GlobalData.CheckingHardwareViewModel;
            GlobalData.CheckingHardwareViewModel.InitHardware(new WindowInteropHelper(this).Handle);
        }
    }
}
