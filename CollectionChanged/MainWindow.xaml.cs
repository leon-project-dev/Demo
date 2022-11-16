using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CollectionChanged
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<string> collection = new ObservableCollection<string>();
            collection.CollectionChanged += Collection_CollectionChanged;
            for (int i = 0; i < 10; i++)
            {
                collection.Add($"AAAA{i}");
            }

            collection.Add("AAAA");
            collection.Add("BBBB");
            collection.Insert(1, "CCCC");            
            collection.Remove("BBBB");
            collection.RemoveAt(1);

            _DataGrid.ItemsSource = collection;
        }

        private void Collection_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MessgeTextBox.Text += $"action: {e.Action}, newIdx: {e.NewStartingIndex}:{e.NewItems}, oldIdx: {e.OldStartingIndex}:{e.OldItems} \n";
        }
    }
}
