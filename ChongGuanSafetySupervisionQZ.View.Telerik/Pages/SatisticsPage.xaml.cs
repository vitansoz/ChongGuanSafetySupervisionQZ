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
    /// SatisticsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SatisticsPage : Page
    {
        public SatisticsPage()
        {
            InitializeComponent();

            GlobalData.SatisticsPageViewModel = new ViewModel.SatisticsPageViewModel();

            this.Loaded += SatisticsPage_Loaded;
        }

        private void SatisticsPage_Loaded(object sender, RoutedEventArgs e)
        {
            //GlobalData.SatisticsPageViewModel.Test();
            this.DataContext = GlobalData.SatisticsPageViewModel;
        }

        private void Button_Do_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.SatisticsPageViewModel.Search();
        }
    }
}
