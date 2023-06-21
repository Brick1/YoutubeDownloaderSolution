using Newtonsoft.Json.Linq;
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
using YoutubeDownloaderDesktop.CustomControls;

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
            LoadSettings();
            //await Task.Delay(TimeSpan.FromSeconds(3));
            Thread.Sleep(2000);
            loading.Hide();
            this.Visibility = Visibility.Visible;

        }

        private void LoadSettings()
        {
            // TODO: code for loading settings into static class
        }

        private void ClsButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SideBar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = SideBar.SelectedItem as NavButton;
            if (selected == null) return;
            MainContent.Navigate(selected.NavLink);
        }

        private void NavButton_Selected()
        {

        }
    }
}
