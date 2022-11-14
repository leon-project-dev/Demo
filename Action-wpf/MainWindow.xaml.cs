using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking.VisualElements;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Hierarchy;
using DevExpress.Xpf.Grid.Native;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ActionWpf
{
    public enum WorkStatus
    {
        WaitingWork,
        RunningWork,
        FinFinishWork,
    }
    
    public class RowDatas
    {
        public ImageSource Picture { get; set; }
        public string Job { get; set; }
        public string Notes { get; set; }
        public Size Size { get; set; }
        public WorkStatus Status { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ButtonItemVM WaitQueueVM { get; set; }
        public ButtonItemVM HotFileVM { get; set; }
        public ButtonItemVM MaterialVM { get; set; }
        public ButtonItemVM ProtectVM { get; set; }
        public ButtonItemVM SettingVM { get; set; }

        public ButtonItemVM LoginVM { get; set; }
        public ButtonItemVM ReduceVM { get; set; }
        public ButtonItemVM ProjectVM { get; set; }

        public ButtonItemVM StartPrintVM { get; set; }
        public ButtonItemVM PrintAginVM { get; set; }
        public ButtonItemVM SelectedAllVM { get; set; }
        public ButtonItemVM CancelSelectedVM { get; set; }
        public ButtonItemVM DeleteVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _GridControl.ItemsSource = GetData();

            InitVms();

            Dispatcher.InvokeAsync(() => {
                int a = 0;
                a++;
            });
            SynchronizationContext.Current.Post((s) => 
            {
                int a = 0;
                a++; 
            }, null);

            TextEditor.Load("../../../App.xaml.cs");
        }

        private void InitVms()
        {
            WaitQueueVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "等待队列",
                CornerRadius = 25,
            };

            HotFileVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "热文件",
                CornerRadius = 25,
            };

            MaterialVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "材料",
                CornerRadius = 25,
            };

            ProtectVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "维护",
                CornerRadius = 25,
            };

            SettingVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "设置",
                CornerRadius = 25,
            };

            LoginVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Icon,
                Icon = Images.Persion,
                CornerRadius = 90,
            };

            ReduceVM = new ButtonItemVM
            {
                DisplayMode = DisplayMode.Icon,
                Icon = Images.Reduce,
                CornerRadius = 90,
            };

            ProjectVM = new ButtonItemVM
            {
                DisplayMode = DisplayMode.Icon,
                Icon = Images.Project,
                CornerRadius = 90,
            };

            StartPrintVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "开始打印",
                CornerRadius = 30,
            };
            PrintAginVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "再次打印",
                CornerRadius = 30,
            };
            SelectedAllVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "全选",
                CornerRadius = 30,
            };
            CancelSelectedVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "取消选择",
                CornerRadius = 30,
            };
            DeleteVM = new ButtonItemVM()
            {
                DisplayMode = DisplayMode.Content,
                DisplayName = "删除",
                CornerRadius = 30,
            };
        }

        public IEnumerable<RowDatas> GetData()
        {
            List<RowDatas> data = new List<RowDatas>();
            for(int i = 0; i < 20; i++)
            {
                data.Add(new RowDatas()
                {
                    Picture = i % 2 == 0 ?Images.Book : Images.Persion,
                    Job = "AAAA \n BBB \n CCC",
                    Notes = $"已完成{i}",
                    Size = new Size(100,500),
                    Status = (WorkStatus)(i % 3),
                });
            }

            return data;
        }

        private void WaitQueueToggleBtn_Checked(object sender, RoutedEventArgs e)
        {
            SettingToggleBtn.IsChecked = false;
            HotFileToggleBtn.IsChecked = false;
            MaterialToggleBtn.IsChecked = false;
            ProtectToggleBtn.IsChecked = false;
        }

        private void HotFileToggleBtn_Checked(object sender, RoutedEventArgs e)
        {
            WaitQueueToggleBtn.IsChecked = false;
            SettingToggleBtn.IsChecked = false;
            MaterialToggleBtn.IsChecked = false;
            ProtectToggleBtn.IsChecked = false;
        }

        private void MaterialToggleBtn_Checked(object sender, RoutedEventArgs e)
        {
            WaitQueueToggleBtn.IsChecked = false;
            HotFileToggleBtn.IsChecked = false;
            SettingToggleBtn.IsChecked = false;
            ProtectToggleBtn.IsChecked = false;
        }

        private void ProtectToggleBtn_Checked(object sender, RoutedEventArgs e)
        {
            WaitQueueToggleBtn.IsChecked = false;
            HotFileToggleBtn.IsChecked = false;
            MaterialToggleBtn.IsChecked = false;
            SettingToggleBtn.IsChecked = false;
        }

        private void SettingToggleBtn_Checked(object sender, RoutedEventArgs e)
        {
            WaitQueueToggleBtn.IsChecked = false;
            HotFileToggleBtn.IsChecked = false;
            MaterialToggleBtn.IsChecked = false;
            ProtectToggleBtn.IsChecked = false;
        }

        private void BarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }

    public class CellDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return base.SelectTemplate(item, container);
        }
    }



}

