using System;
using System.Collections.Generic;
using System.Drawing;
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
            string hex = "#FFFFFF";
            var color = ColorTranslator.FromHtml(hex);
            //tb1.Text = $"R: {color.R} G: {color.B} B: {color.G}";
            TbVoi();


        }

        async void TbVoi()
        {
            //await Task.Run(() => {
            //     while (true)
            //    {
            //        this.Dispatcher.Invoke(() =>
            //        {
            //        tb1.Text = $"R: {CV.R} G: {CV.B} B: {CV.G}";
            //        });
            //    }
            //});
  
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ChangedButton == MouseButton.Left)
            //{
            //     this.DragMove(); 
            //}
        }
    }
}
