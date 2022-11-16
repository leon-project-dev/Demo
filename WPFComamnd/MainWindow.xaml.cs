using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
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

namespace WPFComamnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("CommandBinding 命令");
        }

        public RoutedUICommand ButtonCommand { get; set; }
    }

    public class DataCommandBinding
    {
        
        static DataCommandBinding()
        {
            DoSomething = new RoutedUICommand("DoSomething", nameof(DoSomething), typeof(DataCommandBinding), new InputGestureCollection(new List<InputGesture>() { new KeyGesture(Key.D, ModifierKeys.Control) }));
        }

        public static RoutedUICommand DoSomething { get; private set; }


    }
}
