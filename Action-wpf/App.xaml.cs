using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ActionWpf
{
    public enum DisplayMode
    {
        Content = 0,
        Icon,
        ContentAndIcon
    }

    public class ButtonItemVM : BindableBase, ICloneable
    {
        public bool IsToggleButton { get; set; }
        public DisplayMode DisplayMode
        {
            get
            {
                return DisplayMode_;
            }
            set
            {
                DisplayMode_ = value;
                RaisePropertiesChanged(nameof(DisplayMode));
            }
        }   // 显示模式
        public virtual string DisplayName        // 显示文本
        {
            get
            {
                return DisplayName_;
            }
            set
            {
                DisplayName_ = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }
        public double DisplayLen { get; set; }
        public TextAlignment DisplayTextAlign { get; set; }
        public ImageSource Icon      // 显示的 Icon 
        {
            get
            {
                return Icon_;
            }
            set
            {
                Icon_ = value;
                RaisePropertiesChanged(nameof(Icon));
            }
        }
        public Dock IconDock { get; set; }
        public ICommand Command { get; set; }    // 点击事件    
      
        public Brush BackgroundBrush { get; set; }

        public Brush BorderBrush { get; set; }

        public Thickness BorderThickness { get; set; }

        public float CornerRadius { get; set; }

        public virtual bool IsChecked { get; set; }
        public RoutedEvent Checked { get; set; }  // Toggle button 特有事件

        public ButtonItemVM()
        {
            CornerRadius = 0;
            BorderThickness = new Thickness(0);
            BackgroundBrush = System.Windows.Media.Brushes.Transparent;
            IconDock = Dock.Left;
            DisplayLen = Double.NaN;
            DisplayTextAlign = TextAlignment.Center;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        private ImageSource Icon_;
        private string DisplayName_;
        private DisplayMode DisplayMode_;
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            
            Startup += App_Startup;
            //Exit += App_Exit;
            // ShutdownMode
            //DispatcherUnhandledException
            //SplashScreen splashScreen = new SplashScreen("./Splash.jpg");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // DefaultProperty
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            GlobleMutex.ReleaseMutex();
            // throw new NotImplementedException();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            if (!GlobleMutex.WaitOne(1))
            {
                MessageBox.Show("程序已经运行");
                Shutdown();
                return;
            }
           // ICommand
            //e.Args
            MainWindow window = new MainWindow();
            window.Show();
        }

        public Mutex GlobleMutex = new Mutex(false, "Action-wpf");
    }

    public class DisplayModeToVisibleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2 || !(values[0] is DisplayMode) || !(values[1] is DisplayMode))
                return Visibility.Collapsed;

            return (DisplayMode)values[0] == (DisplayMode)values[1] ? Visibility.Visible : Visibility.Collapsed;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
