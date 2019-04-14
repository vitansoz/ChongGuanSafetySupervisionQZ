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

namespace ChongGuanSafetySupervisionQZ.View.WPF.UserControls
{
    /// <summary>
    /// TalkingMessageControl.xaml 的交互逻辑
    /// </summary>
    public partial class TalkingMessageControl : UserControl
    {
        public TalkingMessageControl()
        {
            InitializeComponent();
        }

        public string MessageTime
        {
            get { return (string)GetValue(MessageTimeProperty); }
            set { SetValue(MessageTimeProperty, value); }
        }

        public static DependencyProperty MessageTimeProperty = DependencyProperty.Register("MessageTime", typeof(string), typeof(TalkingMessageControl),
                                                                new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnDataPropertyChanged)));

        public string MessageContent
        {
            get { return (string)GetValue(MessageContentProperty); }
            set { SetValue(MessageContentProperty, value); }
        }

        public static DependencyProperty MessageContentProperty = DependencyProperty.Register("MessageContent", typeof(string), typeof(TalkingMessageControl),
                                                                new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnDataPropertyChanged)));

        public bool MessageTypeIsParty
        {
            get { return (bool)GetValue(MessageTypeIsPartyProperty); }
            set { SetValue(MessageTypeIsPartyProperty, value); }
        }

        public static DependencyProperty MessageTypeIsPartyProperty = DependencyProperty.Register("MessageTypeIsParty", typeof(bool), typeof(TalkingMessageControl),
                                                                new PropertyMetadata(false, new PropertyChangedCallback(OnDataPropertyChanged)));


        private static void OnDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == MessageTimeProperty)
            {
                TalkingMessageControl myself = d as TalkingMessageControl;
                if (e.NewValue != null)
                {
                    myself.TextBlock_MessageTime.Text = e.NewValue.ToString();
                }
            }

            if (e.Property == MessageContentProperty)
            {
                TalkingMessageControl myself = d as TalkingMessageControl;
                if (e.NewValue != null)
                {
                    myself.TextBlock_MessageContent.Text = e.NewValue.ToString();
                }
            }

            if (e.Property == MessageTypeIsPartyProperty)
            {
                TalkingMessageControl myself = d as TalkingMessageControl;
                if (e.NewValue != null)
                {
                    if ((bool)e.NewValue)
                    {
                        myself.TextBlock_MessageTime.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
                        myself.Button_ChangeMessageSource.Content = "回答";
                    }
                    else
                    {
                        myself.TextBlock_MessageTime.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 128, 64));
                        myself.Button_ChangeMessageSource.Content = "提问";
                    }
                }
            }

        }

        private void Button_ChangeMessageSource_Click(object sender, RoutedEventArgs e)
        {
            MessageTypeIsParty = !MessageTypeIsParty;
        }
    }

}
