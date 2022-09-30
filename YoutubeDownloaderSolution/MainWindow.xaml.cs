using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YoutubeDownloaderSolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            this.Visibility = Visibility.Hidden;
            LoadingWindow loading = new LoadingWindow();
            loading.Show();
            //await Task.Delay(TimeSpan.FromSeconds(3));
            Thread.Sleep(2000);
            loading.Hide();
            this.Visibility = Visibility.Visible;

        }

        private void ClsButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
