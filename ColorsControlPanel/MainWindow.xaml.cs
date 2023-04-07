using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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


namespace ColorsControlPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            CloseBtn.Click += (s, e) =>
            {
                Application.Current.Shutdown();
            };

            MinimizeBtn.Click += (s, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };



            ColorCanvasRun();


            string hex = "#FFFFFF";
            var color = ColorTranslator.FromHtml(hex);
            //tb1.Text = $"R: {color.R} G: {color.B} B: {color.G}";
            MainColorCanvas.SelectedColor = Colors.Black;
            NameVersionTB.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString(2);


        }

        async void ColorCanvasRun()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        // HilightPolygon.Fill = MainColorCanvas.SelectedColor.Value;
                    });
                }
            });

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ToolBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
