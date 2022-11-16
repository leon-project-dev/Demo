using DevExpress.Xpf.Grid;
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

namespace DragDropTest
{
    public class DataClass
    {
        public string Name { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<DataClass> Datas { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            InitData();
        }

        private void InitData()
        {
            Datas = new ObservableCollection<DataClass>();
            for(int i = 0;i <10; i++)
            {
                Datas.Add(new DataClass() { Name = i.ToString() });
            }
        }

        private void TreeListView_StartRecordDrag(object sender, DevExpress.Xpf.Core.StartRecordDragEventArgs e)
        {
            var tlv = sender as TreeListView;
            


        }

        private int handle = -1;
        private void TreeListView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tlv = sender as TreeListView;
            handle = tlv!.GetRowHandleByMouseEventArgs(e);
            var control = tlv.GetRowElementByRowHandle(handle) as RowControl;
            var node = control!.DataContext as TreeListNode;
            // node.ActualLevel
        }


    }
}
