using System;
using System.Collections.Generic;
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
using YoutubeDownloader.Youtube.Models;
using YoutubeDownloaderDesktop.UserControls;
using YoutubeDownloaderSolution;

namespace YoutubeDownloaderDesktop.Pages
{
    /// <summary>
    /// Interaction logic for DownloadPage.xaml
    /// </summary>
    public partial class DownloadPage : Page
    {

        YoutubeManager manager;
        UrlInputUC urlInputUC;
        ActionResolver actionResolver;
        public DownloadPage()
        {
            InitializeComponent();
            manager = new YoutubeManager();
            urlInputUC = new();
            DownloadFrame.Navigate(urlInputUC);
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(urlInputUC.urlInput.Text))
                return;

            var videoId = Helper.ExtractVideoID(urlInputUC.urlInput.Text);
            var playlistId = Helper.ExtractPlaylistID(urlInputUC.urlInput.Text);

            if (videoId.Length > 0 && playlistId.Length > 0)
            {
                actionResolver = new();
                DownloadFrame.Navigate(actionResolver);
            }
            else if(playlistId.Length > 0)
            {
               
            }  
            else if(videoId.Length > 0)
            {

            }
        }
    }
}
