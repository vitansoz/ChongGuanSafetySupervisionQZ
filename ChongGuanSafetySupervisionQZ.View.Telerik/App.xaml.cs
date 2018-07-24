using ChongGuanDotNetUtils.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ChongGuanSafetySupervisionQZ.View.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            StartupUri = new Uri("HomeWindow.xaml", UriKind.Relative);
            //StartupUri = IsReged() ? new Uri("LoginWindow.xaml", UriKind.Relative) : new Uri("MainWindow.xaml", UriKind.Relative);

            foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionary["PrimaryHueMidBrush"] = new SolidColorBrush(Color.FromArgb(255, 48, 105, 164));
                dictionary["PrimaryHueDarkBrush"] = new SolidColorBrush(Color.FromArgb(255, 33, 105, 243));
            }
        }

        private bool IsReged()
        {
            return (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}chongguanData\\isreg.lock"));
            //return RegistryHelper.IsRegistryExist(Microsoft.Win32.Registry.LocalMachine, @"SOFTWARE\ChongGuan\ChongGuanSafetySupervisionQZ", "Registered");
        }

    }
}
