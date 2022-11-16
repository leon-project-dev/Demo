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

namespace DrawShape
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DrawingVisual drawingVisual = new DrawingVisual();
            var content = drawingVisual.RenderOpen();

            content.PushOpacity(0.5);
            content.DrawLine(new Pen(Brushes.Red, 10) { DashCap = PenLineCap.Round }, new Point(0, 0), new Point(100, 0));
            content.Pop();
            content.DrawRectangle(Brushes.DarkRed, new Pen(Brushes.Black, 2), new Rect(0, 0, 50, 50));
            content.DrawText(new FormattedText("ABCD", new System.Globalization.CultureInfo("en-US"), FlowDirection.RightToLeft, new Typeface("Verdana"), 10, Brushes.Chartreuse), new Point(50, 50));

            content.Close();

            this.Content = new Image() { Source = new DrawingImage(drawingVisual.Drawing) };

            List<string> strs = new List<string>()
            {
                "AAAA",
                "BBBB",
                "CCCC",
                "DDDD",
            };

            var items = strs.Select(_ => {
                return _ + "EEEE";
            });
            var rItems = items.Select(_ => _ + "FFF");

            var selectMang = strs.SelectMany(_ => { return _ + "EEEE"; });
        }


        private void Window_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            Mouse.SetCursor(Cursors.Cross);
            e.UseDefaultCursors = false;
            //InfoTextBox.Text += $"{e.Source} GiveFeedback \n";

            e.Handled = true;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed)
            //    DragDrop.DoDragDrop(Line1, new object(), DragDropEffects.All);

           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DrawingVisual drawingVisual = new DrawingVisual();
            //var content = drawingVisual.RenderOpen();
            //content.DrawLine(new Pen(Brushes.Red, 10) { DashCap = PenLineCap.Round }, new Point(0, 0), new Point(100, 0));
            //content.DrawRectangle(Brushes.DarkRed, new Pen(Brushes.Black, 2), new Rect(0, 0, 50, 50));
            //content.Close();

            //this.Content = new Image() { Source = new DrawingImage(drawingVisual.Drawing) };
        }
    }
}
