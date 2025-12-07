using ColorsControlPanel.Register;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorsControlPanel
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainWindow : Window
    {
        private static byte[] DefaultHilight = { 0, 120, 215 };
        private static byte[] DefaultHotTrackingColor = { 0, 102, 204 };
        private static byte[] ChangeHilightBtn;
        private static byte[] ChangeHotTrackingColorBtn;

        public MainWindow()
        {
            InitializeComponent();

            // Screens buttons
            string[] pathImg = {"/Source/Samples/Screen.png", "/Source/Samples/Screen2.png", "/Source/Samples/Screen3.png", "/Source/Samples/Screen4.png", "/Source/Samples/Screen5.png" };
            sbyte countPathImg = 0;
      
            BackScreenBtn.Click += (s, e) =>
            {
                if (countPathImg >= 1)
                {
                    countPathImg--;
                    ScreenImg.Source = new BitmapImage(new Uri(pathImg[countPathImg], UriKind.Relative));
                }
            };

            NextScreenBtn.Click += (s, e) =>
            {
                if (countPathImg <= 3)
                {
                    countPathImg++;
                    ScreenImg.Source = new BitmapImage(new Uri(pathImg[countPathImg], UriKind.Relative));
                }
            };

            // ToolBar
            CloseBtn.Click += (s, e) =>
            {
                Application.Current.Shutdown();
            };

            MinimizeBtn.Click += (s, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };

            // ColorCanvas buttons
            HilightBtn.Click += (s, e) =>
            {
                HilightColorRectangle.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(MainColorCanvas.R, MainColorCanvas.G, MainColorCanvas.B));
                HilightPolygon.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(MainColorCanvas.R, MainColorCanvas.G, MainColorCanvas.B));
                ChangeHilightBtn = new byte[]{MainColorCanvas.R, MainColorCanvas.G, MainColorCanvas.B };
            };

            HotTrackingColorBtn.Click += (s, e) =>
            {
                HotTrackingColorRectangle.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(MainColorCanvas.R, MainColorCanvas.G, MainColorCanvas.B));
                HotTrackingColorPolygon.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(MainColorCanvas.R, MainColorCanvas.G, MainColorCanvas.B));
                ChangeHotTrackingColorBtn = new byte[]{MainColorCanvas.R, MainColorCanvas.G, MainColorCanvas.B };
            };

            SaveChangeColorBtn.Click += (s, e) =>
            {
                WinApi.NotifyWinIniChange("Colors");
                if (ChangeHilightBtn != null & ChangeHotTrackingColorBtn != null)
                {
                    RegisterRead.ColorWrite("Hilight", ChangeHilightBtn);
                    RegisterRead.ColorWrite("HotTrackingColor", ChangeHotTrackingColorBtn);
                    MessageBox.Show("Please restart your computer to see the results.", "ColorPanelControl", MessageBoxButton.OK, MessageBoxImage.Question);
                    // Show("Please restart your computer to see the results.", "ColorPanelControl", MessageBoxButton.OK);
                }
                else if(ChangeHilightBtn != null)
                {
                    RegisterRead.ColorWrite("Hilight", ChangeHilightBtn);
                    MessageBox.Show("Please restart your computer to see the results.", "ColorPanelControl", MessageBoxButton.OK, MessageBoxImage.Question);
                }
                else if (ChangeHotTrackingColorBtn != null)
                {
                    RegisterRead.ColorWrite("HotTrackingColor", ChangeHotTrackingColorBtn);
                    MessageBox.Show("Please restart your computer to see the results.", "ColorPanelControl", MessageBoxButton.OK, MessageBoxImage.Question);
                }
                else
                {
                    MessageBox.Show("Ones are equal.", "ColorPanelControl", MessageBoxButton.OK, MessageBoxImage.Question);
                }

            };

            DefaultChangeColorBtn.Click += (s, e) =>
            {
                RegisterRead.ColorWrite("Hilight", DefaultHilight);
                RegisterRead.ColorWrite("HotTrackingColor", DefaultHotTrackingColor);
                MessageBox.Show("Please restart your computer to see the results.", "ColorPanelControl", MessageBoxButton.OK, MessageBoxImage.Question);
                //Show("Please restart your computer to see the results.", "ColorPanelControl", MessageBoxButton.OK);

                RectangleColorsLoad("Hilight");
                RectangleColorsLoad("HotTrackingColor");
            };

            // other
            RectangleColorsLoad("Hilight");
            RectangleColorsLoad("HotTrackingColor");

            MainColorCanvas.SelectedColor = Colors.Black;
            NameVersionTB.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        }

        private void RectangleColorsLoad(string valueName)
        {
            string RGBvalue = RegisterRead.ColorRead(valueName);
            string[] RGBvalueArray = RGBvalue.Split();
            
            if(valueName == "Hilight")
            {
                HilightColorRectangle.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(byte.Parse(RGBvalueArray[0]), byte.Parse(RGBvalueArray[1]), byte.Parse(RGBvalueArray[2])));
                HilightPolygon.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(byte.Parse(RGBvalueArray[0]), byte.Parse(RGBvalueArray[1]), byte.Parse(RGBvalueArray[2])));
            }
            else
            {
                HotTrackingColorRectangle.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(byte.Parse(RGBvalueArray[0]), byte.Parse(RGBvalueArray[1]), byte.Parse(RGBvalueArray[2])));
                HotTrackingColorPolygon.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(byte.Parse(RGBvalueArray[0]), byte.Parse(RGBvalueArray[1]), byte.Parse(RGBvalueArray[2])));
            }
        }

        public static MessageBoxResult Show(string message, string title, MessageBoxButton buttons)
        {
            var dialog = new CustomMessageBox() { Title = title };
            dialog.MessageContainer.Text = message;
            dialog.AddButtons(buttons);
            dialog.ShowDialog();
            return dialog.Result;
        }

        private void ToolBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }       

        private void MainColorCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            InputRGBTB.Text = $"({MainColorCanvas.R}, {MainColorCanvas.G}, {MainColorCanvas.B})";
        }
    }
}
